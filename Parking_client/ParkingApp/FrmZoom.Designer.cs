
namespace ParkingApp
{
    partial class FrmZoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmZoom));
            this.trackBar = new MetroFramework.Controls.MetroTrackBar();
            this.picZoom = new ParkingApp.ImagePanel();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.BackColor = System.Drawing.Color.Transparent;
            this.trackBar.Location = new System.Drawing.Point(14, 363);
            this.trackBar.Maximum = 200;
            this.trackBar.MouseWheelBarPartitions = 20;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(437, 36);
            this.trackBar.TabIndex = 0;
            this.trackBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.trackBar_Scroll);
            // 
            // picZoom
            // 
            this.picZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picZoom.CanvasSize = new System.Drawing.Size(60, 40);
            this.picZoom.Image = null;
            this.picZoom.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.picZoom.Location = new System.Drawing.Point(14, 63);
            this.picZoom.Name = "picZoom";
            this.picZoom.Size = new System.Drawing.Size(437, 288);
            this.picZoom.TabIndex = 1;
            this.picZoom.TabStop = false;
            this.picZoom.Zoom = 1F;
            // 
            // FrmZoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 413);
            this.Controls.Add(this.picZoom);
            this.Controls.Add(this.trackBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmZoom";
            this.Text = "HÌNH ẢNH";
            this.Load += new System.EventHandler(this.FrmZoom_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTrackBar trackBar;
        private ImagePanel picZoom;
    }
}