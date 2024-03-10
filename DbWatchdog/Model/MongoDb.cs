using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

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

            public override string ToString()
            {
                return $"{Desp}";
            }
        }

        [BsonIgnoreExtraElements]
        class Monitor :IMonitor
        {
            public string Id { get; set; }

            [BsonElement("desc")]
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
        class RecordListID
        {
            public DateTime time;
            public string monitor;
        }

        [BsonIgnoreExtraElements]
        class MtRecord
        {
            public string mtName;
            public double? value;
            public string status;
        }

        class RecordList
        {
            public RecordListID _id;
            public List<MtRecord> mtDataList;
        }

        public MongoDb(string connectionString, string database)
        {
            this._connectionString = connectionString;
            this._database = database;
            _client = new MongoClient(connectionString);
        }

        public async Task<IEnumerable<IMonitor>> GetMonitors()
        {
            var collection = _client.GetDatabase(_database).GetCollection<Monitor>("monitors");
            var filter = Builders<Monitor>.Filter
                .Exists(r => r.Id);
            var ret = await collection.FindAsync(filter);
            return ret.ToList();
        }

        public async Task<IEnumerable<IMonitorType>> GetMonitorTypes()
        {
            var collection = _client.GetDatabase(_database).GetCollection<MonitorType>("monitorTypes");
            var filter = Builders<MonitorType>.Filter
                .Exists(r => r.Id);
            var ret =  await collection.FindAsync(filter);
            return ret.ToList();
        }

        public async Task<SqlDb.IDataRecord> GetLatestRecord(string table, string monitor, List<string> mtList)
        {
            var collection = _client.GetDatabase(_database).GetCollection<RecordList>("min_data");
            var filter = Builders<RecordList>.Filter
                .Eq(r => r._id.monitor, monitor);
            var sort = Builders<RecordList>.Sort.Descending(r => r._id.time);
            var ret = await collection.Find(filter).Sort(sort).Limit(1).ToListAsync();
            var result = new Dictionary<string, double>();
            if (ret.Count == 0)
            {
                return new SqlDb.DataRecord
                {
                    Monitor = monitor,
                    Time = DateTime.MinValue,
                    Values = result
                };
            }
            else
            {
                var record = ret[0];
                foreach (var mt in mtList)
                {
                    var mtRecord = record.mtDataList.Find(r => r.mtName == mt);
                    if (mtRecord != null)
                    {
                        result.Add(mt, mtRecord.value ?? 0);
                    }
                }

                return new SqlDb.DataRecord
                {
                    Monitor = monitor,
                    Time = record._id.time,
                    Values = result
                };
            }
        }
    }
}
