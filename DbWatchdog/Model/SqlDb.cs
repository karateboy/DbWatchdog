using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Dapper;
using Serilog;

namespace DbWatchdog.Model
{
    public interface IMonitorType
    {
        string Id { get; }
        string Desp { get; set; }
    }

    public interface IMonitor
    {
        string Id { get; }
        string Name { get; }
    }

    public class MonitorType : IMonitorType
    {
        public string Id { get; set; }

        // ReSharper disable once IdentifierTypo
        public string Desp { get; set; }

        public override string ToString()
        {
            return $"{Desp}";
        }
    }
    
    public class OldMonitorType : IMonitorType
    {
        public string ITEM { get; set; }
        public string Id => ITEM;

        public string Desp { get; set; }

        public override string ToString()
        {
            return Desp;
        }
    }

    public class Monitor : IMonitor
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? "Unknown" : Name;
        }
    }

    public class OldMonitor : IMonitor
    {
        public string DP_NO { get; set; }
        public string Id => DP_NO;
        public string monitorName { get; set; }
        public string Name => monitorName;

        public override string ToString()
        {
            return !string.IsNullOrEmpty(monitorName) ? monitorName : "Unknown";
        }
    }

    internal interface IDb
    {
        Task<IEnumerable<IMonitor>> GetMonitors();
        Task<IEnumerable<IMonitorType>> GetMonitorTypes();
        Task<SqlDb.IDataRecord> GetLatestRecord(string table, string monitor, List<string> mtList);
    }

    class SqlDb(string connectionString) : IDb
    {
        public async Task<IEnumerable<IMonitor>> GetMonitors()
        {
            var columns = await GetTableColumns("monitor");
            using var connection = new SqlConnection(connectionString);
            if (columns.Exists(column=>column.ToLower() == "id"))
                return await connection.QueryAsync<Monitor>("SELECT * FROM [dbo].[monitor]");
            
            return await connection.QueryAsync<OldMonitor>("SELECT * FROM [dbo].[monitor]");
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

            using var connection = new SqlConnection(connectionString);
            var ret = await connection.QueryAsync<string>(sql, parameters);
            return ret.ToList();
        }

        public async Task<IEnumerable<IMonitorType>> GetMonitorTypes()
        {
            var columns = await GetTableColumns("monitorType");
            using var connection = new SqlConnection(connectionString);
            if (!columns.Exists(column=>string.Compare(column, "measuringBy", StringComparison.OrdinalIgnoreCase) == 0))
                return await connection.QueryAsync<OldMonitorType>("SELECT * FROM [dbo].[monitorType]");

            return await connection.QueryAsync<MonitorType>(
                "SELECT * FROM [dbo].[monitorType] Where [measuringBy] is not null");
        }

        public interface IDataRecord
        {
            DateTime Time { get; }
            Dictionary<string, double> Values { get; }
        }

        public class DataRecord : IDataRecord
        {
            public DateTime Time { get; set; }
            public Dictionary<string, double> Values { get; set; }
        }
        
        public class OldDataRecord : IDataRecord
        {
            public DateTime M_DateTime { get; set; }
            public DateTime Time => M_DateTime;
            
            public string DataList { get; set; }
            public Dictionary<string, double> Values { get; set; }
        }

        public class MtRecord
        {
            public string mtName { get; set; }
            public double? value { get; set; }
        }
        private async Task<IDataRecord> GetOldDbLatestRecord(string table, string monitor, List<string> mtList)
        {
            using var connection = new SqlConnection(connectionString);
            var sql =
                $@"Select Top 1 *
                    From MinRecord2
                    Where [DP_NO] = '{monitor}'
                    Order by [M_DateTime] desc";
            
            var enumerable = await connection.QueryAsync<OldDataRecord>(sql);
            var result = new Dictionary<string, double>();
            var oldDataRecords = enumerable as OldDataRecord[] ?? enumerable.ToArray();
            if (!oldDataRecords.Any())
                return new DataRecord
                {
                    Time = DateTime.MinValue,
                    Values = result
                };

            var record = oldDataRecords.First();
            var mtRecords = JsonSerializer.Deserialize<MtRecord[]>(record.DataList);
            if(mtRecords == null)
                return new DataRecord
                {
                    Time = DateTime.MinValue,
                    Values = result
                };
            
            var mtMap = mtRecords.ToDictionary(mtRecord=>mtRecord.mtName, mtRecord=>mtRecord.value);
            foreach (var mt in mtList)
            {
                if (mtMap.TryGetValue(mt, out var value))
                    result.Add(mt, value ?? 0);
            }

            record.Values = result;
            return record;
        }

        public async Task<IDataRecord> GetLatestRecord(string table, string monitor, List<string> mtList)
        {
            var columns = await GetTableColumns("monitor");
            if (!columns.Contains("id"))
                return await GetOldDbLatestRecord(table, monitor, mtList);
            
            return await GetNewLatestRecord(table, monitor, mtList);
        }
        
        private async Task<IDataRecord> GetNewLatestRecord(string table, string monitor, List<string> mtList)
        {
            using var connection = new SqlConnection(connectionString);
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
                    Time = DateTime.MinValue,
                    Values = result
                };

            var time = reader.GetDateTime(reader.GetOrdinal("time"));
            foreach (var mt in mtList)
            {
                var value = reader.GetValue(reader.GetOrdinal(mt));
                if (value != DBNull.Value)
                    result.Add(mt, Convert.ToDouble(value));
            }

            return new DataRecord
            {
                Time = time,
                Values = result
            };
        }
    }
}