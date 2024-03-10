using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DbWatchdog.Model
{
    internal record WatchdogConfig
    {
        public string System { get; set; } = "昱山資料庫";
        public string ConnectionString { get; set; } = "Server=localhost;Database=logger2;Trusted_Connection=True;";
        public string DbName { get; set; } = "logger2";
        public int CheckInterval { get; set; } = 10;
        public List<string> Monitors { get; set; } = new List<string>();
        public List<string> MonitorTypes { get; set; } = new List<string>();
        public string LineNotifyToken { get; set; } = "";

        public static WatchdogConfig? FromFile(string path)
        {
            var json = File.ReadAllText(path);
            
            return JsonSerializer.Deserialize<WatchdogConfig>(json);
            
        }

        public void SaveToFile(string path)
        {
            var json = JsonSerializer.Serialize(this);
            File.WriteAllText(path, json);
        }
    }

}
