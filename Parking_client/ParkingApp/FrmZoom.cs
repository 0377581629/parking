using System;
using System.Drawing;
using System.Windows.Forms;

namespace ParkingApp
{
    public partial class FrmZoom : MetroFramework.Forms.MetroForm
    {
        public FrmZoom(Bitmap img)
        {
            InitializeComponent();
            if (img != null)
            {
                picZoom.Image = img;
            }
        }

        private void FrmZoom_Load(object sender, EventArgs e)
        {

        }

        private void trackBar_Scroll(object sender, ScrollEventArgs e)
        {
            picZoom.Zoom = trackBar.Value * 0.02f;
        }
    }
}
