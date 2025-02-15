﻿namespace ParkingApp
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.linkExit = new MetroFramework.Controls.MetroLink();
            this.checkRemember = new MetroFramework.Controls.MetroCheckBox();
            this.btnLogin = new MetroFramework.Controls.MetroButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtPass = new MetroFramework.Controls.MetroTextBox();
            this.txtUser = new MetroFramework.Controls.MetroTextBox();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // linkExit
            // 
            this.linkExit.Location = new System.Drawing.Point(331, 240);
            this.linkExit.Margin = new System.Windows.Forms.Padding(1);
            this.linkExit.Name = "linkExit";
            this.linkExit.Size = new System.Drawing.Size(45, 15);
            this.linkExit.TabIndex = 4;
            this.linkExit.Text = "Thoát";
            this.linkExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkExit.UseSelectable = true;
            this.linkExit.Click += new System.EventHandler(this.LinkExit_Click);
            // 
            // checkRemember
            // 
            this.checkRemember.AutoSize = true;
            this.checkRemember.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkRemember.Location = new System.Drawing.Point(51, 240);
            this.checkRemember.Margin = new System.Windows.Forms.Padding(1);
            this.checkRemember.Name = "checkRemember";
            this.checkRemember.Size = new System.Drawing.Size(99, 15);
            this.checkRemember.TabIndex = 3;
            this.checkRemember.Text = "Nhớ mật khẩu";
            this.checkRemember.Theme = MetroFramework.MetroThemeStyle.Light;
            this.checkRemember.UseSelectable = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(51, 283);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(1);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(325, 34);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseSelectable = true;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image) (resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image) (resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(175, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 90);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 33;
            this.pictureBox1.TabStop = false;
            // 
            // txtPass
            // 
            // 
            // 
            // 
            this.txtPass.CustomButton.Image = null;
            this.txtPass.CustomButton.Location = new System.Drawing.Point(289, 2);
            this.txtPass.CustomButton.Name = "";
            this.txtPass.CustomButton.Size = new System.Drawing.Size(33, 33);
            this.txtPass.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPass.CustomButton.TabIndex = 1;
            this.txtPass.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPass.CustomButton.UseSelectable = true;
            this.txtPass.CustomButton.Visible = false;
            this.txtPass.DisplayIcon = true;
            this.txtPass.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtPass.Icon = global::ParkingApp.Properties.Resources.ic_vpn_key;
            this.txtPass.Lines = new string[0];
            this.txtPass.Location = new System.Drawing.Point(51, 187);
            this.txtPass.Margin = new System.Windows.Forms.Padding(1);
            this.txtPass.MaxLength = 32767;
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '●';
            this.txtPass.PromptText = "Nhập thông tin mật khẩu";
            this.txtPass.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPass.SelectedText = "";
            this.txtPass.SelectionLength = 0;
            this.txtPass.SelectionStart = 0;
            this.txtPass.ShortcutsEnabled = true;
            this.txtPass.Size = new System.Drawing.Size(325, 38);
            this.txtPass.TabIndex = 2;
            this.txtPass.UseSelectable = true;
            this.txtPass.UseSystemPasswordChar = true;
            this.txtPass.WaterMark = "Nhập thông tin mật khẩu";
            this.txtPass.WaterMarkColor = System.Drawing.Color.FromArgb(((int) (((byte) (109)))), ((int) (((byte) (109)))), ((int) (((byte) (109)))));
            this.txtPass.WaterMarkFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.txtPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPass_KeyDown);
            // 
            // txtUser
            // 
            // 
            // 
            // 
            this.txtUser.CustomButton.Image = null;
            this.txtUser.CustomButton.Location = new System.Drawing.Point(293, 2);
            this.txtUser.CustomButton.Name = "";
            this.txtUser.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.txtUser.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUser.CustomButton.TabIndex = 1;
            this.txtUser.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUser.CustomButton.UseSelectable = true;
            this.txtUser.CustomButton.Visible = false;
            this.txtUser.DisplayIcon = true;
            this.txtUser.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtUser.Icon = global::ParkingApp.Properties.Resources.ic_person;
            this.txtUser.Lines = new string[0];
            this.txtUser.Location = new System.Drawing.Point(51, 133);
            this.txtUser.Margin = new System.Windows.Forms.Padding(1);
            this.txtUser.MaxLength = 32767;
            this.txtUser.Multiline = true;
            this.txtUser.Name = "txtUser";
            this.txtUser.PasswordChar = '\0';
            this.txtUser.PromptText = "Nhập thông tin tài khoản";
            this.txtUser.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUser.SelectedText = "";
            this.txtUser.SelectionLength = 0;
            this.txtUser.SelectionStart = 0;
            this.txtUser.ShortcutsEnabled = true;
            this.txtUser.Size = new System.Drawing.Size(325, 34);
            this.txtUser.TabIndex = 1;
            this.txtUser.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUser.UseSelectable = true;
            this.txtUser.WaterMark = "Nhập thông tin tài khoản";
            this.txtUser.WaterMarkColor = System.Drawing.Color.FromArgb(((int) (((byte) (109)))), ((int) (((byte) (109)))), ((int) (((byte) (109)))));
            this.txtUser.WaterMarkFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte) (3)));
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 363);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.linkExit);
            this.Controls.Add(this.checkRemember);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtUser);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(431, 363);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(431, 363);
            this.Name = "FrmLogin";
            this.Padding = new System.Windows.Forms.Padding(8, 60, 8, 8);
            ((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private MetroFramework.Controls.MetroTextBox txtPass;
        private MetroFramework.Controls.MetroLink linkExit;
        private MetroFramework.Controls.MetroCheckBox checkRemember;
        private MetroFramework.Controls.MetroButton btnLogin;
        private MetroFramework.Controls.MetroTextBox txtUser;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}