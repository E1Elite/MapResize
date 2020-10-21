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
            ((System.ComponentModel.ISupportInitialize)(this.numTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMap
            // 
            this.lblMap.AutoSize = true;
            this.lblMap.Location = new System.Drawing.Point(10, 12);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(34, 13);
            this.lblMap.TabIndex = 0;
            this.lblMap.Text = "Map :";
            // 
            // tbFilename
            // 
            this.tbFilename.AllowDrop = true;
            this.tbFilename.Location = new System.Drawing.Point(12, 35);
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbFilename.Size = new System.Drawing.Size(560, 20);
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
            this.lblTop.Location = new System.Drawing.Point(241, 68);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(26, 13);
            this.lblTop.TabIndex = 2;
            this.lblTop.Text = "Top";
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.Location = new System.Drawing.Point(60, 147);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(25, 13);
            this.lblLeft.TabIndex = 3;
            this.lblLeft.Text = "Left";
            // 
            // lblRight
            // 
            this.lblRight.AutoSize = true;
            this.lblRight.Location = new System.Drawing.Point(421, 147);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(32, 13);
            this.lblRight.TabIndex = 4;
            this.lblRight.Text = "Right";
            // 
            // lblBottom
            // 
            this.lblBottom.AutoSize = true;
            this.lblBottom.Location = new System.Drawing.Point(236, 274);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new System.Drawing.Size(40, 13);
            this.lblBottom.TabIndex = 5;
            this.lblBottom.Text = "Bottom";
            // 
            // tbCurrentSize
            // 
            this.tbCurrentSize.Location = new System.Drawing.Point(142, 119);
            this.tbCurrentSize.Multiline = true;
            this.tbCurrentSize.Name = "tbCurrentSize";
            this.tbCurrentSize.ReadOnly = true;
            this.tbCurrentSize.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCurrentSize.Size = new System.Drawing.Size(240, 116);
            this.tbCurrentSize.TabIndex = 6;
            // 
            // numTop
            // 
            this.numTop.Location = new System.Drawing.Point(212, 88);
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
            this.numRight.Location = new System.Drawing.Point(395, 168);
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
            this.numBottom.Location = new System.Drawing.Point(212, 246);
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
            this.numLeft.Location = new System.Drawing.Point(30, 168);
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
            this.btnReadMap.Location = new System.Drawing.Point(460, 278);
            this.btnReadMap.Name = "btnReadMap";
            this.btnReadMap.Size = new System.Drawing.Size(108, 32);
            this.btnReadMap.TabIndex = 11;
            this.btnReadMap.Text = "Calculate";
            this.btnReadMap.UseVisualStyleBackColor = true;
            this.btnReadMap.Click += new System.EventHandler(this.btnReadMap_Click);
            // 
            // btnResize
            // 
            this.btnResize.Location = new System.Drawing.Point(460, 348);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(108, 32);
            this.btnResize.TabIndex = 12;
            this.btnResize.Text = "Resize";
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(460, 66);
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
            this.cbWaypointRem.Location = new System.Drawing.Point(29, 294);
            this.cbWaypointRem.Name = "cbWaypointRem";
            this.cbWaypointRem.Size = new System.Drawing.Size(270, 17);
            this.cbWaypointRem.TabIndex = 14;
            this.cbWaypointRem.Text = "Remove waypoints outside map when reducing size";
            this.ttTooltip.SetToolTip(this.cbWaypointRem, "Remove if those waypoints don\'t have any references.\r\nOtherwise by default, those" +
        " are put at the bottom of the map.");
            this.cbWaypointRem.UseVisualStyleBackColor = true;
            // 
            // cbL0ClearTiles
            // 
            this.cbL0ClearTiles.AutoSize = true;
            this.cbL0ClearTiles.Location = new System.Drawing.Point(29, 318);
            this.cbL0ClearTiles.Name = "cbL0ClearTiles";
            this.cbL0ClearTiles.Size = new System.Drawing.Size(274, 17);
            this.cbL0ClearTiles.TabIndex = 15;
            this.cbL0ClearTiles.Text = "Keep level 0 clear tiles during tiles pack compression";
            this.ttTooltip.SetToolTip(this.cbL0ClearTiles, "Removing those gives better IsoMapPack5 compression.");
            this.cbL0ClearTiles.UseVisualStyleBackColor = true;
            // 
            // cbTilesCompress
            // 
            this.cbTilesCompress.AutoSize = true;
            this.cbTilesCompress.Checked = true;
            this.cbTilesCompress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTilesCompress.Location = new System.Drawing.Point(29, 342);
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
            this.cbLocalSizePos.Location = new System.Drawing.Point(29, 366);
            this.cbLocalSizePos.Name = "cbLocalSizePos";
            this.cbLocalSizePos.Size = new System.Drawing.Size(271, 17);
            this.cbLocalSizePos.TabIndex = 17;
            this.cbLocalSizePos.Text = "Maintain LocalSize position (on non-negative inputs)";
            this.ttTooltip.SetToolTip(this.cbLocalSizePos, "Maintain visible map position on size increase.");
            this.cbLocalSizePos.UseVisualStyleBackColor = true;
            // 
            // frmMapResize
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(584, 394);
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
    }
}

