using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace GW2LogWatcher
{
    public partial class MainForm : Form
    {
        private readonly Log log;
        private readonly List<Tuple<string, LogFileHandler>> queue;
        private readonly RaidHeroesHandler raidHeroesHandler = new RaidHeroesHandler();
        private readonly DpsReportHandler dpsReportHandler = new DpsReportHandler();
        private readonly RaidarHandler raidarHandler = new RaidarHandler();

        private volatile bool stopping;

        public MainForm()
        {
            InitializeComponent();
            log = new Log(logLabel, logLabel.Height / 12);
            queue = new List<Tuple<string, LogFileHandler>>();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
            UpdateLogDiretory();

            if (uploadRaidar.Checked)
                CheckRaidarCredentials();

            backgroundWorker.RunWorkerAsync();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopping = true;
            SaveSettings();
        }

        private void LoadSettings()
        { 
            var settings = Properties.Settings.Default;

            if (settings.LogDirectory == "")
            {
                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                settings.LogDirectory = Path.Combine(documents, "Guild Wars 2", "addons", "arcdps", "arcdps.cbtlogs");
            }

            logDirectoryInput.Text = settings.LogDirectory;

            if (settings.RaidHeroesLocation == "")
            {
                var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                settings.LogDirectory = Path.Combine(programFiles, "RaidHeroes", "raid_heroes.exe");
            }

            raidHeroesLocation.Text = settings.RaidHeroesLocation;
            RaidHeroesHandler.RaidHeroesLocation = settings.RaidHeroesLocation;

            runRaidHeroes.Checked = settings.RaidHeroes;
            uploadRaidar.Checked = settings.UploadRaidar;
            uploadDpsReport.Checked = settings.UploadDpsReport;

            raidarUsername.Text = settings.RaidarUsername;
            raidarPassword.Text = settings.RaidarPassword;
        }

        private void SaveSettings()
        {
            var settings = Properties.Settings.Default;

            settings.LogDirectory = logDirectoryInput.Text;
            settings.RaidHeroesLocation = raidHeroesLocation.Text;
            settings.RaidHeroes = runRaidHeroes.Checked;
            settings.UploadRaidar = uploadRaidar.Checked;
            settings.UploadDpsReport = uploadDpsReport.Checked;
            settings.RaidarUsername = raidarUsername.Text;
            settings.RaidarPassword = raidarPassword.Text;

            settings.Save();
        }

        private void UpdateLogDiretory()
        {
            if (Directory.Exists(logDirectoryInput.Text))
            {
                fileSystemWatcher.Path = logDirectoryInput.Text;
                fileSystemWatcher.EnableRaisingEvents = true;
                log.Add("Starting to watch ", logDirectoryInput.Text);
            }
            else
            {
                fileSystemWatcher.EnableRaisingEvents = false;
                log.Add("Stopped watching");
            }
        }

        private void CheckRaidarCredentials()
        {
            if (raidarUsername.Text == "")
            {
                log.Add("Raidar: username missing");
                return;
            }

            if (raidarPassword.Text == "")
            {
                log.Add("Raidar: password missing");
                return;
            }

            try
            {
                RaidarSession.Instance.Login(raidarUsername.Text, raidarPassword.Text);
                log.Add("Raidar: username and password OK");
            }
            catch (WebException ex)
            {
                log.Add("Raidar: ", ex);
            }
        }

        private void fileSystem_Event(object sender, FileSystemEventArgs e)
        {
            if (!e.FullPath.EndsWith(".evtc") && !e.FullPath.EndsWith(".evtc.zip"))
                return;

            //log.Add("File system event: ", e.ChangeType, " ", e.FullPath);

            if (e.ChangeType != WatcherChangeTypes.Created)
                return;

            // TODO: Check duplicates or something?
            // TODO: Wait until file is written?

            if (runRaidHeroes.Checked)
            {
                log.Add("Running RaidHeroes for ", Path.GetFileName(e.FullPath));
                queue.Add(new Tuple<string, LogFileHandler>(e.FullPath, raidHeroesHandler));
                // RunHandler<RaidHeroesHandler>(e.FullPath);
            }

            if (uploadRaidar.Checked)
            {
                if (RaidarSession.Instance.IsLoggedIn)
                {
                    log.Add("Uploading ", Path.GetFileName(e.FullPath), " to Raidar");
                    queue.Add(new Tuple<string, LogFileHandler>(e.FullPath, raidarHandler));
                    // RunHandler<RaidarHandler>(e.FullPath);
                }
                else
                {
                    log.Add("Not uploading ", Path.GetFileName(e.FullPath), " to Raidar - not logged in");
                }
            }

            if (uploadDpsReport.Checked)
            {
                log.Add("Uploading ", Path.GetFileName(e.FullPath), " to dps.report");
                queue.Add(new Tuple<string, LogFileHandler>(e.FullPath, dpsReportHandler));
                // RunHandler<DpsReportHandler>(e.FullPath);
            }
        }

        private void LogResult(LogFileHandler handler, LogFileHandlerResults result)
        {
            if (result.Success == false)
            {
                log.Add(handler, " failed: ", result.Output);
            }
            else if (result.Output.StartsWith("http://"))
            {
                log.Add(handler, " uploaded to ", new LogLink {Label = result.Output.Substring(7), Address = result.Output});
            }
            else if (result.Output.StartsWith("https://"))
            {
                log.Add(handler, " uploaded to ", new LogLink {Label = result.Output.Substring(8), Address = result.Output});
            }
            else
            {
                log.Add(handler, " created ", new LogLink {Label = Path.GetFileName(result.Output), Address = result.Output});
            }
        }

        private void logLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start((string)e.Link.LinkData);
        }

        private void logDirectoryChange_Click(object sender, EventArgs e)
        {
            folderBrowser.SelectedPath = logDirectoryInput.Text;
            if (folderBrowser.ShowDialog(this) == DialogResult.OK)
            {
                logDirectoryInput.Text = folderBrowser.SelectedPath;
                UpdateLogDiretory();
            }
        }

        private void raidHeroesChange_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(raidHeroesLocation.Text);
            }
            catch (ArgumentException)
            {
            }

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                raidHeroesLocation.Text = openFileDialog.FileName;
                RaidHeroesHandler.RaidHeroesLocation = openFileDialog.FileName;
            }
        }

        private void raidarCheck_Click(object sender, EventArgs e)
        {
            CheckRaidarCredentials();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!stopping)
            {
                if (queue.Count > 0)
                {
                    var filename = queue[0].Item1;
                    var handler = queue[0].Item2;
                    queue.RemoveAt(0);

                    var result = handler.Handle(filename);
                    LogResult(handler, result);
                }
                else
                {
                    Thread.Sleep(500);
                }
            }
        }
    }
}
