namespace ParkingApp
{
    partial class FrmCheckInOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCheckInOut));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.picIn = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.picOut = new System.Windows.Forms.PictureBox();
            this.btnOpen = new MetroFramework.Controls.MetroTile();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picRegistry = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtThoiGianRa = new System.Windows.Forms.RichTextBox();
            this.txtThoiGianVao = new System.Windows.Forms.RichTextBox();
            this.txtMaThe = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richNoiDungCanhBao = new System.Windows.Forms.RichTextBox();
            this.btnClose = new MetroFramework.Controls.MetroTile();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picCaptureIn = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.picCaptureOut = new System.Windows.Forms.PictureBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIn)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOut)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRegistry)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptureIn)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptureOut)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.picIn);
            this.groupBox3.Location = new System.Drawing.Point(23, 71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(750, 591);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ẢNH VÀO:";
            // 
            // picIn
            // 
            this.picIn.BackColor = System.Drawing.Color.Gray;
            this.picIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picIn.Location = new System.Drawing.Point(3, 17);
            this.picIn.Name = "picIn";
            this.picIn.Size = new System.Drawing.Size(744, 571);
            this.picIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIn.TabIndex = 0;
            this.picIn.TabStop = false;
            this.picIn.DoubleClick += new System.EventHandler(this.picIn_DoubleClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.picOut);
            this.groupBox4.Location = new System.Drawing.Point(783, 71);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(750, 591);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ẢNH RA:";
            // 
            // picOut
            // 
            this.picOut.BackColor = System.Drawing.Color.Gray;
            this.picOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picOut.Location = new System.Drawing.Point(3, 17);
            this.picOut.Name = "picOut";
            this.picOut.Size = new System.Drawing.Size(744, 571);
            this.picOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOut.TabIndex = 2;
            this.picOut.TabStop = false;
            this.picOut.DoubleClick += new System.EventHandler(this.picOut_DoubleClick);
            // 
            // btnOpen
            // 
            this.btnOpen.ActiveControl = null;
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(1443, 668);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(90, 87);
            this.btnOpen.Style = MetroFramework.MetroColorStyle.Red;
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = "MỞ (F2)";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOpen.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnOpen.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.btnOpen.UseSelectable = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Controls.Add(this.picRegistry);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtThoiGianRa);
            this.groupBox1.Controls.Add(this.txtThoiGianVao);
            this.groupBox1.Controls.Add(this.txtMaThe);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.richNoiDungCanhBao);
            this.groupBox1.Location = new System.Drawing.Point(783, 668);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(654, 197);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "THÔNG TIN: ";
            // 
            // picRegistry
            // 
            this.picRegistry.BackColor = System.Drawing.Color.Gray;
            this.picRegistry.Location = new System.Drawing.Point(19, 30);
            this.picRegistry.Name = "picRegistry";
            this.picRegistry.Size = new System.Drawing.Size(148, 152);
            this.picRegistry.TabIndex = 7;
            this.picRegistry.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(426, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Thời gian ra:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Thời gian vào:";
            // 
            // txtThoiGianRa
            // 
            this.txtThoiGianRa.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtThoiGianRa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtThoiGianRa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtThoiGianRa.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThoiGianRa.ForeColor = System.Drawing.Color.Green;
            this.txtThoiGianRa.Location = new System.Drawing.Point(429, 159);
            this.txtThoiGianRa.Multiline = false;
            this.txtThoiGianRa.Name = "txtThoiGianRa";
            this.txtThoiGianRa.Size = new System.Drawing.Size(208, 23);
            this.txtThoiGianRa.TabIndex = 10;
            this.txtThoiGianRa.Text = "07/01/2020 07h30";
            // 
            // txtThoiGianVao
            // 
            this.txtThoiGianVao.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtThoiGianVao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtThoiGianVao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtThoiGianVao.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThoiGianVao.ForeColor = System.Drawing.Color.Green;
            this.txtThoiGianVao.Location = new System.Drawing.Point(195, 159);
            this.txtThoiGianVao.Multiline = false;
            this.txtThoiGianVao.Name = "txtThoiGianVao";
            this.txtThoiGianVao.Size = new System.Drawing.Size(208, 23);
            this.txtThoiGianVao.TabIndex = 9;
            this.txtThoiGianVao.Text = "07/01/2020 07h30";
            // 
            // txtMaThe
            // 
            this.txtMaThe.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMaThe.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaThe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtMaThe.Location = new System.Drawing.Point(195, 98);
            this.txtMaThe.Multiline = false;
            this.txtMaThe.Name = "txtMaThe";
            this.txtMaThe.ReadOnly = true;
            this.txtMaThe.Size = new System.Drawing.Size(442, 30);
            this.txtMaThe.TabIndex = 5;
            this.txtMaThe.Text = "0123456789";
            this.txtMaThe.TextChanged += new System.EventHandler(this.txtMaThe_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(192, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã thẻ:";
            // 
            // richNoiDungCanhBao
            // 
            this.richNoiDungCanhBao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richNoiDungCanhBao.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richNoiDungCanhBao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.richNoiDungCanhBao.Location = new System.Drawing.Point(195, 21);
            this.richNoiDungCanhBao.Name = "richNoiDungCanhBao";
            this.richNoiDungCanhBao.ReadOnly = true;
            this.richNoiDungCanhBao.Size = new System.Drawing.Size(442, 48);
            this.richNoiDungCanhBao.TabIndex = 2;
            this.richNoiDungCanhBao.Text = "KHÁCH";
            // 
            // btnClose
            // 
            this.btnClose.ActiveControl = null;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1443, 778);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 87);
            this.btnClose.Style = MetroFramework.MetroColorStyle.Green;
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "ĐÓNG (F6)";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClose.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnClose.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.btnClose.UseSelectable = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.picCaptureIn);
            this.groupBox2.Location = new System.Drawing.Point(26, 669);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(345, 196);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CAMERA VÀO: ";
            // 
            // picCaptureIn
            // 
            this.picCaptureIn.BackColor = System.Drawing.Color.Gray;
            this.picCaptureIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCaptureIn.Location = new System.Drawing.Point(3, 17);
            this.picCaptureIn.Name = "picCaptureIn";
            this.picCaptureIn.Size = new System.Drawing.Size(339, 176);
            this.picCaptureIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCaptureIn.TabIndex = 8;
            this.picCaptureIn.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.picCaptureOut);
            this.groupBox5.Location = new System.Drawing.Point(425, 669);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(345, 196);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "CAMERA RA: ";
            // 
            // picCaptureOut
            // 
            this.picCaptureOut.BackColor = System.Drawing.Color.Gray;
            this.picCaptureOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCaptureOut.Location = new System.Drawing.Point(3, 17);
            this.picCaptureOut.Name = "picCaptureOut";
            this.picCaptureOut.Size = new System.Drawing.Size(339, 176);
            this.picCaptureOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCaptureOut.TabIndex = 8;
            this.picCaptureOut.TabStop = false;
            // 
            // FrmCheckInOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1556, 884);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmCheckInOut";
            this.Padding = new System.Windows.Forms.Padding(23, 60, 23, 20);
            this.Text = "KIỂM SOÁT RA VÀO";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCheckInOut_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCheckInOut_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCheckInOut_KeyPress);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picIn)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picOut)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRegistry)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCaptureIn)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCaptureOut)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox picIn;
        private System.Windows.Forms.PictureBox picOut;
        private MetroFramework.Controls.MetroTile btnOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtThoiGianRa;
        private System.Windows.Forms.RichTextBox txtThoiGianVao;
        private System.Windows.Forms.RichTextBox txtMaThe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richNoiDungCanhBao;
        private System.Windows.Forms.PictureBox picRegistry;
        private MetroFramework.Controls.MetroTile btnClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picCaptureIn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.PictureBox picCaptureOut;
    }
}

