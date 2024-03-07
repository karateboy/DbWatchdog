using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DbWatchdog.Model
{
    internal class MongoDb : IDb
    {
        private readonly string _connectionString;
        private readonly string _database;
        private readonly IMongoClient _client;

        [BsonIgnoreExtraElements]
        class MonitorType :IMonitorType
        {
            public string Id { get; set; }

            [BsonElement("desp")]
            public string Desp { get; set; }

            [BsonElement("measuringBy")]
            public string[]? MeasuringBy {get;set;}

            public override string ToString()
            {
                return $"{Desp}";
            }
        }

        public MongoDb(string connectionString, string database)
        {
            this._connectionString = connectionString;
            this._database = database;
            _client = new MongoClient(connectionString);
        }

        public async Task<IEnumerable<IMonitorType>> GetMonitorTypes()
        {
            var collection = _client.GetDatabase(_database).GetCollection<MonitorType>("monitorTypes");
            var filter = Builders<MonitorType>.Filter
                .Exists(r => r.Id);
            var ret =  await collection.FindAsync(filter);
            return ret.ToList();
        }

        public Task<SqlDb.DataRecord> GetLatestRecord(string table, string monitor, List<string> mtList)
        {
            throw new NotImplementedException();
        }
    }
}
