using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Serilog;

namespace DbWatchdog.Model
{
    public interface IMonitorType
    {
        string Id { get; set; }
        string Desp { get; set; }
    }

    public interface IMonitor
    {
        string Id { get; set; }
        string Name { get; set; }
    }

    public class MonitorType : IMonitorType
    {
        public string Id { get; set;}
        // ReSharper disable once IdentifierTypo
        public string Desp { get; set;}

        public override string ToString()
        {
            return $"{Desp}";
        }
    }

    public class Monitor : IMonitor
    {
        public string Id { get; set;}
        public string Name { get; set;}

        public override string ToString()
        {
            return Name;
        }
    }

    internal interface IDb
    {
        Task<IEnumerable<IMonitor>> GetMonitors();
        Task<IEnumerable<IMonitorType>> GetMonitorTypes();
        Task<SqlDb.IDataRecord> GetLatestRecord(string table, string monitor, List<string> mtList);
    }

    class SqlDb : IDb
    {
        private readonly string _connectionString;

        public SqlDb(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task<IEnumerable<IMonitor>> GetMonitors()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Monitor>("SELECT * FROM [dbo].[monitor]");
        }

        public async Task<List<string>> GetTableColumns(string tableName)
        {
            var sql = @"
                SELECT COLUMN_NAME
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = @tableName
            ";
            var parameters = new Dictionary<string, object>
            {
                { "@tableName", tableName }
            };
            
            using var connection = new SqlConnection(_connectionString);
            var ret = await connection.QueryAsync<string>(sql, parameters);
            return ret.ToList();
        }

        public async Task<IEnumerable<IMonitorType>> GetMonitorTypes()
        { 
            var columns = await GetTableColumns("monitorType");
            using var connection = new SqlConnection(_connectionString);
            if(!columns.Contains("measuringBy"))
                return await connection.QueryAsync<MonitorType>("SELECT * FROM [dbo].[monitorType]");
            
            return await connection.QueryAsync<MonitorType>("SELECT * FROM [dbo].[monitorType] Where [measuringBy] is null");
        }

        public interface IDataRecord
        {
            string Monitor { get; set; }
            DateTime Time { get; set; }
            Dictionary<string, double> Values { get; set; }
        }

        public class DataRecord : IDataRecord
        {
            public string Monitor { get; set;}
            public DateTime Time { get; set;}
            public Dictionary<string, double> Values { get; set;}
        }

        public async Task<IDataRecord> GetLatestRecord(string table, string monitor, List<string> mtList)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql =
                $@"Select Top 1 *
                    From {table}
                    Where [monitor] = '{monitor}'
                    Order by [time] desc";

            Log.Information($"GetLatestRecord: {sql}");
            var reader = await connection.ExecuteReaderAsync(sql);
            var result = new Dictionary<string, double>();
            if (!await reader.ReadAsync())
                return new DataRecord
                {
                    Monitor = monitor,
                    Time = DateTime.MinValue,
                    Values = result
                };

            var time = reader.GetDateTime(reader.GetOrdinal("time"));
            foreach (var mt in mtList)
            {
                var value = reader.GetValue(reader.GetOrdinal(mt));
                if(value != DBNull.Value)
                    result.Add(mt, Convert.ToDouble(value));
            }
            return new DataRecord
            {
                Monitor = monitor,
                Time = time,
                Values = result
            };

        }
    }
}
