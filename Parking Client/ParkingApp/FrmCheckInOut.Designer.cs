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
            this.components = new System.ComponentModel.Container();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.picIn = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.picOut = new System.Windows.Forms.PictureBox();
            this.btnOpenBarie = new MetroFramework.Controls.MetroTile();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkInOutContent = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.richRecognitionLicensePlate = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.picRegistry = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutTime = new System.Windows.Forms.RichTextBox();
            this.txtInTime = new System.Windows.Forms.RichTextBox();
            this.txtCardCode = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richCardLicensePlate = new System.Windows.Forms.RichTextBox();
            this.btnCloseBarie = new MetroFramework.Controls.MetroTile();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picCaptureIn = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.picCaptureOut = new System.Windows.Forms.PictureBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
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
            this.picIn.Location = new System.Drawing.Point(3, 17);
            this.picIn.Name = "picIn";
            this.picIn.Size = new System.Drawing.Size(744, 571);
            this.picIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIn.TabIndex = 0;
            this.picIn.TabStop = false;
            this.picIn.DoubleClick += new System.EventHandler(this.PicIn_DoubleClick);
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
            this.picOut.DoubleClick += new System.EventHandler(this.PicOut_DoubleClick);
            // 
            // btnOpenBarie
            // 
            this.btnOpenBarie.ActiveControl = null;
            this.btnOpenBarie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenBarie.Location = new System.Drawing.Point(1443, 668);
            this.btnOpenBarie.Name = "btnOpenBarie";
            this.btnOpenBarie.Size = new System.Drawing.Size(90, 87);
            this.btnOpenBarie.Style = MetroFramework.MetroColorStyle.Red;
            this.btnOpenBarie.TabIndex = 4;
            this.btnOpenBarie.Text = "MỞ (F1)";
            this.btnOpenBarie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOpenBarie.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnOpenBarie.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.btnOpenBarie.UseSelectable = true;
            this.btnOpenBarie.Click += new System.EventHandler(this.BtnOpenBarieClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Controls.Add(this.checkInOutContent);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.richRecognitionLicensePlate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.picRegistry);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtOutTime);
            this.groupBox1.Controls.Add(this.txtInTime);
            this.groupBox1.Controls.Add(this.txtCardCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.richCardLicensePlate);
            this.groupBox1.Location = new System.Drawing.Point(783, 668);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(654, 197);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "THÔNG TIN: ";
            // 
            // checkInOutContent
            // 
            this.checkInOutContent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkInOutContent.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.checkInOutContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.checkInOutContent.Location = new System.Drawing.Point(195, 10);
            this.checkInOutContent.Name = "checkInOutContent";
            this.checkInOutContent.ReadOnly = true;
            this.checkInOutContent.Size = new System.Drawing.Size(442, 30);
            this.checkInOutContent.TabIndex = 16;
            this.checkInOutContent.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(426, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Biển số nhận diện:";
            // 
            // richRecognitionLicensePlate
            // 
            this.richRecognitionLicensePlate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richRecognitionLicensePlate.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold);
            this.richRecognitionLicensePlate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.richRecognitionLicensePlate.Location = new System.Drawing.Point(429, 59);
            this.richRecognitionLicensePlate.Name = "richRecognitionLicensePlate";
            this.richRecognitionLicensePlate.ReadOnly = true;
            this.richRecognitionLicensePlate.Size = new System.Drawing.Size(208, 30);
            this.richRecognitionLicensePlate.TabIndex = 14;
            this.richRecognitionLicensePlate.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(192, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Biển số của thẻ:";
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
            // txtOutTime
            // 
            this.txtOutTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtOutTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtOutTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutTime.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutTime.ForeColor = System.Drawing.Color.Green;
            this.txtOutTime.Location = new System.Drawing.Point(429, 159);
            this.txtOutTime.Multiline = false;
            this.txtOutTime.Name = "txtOutTime";
            this.txtOutTime.Size = new System.Drawing.Size(208, 23);
            this.txtOutTime.TabIndex = 10;
            this.txtOutTime.Text = "";
            // 
            // txtInTime
            // 
            this.txtInTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtInTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtInTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInTime.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInTime.ForeColor = System.Drawing.Color.Green;
            this.txtInTime.Location = new System.Drawing.Point(195, 159);
            this.txtInTime.Multiline = false;
            this.txtInTime.Name = "txtInTime";
            this.txtInTime.Size = new System.Drawing.Size(208, 23);
            this.txtInTime.TabIndex = 9;
            this.txtInTime.Text = "";
            // 
            // txtCardCode
            // 
            this.txtCardCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCardCode.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtCardCode.Location = new System.Drawing.Point(195, 108);
            this.txtCardCode.Multiline = false;
            this.txtCardCode.Name = "txtCardCode";
            this.txtCardCode.ReadOnly = true;
            this.txtCardCode.Size = new System.Drawing.Size(442, 30);
            this.txtCardCode.TabIndex = 5;
            this.txtCardCode.Text = "";
            this.txtCardCode.TextChanged += new System.EventHandler(this.txtCardCode_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(192, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã thẻ:";
            // 
            // richCardLicensePlate
            // 
            this.richCardLicensePlate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.richCardLicensePlate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richCardLicensePlate.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold);
            this.richCardLicensePlate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.richCardLicensePlate.Location = new System.Drawing.Point(195, 59);
            this.richCardLicensePlate.Name = "richCardLicensePlate";
            this.richCardLicensePlate.ReadOnly = true;
            this.richCardLicensePlate.Size = new System.Drawing.Size(208, 30);
            this.richCardLicensePlate.TabIndex = 2;
            this.richCardLicensePlate.Text = "";
            // 
            // btnCloseBarie
            // 
            this.btnCloseBarie.ActiveControl = null;
            this.btnCloseBarie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseBarie.Location = new System.Drawing.Point(1443, 778);
            this.btnCloseBarie.Name = "btnCloseBarie";
            this.btnCloseBarie.Size = new System.Drawing.Size(90, 87);
            this.btnCloseBarie.Style = MetroFramework.MetroColorStyle.Green;
            this.btnCloseBarie.TabIndex = 7;
            this.btnCloseBarie.Text = "ĐÓNG (F2)";
            this.btnCloseBarie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCloseBarie.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnCloseBarie.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.btnCloseBarie.UseSelectable = true;
            this.btnCloseBarie.Click += new System.EventHandler(this.BtnCloseBarieClick);
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
            this.Controls.Add(this.btnCloseBarie);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOpenBarie);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "FrmCheckInOut";
            this.Padding = new System.Windows.Forms.Padding(23, 60, 23, 20);
            this.Text = "KIỂM SOÁT RA VÀO";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCheckInOut_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private MetroFramework.Controls.MetroTile btnOpenBarie;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtOutTime;
        private System.Windows.Forms.RichTextBox txtInTime;
        private System.Windows.Forms.RichTextBox txtCardCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richCardLicensePlate;
        private MetroFramework.Controls.MetroTile btnCloseBarie;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picCaptureIn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.PictureBox picCaptureOut;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox richRecognitionLicensePlate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox checkInOutContent;
        private System.Windows.Forms.PictureBox picRegistry;
        private System.IO.Ports.SerialPort serialPort1;
    }
}

