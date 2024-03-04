using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace DbWatchdog
{
    public partial class MainForm : Form
    {
        private bool allowVisible;

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
            this.txtDbConnectionStr.Text = "Server=localhost;Database=logger2;User Id=sa;Password=123456";
            this.textDatabase.Text = "";
            this.textDatabase.Enabled = false;
            this.btnConnect.Enabled = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["Database"] = this.textDatabase.Text;
            Properties.Settings.Default["DbConnectionStr"] = this.txtDbConnectionStr.Text;
            Properties.Settings.Default["CheckInterval"] = (int)numCheckInterval.Value;
            Properties.Settings.Default.Save();
            this.Hide();
            this.notifyIcon.Visible = true;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;

        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.notifyIcon.Visible = false;
        }

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

        private void MainForm_Load(object sender, EventArgs e)
        {
            textLineToken.Text = Properties.Settings.Default.LineToken;
            textDatabase.Text = Properties.Settings.Default.Database;
            txtDbConnectionStr.Text = Properties.Settings.Default.DbConnectionStr;
            numCheckInterval.Value = Properties.Settings.Default.CheckInterval;
        }

        private void txtDbConnectionStr_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = txtDbConnectionStr.Text.Length > 0;
        }
    }
}