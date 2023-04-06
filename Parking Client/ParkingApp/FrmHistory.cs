using MetroFramework;
using ParkingLib;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private List<HistoryData> _lstHistoryDatas;
        private HistoryData _historyDataSelected;
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
            var lstHistoryDataSearch = new List<HistoryData>();
            var mes = string.Empty;
            if (txtSearch.Text.Trim() == "")
            {
                #region -- Show all --
                lstHistoryDataSearch = new List<HistoryData>();
                _helperDLL.LoadHistoryData(dateTimePickerFrom.Value, dateTimePickerTo.Value, ref lstHistoryDataSearch, false);
                ReloadGrid(lstHistoryDataSearch);
                return;
                #endregion
            }
            else
            {
                var tokens = txtSearch.Text.Trim().Split('|');

                for (var j = 0; j < tokens.Length; j++)
                {
                    var strSearch = Helper.ConvertNoUnicode(tokens[j].ToString().Trim()).ToLower();
                    for (var i = 0; i < _lstHistoryDatas.Count; i++)
                    {

                        if (_lstHistoryDatas[i].CardNumber.Contains(strSearch))
                        {
                            if (lstHistoryDataSearch.Count == 0)
                            {
                                lstHistoryDataSearch.Add(_lstHistoryDatas[i]);
                            }
                            else
                            {
                                var result = lstHistoryDataSearch.FirstOrDefault(s => s.Id == _lstHistoryDatas[i].Id);
                                if (result == null) lstHistoryDataSearch.Add(_lstHistoryDatas[i]);
                            }
                        }
                    }
                }
            }

            ReloadGrid(lstHistoryDataSearch);
        }

        private void ReloadGrid(List<HistoryData> _lstHistoryDatas)
        {
            int scrollPosition = dgvHistory.FirstDisplayedScrollingRowIndex;
            dgvHistory.Rows.Clear();
            for (var i = 0; i < _lstHistoryDatas.Count; i++)
            {
                dgvHistory.Rows.Add();
                dgvHistory.Rows[i].Cells["STT"].Value = i + 1;
                dgvHistory.Rows[i].Cells["historyDataID"].Value = _lstHistoryDatas[i].Id;
                dgvHistory.Rows[i].Cells["cardNumber"].Value = _lstHistoryDatas[i].CardNumber.ToString();
                
                var type = string.Empty;
                switch (_lstHistoryDatas[i].Type)
                {
                    case (int)Helper.HistoryDataStatus.In:
                        type = "Vào";
                        break;
                    case (int)Helper.HistoryDataStatus.Out:
                        type = "Ra";
                        break;
                    default:
                        type = string.Empty;
                        break;
                }
                dgvHistory.Rows[i].Cells["type"].Value = type;
                
                dgvHistory.Rows[i].Cells["time"].Value = _lstHistoryDatas[i].Time.ToString(CultureInfo.InvariantCulture);
                dgvHistory.Rows[i].Cells["vehicleTypeName"].Value = _lstHistoryDatas[i].VehicleTypeName;
                dgvHistory.Rows[i].Cells["cardTypeName"].Value = _lstHistoryDatas[i].CardTypeName;
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
