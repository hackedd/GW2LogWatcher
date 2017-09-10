namespace GW2LogWatcher
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
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.logDirectoryChange = new System.Windows.Forms.Button();
            this.logDirectoryInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.raidHeroesChange = new System.Windows.Forms.Button();
            this.runRaidHeroes = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.raidHeroesLocation = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.raidarPassword = new System.Windows.Forms.TextBox();
            this.raidarCheck = new System.Windows.Forms.Button();
            this.raidarUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.uploadRaidar = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.uploadDpsReport = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.logLabel = new System.Windows.Forms.LinkLabel();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileSystemWatcher
            // 
            this.fileSystemWatcher.EnableRaisingEvents = true;
            this.fileSystemWatcher.IncludeSubdirectories = true;
            this.fileSystemWatcher.SynchronizingObject = this;
            this.fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.fileSystem_Event);
            this.fileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.fileSystem_Event);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logDirectoryChange);
            this.groupBox1.Controls.Add(this.logDirectoryInput);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 66);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Settings";
            // 
            // logDirectoryChange
            // 
            this.logDirectoryChange.Location = new System.Drawing.Point(430, 33);
            this.logDirectoryChange.Name = "logDirectoryChange";
            this.logDirectoryChange.Size = new System.Drawing.Size(75, 20);
            this.logDirectoryChange.TabIndex = 1;
            this.logDirectoryChange.Text = "Change...";
            this.logDirectoryChange.UseVisualStyleBackColor = true;
            this.logDirectoryChange.Click += new System.EventHandler(this.logDirectoryChange_Click);
            // 
            // logDirectoryInput
            // 
            this.logDirectoryInput.Location = new System.Drawing.Point(9, 33);
            this.logDirectoryInput.Name = "logDirectoryInput";
            this.logDirectoryInput.Size = new System.Drawing.Size(415, 20);
            this.logDirectoryInput.TabIndex = 1;
            this.logDirectoryInput.TabStop = false;
            this.logDirectoryInput.Text = "D:\\Paul\\Documents\\Guild Wars 2\\addons\\arcdps\\arcdps.cbtlogs";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(494, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "arcdps Log Directory";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.raidHeroesChange);
            this.groupBox2.Controls.Add(this.runRaidHeroes);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.raidHeroesLocation);
            this.groupBox2.Location = new System.Drawing.Point(12, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(513, 84);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Raid Heroes";
            // 
            // raidHeroesChange
            // 
            this.raidHeroesChange.Location = new System.Drawing.Point(430, 56);
            this.raidHeroesChange.Name = "raidHeroesChange";
            this.raidHeroesChange.Size = new System.Drawing.Size(75, 20);
            this.raidHeroesChange.TabIndex = 3;
            this.raidHeroesChange.Text = "Change...";
            this.raidHeroesChange.UseVisualStyleBackColor = true;
            this.raidHeroesChange.Click += new System.EventHandler(this.raidHeroesChange_Click);
            // 
            // runRaidHeroes
            // 
            this.runRaidHeroes.Checked = true;
            this.runRaidHeroes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.runRaidHeroes.Location = new System.Drawing.Point(9, 19);
            this.runRaidHeroes.Name = "runRaidHeroes";
            this.runRaidHeroes.Size = new System.Drawing.Size(496, 17);
            this.runRaidHeroes.TabIndex = 2;
            this.runRaidHeroes.Text = "Run RaidHeroes";
            this.runRaidHeroes.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(499, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "RaidHeroes Location";
            // 
            // raidHeroesLocation
            // 
            this.raidHeroesLocation.Location = new System.Drawing.Point(9, 56);
            this.raidHeroesLocation.Name = "raidHeroesLocation";
            this.raidHeroesLocation.Size = new System.Drawing.Size(415, 20);
            this.raidHeroesLocation.TabIndex = 9;
            this.raidHeroesLocation.TabStop = false;
            this.raidHeroesLocation.Text = "D:\\Programs\\RaidHeroes\\raid_heroes.exe";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.raidarPassword);
            this.groupBox3.Controls.Add(this.raidarCheck);
            this.groupBox3.Controls.Add(this.raidarUsername);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.uploadRaidar);
            this.groupBox3.Location = new System.Drawing.Point(12, 176);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(513, 87);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "GW2 Raidar";
            // 
            // raidarPassword
            // 
            this.raidarPassword.Location = new System.Drawing.Point(219, 58);
            this.raidarPassword.Name = "raidarPassword";
            this.raidarPassword.Size = new System.Drawing.Size(205, 20);
            this.raidarPassword.TabIndex = 6;
            this.raidarPassword.UseSystemPasswordChar = true;
            // 
            // raidarCheck
            // 
            this.raidarCheck.Location = new System.Drawing.Point(430, 58);
            this.raidarCheck.Name = "raidarCheck";
            this.raidarCheck.Size = new System.Drawing.Size(75, 20);
            this.raidarCheck.TabIndex = 7;
            this.raidarCheck.Text = "Check...";
            this.raidarCheck.UseVisualStyleBackColor = true;
            this.raidarCheck.Click += new System.EventHandler(this.raidarCheck_Click);
            // 
            // raidarUsername
            // 
            this.raidarUsername.Location = new System.Drawing.Point(9, 58);
            this.raidarUsername.Name = "raidarUsername";
            this.raidarUsername.Size = new System.Drawing.Size(205, 20);
            this.raidarUsername.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(494, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "GW2 Raidar Account";
            // 
            // uploadRaidar
            // 
            this.uploadRaidar.Location = new System.Drawing.Point(6, 19);
            this.uploadRaidar.Name = "uploadRaidar";
            this.uploadRaidar.Size = new System.Drawing.Size(496, 17);
            this.uploadRaidar.TabIndex = 4;
            this.uploadRaidar.Text = "Upload to GW2 Raidar";
            this.uploadRaidar.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.uploadDpsReport);
            this.groupBox4.Location = new System.Drawing.Point(12, 269);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(513, 46);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "dps.report";
            // 
            // uploadDpsReport
            // 
            this.uploadDpsReport.Location = new System.Drawing.Point(6, 19);
            this.uploadDpsReport.Name = "uploadDpsReport";
            this.uploadDpsReport.Size = new System.Drawing.Size(496, 17);
            this.uploadDpsReport.TabIndex = 8;
            this.uploadDpsReport.Text = "Upload to dps.report";
            this.uploadDpsReport.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.logLabel);
            this.groupBox5.Location = new System.Drawing.Point(12, 322);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(513, 156);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Log";
            // 
            // logLabel
            // 
            this.logLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logLabel.Location = new System.Drawing.Point(3, 16);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(507, 137);
            this.logLabel.TabIndex = 15;
            this.logLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.logLabel_LinkClicked);
            // 
            // folderBrowser
            // 
            this.folderBrowser.ShowNewFolderButton = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Raid Heroes|raid_heroes.exe|All Files|*.*";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 490);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Guild Wars 2 Log Watcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.FileSystemWatcher fileSystemWatcher;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button logDirectoryChange;
        private System.Windows.Forms.TextBox logDirectoryInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox uploadDpsReport;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button raidHeroesChange;
        private System.Windows.Forms.CheckBox runRaidHeroes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox raidHeroesLocation;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox raidarPassword;
        private System.Windows.Forms.Button raidarCheck;
        private System.Windows.Forms.TextBox raidarUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox uploadRaidar;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.LinkLabel logLabel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}

