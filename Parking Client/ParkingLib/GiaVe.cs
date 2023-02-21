using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Data.SQLite;
//using Dapper;
using System.Linq;
using DataAccess;

namespace ParkingLib
{
    public class GiaVe
    {
        #region Structures
        private Int32 _iDGiaVe=0;
        [DisplayName("IDGiaVe")]
        [JsonProperty("IDGiaVe", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 IDGiaVe
        {
            get { return _iDGiaVe; }
            set { _iDGiaVe = value; }
        }
        private Int32 _iDLoaiThe=0;
        [DisplayName("IDLoaiThe")]
        [JsonProperty("IDLoaiThe", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 IDLoaiThe
        {
            get { return _iDLoaiThe; }
            set { _iDLoaiThe = value; }
        }
        private Int32 _iDLoaiXe=0;
        [DisplayName("IDLoaiXe")]
        [JsonProperty("IDLoaiXe", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 IDLoaiXe
        {
            get { return _iDLoaiXe; }
            set { _iDLoaiXe = value; }
        }
        private Double _donGia=0;
        [DisplayName("DonGia")]
        [JsonProperty("DonGia", NullValueHandling = NullValueHandling.Ignore)]
        public Double DonGia
        {
            get { return _donGia; }
            set { _donGia = value; }
        }
        private String _ngayApDung=String.Empty;
        [DisplayName("NgayApDung")]
        [JsonProperty("NgayApDung", NullValueHandling = NullValueHandling.Ignore)]
        public String NgayApDung
        {
            get { return _ngayApDung; }
            set { _ngayApDung = value; }
        }
        private String _ngayKetThuc=String.Empty;
        [DisplayName("NgayKetThuc")]
        [JsonProperty("NgayKetThuc", NullValueHandling = NullValueHandling.Ignore)]
        public String NgayKetThuc
        {
            get { return _ngayKetThuc; }
            set { _ngayKetThuc = value; }
        }
        private Int32 _trangThaiHoatDong=0;
        [DisplayName("TrangThaiHoatDong")]
        [JsonProperty("TrangThaiHoatDong", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 TrangThaiHoatDong
        {
            get { return _trangThaiHoatDong; }
            set { _trangThaiHoatDong = value; }
        }
        public GiaVe(){}
        #endregion
        private String _errorStr = String.Empty;
        public String ErrorStr
        {
            get { return _errorStr; }
            set { _errorStr = value; }
        }
        public bool Valid()
        {
            bool _res=true;
            if (_iDGiaVe == null) 
            {
                _res = false; 
                _errorStr = "_IDGIAVE không được bỏ trống!";
            }

            return _res;
        }

        #region Data
        private SQLiteConnection _conn = DBExecute.OpenConnection();
        private SQLiteCommand _cmd = new SQLiteCommand();
        public void Add()
        {
            var _strGiaVe = "INSERT INTO GiaVe([IDLoaiThe],[IDLoaiXe],[DonGia],[NgayApDung],[NgayKetThuc],[TrangThaiHoatDong]) Values(@IDLoaiThe,@IDLoaiXe,@DonGia,@NgayApDung,@NgayKetThuc,@TrangThaiHoatDong)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strGiaVe; 
            _cmd.Parameters.Add("@IDGiaVe",DbType.Int32).Value = _iDGiaVe;
            _cmd.Parameters.Add("@IDLoaiThe",DbType.Int32).Value = _iDLoaiThe;
            _cmd.Parameters.Add("@IDLoaiXe",DbType.Int32).Value = _iDLoaiXe;
            _cmd.Parameters.Add("@DonGia",DbType.Double).Value = _donGia;
            _cmd.Parameters.Add("@NgayApDung",DbType.String).Value = String.IsNullOrEmpty(_ngayApDung) ? "" : _ngayApDung;
            _cmd.Parameters.Add("@NgayKetThuc",DbType.String).Value = String.IsNullOrEmpty(_ngayKetThuc) ? "" : _ngayKetThuc;
            _cmd.Parameters.Add("@TrangThaiHoatDong",DbType.Int32).Value = _trangThaiHoatDong;

            _cmd.ExecuteNonQuery();
                        var _cmdID = new SQLiteCommand( " SELECT last_insert_rowid() AS  IDGiaVe FROM GiaVe", _conn);
            System.Object temp = _cmdID.ExecuteScalar();
            _iDGiaVe = Convert.ToInt32(temp);

            _conn.Close();
        }
        public void Edit()
        {
            var _strGiaVe = "UPDATE GiaVe SET IDLoaiThe = @IDLoaiThe,IDLoaiXe = @IDLoaiXe,DonGia = @DonGia,NgayApDung = @NgayApDung,NgayKetThuc = @NgayKetThuc,TrangThaiHoatDong = @TrangThaiHoatDong WHERE (IDGiaVe = @IDGiaVe)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strGiaVe; 
            _cmd.Parameters.Add("@IDGiaVe",DbType.Int32).Value = _iDGiaVe;
            _cmd.Parameters.Add("@IDLoaiThe",DbType.Int32).Value = _iDLoaiThe;
            _cmd.Parameters.Add("@IDLoaiXe",DbType.Int32).Value = _iDLoaiXe;
            _cmd.Parameters.Add("@DonGia",DbType.Double).Value = _donGia;
            _cmd.Parameters.Add("@NgayApDung",DbType.String).Value = String.IsNullOrEmpty(_ngayApDung) ? "" : _ngayApDung;
            _cmd.Parameters.Add("@NgayKetThuc",DbType.String).Value = String.IsNullOrEmpty(_ngayKetThuc) ? "" : _ngayKetThuc;
            _cmd.Parameters.Add("@TrangThaiHoatDong",DbType.Int32).Value = _trangThaiHoatDong;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }
        public void Del()
        {
            var _strGiaVe = "DELETE FROM GiaVe WHERE  (IDGiaVe = @IDGiaVe)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strGiaVe; 
            _cmd.Parameters.Add("@IDGiaVe",DbType.Int32).Value = _iDGiaVe;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }
        public GiaVe Get()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            GiaVe _GiaVe = null;
            var _strGiaVe = "SELECT * FROM GiaVe WHERE IDGiaVe = "+ _iDGiaVe;
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strGiaVe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "GiaVe");
                    dt = ds.Tables["GiaVe"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                DataRow dr = dt.Rows[i];
                _GiaVe = new GiaVe();
                    _GiaVe.IDGiaVe=Convert.ToInt32(dr["IDGiaVe"]);
                    _GiaVe.IDLoaiThe=Convert.ToInt32(dr["IDLoaiThe"]);
                    _GiaVe.IDLoaiXe=Convert.ToInt32(dr["IDLoaiXe"]);
                    _GiaVe.DonGia=Convert.ToDouble(dr["DonGia"]);
                    _GiaVe.NgayApDung=Convert.ToString(dr["NgayApDung"]);
                    _GiaVe.NgayKetThuc=Convert.ToString(dr["NgayKetThuc"]);
                    _GiaVe.TrangThaiHoatDong=Convert.ToInt32(dr["TrangThaiHoatDong"]);

                }
            _conn.Close();
            return _GiaVe;
        }
        public DataTable Get_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strGiaVe = "SELECT * FROM GiaVe WHERE IDGiaVe = "+ _iDGiaVe;
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strGiaVe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "GiaVe");
                    dt = ds.Tables["GiaVe"];
                }
            }
            _conn.Close();
            return dt;
        }
        public List<GiaVe> Gets()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List < GiaVe > _lstGiaVe = new List < GiaVe >();
            var _strGiaVe = "SELECT * FROM GiaVe";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strGiaVe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "GiaVe");
                    dt = ds.Tables["GiaVe"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                DataRow dr = dt.Rows[i];
                var _GiaVe = new GiaVe();
                    _GiaVe.IDGiaVe=Convert.ToInt32(dr["IDGiaVe"]);
                    _GiaVe.IDLoaiThe=Convert.ToInt32(dr["IDLoaiThe"]);
                    _GiaVe.IDLoaiXe=Convert.ToInt32(dr["IDLoaiXe"]);
                    _GiaVe.DonGia=Convert.ToDouble(dr["DonGia"]);
                    _GiaVe.NgayApDung=Convert.ToString(dr["NgayApDung"]);
                    _GiaVe.NgayKetThuc=Convert.ToString(dr["NgayKetThuc"]);
                    _GiaVe.TrangThaiHoatDong=Convert.ToInt32(dr["TrangThaiHoatDong"]);

                _lstGiaVe.Add(_GiaVe);
                }
            _conn.Close();
            return _lstGiaVe;
        }
        public DataTable Gets_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strGiaVe = "SELECT * FROM GiaVe";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strGiaVe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "GiaVe");
                    dt = ds.Tables["GiaVe"];
                }
            }
            _conn.Close();
            return dt;
        }
        #endregion
    }
}