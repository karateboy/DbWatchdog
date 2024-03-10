using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbWatchdog.Model;
using RestSharp;
using Serilog;

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
            this.txtDbConnectionStr.Text = "mongodb://localhost";
            this.textDatabase.Text = "logger2";
            this.textDatabase.Enabled = true;
            this.btnConnect.Enabled = true;
        }

        private void btnSQL_CheckedChanged(object sender, EventArgs e)
        {
            this.txtDbConnectionStr.Text = "Server=localhost;Database=logger2;Trusted_Connection=True;";
            this.textDatabase.Text = "";
            this.textDatabase.Enabled = false;
            this.btnConnect.Enabled = true;
        }

        private void SaveConfig()
        {
            _config.System = this.textSystem.Text;
            _config.DbName = this.textDatabase.Text;
            _config.ConnectionString = this.txtDbConnectionStr.Text;
            _config.CheckInterval = (int)numCheckInterval.Value;
            _config.LineNotifyToken = this.textLineToken.Text;
            _config.Monitors = this.clbMonitors.CheckedItems.Cast<IMonitor>().Select(m=>m.Id).ToList();
            _config.MonitorTypes = this.clbMonitorTypes.CheckedItems.Cast<IMonitorType>().Select(mt=>mt.Id).ToList();
            _config.SaveToFile(_configPath);
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveConfig();
            Hide();
            notifyIcon.Text = Text;
            notifyIcon.BalloonTipText = @"程式已最小化到系統列";
            notifyIcon.Visible = true;


            // setup the timer
            SetupCheckTimer();
        }

        private async Task InitMonitors()
        {
            try
            {
                IDb db = btnMongo.Checked
                    ? new MongoDb(this.txtDbConnectionStr.Text, this.textDatabase.Text)
                    : new SqlDb(this.txtDbConnectionStr.Text);
                var enumMonitors = await db.GetMonitors();
                var monitors = enumMonitors.ToList();
                this.btnApply.Enabled = true;
                this.clbMonitors.Items.Clear();
                foreach (var monitor in monitors)
                {
                    this.clbMonitors.Items.Add(monitor);
                }

                this.clbMonitors.Enabled = true;
                var monitorIds = monitors.Select(m => m.Id).ToList();
                var filteredSelectedMonitors = _config.Monitors.Where(m => monitorIds.Contains(m)).ToList();

                // select the monitors that were previously selected
                for (int i = 0; i < this.clbMonitors.Items.Count; i++)
                {
                    var monitor = (IMonitor)this.clbMonitors.Items[i];
                    if (filteredSelectedMonitors.Contains(monitor.Id))
                    {
                        this.clbMonitors.SetItemChecked(i, true);
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
                IDb db = btnMongo.Checked? new MongoDb(this.txtDbConnectionStr.Text, this.textDatabase.Text):
                    new SqlDb(this.txtDbConnectionStr.Text);
                var enumMonitorTypes = await db.GetMonitorTypes();
                var monitorTypes = enumMonitorTypes.ToList();
                this.btnApply.Enabled = true;
                this.clbMonitorTypes.Items.Clear();
                foreach (var monitorType in monitorTypes)
                {
                    this.clbMonitorTypes.Items.Add(monitorType);
                }
                this.clbMonitorTypes.Enabled = true;
                var monitorTypeIds = monitorTypes.Select(mt => mt.Id).ToList();
                var filteredSelectedMonitorTypes = _config.MonitorTypes.Where(mt => monitorTypeIds.Contains(mt)).ToList();
                
                // select the monitor types that were previously selected
                for (int i = 0; i < this.clbMonitorTypes.Items.Count; i++)
                {
                    var monitorType = (IMonitorType)this.clbMonitorTypes.Items[i];
                    if (filteredSelectedMonitorTypes.Contains(monitorType.Id))
                    {
                        this.clbMonitorTypes.SetItemChecked(i, true);
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
            this.Show();
            this.notifyIcon.Visible = false;
        }

        // Test the line token qjhK8FGHl6d12H7qAOAkOlY0kzkWcFUTw33Kr39lwK8

        private async void btnTestLine_Click(object sender, EventArgs e)
        {
            try
            {
                var options = new RestClientOptions("https://notify-api.line.me/");
                var client = new RestClient(options);
                var request = new RestRequest("api/notify")
                    .AddHeader("Authorization", $"Bearer {this.textLineToken.Text}")
                    .AddParameter("message", "資料庫Watchdog 測試訊息!");

                var response = await client.PostAsync(request);
                MessageBox.Show(response.StatusCode == System.Net.HttpStatusCode.OK ? "訊息已送出!" : "訊息送出失敗!");
                if (response.StatusCode != System.Net.HttpStatusCode.OK) return;
                _config.LineNotifyToken = this.textLineToken.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textLineToken_TextChanged(object sender, EventArgs e)
        {
            this.btnTestLine.Enabled = this.textLineToken.Text.Length > 0;
        }

        private async Task UpdateUi()
        {
            this.Text = $@"監控程式 - {_config.System}";
            textSystem.Text = _config.System;
            textLineToken.Text = _config.LineNotifyToken;
            textDatabase.Text = _config.DbName;
            txtDbConnectionStr.Text = _config.ConnectionString;
            if (txtDbConnectionStr.Text.Contains("mongodb://"))
            {
                btnMongo.Checked = true;
                btnSQL.Checked = false;
            }
            else
            {
                btnMongo.Checked = false;
                btnSQL.Checked = true;
            }

            numCheckInterval.Value = _config.CheckInterval;
            await InitMonitors();
            await InitMonitorTypes();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                _config = WatchdogConfig.FromFile(_configPath) ?? new WatchdogConfig();
            }
            catch (Exception)
            {
                _config = new WatchdogConfig();
            }
            
            await UpdateUi();
        }

        private void txtDbConnectionStr_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = txtDbConnectionStr.Text.Length > 0;
        }

        private Timer? _checkTimer = null;
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
            _checkTimer.Tick += async (sender, args) =>
            {
                await CheckDatabase();
            };
            _checkTimer.Start();
        }

        private async Task<bool> NotifyLine(string message)
        {
            if(string.IsNullOrEmpty(this.textLineToken.Text)) return false;

            var options = new RestClientOptions("https://notify-api.line.me/");
            var client = new RestClient(options);
            var request = new RestRequest("api/notify")
                .AddHeader("Authorization", $"Bearer {this.textLineToken.Text}")
                .AddParameter("message", message);
            var response = await client.PostAsync(request);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        private async Task<bool> CheckDatabase()
        {
            try
            {
                IDb db = btnMongo.Checked? new MongoDb(this.txtDbConnectionStr.Text, this.textDatabase.Text):
                    new SqlDb(this.txtDbConnectionStr.Text);

                foreach (var monitor in _config.Monitors)
                {
                    var data = await db.GetLatestRecord("min_data", monitor, _config.MonitorTypes);
                    await CheckData(data);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred");
                throw;
            }
            return true;

            async Task<bool> CheckData(SqlDb.IDataRecord data)
            {
                if (data.Time == DateTime.MinValue)
                {
                    Log.Information("No data found");
                    await NotifyLine("找不到分鐘資料");
                    return false;
                }


                if (data.Time < DateTime.Now.AddMinutes(-(int)numCheckInterval.Value))
                {
                    Log.Information("Data is too old");
                    await NotifyLine($"全測項分鐘資料未更新! 最新資料時間{data.Time:G}");
                    return false;
                }

                foreach (var mt in _config.MonitorTypes.Where(mt => !data.Values.ContainsKey(mt)))
                {
                    Log.Information($"{mt}: N/A");
                    await NotifyLine($"未收到{mt}測項資料");
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
            if(saveConfigDlg.ShowDialog() != DialogResult.OK) return;
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
                    this.txtDbConnectionStr.Text = dlg.GetConnectionString();
                }
                else
                {
                    this.txtDbConnectionStr.Text = $@"mongodb://{dlg.GetServer()}";
                    this.textDatabase.Text = dlg.GetDatabase();
                }
            }
        }
    }
}