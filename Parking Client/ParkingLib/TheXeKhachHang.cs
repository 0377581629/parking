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
    public class TheXeKhachHang
    {
        #region Structures
        private Int32 _iDTheXeKhachHang=0;
        [DisplayName("IDTheXeKhachHang")]
        [JsonProperty("IDTheXeKhachHang", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 IDTheXeKhachHang
        {
            get { return _iDTheXeKhachHang; }
            set { _iDTheXeKhachHang = value; }
        }
        private Int32 _iDKhachHang=0;
        [DisplayName("IDKhachHang")]
        [JsonProperty("IDKhachHang", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 IDKhachHang
        {
            get { return _iDKhachHang; }
            set { _iDKhachHang = value; }
        }
        private String _maThe=String.Empty;
        [DisplayName("MaThe")]
        [JsonProperty("MaThe", NullValueHandling = NullValueHandling.Ignore)]
        public String MaThe
        {
            get { return _maThe; }
            set { _maThe = value; }
        }
        private Int32 _iDLoaiThe=0;
        [DisplayName("IDLoaiThe")]
        [JsonProperty("IDLoaiThe", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 IDLoaiThe
        {
            get { return _iDLoaiThe; }
            set { _iDLoaiThe = value; }
        }
        private String _tenXe=String.Empty;
        [DisplayName("TenXe")]
        [JsonProperty("TenXe", NullValueHandling = NullValueHandling.Ignore)]
        public String TenXe
        {
            get { return _tenXe; }
            set { _tenXe = value; }
        }
        private String _bienSo=String.Empty;
        [DisplayName("BienSo")]
        [JsonProperty("BienSo", NullValueHandling = NullValueHandling.Ignore)]
        public String BienSo
        {
            get { return _bienSo; }
            set { _bienSo = value; }
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
        public TheXeKhachHang(){}
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
            if (_iDTheXeKhachHang == null) 
            {
                _res = false; 
                _errorStr = "_IDTHEXEKHACHHANG không được bỏ trống!";
            }

            return _res;
        }

        #region Data
        private SQLiteConnection _conn = DBExecute.OpenConnection();
        private SQLiteCommand _cmd=new SQLiteCommand();
        public void Add()
        {
            var _strTheXeKhachHang = "INSERT INTO TheXeKhachHang([IDKhachHang],[MaThe],[IDLoaiThe],[TenXe],[BienSo],[NgayApDung],[NgayKetThuc],[TrangThaiHoatDong]) Values(@IDKhachHang,@MaThe,@IDLoaiThe,@TenXe,@BienSo,@NgayApDung,@NgayKetThuc,@TrangThaiHoatDong)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strTheXeKhachHang; 
            _cmd.Parameters.Add("@IDTheXeKhachHang",DbType.Int32).Value = _iDTheXeKhachHang;
            _cmd.Parameters.Add("@IDKhachHang",DbType.Int32).Value = _iDKhachHang;
            _cmd.Parameters.Add("@MaThe",DbType.String).Value = String.IsNullOrEmpty(_maThe) ? "" : _maThe;
            _cmd.Parameters.Add("@IDLoaiThe",DbType.Int32).Value = _iDLoaiThe;
            _cmd.Parameters.Add("@TenXe",DbType.String).Value = String.IsNullOrEmpty(_tenXe) ? "" : _tenXe;
            _cmd.Parameters.Add("@BienSo",DbType.String).Value = String.IsNullOrEmpty(_bienSo) ? "" : _bienSo;
            _cmd.Parameters.Add("@NgayApDung",DbType.String).Value = String.IsNullOrEmpty(_ngayApDung) ? "" : _ngayApDung;
            _cmd.Parameters.Add("@NgayKetThuc",DbType.String).Value = String.IsNullOrEmpty(_ngayKetThuc) ? "" : _ngayKetThuc;
            _cmd.Parameters.Add("@TrangThaiHoatDong",DbType.Int32).Value = _trangThaiHoatDong;

            _cmd.ExecuteNonQuery();
                        var _cmdID = new SQLiteCommand( " SELECT last_insert_rowid() AS  IDTheXeKhachHang FROM TheXeKhachHang", _conn);
            System.Object temp = _cmdID.ExecuteScalar();
            _iDTheXeKhachHang = Convert.ToInt32(temp);

            _conn.Close();
        }
        public void Edit()
        {
            var _strTheXeKhachHang = "UPDATE TheXeKhachHang SET IDKhachHang = @IDKhachHang,MaThe = @MaThe,IDLoaiThe = @IDLoaiThe,TenXe = @TenXe,BienSo = @BienSo,NgayApDung = @NgayApDung,NgayKetThuc = @NgayKetThuc,TrangThaiHoatDong = @TrangThaiHoatDong WHERE (IDTheXeKhachHang = @IDTheXeKhachHang)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strTheXeKhachHang; 
            _cmd.Parameters.Add("@IDTheXeKhachHang",DbType.Int32).Value = _iDTheXeKhachHang;
            _cmd.Parameters.Add("@IDKhachHang",DbType.Int32).Value = _iDKhachHang;
            _cmd.Parameters.Add("@MaThe",DbType.String).Value = String.IsNullOrEmpty(_maThe) ? "" : _maThe;
            _cmd.Parameters.Add("@IDLoaiThe",DbType.Int32).Value = _iDLoaiThe;
            _cmd.Parameters.Add("@TenXe",DbType.String).Value = String.IsNullOrEmpty(_tenXe) ? "" : _tenXe;
            _cmd.Parameters.Add("@BienSo",DbType.String).Value = String.IsNullOrEmpty(_bienSo) ? "" : _bienSo;
            _cmd.Parameters.Add("@NgayApDung",DbType.String).Value = String.IsNullOrEmpty(_ngayApDung) ? "" : _ngayApDung;
            _cmd.Parameters.Add("@NgayKetThuc",DbType.String).Value = String.IsNullOrEmpty(_ngayKetThuc) ? "" : _ngayKetThuc;
            _cmd.Parameters.Add("@TrangThaiHoatDong",DbType.Int32).Value = _trangThaiHoatDong;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }
        public void Del()
        {
            var _strTheXeKhachHang = "DELETE FROM TheXeKhachHang WHERE  (IDTheXeKhachHang = @IDTheXeKhachHang)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strTheXeKhachHang; 
            _cmd.Parameters.Add("@IDTheXeKhachHang",DbType.Int32).Value = _iDTheXeKhachHang;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }
        public TheXeKhachHang Get()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            TheXeKhachHang _TheXeKhachHang = null;
            var _strTheXeKhachHang = "SELECT * FROM TheXeKhachHang WHERE IDTheXeKhachHang = "+ _iDTheXeKhachHang;
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strTheXeKhachHang, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "TheXeKhachHang");
                    dt = ds.Tables["TheXeKhachHang"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                DataRow dr = dt.Rows[i];
                _TheXeKhachHang = new TheXeKhachHang();
                    _TheXeKhachHang.IDTheXeKhachHang=Convert.ToInt32(dr["IDTheXeKhachHang"]);
                    _TheXeKhachHang.IDKhachHang=Convert.ToInt32(dr["IDKhachHang"]);
                    _TheXeKhachHang.MaThe=Convert.ToString(dr["MaThe"]);
                    _TheXeKhachHang.IDLoaiThe=Convert.ToInt32(dr["IDLoaiThe"]);
                    _TheXeKhachHang.TenXe=Convert.ToString(dr["TenXe"]);
                    _TheXeKhachHang.BienSo=Convert.ToString(dr["BienSo"]);
                    _TheXeKhachHang.NgayApDung=Convert.ToString(dr["NgayApDung"]);
                    _TheXeKhachHang.NgayKetThuc=Convert.ToString(dr["NgayKetThuc"]);
                    _TheXeKhachHang.TrangThaiHoatDong=Convert.ToInt32(dr["TrangThaiHoatDong"]);

                }
            _conn.Close();
            return _TheXeKhachHang;
        }
        public DataTable Get_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strTheXeKhachHang = "SELECT * FROM TheXeKhachHang WHERE IDTheXeKhachHang = "+ _iDTheXeKhachHang;
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strTheXeKhachHang, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "TheXeKhachHang");
                    dt = ds.Tables["TheXeKhachHang"];
                }
            }
            _conn.Close();
            return dt;
        }
        public List<TheXeKhachHang> Gets()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List < TheXeKhachHang > _lstTheXeKhachHang = new List < TheXeKhachHang >();
            var _strTheXeKhachHang = "SELECT * FROM TheXeKhachHang";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strTheXeKhachHang, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "TheXeKhachHang");
                    dt = ds.Tables["TheXeKhachHang"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                DataRow dr = dt.Rows[i];
                var _TheXeKhachHang = new TheXeKhachHang();
                    _TheXeKhachHang.IDTheXeKhachHang=Convert.ToInt32(dr["IDTheXeKhachHang"]);
                    _TheXeKhachHang.IDKhachHang=Convert.ToInt32(dr["IDKhachHang"]);
                    _TheXeKhachHang.MaThe=Convert.ToString(dr["MaThe"]);
                    _TheXeKhachHang.IDLoaiThe=Convert.ToInt32(dr["IDLoaiThe"]);
                    _TheXeKhachHang.TenXe=Convert.ToString(dr["TenXe"]);
                    _TheXeKhachHang.BienSo=Convert.ToString(dr["BienSo"]);
                    _TheXeKhachHang.NgayApDung=Convert.ToString(dr["NgayApDung"]);
                    _TheXeKhachHang.NgayKetThuc=Convert.ToString(dr["NgayKetThuc"]);
                    _TheXeKhachHang.TrangThaiHoatDong=Convert.ToInt32(dr["TrangThaiHoatDong"]);

                _lstTheXeKhachHang.Add(_TheXeKhachHang);
                }
            _conn.Close();
            return _lstTheXeKhachHang;
        }
        public DataTable Gets_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strTheXeKhachHang = "SELECT * FROM TheXeKhachHang";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strTheXeKhachHang, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "TheXeKhachHang");
                    dt = ds.Tables["TheXeKhachHang"];
                }
            }
            _conn.Close();
            return dt;
        }
        #endregion
    }
}