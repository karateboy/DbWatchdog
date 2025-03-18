using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbWatchdog.Model;
using RestSharp;
using Serilog;
using Timer = System.Windows.Forms.Timer;

namespace DbWatchdog
{
    public partial class MainForm : Form
    {
        private WatchdogConfig _config = new();
        private string _configPath = "config.json";

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMongo_CheckedChanged(object sender, EventArgs e)
        {
            if (btnMongo.Checked)
            {
                if(!txtDbConnectionStr.Text.Contains("mongodb://localhost"))
                {
                    txtDbConnectionStr.Text = "mongodb://localhost";
                    textDatabase.Text = "logger2";
                }
                
                textDatabase.Enabled = true;
                btnConnect.Enabled = true;
            }
        }

        private void btnSQL_CheckedChanged(object sender, EventArgs e)
        {
            if (btnSQL.Checked)
            {
                if (txtDbConnectionStr.Text.Contains("mongodb://"))
                {
                    txtDbConnectionStr.Text = "Server=localhost;Database=logger2;Trusted_Connection=True;";
                    textDatabase.Text = "";    
                }
                
                textDatabase.Enabled = false;
                btnConnect.Enabled = true;
            }
        }

        private void SaveConfig()
        {
            _config.System = textSystem.Text;
            _config.DbName = textDatabase.Text;
            _config.ConnectionString = txtDbConnectionStr.Text;
            _config.CheckInterval = (int)numCheckInterval.Value;
            _config.LineNotifyToken = textLineToken.Text;
            _config.Monitors = clbMonitors.CheckedItems.Cast<IMonitor>().Select(m => m.Id).ToList();
            _config.MonitorTypes = clbMonitorTypes.CheckedItems.Cast<IMonitorType>().Select(mt => mt.Id).ToList();
            _config.SaveToFile(_configPath);
        }

        private void StartMonitoring()
        {
            Hide();
            notifyIcon.Text = Text;
            notifyIcon.BalloonTipText = @"程式已最小化到系統列";
            notifyIcon.Visible = true;
            // setup the timer
            SetupCheckTimer();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            StartMonitoring();
        }

