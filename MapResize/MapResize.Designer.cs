namespace MapResize
{
    partial class frmMapResize
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
            this.lblMap = new System.Windows.Forms.Label();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.ofdBrowse = new System.Windows.Forms.OpenFileDialog();
            this.lblTop = new System.Windows.Forms.Label();
            this.lblLeft = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblBottom = new System.Windows.Forms.Label();
            this.tbCurrentSize = new System.Windows.Forms.TextBox();
            this.numTop = new System.Windows.Forms.NumericUpDown();
            this.numRight = new System.Windows.Forms.NumericUpDown();
            this.numBottom = new System.Windows.Forms.NumericUpDown();
            this.numLeft = new System.Windows.Forms.NumericUpDown();
            this.btnReadMap = new System.Windows.Forms.Button();
            this.btnResize = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.cbWaypointRem = new System.Windows.Forms.CheckBox();
            this.cbL0ClearTiles = new System.Windows.Forms.CheckBox();
            this.ttTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.cbTilesCompress = new System.Windows.Forms.CheckBox();
            this.cbLocalSizePos = new System.Windows.Forms.CheckBox();
            this.lblMapName = new System.Windows.Forms.Label();
            this.rbNewFile = new System.Windows.Forms.RadioButton();
            this.rbOverwrite = new System.Windows.Forms.RadioButton();
            this.rbSaveTo = new System.Windows.Forms.RadioButton();
            this.tbMapName = new System.Windows.Forms.TextBox();
            this.tbSaveTo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMap
            // 
            this.lblMap.AutoSize = true;
            this.lblMap.Location = new System.Drawing.Point(10, 10);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(34, 13);
            this.lblMap.TabIndex = 0;
            this.lblMap.Text = "Map :";
            // 
            // tbFilename
            // 
            this.tbFilename.AllowDrop = true;
            this.tbFilename.Location = new System.Drawing.Point(12, 33);
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbFilename.Size = new System.Drawing.Size(600, 20);
            this.tbFilename.TabIndex = 1;
            this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
            // 
            // ofdBrowse
            // 
            this.ofdBrowse.DefaultExt = "map";
            this.ofdBrowse.Filter = "Map files (*.map;*.mpr;*.yrm)|*.map;*.mpr;*.yrm|All files (*.*)|*.*";
            this.ofdBrowse.RestoreDirectory = true;
            this.ofdBrowse.Title = "Browse";
            this.ofdBrowse.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdBrowse_FileOk);
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.Location = new System.Drawing.Point(261, 60);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(26, 13);
            this.lblTop.TabIndex = 2;
            this.lblTop.Text = "Top";
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.Location = new System.Drawing.Point(60, 139);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(25, 13);
            this.lblLeft.TabIndex = 3;
            this.lblLeft.Text = "Left";
            // 
            // lblRight
            // 
            this.lblRight.AutoSize = true;
            this.lblRight.Location = new System.Drawing.Point(461, 139);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(32, 13);
            this.lblRight.TabIndex = 4;
            this.lblRight.Text = "Right";
            // 
            // lblBottom
            // 
            this.lblBottom.AutoSize = true;
            this.lblBottom.Location = new System.Drawing.Point(256, 266);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new System.Drawing.Size(40, 13);
            this.lblBottom.TabIndex = 5;
            this.lblBottom.Text = "Bottom";
            // 
            // tbCurrentSize
            // 
            this.tbCurrentSize.Location = new System.Drawing.Point(142, 111);
            this.tbCurrentSize.Multiline = true;
            this.tbCurrentSize.Name = "tbCurrentSize";
            this.tbCurrentSize.ReadOnly = true;
            this.tbCurrentSize.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCurrentSize.Size = new System.Drawing.Size(280, 116);
            this.tbCurrentSize.TabIndex = 6;
            // 
            // numTop
            // 
            this.numTop.Location = new System.Drawing.Point(232, 80);
            this.numTop.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.numTop.Minimum = new decimal(new int[] {
            511,
            0,
            0,
            -2147483648});
            this.numTop.Name = "numTop";
            this.numTop.Size = new System.Drawing.Size(100, 20);
            this.numTop.TabIndex = 7;
            this.numTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numTop.ValueChanged += new System.EventHandler(this.numTop_ValueChanged);
            // 
            // numRight
            // 
            this.numRight.Location = new System.Drawing.Point(435, 160);
            this.numRight.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.numRight.Minimum = new decimal(new int[] {
            511,
            0,
            0,
            -2147483648});
            this.numRight.Name = "numRight";
            this.numRight.Size = new System.Drawing.Size(100, 20);
            this.numRight.TabIndex = 8;
            this.numRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numRight.ValueChanged += new System.EventHandler(this.numRight_ValueChanged);
            // 
            // numBottom
            // 
            this.numBottom.Location = new System.Drawing.Point(232, 238);
            this.numBottom.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.numBottom.Minimum = new decimal(new int[] {
            511,
            0,
            0,
            -2147483648});
            this.numBottom.Name = "numBottom";
            this.numBottom.Size = new System.Drawing.Size(100, 20);
            this.numBottom.TabIndex = 9;
            this.numBottom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numBottom.ValueChanged += new System.EventHandler(this.numBottom_ValueChanged);
            // 
            // numLeft
            // 
            this.numLeft.Location = new System.Drawing.Point(30, 160);
            this.numLeft.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.numLeft.Minimum = new decimal(new int[] {
            511,
            0,
            0,
            -2147483648});
            this.numLeft.Name = "numLeft";
            this.numLeft.Size = new System.Drawing.Size(100, 20);
            this.numLeft.TabIndex = 10;
            this.numLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numLeft.ValueChanged += new System.EventHandler(this.numLeft_ValueChanged);
            // 
            // btnReadMap
            // 
            this.btnReadMap.Location = new System.Drawing.Point(500, 225);
            this.btnReadMap.Name = "btnReadMap";
            this.btnReadMap.Size = new System.Drawing.Size(108, 32);
            this.btnReadMap.TabIndex = 11;
            this.btnReadMap.Text = "Calculate";
            this.btnReadMap.UseVisualStyleBackColor = true;
            this.btnReadMap.Click += new System.EventHandler(this.btnReadMap_Click);
            // 
            // btnResize
            // 
            this.btnResize.Location = new System.Drawing.Point(500, 396);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(108, 32);
            this.btnResize.TabIndex = 12;
            this.btnResize.Text = "Resize";
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(500, 64);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(108, 32);
            this.btnBrowse.TabIndex = 13;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // cbWaypointRem
            // 
            this.cbWaypointRem.AutoSize = true;
            this.cbWaypointRem.Location = new System.Drawing.Point(16, 288);
            this.cbWaypointRem.Name = "cbWaypointRem";
            this.cbWaypointRem.Size = new System.Drawing.Size(176, 17);
            this.cbWaypointRem.TabIndex = 14;
            this.cbWaypointRem.Text = "Remove waypoints outside map";
            this.ttTooltip.SetToolTip(this.cbWaypointRem, "Remove if those waypoints don\'t have any references.\r\nOtherwise by default, those" +
        " are put at the bottom of the map.");
            this.cbWaypointRem.UseVisualStyleBackColor = true;
            // 
            // cbL0ClearTiles
            // 
            this.cbL0ClearTiles.AutoSize = true;
            this.cbL0ClearTiles.Location = new System.Drawing.Point(16, 312);
            this.cbL0ClearTiles.Name = "cbL0ClearTiles";
            this.cbL0ClearTiles.Size = new System.Drawing.Size(132, 17);
            this.cbL0ClearTiles.TabIndex = 15;
            this.cbL0ClearTiles.Text = "Keep level 0 clear tiles";
            this.ttTooltip.SetToolTip(this.cbL0ClearTiles, "Removing those gives better IsoMapPack5 compression.");
            this.cbL0ClearTiles.UseVisualStyleBackColor = true;
            // 
            // cbTilesCompress
            // 
            this.cbTilesCompress.AutoSize = true;
            this.cbTilesCompress.Checked = true;
            this.cbTilesCompress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTilesCompress.Location = new System.Drawing.Point(320, 288);
            this.cbTilesCompress.Name = "cbTilesCompress";
            this.cbTilesCompress.Size = new System.Drawing.Size(164, 17);
            this.cbTilesCompress.TabIndex = 16;
            this.cbTilesCompress.Text = "Better tiles pack compression";
            this.ttTooltip.SetToolTip(this.cbTilesCompress, "A bit slower.");
            this.cbTilesCompress.UseVisualStyleBackColor = true;
            // 
            // cbLocalSizePos
            // 
            this.cbLocalSizePos.AutoSize = true;
            this.cbLocalSizePos.Location = new System.Drawing.Point(320, 312);
            this.cbLocalSizePos.Name = "cbLocalSizePos";
            this.cbLocalSizePos.Size = new System.Drawing.Size(154, 17);
            this.cbLocalSizePos.TabIndex = 17;
            this.cbLocalSizePos.Text = "Maintain LocalSize position";
            this.ttTooltip.SetToolTip(this.cbLocalSizePos, "Maintain visible map position on non-negative resize inputs.");
            this.cbLocalSizePos.UseVisualStyleBackColor = true;
            // 
            // lblMapName
            // 
            this.lblMapName.AutoSize = true;
            this.lblMapName.Location = new System.Drawing.Point(16, 400);
            this.lblMapName.Name = "lblMapName";
            this.lblMapName.Size = new System.Drawing.Size(63, 13);
            this.lblMapName.TabIndex = 23;
            this.lblMapName.Text = "Map name: ";
            this.ttTooltip.SetToolTip(this.lblMapName, "Overwrite Name in [Basic] section. No change if left empty.");
            // 
            // rbNewFile
            // 
            this.rbNewFile.AutoSize = true;
            this.rbNewFile.Checked = true;
            this.rbNewFile.Location = new System.Drawing.Point(16, 338);
            this.rbNewFile.Name = "rbNewFile";
            this.rbNewFile.Size = new System.Drawing.Size(101, 17);
            this.rbNewFile.TabIndex = 19;
            this.rbNewFile.TabStop = true;
            this.rbNewFile.Text = "Save to new file";
            this.ttTooltip.SetToolTip(this.rbNewFile, "Save to auto generated filename with timestamp.");
            this.rbNewFile.UseVisualStyleBackColor = true;
            this.rbNewFile.CheckedChanged += new System.EventHandler(this.rbNewFile_CheckedChanged);
            // 
            // rbOverwrite
            // 
            this.rbOverwrite.AutoSize = true;
            this.rbOverwrite.Location = new System.Drawing.Point(224, 338);
            this.rbOverwrite.Name = "rbOverwrite";
            this.rbOverwrite.Size = new System.Drawing.Size(122, 17);
            this.rbOverwrite.TabIndex = 20;
            this.rbOverwrite.TabStop = true;
            this.rbOverwrite.Text = "Overwrite original file";
            this.ttTooltip.SetToolTip(this.rbOverwrite, "Save to the original file, will be overwritten.");
            this.rbOverwrite.UseVisualStyleBackColor = true;
            this.rbOverwrite.CheckedChanged += new System.EventHandler(this.rbOverwrite_CheckedChanged);
            // 
            // rbSaveTo
            // 
            this.rbSaveTo.AutoSize = true;
            this.rbSaveTo.Location = new System.Drawing.Point(434, 338);
            this.rbSaveTo.Name = "rbSaveTo";
            this.rbSaveTo.Size = new System.Drawing.Size(106, 17);
            this.rbSaveTo.TabIndex = 21;
            this.rbSaveTo.TabStop = true;
            this.rbSaveTo.Text = "Save to following:";
            this.ttTooltip.SetToolTip(this.rbSaveTo, "Save to a valid filename given in the following text box. New directories are not" +
        " created.");
            this.rbSaveTo.UseVisualStyleBackColor = true;
            this.rbSaveTo.CheckedChanged += new System.EventHandler(this.rbSaveTo_CheckedChanged);
            // 
            // tbMapName
            // 
            this.tbMapName.Location = new System.Drawing.Point(97, 399);
            this.tbMapName.Name = "tbMapName";
            this.tbMapName.Size = new System.Drawing.Size(320, 20);
            this.tbMapName.TabIndex = 24;
            // 
            // tbSaveTo
            // 
            this.tbSaveTo.Enabled = false;
            this.tbSaveTo.Location = new System.Drawing.Point(12, 363);
            this.tbSaveTo.Name = "tbSaveTo";
            this.tbSaveTo.Size = new System.Drawing.Size(600, 20);
            this.tbSaveTo.TabIndex = 22;
            // 
            // frmMapResize
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.tbSaveTo);
            this.Controls.Add(this.rbSaveTo);
            this.Controls.Add(this.rbOverwrite);
            this.Controls.Add(this.rbNewFile);
            this.Controls.Add(this.tbMapName);
            this.Controls.Add(this.lblMapName);
            this.Controls.Add(this.cbLocalSizePos);
            this.Controls.Add(this.cbTilesCompress);
            this.Controls.Add(this.cbL0ClearTiles);
            this.Controls.Add(this.cbWaypointRem);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnResize);
            this.Controls.Add(this.btnReadMap);
            this.Controls.Add(this.numLeft);
            this.Controls.Add(this.numBottom);
            this.Controls.Add(this.numRight);
            this.Controls.Add(this.numTop);
            this.Controls.Add(this.tbCurrentSize);
            this.Controls.Add(this.lblBottom);
            this.Controls.Add(this.lblRight);
            this.Controls.Add(this.lblLeft);
            this.Controls.Add(this.lblTop);
            this.Controls.Add(this.tbFilename);
            this.Controls.Add(this.lblMap);
            this.MaximizeBox = false;
            this.Name = "frmMapResize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map Resize (TS & RA2)";
            ((System.ComponentModel.ISupportInitialize)(this.numTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMap;
        private System.Windows.Forms.TextBox tbFilename;
        private System.Windows.Forms.OpenFileDialog ofdBrowse;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Label lblBottom;
        private System.Windows.Forms.TextBox tbCurrentSize;
        private System.Windows.Forms.NumericUpDown numTop;
        private System.Windows.Forms.NumericUpDown numRight;
        private System.Windows.Forms.NumericUpDown numBottom;
        private System.Windows.Forms.NumericUpDown numLeft;
        private System.Windows.Forms.Button btnReadMap;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox cbWaypointRem;
        private System.Windows.Forms.CheckBox cbL0ClearTiles;
        private System.Windows.Forms.ToolTip ttTooltip;
        private System.Windows.Forms.CheckBox cbTilesCompress;
        private System.Windows.Forms.CheckBox cbLocalSizePos;
        private System.Windows.Forms.Label lblMapName;
        private System.Windows.Forms.TextBox tbMapName;
        private System.Windows.Forms.RadioButton rbNewFile;
        private System.Windows.Forms.RadioButton rbOverwrite;
        private System.Windows.Forms.RadioButton rbSaveTo;
        private System.Windows.Forms.TextBox tbSaveTo;
    }
}

