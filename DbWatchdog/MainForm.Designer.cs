namespace DbWatchdog
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.btnMongo = new System.Windows.Forms.RadioButton();
            this.btnSQL = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDbConnectionStr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numCheckInterval = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.textLineToken = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.textDatabase = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnTestLine = new System.Windows.Forms.Button();
            this.clbMonitorTypes = new System.Windows.Forms.CheckedListBox();
            this.btnTestMonitor = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textSystem = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.clbMonitors = new System.Windows.Forms.CheckedListBox();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.saveConfigDlg = new System.Windows.Forms.SaveFileDialog();
            this.openConfigDlg = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numCheckInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(19, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "資料庫類型:";
            // 
            // btnMongo
            // 
            this.btnMongo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMongo.Location = new System.Drawing.Point(187, 61);
            this.btnMongo.Name = "btnMongo";
            this.btnMongo.Size = new System.Drawing.Size(158, 31);
            this.btnMongo.TabIndex = 1;
            this.btnMongo.TabStop = true;
            this.btnMongo.Text = "MongoDb";
            this.btnMongo.UseVisualStyleBackColor = true;
            this.btnMongo.CheckedChanged += new System.EventHandler(this.btnMongo_CheckedChanged);
            // 
            // btnSQL
            // 
            this.btnSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSQL.Location = new System.Drawing.Point(351, 61);
            this.btnSQL.Name = "btnSQL";
            this.btnSQL.Size = new System.Drawing.Size(154, 31);
            this.btnSQL.TabIndex = 2;
            this.btnSQL.TabStop = true;
            this.btnSQL.Text = "SQL Server";
            this.btnSQL.UseVisualStyleBackColor = true;
            this.btnSQL.CheckedChanged += new System.EventHandler(this.btnSQL_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Location = new System.Drawing.Point(19, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "連接字串:";
            // 
            // txtDbConnectionStr
            // 
            this.txtDbConnectionStr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDbConnectionStr.Location = new System.Drawing.Point(184, 137);
            this.txtDbConnectionStr.Name = "txtDbConnectionStr";
            this.txtDbConnectionStr.Size = new System.Drawing.Size(515, 33);
            this.txtDbConnectionStr.TabIndex = 4;
            this.txtDbConnectionStr.TextChanged += new System.EventHandler(this.txtDbConnectionStr_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(19, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "測項:";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(708, 137);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(76, 33);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "連接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.Location = new System.Drawing.Point(19, 587);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "檢查頻率(分):";
            // 
            // numCheckInterval
            // 
            this.numCheckInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numCheckInterval.Location = new System.Drawing.Point(184, 587);
            this.numCheckInterval.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numCheckInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCheckInterval.Name = "numCheckInterval";
            this.numCheckInterval.Size = new System.Drawing.Size(161, 33);
            this.numCheckInterval.TabIndex = 9;
            this.numCheckInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.Location = new System.Drawing.Point(19, 626);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 25);
            this.label5.TabIndex = 10;
            this.label5.Text = "Line通報權杖:";
            // 
            // textLineToken
            // 
            this.textLineToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textLineToken.Location = new System.Drawing.Point(184, 626);
            this.textLineToken.Name = "textLineToken";
            this.textLineToken.Size = new System.Drawing.Size(515, 33);
            this.textLineToken.TabIndex = 11;
            this.textLineToken.TextChanged += new System.EventHandler(this.textLineToken_TextChanged);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(184, 665);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(106, 33);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "開始監測";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStop.Location = new System.Drawing.Point(408, 665);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(123, 33);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "結束";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // textDatabase
            // 
            this.textDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textDatabase.Location = new System.Drawing.Point(184, 98);
            this.textDatabase.Name = "textDatabase";
            this.textDatabase.Size = new System.Drawing.Size(515, 33);
            this.textDatabase.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.Location = new System.Drawing.Point(19, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "資料庫名稱:";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "資料庫Watchdog";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // btnTestLine
            // 
            this.btnTestLine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnTestLine.Enabled = false;
            this.btnTestLine.Location = new System.Drawing.Point(705, 626);
            this.btnTestLine.Name = "btnTestLine";
            this.btnTestLine.Size = new System.Drawing.Size(76, 33);
            this.btnTestLine.TabIndex = 16;
            this.btnTestLine.Text = "測試";
            this.btnTestLine.UseVisualStyleBackColor = true;
            this.btnTestLine.Click += new System.EventHandler(this.btnTestLine_Click);
            // 
            // clbMonitorTypes
            // 
            this.clbMonitorTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.clbMonitorTypes.Enabled = false;
            this.clbMonitorTypes.FormattingEnabled = true;
            this.clbMonitorTypes.Location = new System.Drawing.Point(184, 353);
            this.clbMonitorTypes.Name = "clbMonitorTypes";
            this.clbMonitorTypes.Size = new System.Drawing.Size(515, 228);
            this.clbMonitorTypes.TabIndex = 7;
            // 
            // btnTestMonitor
            // 
            this.btnTestMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTestMonitor.Location = new System.Drawing.Point(296, 665);
            this.btnTestMonitor.Name = "btnTestMonitor";
            this.btnTestMonitor.Size = new System.Drawing.Size(106, 33);
            this.btnTestMonitor.TabIndex = 17;
            this.btnTestMonitor.Text = "立刻檢查";
            this.btnTestMonitor.UseVisualStyleBackColor = true;
            this.btnTestMonitor.Click += new System.EventHandler(this.btnTestMonitor_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.Location = new System.Drawing.Point(19, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 25);
            this.label7.TabIndex = 18;
            this.label7.Text = "系統:";
            // 
            // textSystem
            // 
            this.textSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textSystem.Location = new System.Drawing.Point(187, 22);
            this.textSystem.Name = "textSystem";
            this.textSystem.Size = new System.Drawing.Size(271, 33);
            this.textSystem.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.Location = new System.Drawing.Point(19, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 25);
            this.label8.TabIndex = 20;
            this.label8.Text = "測站:";
            // 
            // clbMonitors
            // 
            this.clbMonitors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.clbMonitors.Enabled = false;
            this.clbMonitors.FormattingEnabled = true;
            this.clbMonitors.Location = new System.Drawing.Point(184, 176);
            this.clbMonitors.Name = "clbMonitors";
            this.clbMonitors.Size = new System.Drawing.Size(515, 172);
            this.clbMonitors.TabIndex = 21;
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnLoadConfig.Location = new System.Drawing.Point(464, 22);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(106, 33);
            this.btnLoadConfig.TabIndex = 22;
            this.btnLoadConfig.Text = "載入設定";
            this.btnLoadConfig.UseVisualStyleBackColor = true;
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSaveConfig.Location = new System.Drawing.Point(576, 22);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(106, 33);
            this.btnSaveConfig.TabIndex = 23;
            this.btnSaveConfig.Text = "儲存設定";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.Location = new System.Drawing.Point(553, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 33);
            this.button1.TabIndex = 24;
            this.button1.Text = "建立連接字串";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveConfigDlg
            // 
            this.saveConfigDlg.DefaultExt = "json";
            this.saveConfigDlg.FileName = "config.json";
            this.saveConfigDlg.Filter = "設定檔案|*.json|所有檔案|*.*";
            // 
            // openConfigDlg
            // 
            this.openConfigDlg.DefaultExt = "json";
            this.openConfigDlg.FileName = "config";
            this.openConfigDlg.Filter = "設定檔案|*.json|所有檔案|*.*";
            this.openConfigDlg.Title = "開啟設定";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(794, 710);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSaveConfig);
            this.Controls.Add(this.btnLoadConfig);
            this.Controls.Add(this.clbMonitors);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textSystem);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnTestMonitor);
            this.Controls.Add(this.btnTestLine);
            this.Controls.Add(this.textDatabase);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.textLineToken);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numCheckInterval);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.clbMonitorTypes);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDbConnectionStr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSQL);
            this.Controls.Add(this.btnMongo);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 14.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "MainForm";
            this.Text = "資料庫Watchdog";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCheckInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton btnMongo;
        private System.Windows.Forms.RadioButton btnSQL;

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDbConnectionStr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numCheckInterval;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textLineToken;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox textDatabase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button btnTestLine;
        private System.Windows.Forms.CheckedListBox clbMonitorTypes;
        private System.Windows.Forms.Button btnTestMonitor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textSystem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox clbMonitors;
        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveConfigDlg;
        private System.Windows.Forms.OpenFileDialog openConfigDlg;
    }
}