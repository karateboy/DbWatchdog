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
            this.clbMonitorTypes = new System.Windows.Forms.CheckedListBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.numCheckInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "資料庫類型:";
            // 
            // btnMongo
            // 
            this.btnMongo.Location = new System.Drawing.Point(325, 35);
            this.btnMongo.Name = "btnMongo";
            this.btnMongo.Size = new System.Drawing.Size(214, 50);
            this.btnMongo.TabIndex = 1;
            this.btnMongo.TabStop = true;
            this.btnMongo.Text = "MongoDb";
            this.btnMongo.UseVisualStyleBackColor = true;
            this.btnMongo.CheckedChanged += new System.EventHandler(this.btnMongo_CheckedChanged);
            // 
            // btnSQL
            // 
            this.btnSQL.Location = new System.Drawing.Point(545, 35);
            this.btnSQL.Name = "btnSQL";
            this.btnSQL.Size = new System.Drawing.Size(254, 50);
            this.btnSQL.TabIndex = 2;
            this.btnSQL.TabStop = true;
            this.btnSQL.Text = "SQL Server";
            this.btnSQL.UseVisualStyleBackColor = true;
            this.btnSQL.CheckedChanged += new System.EventHandler(this.btnSQL_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 50);
            this.label2.TabIndex = 3;
            this.label2.Text = "連接字串:";
            // 
            // txtDbConnectionStr
            // 
            this.txtDbConnectionStr.Location = new System.Drawing.Point(325, 128);
            this.txtDbConnectionStr.Name = "txtDbConnectionStr";
            this.txtDbConnectionStr.Size = new System.Drawing.Size(933, 51);
            this.txtDbConnectionStr.TabIndex = 4;
            this.txtDbConnectionStr.TextChanged += new System.EventHandler(this.txtDbConnectionStr_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 314);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(224, 50);
            this.label3.TabIndex = 5;
            this.label3.Text = "檢查測項:";
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(1290, 124);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(130, 50);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "連接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // clbMonitorTypes
            // 
            this.clbMonitorTypes.Enabled = false;
            this.clbMonitorTypes.FormattingEnabled = true;
            this.clbMonitorTypes.Location = new System.Drawing.Point(325, 314);
            this.clbMonitorTypes.Name = "clbMonitorTypes";
            this.clbMonitorTypes.Size = new System.Drawing.Size(374, 52);
            this.clbMonitorTypes.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 407);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(224, 50);
            this.label4.TabIndex = 8;
            this.label4.Text = "檢查頻率:";
            // 
            // numCheckInterval
            // 
            this.numCheckInterval.Location = new System.Drawing.Point(325, 407);
            this.numCheckInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCheckInterval.Name = "numCheckInterval";
            this.numCheckInterval.Size = new System.Drawing.Size(120, 51);
            this.numCheckInterval.TabIndex = 9;
            this.numCheckInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 500);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(299, 50);
            this.label5.TabIndex = 10;
            this.label5.Text = "Line通報權杖:";
            // 
            // textLineToken
            // 
            this.textLineToken.Location = new System.Drawing.Point(325, 500);
            this.textLineToken.Name = "textLineToken";
            this.textLineToken.Size = new System.Drawing.Size(933, 51);
            this.textLineToken.TabIndex = 11;
            this.textLineToken.TextChanged += new System.EventHandler(this.textLineToken_TextChanged);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(325, 605);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(178, 69);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "開始監測";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(545, 605);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(178, 69);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "結束";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // textDatabase
            // 
            this.textDatabase.Location = new System.Drawing.Point(325, 221);
            this.textDatabase.Name = "textDatabase";
            this.textDatabase.Size = new System.Drawing.Size(933, 51);
            this.textDatabase.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(24, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(224, 50);
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
            this.btnTestLine.Enabled = false;
            this.btnTestLine.Location = new System.Drawing.Point(1290, 501);
            this.btnTestLine.Name = "btnTestLine";
            this.btnTestLine.Size = new System.Drawing.Size(130, 50);
            this.btnTestLine.TabIndex = 16;
            this.btnTestLine.Text = "測試";
            this.btnTestLine.UseVisualStyleBackColor = true;
            this.btnTestLine.Click += new System.EventHandler(this.btnTestLine_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(20F, 42F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1440, 712);
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
        private System.Windows.Forms.CheckedListBox clbMonitorTypes;
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
    }
}