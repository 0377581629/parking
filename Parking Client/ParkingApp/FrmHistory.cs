using MetroFramework;
using ParkingLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ParkingApp
{
    public partial class FrmHistory : MetroFramework.Forms.MetroForm
    {
        private enumCheckIn _xuLy;
        private string mes = string.Empty;
        private bool _check = false;
        readonly Helper _helperDLL = new Helper();
        private List<SecurityData> _lstSecurityDatas;
        private SecurityData _securityDataSelected;
        private int _rowIndex = -1;

        public FrmHistory()
        {
            InitializeComponent();

            dateTimePickerFrom.Format = DateTimePickerFormat.Custom;
            dateTimePickerFrom.CustomFormat = "dd/MM/yyyy";

            dateTimePickerTo.Format = DateTimePickerFormat.Custom;
            dateTimePickerTo.CustomFormat = "dd/MM/yyyy";
        }

        private void SearchInfo()
        {
            var lstSecurityDataSearch = new List<SecurityData>();
            var mes = string.Empty;
            if (txtSearch.Text.Trim() == "")
            {
                #region -- Show all --
                lstSecurityDataSearch = new List<SecurityData>();
                _helperDLL.LoadSecurityData(dateTimePickerFrom.Value, dateTimePickerTo.Value, ref lstSecurityDataSearch, false);
                ReloadGrid(lstSecurityDataSearch);
                return;
                #endregion
            }
            else
            {
                var tokens = txtSearch.Text.Trim().Split('|');

                for (var j = 0; j < tokens.Length; j++)
                {
                    var strSearch = Helper.ConvertNoUnicode(tokens[j].ToString().Trim()).ToLower();
                    for (var i = 0; i < _lstSecurityDatas.Count; i++)
                    {

                        if (_lstSecurityDatas[i].CardNumber.Contains(strSearch))
                        {
                            if (lstSecurityDataSearch.Count == 0)
                            {
                                lstSecurityDataSearch.Add(_lstSecurityDatas[i]);
                            }
                            else
                            {
                                var result = lstSecurityDataSearch.FirstOrDefault(s => s.Id == _lstSecurityDatas[i].Id);
                                if (result == null) lstSecurityDataSearch.Add(_lstSecurityDatas[i]);
                            }
                        }
                    }
                }
            }

            ReloadGrid(lstSecurityDataSearch);
        }

        private void LoadData()
        {
            var strTitle = "Đang tiến hành tải dữ liệu !";
            _xuLy = enumCheckIn.LoadData;
            var result = WaitWindow.WaitWindow.Show(WartingSyncData, strTitle);
        }

        private void ReloadGrid(List<SecurityData> _lstSecurityDatas)
        {
            int scrollPosition = dgvHistory.FirstDisplayedScrollingRowIndex;
            dgvHistory.Rows.Clear();
            for (var i = 0; i < _lstSecurityDatas.Count; i++)
            {
                dgvHistory.Rows.Add();
                dgvHistory.Rows[i].Cells["STT"].Value = i + 1;
                dgvHistory.Rows[i].Cells["securityDataID"].Value = _lstSecurityDatas[i].Id;
                dgvHistory.Rows[i].Cells["cardNumber"].Value = _lstSecurityDatas[i].CardNumber.ToString();
                var fullName = string.Empty;
                if(_lstSecurityDatas[i].StudentInfo!= null)
                {
                    fullName = _lstSecurityDatas[i].StudentInfo.Name;
                }
                else
                {
                    fullName = "Khách";
                }
                dgvHistory.Rows[i].Cells["studentFullName"].Value = fullName;
                dgvHistory.Rows[i].Cells["securityDataDate"].Value = _lstSecurityDatas[i].SecurityDateStr.ToString();
                var stt = "";
                switch (_lstSecurityDatas[i].Status)
                {
                    case (int)Helper.SecurityDataStatus.In:
                        stt = "Vào";
                        break;
                    case (int)Helper.SecurityDataStatus.Out:
                        stt = "Ra";
                        break;
                    default:
                        stt = string.Empty;
                        break;
                }
                dgvHistory.Rows[i].Cells["status"].Value = stt;
            }

            for (var i = 0; i < dgvHistory.Rows.Count; i++)
            {
                dgvHistory.Rows[i].Selected = false;
            }
            // Sau khi load lại trả lại vị trí của scroll trước khi reload
            if (scrollPosition > 0)
            {
                dgvHistory.FirstDisplayedScrollingRowIndex = scrollPosition;
            }
        }

        private void SyncData()
        {
            switch (_xuLy)
            {
                case enumCheckIn.LoadData:
                    {
                        _check = _helperDLL.LoadSecurityData(dateTimePickerFrom.Value, dateTimePickerTo.Value, ref _lstSecurityDatas, false);
                    }
                    break;
                case enumCheckIn.Default:
                    break;
                default:
                    break;
            }
        }


        private void FrmCheckIn_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvCandidate_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _rowIndex = e.RowIndex;
            if (_rowIndex <= 0) return;
            dgvHistory.Rows[_rowIndex].Selected = true;
        }

        private void dgvCandidate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _rowIndex = e.RowIndex;
            if (_rowIndex < 0) return;
            dgvHistory.Rows[_rowIndex].Selected = true;
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

        private void txtSearchCandidate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchInfo();
            }
        }

        private void FrmHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchInfo();
        }
    }

    enum enumCheckIn
    {
        LoadData,
        Default
    }
}
