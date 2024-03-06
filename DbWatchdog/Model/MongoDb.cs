using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWatchdog.Model
{
    internal class MongoDb : IDb
    {
        private readonly string _connectionString;
        private readonly string _database;

        public MongoDb(string connectionString, string database)
        {
            this._connectionString = connectionString;
            this._database = database;
        }

        public Task<IEnumerable<MonitorType>> GetMonitorTypes()
        {
            throw new NotImplementedException();
        }

        public Task<SqlDb.DataRecord> GetLatestRecord(string table, string monitor, List<string> mtList)
        {
            throw new NotImplementedException();
        }
    }
}
