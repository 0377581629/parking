namespace ParkingApp
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.lbDay = new System.Windows.Forms.Label();
            this.lbDayOfWeek = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnConfig = new MetroFramework.Controls.MetroTile();
            this.btnSupervise = new MetroFramework.Controls.MetroTile();
            this.btnHistory = new MetroFramework.Controls.MetroTile();
            this.btnTestModelAI = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbDay
            // 
            this.lbDay.AutoSize = true;
            this.lbDay.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDay.ForeColor = System.Drawing.Color.Maroon;
            this.lbDay.Location = new System.Drawing.Point(242, 329);
            this.lbDay.Name = "lbDay";
            this.lbDay.Size = new System.Drawing.Size(116, 25);
            this.lbDay.TabIndex = 66;
            this.lbDay.Text = "00/00/0000";
            // 
            // lbDayOfWeek
            // 
            this.lbDayOfWeek.AutoSize = true;
            this.lbDayOfWeek.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDayOfWeek.ForeColor = System.Drawing.Color.Maroon;
            this.lbDayOfWeek.Location = new System.Drawing.Point(252, 295);
            this.lbDayOfWeek.Name = "lbDayOfWeek";
            this.lbDayOfWeek.Size = new System.Drawing.Size(98, 25);
            this.lbDayOfWeek.TabIndex = 67;
            this.lbDayOfWeek.Text = "Chủ nhật";
            this.lbDayOfWeek.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ParkingApp.Properties.Resources.soict_logo;
            this.pictureBox1.Location = new System.Drawing.Point(23, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(288, 116);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 59;
            this.pictureBox1.TabStop = false;
            // 
            // btnConfig
            // 
            this.btnConfig.ActiveControl = null;
            this.btnConfig.Location = new System.Drawing.Point(23, 148);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(140, 106);
            this.btnConfig.TabIndex = 57;
            this.btnConfig.Tag = "4";
            this.btnConfig.Text = "Cấu hình";
            this.btnConfig.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnConfig.TileImage = ((System.Drawing.Image)(resources.GetObject("btnConfig.TileImage")));
            this.btnConfig.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnConfig.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.btnConfig.UseSelectable = true;
            this.btnConfig.UseTileImage = true;
            this.btnConfig.Click += new System.EventHandler(this.BtnConfig_Click);
            // 
            // btnSupervise
            // 
            this.btnSupervise.ActiveControl = null;
            this.btnSupervise.Location = new System.Drawing.Point(318, 148);
            this.btnSupervise.Name = "btnSupervise";
            this.btnSupervise.Size = new System.Drawing.Size(283, 106);
            this.btnSupervise.Style = MetroFramework.MetroColorStyle.Green;
            this.btnSupervise.TabIndex = 54;
            this.btnSupervise.Tag = "0";
            this.btnSupervise.Text = "Giám sát";
            this.btnSupervise.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSupervise.TileImage = ((System.Drawing.Image)(resources.GetObject("btnSupervise.TileImage")));
            this.btnSupervise.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSupervise.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.btnSupervise.UseSelectable = true;
            this.btnSupervise.UseTileImage = true;
            this.btnSupervise.Click += new System.EventHandler(this.BtnSupervise_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.ActiveControl = null;
            this.btnHistory.Location = new System.Drawing.Point(172, 148);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(140, 106);
            this.btnHistory.TabIndex = 55;
            this.btnHistory.Tag = "0";
            this.btnHistory.Text = "Lịch sử";
            this.btnHistory.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHistory.TileImage = ((System.Drawing.Image)(resources.GetObject("btnHistory.TileImage")));
            this.btnHistory.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnHistory.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.btnHistory.UseSelectable = true;
            this.btnHistory.UseTileImage = true;
            this.btnHistory.Click += new System.EventHandler(this.BtnHistory_Click);
            // 
            // btnTestModelAI
            // 
            this.btnTestModelAI.BackColor = System.Drawing.SystemColors.Control;
            this.btnTestModelAI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestModelAI.Location = new System.Drawing.Point(317, 41);
            this.btnTestModelAI.Name = "btnTestModelAI";
            this.btnTestModelAI.Size = new System.Drawing.Size(284, 101);
            this.btnTestModelAI.TabIndex = 68;
            this.btnTestModelAI.Text = "Test Model AI";
            this.btnTestModelAI.UseVisualStyleBackColor = false;
            this.btnTestModelAI.Click += new System.EventHandler(this.BtnTestModelAI_click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 409);
            this.Controls.Add(this.btnTestModelAI);
            this.Controls.Add(this.lbDayOfWeek);
            this.Controls.Add(this.lbDay);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnSupervise);
            this.Controls.Add(this.btnHistory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTile btnConfig;
        private MetroFramework.Controls.MetroTile btnSupervise;
        private MetroFramework.Controls.MetroTile btnHistory;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbDay;
        private System.Windows.Forms.Label lbDayOfWeek;
        private System.Windows.Forms.Button btnTestModelAI;
    }
}