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
    public record MonitorType
    {
        public string Id { get; set;}
        // ReSharper disable once IdentifierTypo
        public string Desp { get; set;}

        public override string ToString()
        {
            return $"{Desp}";
        }
    }

    internal interface IDb
    {
        Task<IEnumerable<MonitorType>> GetMonitorTypes();
        Task<SqlDb.DataRecord> GetLatestRecord(string table, string monitor, List<string> mtList);
    }

    class SqlDb : IDb
    {
        private readonly string _connectionString;

        public SqlDb(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task<IEnumerable<MonitorType>> GetMonitorTypes()
        { 
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<MonitorType>("SELECT * FROM [dbo].[monitorType] Where [measuringBy] is not null");
        }

        public class DataRecord
        {
            public string Monitor { get; set;}
            public DateTime Time { get; set;}
            public Dictionary<string, double> Values { get; set;}
        }

        public async Task<DataRecord> GetLatestRecord(string table, string monitor, List<string> mtList)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql =
                $@"Select Top 1 *
                    From {table}
                    Where [monitor] = '{monitor}'
                    Order by [time]";

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
