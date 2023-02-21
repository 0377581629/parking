namespace ParkingApp
{
    partial class FrmHistory
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
        [System.Obsolete]
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHistory));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.stt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.securityDataID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.studentFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.securityDataDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new MetroFramework.Controls.MetroTextBox();
            this.btnSearch = new MetroFramework.Controls.MetroButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvHistory);
            this.groupBox2.Location = new System.Drawing.Point(18, 202);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(850, 373);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách dữ liệu:";
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stt,
            this.securityDataID,
            this.cardNumber,
            this.studentFullName,
            this.securityDataDate,
            this.status});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHistory.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.EnableHeadersVisualStyles = false;
            this.dgvHistory.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvHistory.Location = new System.Drawing.Point(4, 18);
            this.dgvHistory.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.dgvHistory.MultiSelect = false;
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.RowHeadersWidth = 102;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Gray;
            this.dgvHistory.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvHistory.Size = new System.Drawing.Size(842, 352);
            this.dgvHistory.TabIndex = 4;
            this.dgvHistory.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCandidate_CellClick);
            this.dgvHistory.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCandidate_RowEnter);
            // 
            // stt
            // 
            this.stt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.stt.DefaultCellStyle = dataGridViewCellStyle2;
            this.stt.HeaderText = "STT";
            this.stt.MinimumWidth = 12;
            this.stt.Name = "stt";
            this.stt.ReadOnly = true;
            this.stt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.stt.Width = 69;
            // 
            // securityDataID
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.securityDataID.DefaultCellStyle = dataGridViewCellStyle3;
            this.securityDataID.HeaderText = "candidateID";
            this.securityDataID.MinimumWidth = 12;
            this.securityDataID.Name = "securityDataID";
            this.securityDataID.ReadOnly = true;
            this.securityDataID.Visible = false;
            this.securityDataID.Width = 250;
            // 
            // cardNumber
            // 
            this.cardNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cardNumber.DefaultCellStyle = dataGridViewCellStyle4;
            this.cardNumber.HeaderText = "Số thẻ";
            this.cardNumber.MinimumWidth = 180;
            this.cardNumber.Name = "cardNumber";
            this.cardNumber.ReadOnly = true;
            this.cardNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cardNumber.Width = 180;
            // 
            // studentFullName
            // 
            this.studentFullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.studentFullName.DefaultCellStyle = dataGridViewCellStyle5;
            this.studentFullName.HeaderText = "Họ tên";
            this.studentFullName.MinimumWidth = 160;
            this.studentFullName.Name = "studentFullName";
            this.studentFullName.ReadOnly = true;
            this.studentFullName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.studentFullName.Width = 260;
            // 
            // securityDataDate
            // 
            this.securityDataDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.securityDataDate.DefaultCellStyle = dataGridViewCellStyle6;
            this.securityDataDate.HeaderText = "Thời gian";
            this.securityDataDate.MinimumWidth = 220;
            this.securityDataDate.Name = "securityDataDate";
            this.securityDataDate.ReadOnly = true;
            this.securityDataDate.Width = 220;
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.status.DefaultCellStyle = dataGridViewCellStyle7;
            this.status.HeaderText = "Trạng thái";
            this.status.MinimumWidth = 100;
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 120;
            // 
            // txtSearch
            // 
            // 
            // 
            // 
            this.txtSearch.CustomButton.Image = null;
            this.txtSearch.CustomButton.Location = new System.Drawing.Point(666, 1);
            this.txtSearch.CustomButton.Margin = new System.Windows.Forms.Padding(5);
            this.txtSearch.CustomButton.Name = "";
            this.txtSearch.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtSearch.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearch.CustomButton.TabIndex = 1;
            this.txtSearch.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearch.CustomButton.UseSelectable = true;
            this.txtSearch.CustomButton.Visible = false;
            this.txtSearch.Lines = new string[0];
            this.txtSearch.Location = new System.Drawing.Point(13, 72);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.txtSearch.MaxLength = 32767;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.PromptText = "Nhập số thẻ !";
            this.txtSearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearch.SelectedText = "";
            this.txtSearch.SelectionLength = 0;
            this.txtSearch.SelectionStart = 0;
            this.txtSearch.ShortcutsEnabled = true;
            this.txtSearch.Size = new System.Drawing.Size(694, 29);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.TabStop = false;
            this.txtSearch.UseSelectable = true;
            this.txtSearch.WaterMark = "Nhập số thẻ !";
            this.txtSearch.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearch.WaterMarkFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchCandidate_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(727, 30);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(109, 71);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseSelectable = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateTimePickerTo);
            this.groupBox1.Controls.Add(this.dateTimePickerFrom);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Location = new System.Drawing.Point(22, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(846, 122);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thao tác";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(385, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 14);
            this.label2.TabIndex = 11;
            this.label2.Text = "Đến ngày:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 14);
            this.label1.TabIndex = 10;
            this.label1.Text = "Từ ngày:";
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerTo.Location = new System.Drawing.Point(463, 30);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(244, 22);
            this.dateTimePickerTo.TabIndex = 9;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(81, 30);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(244, 22);
            this.dateTimePickerFrom.TabIndex = 8;
            // 
            // FrmHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 600);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmHistory";
            this.Padding = new System.Windows.Forms.Padding(27, 65, 27, 22);
            this.Text = "LỊCH SỬ TRA CỨU";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmHistory_FormClosing);
            this.Load += new System.EventHandler(this.FrmCheckIn_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroButton btnSearch;
        private System.Windows.Forms.DataGridView dgvHistory;
        private MetroFramework.Controls.MetroTextBox txtSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn stt;
        private System.Windows.Forms.DataGridViewTextBoxColumn securityDataID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn studentFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn securityDataDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
    }
}