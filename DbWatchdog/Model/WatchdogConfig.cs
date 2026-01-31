using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace DbWatchdog.Model
{
    internal record WatchdogConfig
    {
        public string System { get; set; } = "昱山資料庫";

        public string ConnectionString { get; set; } = "Server=localhost;Database=logger2;Trusted_Connection=True;";

        public string DbName { get; set; } = "logger2";

        public int CheckInterval { get; set; } = 10;

        [DefaultValue(10)]            
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int LagAllowed { get; set; } = 10;

        public List<string> Monitors { get; set; } = new List<string>();

        public List<string> MonitorTypes { get; set; } = new List<string>();

        public string LineNotifyToken { get; set; } = "";

        public bool CheckHourData { get; set; } = false;


        public static WatchdogConfig? FromFile(string path)
        {
            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<WatchdogConfig>(json);

        }

        public void SaveToFile(string path)
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }

}
