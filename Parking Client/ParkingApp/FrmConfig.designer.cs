namespace ParkingApp
{
    partial class FrmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig));
            this.txtRtspIn = new MetroFramework.Controls.MetroTextBox();
            this.btnLoad = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.txtRtspOut = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtCardReaderIn = new MetroFramework.Controls.MetroTextBox();
            this.txtCardReaderOut = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // txtRtspIn
            // 
            // 
            // 
            // 
            this.txtRtspIn.CustomButton.Image = null;
            this.txtRtspIn.CustomButton.Location = new System.Drawing.Point(313, 1);
            this.txtRtspIn.CustomButton.Margin = new System.Windows.Forms.Padding(5);
            this.txtRtspIn.CustomButton.Name = "";
            this.txtRtspIn.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtRtspIn.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtRtspIn.CustomButton.TabIndex = 1;
            this.txtRtspIn.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtRtspIn.CustomButton.UseSelectable = true;
            this.txtRtspIn.CustomButton.Visible = false;
            this.txtRtspIn.Lines = new string[0];
            this.txtRtspIn.Location = new System.Drawing.Point(166, 77);
            this.txtRtspIn.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.txtRtspIn.MaxLength = 32767;
            this.txtRtspIn.Name = "txtRtspIn";
            this.txtRtspIn.PasswordChar = '\0';
            this.txtRtspIn.PromptText = "Nhập RTSP của máy camera vào !";
            this.txtRtspIn.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtRtspIn.SelectedText = "";
            this.txtRtspIn.SelectionLength = 0;
            this.txtRtspIn.SelectionStart = 0;
            this.txtRtspIn.ShortcutsEnabled = true;
            this.txtRtspIn.Size = new System.Drawing.Size(341, 29);
            this.txtRtspIn.TabIndex = 21;
            this.txtRtspIn.TabStop = false;
            this.txtRtspIn.UseSelectable = true;
            this.txtRtspIn.WaterMark = "Nhập RTSP của máy camera vào !";
            this.txtRtspIn.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtRtspIn.WaterMarkFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.BackColor = System.Drawing.Color.Transparent;
            this.btnLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnLoad.Location = new System.Drawing.Point(387, 251);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(120, 29);
            this.btnLoad.TabIndex = 22;
            this.btnLoad.TabStop = false;
            this.btnLoad.Text = "Lưu thông tin";
            this.btnLoad.UseSelectable = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(32, 81);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(113, 19);
            this.metroLabel2.TabIndex = 23;
            this.metroLabel2.Text = "Rtsp Camera vào:";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(32, 124);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(104, 19);
            this.metroLabel5.TabIndex = 31;
            this.metroLabel5.Text = "Rtsp Camera ra:";
            // 
            // txtRtspOut
            // 
            // 
            // 
            // 
            this.txtRtspOut.CustomButton.Image = null;
            this.txtRtspOut.CustomButton.Location = new System.Drawing.Point(313, 1);
            this.txtRtspOut.CustomButton.Margin = new System.Windows.Forms.Padding(5);
            this.txtRtspOut.CustomButton.Name = "";
            this.txtRtspOut.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtRtspOut.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtRtspOut.CustomButton.TabIndex = 1;
            this.txtRtspOut.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtRtspOut.CustomButton.UseSelectable = true;
            this.txtRtspOut.CustomButton.Visible = false;
            this.txtRtspOut.Lines = new string[0];
            this.txtRtspOut.Location = new System.Drawing.Point(166, 120);
            this.txtRtspOut.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.txtRtspOut.MaxLength = 32767;
            this.txtRtspOut.Name = "txtRtspOut";
            this.txtRtspOut.PasswordChar = '\0';
            this.txtRtspOut.PromptText = "Nhập RTSP của máy camera ra !";
            this.txtRtspOut.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtRtspOut.SelectedText = "";
            this.txtRtspOut.SelectionLength = 0;
            this.txtRtspOut.SelectionStart = 0;
            this.txtRtspOut.ShortcutsEnabled = true;
            this.txtRtspOut.Size = new System.Drawing.Size(341, 29);
            this.txtRtspOut.TabIndex = 30;
            this.txtRtspOut.TabStop = false;
            this.txtRtspOut.UseSelectable = true;
            this.txtRtspOut.WaterMark = "Nhập RTSP của máy camera ra !";
            this.txtRtspOut.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtRtspOut.WaterMarkFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(32, 169);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(121, 19);
            this.metroLabel1.TabIndex = 32;
            this.metroLabel1.Text = "Code thẻ quẹt vào:";
            // 
            // txtCardReaderIn
            // 
            // 
            // 
            // 
            this.txtCardReaderIn.CustomButton.Image = null;
            this.txtCardReaderIn.CustomButton.Location = new System.Drawing.Point(150, 1);
            this.txtCardReaderIn.CustomButton.Margin = new System.Windows.Forms.Padding(5);
            this.txtCardReaderIn.CustomButton.Name = "";
            this.txtCardReaderIn.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtCardReaderIn.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCardReaderIn.CustomButton.TabIndex = 1;
            this.txtCardReaderIn.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCardReaderIn.CustomButton.UseSelectable = true;
            this.txtCardReaderIn.CustomButton.Visible = false;
            this.txtCardReaderIn.Lines = new string[0];
            this.txtCardReaderIn.Location = new System.Drawing.Point(166, 163);
            this.txtCardReaderIn.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.txtCardReaderIn.MaxLength = 32767;
            this.txtCardReaderIn.Name = "txtCardReaderIn";
            this.txtCardReaderIn.PasswordChar = '\0';
            this.txtCardReaderIn.PromptText = "Code thẻ vào!";
            this.txtCardReaderIn.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCardReaderIn.SelectedText = "";
            this.txtCardReaderIn.SelectionLength = 0;
            this.txtCardReaderIn.SelectionStart = 0;
            this.txtCardReaderIn.ShortcutsEnabled = true;
            this.txtCardReaderIn.Size = new System.Drawing.Size(178, 29);
            this.txtCardReaderIn.TabIndex = 33;
            this.txtCardReaderIn.TabStop = false;
            this.txtCardReaderIn.UseSelectable = true;
            this.txtCardReaderIn.WaterMark = "Code thẻ vào!";
            this.txtCardReaderIn.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCardReaderIn.WaterMarkFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardReaderIn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimerUpload_KeyPress);
            // 
            // txtCardReaderOut
            // 
            // 
            // 
            // 
            this.txtCardReaderOut.CustomButton.Image = null;
            this.txtCardReaderOut.CustomButton.Location = new System.Drawing.Point(150, 1);
            this.txtCardReaderOut.CustomButton.Margin = new System.Windows.Forms.Padding(5);
            this.txtCardReaderOut.CustomButton.Name = "";
            this.txtCardReaderOut.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtCardReaderOut.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCardReaderOut.CustomButton.TabIndex = 1;
            this.txtCardReaderOut.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCardReaderOut.CustomButton.UseSelectable = true;
            this.txtCardReaderOut.CustomButton.Visible = false;
            this.txtCardReaderOut.Lines = new string[0];
            this.txtCardReaderOut.Location = new System.Drawing.Point(166, 206);
            this.txtCardReaderOut.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.txtCardReaderOut.MaxLength = 32767;
            this.txtCardReaderOut.Name = "txtCardReaderOut";
            this.txtCardReaderOut.PasswordChar = '\0';
            this.txtCardReaderOut.PromptText = "Code thẻ ra!";
            this.txtCardReaderOut.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCardReaderOut.SelectedText = "";
            this.txtCardReaderOut.SelectionLength = 0;
            this.txtCardReaderOut.SelectionStart = 0;
            this.txtCardReaderOut.ShortcutsEnabled = true;
            this.txtCardReaderOut.Size = new System.Drawing.Size(178, 29);
            this.txtCardReaderOut.TabIndex = 35;
            this.txtCardReaderOut.TabStop = false;
            this.txtCardReaderOut.UseSelectable = true;
            this.txtCardReaderOut.WaterMark = "Code thẻ ra!";
            this.txtCardReaderOut.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCardReaderOut.WaterMarkFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(32, 212);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(112, 19);
            this.metroLabel3.TabIndex = 34;
            this.metroLabel3.Text = "Code thẻ quẹt ra:";
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 306);
            this.Controls.Add(this.txtCardReaderOut);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.txtCardReaderIn);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.txtRtspOut);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.txtRtspIn);
            this.Controls.Add(this.btnLoad);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(537, 306);
            this.MinimumSize = new System.Drawing.Size(537, 306);
            this.Name = "FrmConfig";
            this.Text = "CẤU HÌNH KẾT NỐI";
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox txtRtspIn;
        private MetroFramework.Controls.MetroButton btnLoad;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroTextBox txtRtspOut;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txtCardReaderIn;
        private MetroFramework.Controls.MetroTextBox txtCardReaderOut;
        private MetroFramework.Controls.MetroLabel metroLabel3;
    }
}