        private async Task InitMonitors()
        {
            try
            {
                IDb db = btnMongo.Checked
                    ? new MongoDb(txtDbConnectionStr.Text, textDatabase.Text)
                    : new SqlDb(txtDbConnectionStr.Text);
                var enumMonitors = await db.GetMonitors();
                var monitors = enumMonitors.ToList();
                btnApply.Enabled = true;
                clbMonitors.Items.Clear();
                foreach (var monitor in monitors)
                {
                    clbMonitors.Items.Add(monitor);
                }

                clbMonitors.Enabled = true;
                var monitorIds = monitors.Select(m => m.Id).ToList();
                var filteredSelectedMonitors = _config.Monitors.Where(m => monitorIds.Contains(m)).ToList();

                // select the monitors that were previously selected
                for (int i = 0; i < clbMonitors.Items.Count; i++)
                {
                    var monitor = (IMonitor)clbMonitors.Items[i];
                    if (filteredSelectedMonitors.Contains(monitor.Id))
                    {
                        clbMonitors.SetItemChecked(i, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task InitMonitorTypes()
        {
            try
            {
                IDb db = btnMongo.Checked
                    ? new MongoDb(txtDbConnectionStr.Text, textDatabase.Text)
                    : new SqlDb(txtDbConnectionStr.Text);
                var enumMonitorTypes = await db.GetMonitorTypes();
                var monitorTypes = enumMonitorTypes.ToList();
                btnApply.Enabled = true;
                clbMonitorTypes.Items.Clear();
                foreach (var monitorType in monitorTypes)
                {
                    clbMonitorTypes.Items.Add(monitorType);
                }

                clbMonitorTypes.Enabled = true;
                var monitorTypeIds = monitorTypes.Select(mt => mt.Id).ToList();
                var filteredSelectedMonitorTypes =
                    _config.MonitorTypes.Where(mt => monitorTypeIds.Contains(mt)).ToList();

                // select the monitor types that were previously selected
                for (int i = 0; i < clbMonitorTypes.Items.Count; i++)
                {
                    var monitorType = (IMonitorType)clbMonitorTypes.Items[i];
                    if (filteredSelectedMonitorTypes.Contains(monitorType.Id))
                    {
                        clbMonitorTypes.SetItemChecked(i, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            await InitMonitors();
            await InitMonitorTypes();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            notifyIcon.Visible = false;
        }

        // Test the line token qjhK8FGHl6d12H7qAOAkOlY0kzkWcFUTw33Kr39lwK8

        private async void btnTestLine_Click(object sender, EventArgs e)
        {
            try
            { 
                var resp = await NotifyLine("資料庫Watchdog 測試訊息!");
             /*
                var options = new RestClientOptions("https://api.line.me/");
                var client = new RestClient(options);
                //"資料庫Watchdog 測試訊息!"
                var request = new RestRequest("v2/bot/message/broadcast")
                    .AddHeader("Authorization", $"Bearer {textLineToken.Text}")
                    .AddJsonBody(new { messages = new [] { new { type = "text", text = "資料庫Watchdog 測試訊息!" } } });

                var response = await client.PostAsync(request);
             */
                MessageBox.Show(resp ? "訊息已送出!" : "訊息送出失敗!");
                if (resp) _config.LineNotifyToken = textLineToken.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textLineToken_TextChanged(object sender, EventArgs e)
        {
            btnTestLine.Enabled = textLineToken.Text.Length > 0;
        }
        
        private async Task UpdateUi()
        {
            // Get the version of the executing assembly
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            Text = $@"監控程式 v{version} - {_config.System}";
            textSystem.Text = _config.System;
            textLineToken.Text = _config.LineNotifyToken;
            textDatabase.Text = _config.DbName;
            txtDbConnectionStr.Text = _config.ConnectionString;

            if (txtDbConnectionStr.Text.Contains("mongodb://"))
            {
                Invoke(() =>
                {
                    btnMongo.Checked = true;
                    btnSQL.Checked = false;
                });
            }
            else
            {
                Invoke(() =>
                {
                    btnMongo.Checked = false;
                    btnSQL.Checked = true;
                });
            }


            numCheckInterval.Value = _config.CheckInterval;
            await InitMonitors();
            await InitMonitorTypes();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            bool assignedByArgs = false;
            try
            {
                string[] args = Environment.GetCommandLineArgs();
                if (args.Length > 1)
                {
                    assignedByArgs = true;
                    _configPath = args[1];
                }

                var argsConfig = WatchdogConfig.FromFile(_configPath);
                if (argsConfig is not null)
                {
                    _config = argsConfig;
                    Log.Information("Load config from {ConfigPath}", _configPath);
                }
                else
                {
                    Log.Information("Config file not found, use default config");
                    assignedByArgs = false;
                    _config = new WatchdogConfig();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred");
                _config = new WatchdogConfig();
            }

            await UpdateUi();
            if (!assignedByArgs ||
                _config.Monitors.Count <= 0 || _config.MonitorTypes.Count <= 0) return;

            Log.Information("Start monitoring");
            StartMonitoring();
        }

        private void txtDbConnectionStr_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = txtDbConnectionStr.Text.Length > 0;
        }

        private Timer? _checkTimer;

        private void SetupCheckTimer()
        {
            if (_checkTimer is not null)
            {
                Log.Information("Stop the previous timer");
                _checkTimer.Stop();
                _checkTimer.Dispose();
                _checkTimer = null;
            }

            Log.Information("Start timer with {Interval} min interval", numCheckInterval.Value);
            _checkTimer = new Timer();
            _checkTimer.Interval = (int)numCheckInterval.Value * 1000 * 60;
            _checkTimer.Tick += async (sender, args) => { await CheckDatabase(); };
            _checkTimer.Start();
        }

        private async Task<bool> NotifyLine(string message)
        {
            if (string.IsNullOrEmpty(textLineToken.Text)) return false;

            var options = new RestClientOptions("https://api.line.me/");
            var client = new RestClient(options);
            var request = new RestRequest("v2/bot/message/broadcast")
                .AddHeader("Authorization", $"Bearer {textLineToken.Text}")
                .AddJsonBody(new { messages = new [] { new { type = "text", text = message } } });
            var response = await client.PostAsync(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        private async Task<bool> CheckDatabase()
        {
            try
            {
                IDb db = btnMongo.Checked
                    ? new MongoDb(txtDbConnectionStr.Text, textDatabase.Text)
                    : new SqlDb(txtDbConnectionStr.Text);
                var monitors = await db.GetMonitors();
                var monitorMap = monitors.ToDictionary(m => m.Id);
                foreach (var monitorId in _config.Monitors)
                {
                    var data = await db.GetLatestRecord("min_data", monitorId, _config.MonitorTypes);
                    if (monitorMap.TryGetValue(monitorId, out var monitor))
                    {
                        await CheckData(monitor, data);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred");
                throw;
            }

            return true;

            async Task<bool> CheckData(IMonitor monitor, SqlDb.IDataRecord data)
            {
                if (data.Time == DateTime.MinValue)
                {
                    Log.Information("No data found");
                    await NotifyLine($"{_config.System} - {monitor.Name}找不到分鐘資料");
                    return false;
                }


                if (data.Time < DateTime.Now.AddMinutes(-(int)numCheckInterval.Value))
                {
                    Log.Information("Data is too old");
                    await NotifyLine($"{_config.System} - {monitor.Name}全測項分鐘資料未更新! 最新資料時間{data.Time:G}");
                    return false;
                }

                foreach (var mt in _config.MonitorTypes.Where(mt => !data.Values.ContainsKey(mt)))
                {
                    Log.Information($"{mt}: N/A");
                    await NotifyLine($"{_config.System} - {monitor.Name}未收到{mt}測項資料");
                }

                return true;
            }
        }

        private async void btnTestMonitor_Click(object sender, EventArgs e)
        {
            SaveConfig();
            try
            {
                await CheckDatabase();
                MessageBox.Show(@"測試完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnLoadConfig_Click(object sender, EventArgs e)
        {
            if (openConfigDlg.ShowDialog() != DialogResult.OK) return;

            _configPath = openConfigDlg.FileName;
            _config = WatchdogConfig.FromFile(_configPath) ?? new WatchdogConfig();
            await UpdateUi();
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            if (saveConfigDlg.ShowDialog() != DialogResult.OK) return;
            _configPath = saveConfigDlg.FileName;
            SaveConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dlg = new DbConnectionStrForm();
            dlg.IsSQL = btnSQL.Checked;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.IsSQL)
                {
                    txtDbConnectionStr.Text = dlg.GetConnectionString();
                }
                else
                {
                    txtDbConnectionStr.Text = $@"mongodb://{dlg.GetServer()}";
                    textDatabase.Text = dlg.GetDatabase();
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}