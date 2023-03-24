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
            this.btnDongBo = new MetroFramework.Controls.MetroTile();
            this.btnCauHinh = new MetroFramework.Controls.MetroTile();
            this.btnUpload = new MetroFramework.Controls.MetroTile();
            this.btnGiamSat = new MetroFramework.Controls.MetroTile();
            this.btnTraCuu = new MetroFramework.Controls.MetroTile();
            this.btnTestModelAI = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbDay
            // 
            this.lbDay.AutoSize = true;
            this.lbDay.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDay.ForeColor = System.Drawing.Color.Maroon;
            this.lbDay.Location = new System.Drawing.Point(403, 312);
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
            this.lbDayOfWeek.Location = new System.Drawing.Point(413, 278);
            this.lbDayOfWeek.Name = "lbDayOfWeek";
            this.lbDayOfWeek.Size = new System.Drawing.Size(98, 25);
            this.lbDayOfWeek.TabIndex = 67;
            this.lbDayOfWeek.Text = "Chủ nhật";
            this.lbDayOfWeek.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ParkingApp.Properties.Resources.soict_logo;
            this.pictureBox1.Location = new System.Drawing.Point(24, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(288, 116);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 59;
            this.pictureBox1.TabStop = false;
            // 
            // btnDongBo
            // 
            this.btnDongBo.ActiveControl = null;
            this.btnDongBo.Location = new System.Drawing.Point(24, 148);
            this.btnDongBo.Name = "btnDongBo";
            this.btnDongBo.Size = new System.Drawing.Size(139, 106);
            this.btnDongBo.Style = MetroFramework.MetroColorStyle.Purple;
            this.btnDongBo.TabIndex = 58;
            this.btnDongBo.Text = "1. Tải về CSDL";
            this.btnDongBo.TileImage = global::ParkingApp.Properties.Resources.ic_repeat_one_white_48pt;
            this.btnDongBo.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDongBo.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.btnDongBo.UseSelectable = true;
            this.btnDongBo.UseTileImage = true;
            this.btnDongBo.Click += new System.EventHandler(this.btnDongBo_Click);
            // 
            // btnCauHinh
            // 
            this.btnCauHinh.ActiveControl = null;
            this.btnCauHinh.Location = new System.Drawing.Point(23, 272);
            this.btnCauHinh.Name = "btnCauHinh";
            this.btnCauHinh.Size = new System.Drawing.Size(140, 106);
            this.btnCauHinh.TabIndex = 57;
            this.btnCauHinh.Tag = "4";
            this.btnCauHinh.Text = "3. Cấu hình";
            this.btnCauHinh.TileImage = ((System.Drawing.Image)(resources.GetObject("btnCauHinh.TileImage")));
            this.btnCauHinh.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCauHinh.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.btnCauHinh.UseSelectable = true;
            this.btnCauHinh.UseTileImage = true;
            this.btnCauHinh.Click += new System.EventHandler(this.btnCauHinh_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.ActiveControl = null;
            this.btnUpload.Location = new System.Drawing.Point(324, 148);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(289, 106);
            this.btnUpload.Style = MetroFramework.MetroColorStyle.Orange;
            this.btnUpload.TabIndex = 56;
            this.btnUpload.Text = "5. Gửi dữ liệu lên Server";
            this.btnUpload.TileImage = ((System.Drawing.Image)(resources.GetObject("btnUpload.TileImage")));
            this.btnUpload.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpload.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.btnUpload.UseSelectable = true;
            this.btnUpload.UseTileImage = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnGiamSat
            // 
            this.btnGiamSat.ActiveControl = null;
            this.btnGiamSat.Location = new System.Drawing.Point(172, 272);
            this.btnGiamSat.Name = "btnGiamSat";
            this.btnGiamSat.Size = new System.Drawing.Size(140, 106);
            this.btnGiamSat.Style = MetroFramework.MetroColorStyle.Green;
            this.btnGiamSat.TabIndex = 54;
            this.btnGiamSat.Tag = "0";
            this.btnGiamSat.Text = "4. Giám sát";
            this.btnGiamSat.TileImage = ((System.Drawing.Image)(resources.GetObject("btnGiamSat.TileImage")));
            this.btnGiamSat.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnGiamSat.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.btnGiamSat.UseSelectable = true;
            this.btnGiamSat.UseTileImage = true;
            this.btnGiamSat.Click += new System.EventHandler(this.btnGiamSat_Click);
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.ActiveControl = null;
            this.btnTraCuu.Location = new System.Drawing.Point(172, 148);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(140, 106);
            this.btnTraCuu.TabIndex = 55;
            this.btnTraCuu.Tag = "0";
            this.btnTraCuu.Text = "2. Tra cứu";
            this.btnTraCuu.TileImage = ((System.Drawing.Image)(resources.GetObject("btnTraCuu.TileImage")));
            this.btnTraCuu.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnTraCuu.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.btnTraCuu.UseSelectable = true;
            this.btnTraCuu.UseTileImage = true;
            this.btnTraCuu.Click += new System.EventHandler(this.btnTraCuu_Click);
            // 
            // btnTestModelAI
            // 
            this.btnTestModelAI.BackColor = System.Drawing.SystemColors.Control;
            this.btnTestModelAI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestModelAI.Location = new System.Drawing.Point(324, 35);
            this.btnTestModelAI.Name = "btnTestModelAI";
            this.btnTestModelAI.Size = new System.Drawing.Size(289, 98);
            this.btnTestModelAI.TabIndex = 68;
            this.btnTestModelAI.Text = "Test Model AI";
            this.btnTestModelAI.UseVisualStyleBackColor = false;
            this.btnTestModelAI.Click += new System.EventHandler(this.btnTestModelAI_click);
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
            this.Controls.Add(this.btnDongBo);
            this.Controls.Add(this.btnCauHinh);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnGiamSat);
            this.Controls.Add(this.btnTraCuu);
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

        private MetroFramework.Controls.MetroTile btnDongBo;
        private MetroFramework.Controls.MetroTile btnCauHinh;
        private MetroFramework.Controls.MetroTile btnUpload;
        private MetroFramework.Controls.MetroTile btnGiamSat;
        private MetroFramework.Controls.MetroTile btnTraCuu;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbDay;
        private System.Windows.Forms.Label lbDayOfWeek;
        private System.Windows.Forms.Button btnTestModelAI;
    }
}