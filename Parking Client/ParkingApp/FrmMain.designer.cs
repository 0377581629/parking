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
            this.linkVersion = new MetroFramework.Controls.MetroLink();
            this.linkInfo = new MetroFramework.Controls.MetroLink();
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.lbDay = new System.Windows.Forms.Label();
            this.lbDayOfWeek = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDongBo = new MetroFramework.Controls.MetroTile();
            this.btnCauHinh = new MetroFramework.Controls.MetroTile();
            this.btnUpload = new MetroFramework.Controls.MetroTile();
            this.btnGiamSat = new MetroFramework.Controls.MetroTile();
            this.btnTraCuu = new MetroFramework.Controls.MetroTile();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // linkVersion
            // 
            this.linkVersion.Location = new System.Drawing.Point(119, 93);
            this.linkVersion.Margin = new System.Windows.Forms.Padding(2);
            this.linkVersion.Name = "linkVersion";
            this.linkVersion.Size = new System.Drawing.Size(70, 19);
            this.linkVersion.TabIndex = 60;
            this.linkVersion.Text = "V 1.2.0";
            this.linkVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkVersion.UseSelectable = true;
            // 
            // linkInfo
            // 
            this.linkInfo.Location = new System.Drawing.Point(537, 89);
            this.linkInfo.Name = "linkInfo";
            this.linkInfo.Size = new System.Drawing.Size(75, 23);
            this.linkInfo.TabIndex = 61;
            this.linkInfo.Text = "Thông tin";
            this.linkInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkInfo.UseSelectable = true;
            this.linkInfo.Click += new System.EventHandler(this.linkInfo_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbStatus.Location = new System.Drawing.Point(20, 382);
            this.lbStatus.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(221, 13);
            this.lbStatus.TabIndex = 62;
            this.lbStatus.Text = "PHIÊN BẢN BETA PHÁT HÀNH NỘI BỘ";
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.ForeColor = System.Drawing.Color.Maroon;
            this.lbTime.Location = new System.Drawing.Point(318, 257);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(166, 45);
            this.lbTime.TabIndex = 65;
            this.lbTime.Text = "00:00:00";
            // 
            // lbDay
            // 
            this.lbDay.AutoSize = true;
            this.lbDay.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDay.ForeColor = System.Drawing.Color.Maroon;
            this.lbDay.Location = new System.Drawing.Point(496, 338);
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
            this.lbDayOfWeek.Location = new System.Drawing.Point(513, 304);
            this.lbDayOfWeek.Name = "lbDayOfWeek";
            this.lbDayOfWeek.Size = new System.Drawing.Size(98, 25);
            this.lbDayOfWeek.TabIndex = 67;
            this.lbDayOfWeek.Text = "Chủ nhật";
            this.lbDayOfWeek.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ParkingApp.Properties.Resources.vnua_logo;
            this.pictureBox1.Location = new System.Drawing.Point(23, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 59;
            this.pictureBox1.TabStop = false;
            // 
            // btnDongBo
            // 
            this.btnDongBo.ActiveControl = null;
            this.btnDongBo.Location = new System.Drawing.Point(24, 133);
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
            this.btnCauHinh.Location = new System.Drawing.Point(23, 257);
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
            this.btnUpload.Location = new System.Drawing.Point(323, 133);
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
            this.btnGiamSat.Location = new System.Drawing.Point(172, 257);
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
            this.btnTraCuu.Location = new System.Drawing.Point(172, 133);
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
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 409);
            this.Controls.Add(this.lbDayOfWeek);
            this.Controls.Add(this.lbDay);
            this.Controls.Add(this.lbTime);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.linkInfo);
            this.Controls.Add(this.linkVersion);
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
        private MetroFramework.Controls.MetroLink linkVersion;
        private MetroFramework.Controls.MetroLink linkInfo;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Label lbDay;
        private System.Windows.Forms.Label lbDayOfWeek;
    }
}