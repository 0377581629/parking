using MetroFramework;
using ParkingLib;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ParkingApp
{
    public partial class FrmMain : MetroFramework.Forms.MetroForm
    {
        private static SyncDataModels.SyncClientDto _educateData;
        readonly Helper _helperDLL = new Helper();
        
        private enumMain _xuLy;
        private System.Windows.Forms.Timer aTimer;

        public FrmMain()
        {
            InitializeComponent();

            aTimer = new System.Windows.Forms.Timer();
            aTimer.Tick += new EventHandler(timer_Tick);
        }

        private string mes = string.Empty;
        private bool _check = false;
        private void SyncData()
        {
            switch (_xuLy)
            {
                case enumMain.SyncUpLoad:
                    {
                        var lstSecurityData = new SecurityData().Gets().Where(x => x.Status == 0);

                        if (lstSecurityData.Any())
                        {
                            var syncData = new SyncDataModels.SyncClientDto();
                            _check = _helperDLL.SyncUpData(ref syncData, ref mes);

                            if (_check)
                            {
                                var pushDataSuccess = SyncDataClient.AsyncHelper.RunSync(() => SyncDataClient.Sync.SendSyncClientData(syncData));

                                if(pushDataSuccess)
                                {
                                    var lstId = lstSecurityData.Select(x => x.Id).ToList();
                                    var updStatus = new SecurityData().UpdateStatus(lstId, 1);
                                }
                            }
                        }
                    }
                    break;
                case enumMain.SyncDownLoad:
                    {
                        // Xóa dữ liệu trong CSDL
                        new SecurityData().DelAll();
                        new StudentData().DelAll();
                        // Xoa file cache
                        var path = Application.StartupPath + "/Cache";
                        DirectoryInfo di = new DirectoryInfo(path);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in di.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                        // Tải dữ liệu từ serer về
                        _educateData = SyncDataClient.AsyncHelper.RunSync(() => SyncDataClient.Sync.GetSyncClientData());
                        // Lưu dữ liệu tải về vào CSDL
                        _check = _helperDLL.SyncDownData(_educateData, ref mes);
                    }
                    break;
                default:
                    break;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            aTimer.Enabled = true;
            aTimer.Start();
            ShowDateTime();
        }

        private void WartingSyncData(object sender, WaitWindow.WaitWindowEventArgs e)
        {
            var stt = "Quá trình xử lý dữ liệu hoàn tất !";
            var thread = new Thread(SyncData);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            if (stt == "")
                stt = "Quá trình xử lý dữ liệu hoàn tất !";
            e.Result = e.Arguments.Count > 0 ? e.Arguments[0].ToString() : stt;
        }

        private void ShowDateTime()
        {
            var strDayOfWeek = string.Empty;
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    strDayOfWeek = "Chủ nhật";
                    break;
                case DayOfWeek.Monday:
                    strDayOfWeek = "Thứ hai";
                    break;
                case DayOfWeek.Tuesday:
                    strDayOfWeek = "Thứ ba";
                    break;
                case DayOfWeek.Wednesday:
                    strDayOfWeek = "Thứ tư";
                    break;
                case DayOfWeek.Thursday:
                    strDayOfWeek = "Thứ năm";
                    break;
                case DayOfWeek.Friday:
                    strDayOfWeek = "Thứ sáu";
                    break;
                case DayOfWeek.Saturday:
                    strDayOfWeek = "Thứ bảy";
                    break;
                default:
                    break;
            }
            lbDay.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lbDayOfWeek.Text = strDayOfWeek;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ShowDateTime();
        }


        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            var frmHistory = new FrmHistory();
            var result = frmHistory.ShowDialog();

            if (result == DialogResult.OK)
            {
                frmHistory.Close();
            }
        }

        private void btnDongBo_Click(object sender, EventArgs e)
        {
            DialogResult dr = MetroMessageBox.Show(this, "\n\nViệc đồng bộ sẽ xóa hết dữ liệu hiện tại ở local. Bạn có muốn tiếp tục?", "Đồng bộ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {

                var strTitle = "Đang tiến hành tải dữ liệu về !";
                _xuLy = enumMain.SyncDownLoad;
                var result = WaitWindow.WaitWindow.Show(WartingSyncData, strTitle);
                if (!_check)
                {
                    MetroMessageBox.Show(this, mes, "Cảnh báo");
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            var strTitle = "Đang tiến hành tải dữ liệu lên !";
            _xuLy = enumMain.SyncUpLoad;
            var result = WaitWindow.WaitWindow.Show(WartingSyncData, strTitle);
            if (!_check)
            {
                MetroMessageBox.Show(this, mes, "Cảnh báo");
            }
        }

        private void btnGiamSat_Click(object sender, EventArgs e)
        {
            var frmCheckInOut = new FrmCheckInOut();
            var result = frmCheckInOut.ShowDialog();

            if (result == DialogResult.OK)
            {
                frmCheckInOut.Close();
            }
        }

        private void btnCauHinh_Click(object sender, EventArgs e)
        {
            var config = new FrmConfig();
            var result = config.ShowDialog();

            if (result == DialogResult.OK)
            {
                config.Close();
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnTestModelAI_click(object sender, EventArgs e)
        {
            var frmTestModelAI = new FrmTestModelAI();
            var result = frmTestModelAI.ShowDialog();

            if (result == DialogResult.OK)
            {
                frmTestModelAI.Close();
            }
        }
    }

    enum enumMain
    {
        SyncUpLoad,
        SyncDownLoad,
        Default
    }
}
