using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbWatchdog.Model;
using RestSharp;
using Serilog;
using Serilog.Core;

namespace DbWatchdog
{
    public partial class MainForm : Form
    {
        private bool allowVisible;
        
        private List<string> _selectedMonitorTypes = new();


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

        private void SetupParams()
        {
            Properties.Settings.Default["Database"] = this.textDatabase.Text;
            Properties.Settings.Default["DbConnectionStr"] = this.txtDbConnectionStr.Text;
            Properties.Settings.Default["CheckInterval"] = (int)numCheckInterval.Value;
            var monitorTypes = this.clbMonitorTypes.CheckedItems.Cast<MonitorType>().Select(mt=>mt.Id).ToArray();
            _selectedMonitorTypes = monitorTypes.ToList();
            var monitorTypesStrCollection = new StringCollection();
            monitorTypesStrCollection.AddRange(monitorTypes);
            Properties.Settings.Default.MonitorTypes = monitorTypesStrCollection;
            Properties.Settings.Default.Save();
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            SetupParams();
            this.Hide();
            this.notifyIcon.Visible = true;

            // setup the timer
            SetupCheckTimer();
        }

        private async Task InitMonitorTypes()
        {
            try
            {
                var db = new SqlDb(this.txtDbConnectionStr.Text);
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
                var filteredSelectedMonitorTypes = _selectedMonitorTypes.Where(mt => monitorTypeIds.Contains(mt)).ToList();
                
                // select the monitor types that were previously selected
                for (int i = 0; i < this.clbMonitorTypes.Items.Count; i++)
                {
                    var monitorType = (MonitorType)this.clbMonitorTypes.Items[i];
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
                Properties.Settings.Default["LineToken"] = this.textLineToken.Text;
                Properties.Settings.Default.Save();

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

        private async void MainForm_Load(object sender, EventArgs e)
        {
            textLineToken.Text = Properties.Settings.Default.LineToken;
            textDatabase.Text = Properties.Settings.Default.Database;
            txtDbConnectionStr.Text = Properties.Settings.Default.DbConnectionStr;
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

            numCheckInterval.Value = Properties.Settings.Default.CheckInterval;
            var monitorTypes = Properties.Settings.Default.MonitorTypes;
            if (monitorTypes is not null)
            {
                _selectedMonitorTypes = monitorTypes.Cast<string>().ToList();
            }

            Log.Information("MonitorTypes: " + string.Join(",", _selectedMonitorTypes));
            await InitMonitorTypes();
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

                var data = await db.GetLatestRecord("min_data", "me", _selectedMonitorTypes);
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

                foreach (var mt in _selectedMonitorTypes.Where(mt => !data.Values.ContainsKey(mt)))
                {
                    Log.Information($"{mt}: N/A");
                    await NotifyLine($"未收到{mt}測項資料");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred");
                throw;
            }
            return true;
        }

        private async void btnTestMonitor_Click(object sender, EventArgs e)
        {
            SetupParams();
            try
            {
                await CheckDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}