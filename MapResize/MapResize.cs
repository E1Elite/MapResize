using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace MapResize
{
    public partial class frmMapResize : Form
    {
		static Logger log = LogManager.GetCurrentClassLogger();

        public frmMapResize()
        {
            InitializeComponent();
            this.Text = "Map Resize (TS & RA2) " + System.Windows.Forms.Application.ProductVersion;

			// Init logger
			var logconfig = new LoggingConfiguration();
			var logdir = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Log") + Path.DirectorySeparatorChar;
			var logfile = new FileTarget("logfile")
			{
				FileName = logdir + "log.txt",
				Layout = "${longdate} ${level}: ${message}",
				CreateDirs = true,
				ArchiveFileName = logdir + "log_{##}.txt",
				ArchiveNumbering = ArchiveNumberingMode.DateAndSequence,
				ArchiveDateFormat = "yyyyMMddHHmmss",
				ArchiveOldFileOnStartup = true,
				MaxArchiveFiles = 24,
			};
			#if DEBUG
			logconfig.AddRuleForAllLevels(logfile);
			#else
			logconfig.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
			#endif
			LogManager.Configuration = logconfig;
        }

        private void numTop_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numRight_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numBottom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numLeft_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnReadMap_Click(object sender, EventArgs e)
        {
			MapParser parser = new MapParser();
			string sizeValue = parser.GetMapSize(tbFilename.Text.Trim());
			tbCurrentSize.Text = "";
			if (!String.IsNullOrEmpty(sizeValue))
			{
				if (sizeValue.Contains("|"))
				{
                    int width = -1;
                    int height = -1;
                    int wNew = -1;
                    int hNew = -1;
					string[] parts = sizeValue.Split('|');
					if (parts.Length >= 2 && parts[0] != null  && parts[1] != null)
					{
						int.TryParse(parts[0], out width);
						int.TryParse(parts[1], out height);
					}
					sizeValue = "Filename: " + Path.GetFileName(tbFilename.Text) + "\r\n";
					sizeValue += "Current size: " + width + "x" + height + "\r\n";
					wNew = width + (int)numLeft.Value + (int)numRight.Value;
					hNew = height + (int)numTop.Value + (int)numBottom.Value;
					sizeValue += "Expected new size: " + wNew + "x" + hNew + "\r\n";
					if (wNew > 511 || hNew > 511)
						sizeValue += "Max size limit of W or H is 511\r\n";
					else if (wNew + hNew > 512)
						sizeValue += "Max size game limit W+H=512\r\n";
					if (wNew < 0 || hNew < 0)
						sizeValue += "Map size in negative\r\n";
					else if ((wNew * 2 - 1) * hNew < 780)
						sizeValue += "Map size too small\r\n";
				}
				tbCurrentSize.Text += sizeValue;
			}
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
			Map map = new Map();
			string filename = "";
			string filenameForUI = "";
			string sizeValue = "";
			string mapName = "";

			if (!String.IsNullOrEmpty(tbFilename.Text.Trim()))
				filename = Path.GetFullPath(tbFilename.Text.Trim());
			if (!String.IsNullOrEmpty(filename))
				filenameForUI =  Path.GetFileName(filename);
			if (!String.IsNullOrEmpty(tbMapName.Text.Trim()))
				mapName = tbMapName.Text.Trim();

			Options options = new Options
			{
				RemOutsideWaypoints = cbWaypointRem.Checked,
				KeepL0ClearTiles = cbL0ClearTiles.Checked,
				BetterTilesPackCompression = cbTilesCompress.Checked,
				MaintainLocalSize = cbLocalSizePos.Checked,
				MapName = mapName,
			};

			tbCurrentSize.Text = "Processing ...\r\n";
			btnResize.Enabled = false;
			if (!String.IsNullOrEmpty(filename) && map.Initialize(filename, options))
			{
				tbCurrentSize.Text += "Initializing map: " + filenameForUI + "\r\n";
				try
				{
					map.Resize((int)numTop.Value, (int)numRight.Value, (int)numBottom.Value, (int)numLeft.Value);
					if (rbSaveTo.Checked)
					{
						string saveToFilename = Path.GetFullPath(tbSaveTo.Text.Trim());
                        if (Directory.Exists(Path.GetDirectoryName(saveToFilename)))
						{
							filenameForUI = Path.GetFileName(saveToFilename);
							if (!File.Exists(saveToFilename))
							{
								map.Save(saveToFilename);
								sizeValue += "Resized map: " + filenameForUI + ".\r\nDone.";
							}
							else
							{
								var dialogResult = MessageBox.Show("Overwrite " + filenameForUI + "?", "File exists!", 
									MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
								if (dialogResult == DialogResult.Yes)
								{
									map.Save(saveToFilename);
									sizeValue += "Resized map: " + filenameForUI + ".\r\nDone.";
								}
							}
						}
						else
						{
							log.Error("Unable to find directory to save!\r\n");
							sizeValue += "Error in finding directory path.\r\n";
							throw new Exception("Invalid save path.");
						}
					}
					else if (rbOverwrite.Checked)
					{
						map.Save(filename);
						sizeValue += "Resized map: " + filenameForUI + ".\r\nDone.";
					}
					else
					{
						string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
						string fileInputNoExtn = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename));
						fileInputNoExtn = fileInputNoExtn.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
						string newFilename = fileInputNoExtn + "_" + timestamp + Path.GetExtension(filename);
						map.Save(newFilename);
						filenameForUI = Path.GetFileName(newFilename);
						sizeValue += "Resized map: " + filenameForUI + ".\r\nDone.";
					}
				}
				catch (Exception ex)
				{
					log.Error("Unable to generate resized map file!\r\n" + ex.ToString());
					sizeValue += "Error in resizing/saving map. Check log.\r\n";
				}
			}
			else
				sizeValue += "Error: Unable to initialize " + filenameForUI + "\r\n";
			tbCurrentSize.Text += sizeValue;
			btnResize.Enabled = true;
		}

        private void tbFilename_TextChanged(object sender, EventArgs e)
        {

        }

        private void ofdBrowse_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
			ofdBrowse.CheckFileExists = true;
			ofdBrowse.Multiselect = false;
			ofdBrowse.FileName = "";
			if (ofdBrowse.ShowDialog() == DialogResult.OK)
			{
				tbFilename.Text = ofdBrowse.FileName;
				tbSaveTo.Text = tbFilename.Text;
				ofdBrowse.InitialDirectory = Path.GetDirectoryName(ofdBrowse.FileName);
				tbMapName.Text = "";
			}
        }

        private void rbNewFile_CheckedChanged(object sender, EventArgs e)
        {
			updateSaveTo();
        }

        private void rbOverwrite_CheckedChanged(object sender, EventArgs e)
        {
			updateSaveTo();
        }

        private void rbSaveTo_CheckedChanged(object sender, EventArgs e)
        {
			updateSaveTo();
        }

        private void updateSaveTo()
		{
			if (rbNewFile.Checked || rbOverwrite.Checked)
				tbSaveTo.Enabled = false;
			else if (rbSaveTo.Checked)
				tbSaveTo.Enabled = true;
		}
    }
}
