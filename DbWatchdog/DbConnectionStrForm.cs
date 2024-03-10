using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbWatchdog
{
    public partial class DbConnectionStrForm : Form
    {
        public bool IsSQL { get; set; } = true;
        public string ConnectionString { get; set; } = string.Empty;

        public DbConnectionStrForm()
        {
            InitializeComponent();
        }

        private void DbConnectionStrForm_Load(object sender, EventArgs e)
        {


        }

        public string GetConnectionString()
        {
            string connStr = string.Empty;
            if (IsSQL)
            {
                connStr = $"Data Source={textServer.Text};Initial Catalog={textDatabase.Text};";
                if (chkIntegratedSecurity.Checked)
                {
                    connStr += "Integrated Security=True;";
                }
                else
                {
                    connStr += $"User ID={textUser.Text};Password={textPassword.Text};";
                }
            }
            else
            {
                connStr = $"Server={textServer.Text};Database={textDatabase.Text};";
                if (chkIntegratedSecurity.Checked)
                {
                    connStr += "Integrated Security=True;";
                }
                else
                {
                    connStr += $"User ID={textUser.Text};Password={textPassword.Text};";
                }
            }
            return connStr;
        }

        public string GetDatabase()
        {
            return textDatabase.Text;
        }

        public string GetServer()
        {
            return textServer.Text;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void chkIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIntegratedSecurity.Checked)
            {
                textUser.Enabled = false;
                textPassword.Enabled = false;

            }
            else
            {
                textUser.Enabled = true;
                textPassword.Enabled = true;
            }
        }
    }
}
