using ParkingLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ParkingApp
{
    public partial class FrmHistory : MetroFramework.Forms.MetroForm
    {
        private readonly Helper _helperDll = new Helper();
        private int _rowIndex = -1;

        public FrmHistory()
        {
            InitializeComponent();

            dateTimePickerFrom.Format = DateTimePickerFormat.Custom;
            dateTimePickerFrom.CustomFormat = GlobalConfig.DateTimePickerFormat;

            dateTimePickerTo.Format = DateTimePickerFormat.Custom;
            dateTimePickerTo.CustomFormat = GlobalConfig.DateTimePickerFormat;
        }

        private void SearchInfo()
        {
            var lstHistoryDataFilter = new List<HistoryData>();
            _helperDll.LoadHistoryData(dateTimePickerFrom.Value, dateTimePickerTo.Value, ref lstHistoryDataFilter, false);

            if (txtSearch.Text.Trim() == "")
            {
                ReloadGrid(lstHistoryDataFilter);
            }
            else
            {
                var tokens = txtSearch.Text.Trim().Split('|');
                var lstHistoryDataSearchAndFilter = new List<HistoryData>();
                foreach (var token in tokens)
                {
                    var strSearch = Helper.ConvertNoUnicode(token.Trim()).ToLower();
                    var history = lstHistoryDataFilter.FirstOrDefault(o => o.CardNumber.Contains(strSearch));
                    if (history != null)
                    {
                        lstHistoryDataSearchAndFilter.Add(history);
                        lstHistoryDataFilter.Remove(history);
                    }

                    ReloadGrid(lstHistoryDataSearchAndFilter);
                }
            }
        }

        private void ReloadGrid(IReadOnlyList<HistoryData> lstHistoryData)
        {
            var scrollPosition = dgvHistory.FirstDisplayedScrollingRowIndex;
            dgvHistory.Rows.Clear();
            for (var i = 0; i < lstHistoryData.Count; i++)
            {
                dgvHistory.Rows.Add();
                dgvHistory.Rows[i].Cells["STT"].Value = i + 1;
                dgvHistory.Rows[i].Cells["CardNumber"].Value = lstHistoryData[i].CardNumber;
                dgvHistory.Rows[i].Cells["LicensePlate"].Value = lstHistoryData[i].LicensePlate;
                dgvHistory.Rows[i].Cells["Price"].Value = lstHistoryData[i].Price;

                string type;
                switch (lstHistoryData[i].Type)
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
                dgvHistory.Rows[i].Cells["Type"].Value = type;

                dgvHistory.Rows[i].Cells["Time"].Value = lstHistoryData[i].Time.ToString(CultureInfo.InvariantCulture);
                dgvHistory.Rows[i].Cells["VehicleTypeName"].Value = lstHistoryData[i].VehicleTypeName;
                dgvHistory.Rows[i].Cells["CardTypeName"].Value = lstHistoryData[i].CardTypeName;
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

        private void DgvHistory_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _rowIndex = e.RowIndex;
            if (_rowIndex <= 0) return;
            dgvHistory.Rows[_rowIndex].Selected = true;
        }

        private void DgvHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _rowIndex = e.RowIndex;
            if (_rowIndex < 0) return;
            dgvHistory.Rows[_rowIndex].Selected = true;
        }

        private void TxtSearchCandidate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchInfo();
            }
        }

        private void FrmHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchInfo();
        }
    }
}
