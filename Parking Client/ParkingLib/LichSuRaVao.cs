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
    public class LichSuRaVao
    {
        #region Structures
        private Int32 _iDLichSu=0;
        [DisplayName("IDLichSu")]
        [JsonProperty("IDLichSu", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 IDLichSu
        {
            get { return _iDLichSu; }
            set { _iDLichSu = value; }
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
        private String _bienSo=String.Empty;
        [DisplayName("BienSo")]
        [JsonProperty("BienSo", NullValueHandling = NullValueHandling.Ignore)]
        public String BienSo
        {
            get { return _bienSo; }
            set { _bienSo = value; }
        }
        private Int32 _iDLoaiXe=0;
        [DisplayName("IDLoaiXe")]
        [JsonProperty("IDLoaiXe", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 IDLoaiXe
        {
            get { return _iDLoaiXe; }
            set { _iDLoaiXe = value; }
        }
        private Double _giaTien=0;
        [DisplayName("GiaTien")]
        [JsonProperty("GiaTien", NullValueHandling = NullValueHandling.Ignore)]
        public Double GiaTien
        {
            get { return _giaTien; }
            set { _giaTien = value; }
        }
        private String _ngayVao=String.Empty;
        [DisplayName("NgayVao")]
        [JsonProperty("NgayVao", NullValueHandling = NullValueHandling.Ignore)]
        public String NgayVao
        {
            get { return _ngayVao; }
            set { _ngayVao = value; }
        }
        private String _ngayRa=String.Empty;
        [DisplayName("NgayRa")]
        [JsonProperty("NgayRa", NullValueHandling = NullValueHandling.Ignore)]
        public String NgayRa
        {
            get { return _ngayRa; }
            set { _ngayRa = value; }
        }
        private String _anhVao01=String.Empty;
        [DisplayName("AnhVao01")]
        [JsonProperty("AnhVao01", NullValueHandling = NullValueHandling.Ignore)]
        public String AnhVao01
        {
            get { return _anhVao01; }
            set { _anhVao01 = value; }
        }
        private String _anhVao02=String.Empty;
        [DisplayName("AnhVao02")]
        [JsonProperty("AnhVao02", NullValueHandling = NullValueHandling.Ignore)]
        public String AnhVao02
        {
            get { return _anhVao02; }
            set { _anhVao02 = value; }
        }
        private String _anhRa01=String.Empty;
        [DisplayName("AnhRa01")]
        [JsonProperty("AnhRa01", NullValueHandling = NullValueHandling.Ignore)]
        public String AnhRa01
        {
            get { return _anhRa01; }
            set { _anhRa01 = value; }
        }
        private String _anhRa02=String.Empty;
        [DisplayName("AnhRa02")]
        [JsonProperty("AnhRa02", NullValueHandling = NullValueHandling.Ignore)]
        public String AnhRa02
        {
            get { return _anhRa02; }
            set { _anhRa02 = value; }
        }
        private Int32 _trangThaiHoatDong=0;
        [DisplayName("TrangThaiHoatDong")]
        [JsonProperty("TrangThaiHoatDong", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 TrangThaiHoatDong
        {
            get { return _trangThaiHoatDong; }
            set { _trangThaiHoatDong = value; }
        }
        public LichSuRaVao(){}
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
            if (_iDLichSu == null) 
            {
                _res = false; 
                _errorStr = "_IDLICHSU không được bỏ trống!";
            }

            return _res;
        }

        #region Data
        private SQLiteConnection _conn = DBExecute.OpenConnection();
        private SQLiteCommand _cmd=new SQLiteCommand();
        public void Add()
        {
            var _strLichSuRaVao = "INSERT INTO LichSuRaVao([MaThe],[IDLoaiThe],[BienSo],[IDLoaiXe],[GiaTien],[NgayVao],[NgayRa],[AnhVao01],[AnhVao02],[AnhRa01],[AnhRa02],[TrangThaiHoatDong]) Values(@MaThe,@IDLoaiThe,@BienSo,@IDLoaiXe,@GiaTien,@NgayVao,@NgayRa,@AnhVao01,@AnhVao02,@AnhRa01,@AnhRa02,@TrangThaiHoatDong)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strLichSuRaVao; 
            _cmd.Parameters.Add("@IDLichSu",DbType.Int32).Value = _iDLichSu;
            _cmd.Parameters.Add("@MaThe",DbType.String).Value = String.IsNullOrEmpty(_maThe) ? "" : _maThe;
            _cmd.Parameters.Add("@IDLoaiThe",DbType.Int32).Value = _iDLoaiThe;
            _cmd.Parameters.Add("@BienSo",DbType.String).Value = String.IsNullOrEmpty(_bienSo) ? "" : _bienSo;
            _cmd.Parameters.Add("@IDLoaiXe",DbType.Int32).Value = _iDLoaiXe;
            _cmd.Parameters.Add("@GiaTien",DbType.Double).Value = _giaTien;
            _cmd.Parameters.Add("@NgayVao",DbType.String).Value = String.IsNullOrEmpty(_ngayVao) ? "" : _ngayVao;
            _cmd.Parameters.Add("@NgayRa",DbType.String).Value = String.IsNullOrEmpty(_ngayRa) ? "" : _ngayRa;
            _cmd.Parameters.Add("@AnhVao01",DbType.String).Value = String.IsNullOrEmpty(_anhVao01) ? "" : _anhVao01;
            _cmd.Parameters.Add("@AnhVao02",DbType.String).Value = String.IsNullOrEmpty(_anhVao02) ? "" : _anhVao02;
            _cmd.Parameters.Add("@AnhRa01",DbType.String).Value = String.IsNullOrEmpty(_anhRa01) ? "" : _anhRa01;
            _cmd.Parameters.Add("@AnhRa02",DbType.String).Value = String.IsNullOrEmpty(_anhRa02) ? "" : _anhRa02;
            _cmd.Parameters.Add("@TrangThaiHoatDong",DbType.Int32).Value = _trangThaiHoatDong;

            _cmd.ExecuteNonQuery();
                        var _cmdID = new SQLiteCommand( " SELECT last_insert_rowid() AS  IDLichSu FROM LichSuRaVao", _conn);
            System.Object temp = _cmdID.ExecuteScalar();
            _iDLichSu = Convert.ToInt32(temp);

            _conn.Close();
        }
        public void Edit()
        {
            var _strLichSuRaVao = "UPDATE LichSuRaVao SET MaThe = @MaThe,IDLoaiThe = @IDLoaiThe,BienSo = @BienSo,IDLoaiXe = @IDLoaiXe,GiaTien = @GiaTien,NgayVao = @NgayVao,NgayRa = @NgayRa,AnhVao01 = @AnhVao01,AnhVao02 = @AnhVao02,AnhRa01 = @AnhRa01,AnhRa02 = @AnhRa02,TrangThaiHoatDong = @TrangThaiHoatDong WHERE (IDLichSu = @IDLichSu)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strLichSuRaVao; 
            _cmd.Parameters.Add("@IDLichSu",DbType.Int32).Value = _iDLichSu;
            _cmd.Parameters.Add("@MaThe",DbType.String).Value = String.IsNullOrEmpty(_maThe) ? "" : _maThe;
            _cmd.Parameters.Add("@IDLoaiThe",DbType.Int32).Value = _iDLoaiThe;
            _cmd.Parameters.Add("@BienSo",DbType.String).Value = String.IsNullOrEmpty(_bienSo) ? "" : _bienSo;
            _cmd.Parameters.Add("@IDLoaiXe",DbType.Int32).Value = _iDLoaiXe;
            _cmd.Parameters.Add("@GiaTien",DbType.Double).Value = _giaTien;
            _cmd.Parameters.Add("@NgayVao",DbType.String).Value = String.IsNullOrEmpty(_ngayVao) ? "" : _ngayVao;
            _cmd.Parameters.Add("@NgayRa",DbType.String).Value = String.IsNullOrEmpty(_ngayRa) ? "" : _ngayRa;
            _cmd.Parameters.Add("@AnhVao01",DbType.String).Value = String.IsNullOrEmpty(_anhVao01) ? "" : _anhVao01;
            _cmd.Parameters.Add("@AnhVao02",DbType.String).Value = String.IsNullOrEmpty(_anhVao02) ? "" : _anhVao02;
            _cmd.Parameters.Add("@AnhRa01",DbType.String).Value = String.IsNullOrEmpty(_anhRa01) ? "" : _anhRa01;
            _cmd.Parameters.Add("@AnhRa02",DbType.String).Value = String.IsNullOrEmpty(_anhRa02) ? "" : _anhRa02;
            _cmd.Parameters.Add("@TrangThaiHoatDong",DbType.Int32).Value = _trangThaiHoatDong;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }
        public void Del()
        {
            var _strLichSuRaVao = "DELETE FROM LichSuRaVao WHERE  (IDLichSu = @IDLichSu)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strLichSuRaVao; 
            _cmd.Parameters.Add("@IDLichSu",DbType.Int32).Value = _iDLichSu;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }
        public LichSuRaVao Get()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            LichSuRaVao _LichSuRaVao = null;
            var _strLichSuRaVao = "SELECT * FROM LichSuRaVao WHERE IDLichSu = "+ _iDLichSu;
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strLichSuRaVao, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "LichSuRaVao");
                    dt = ds.Tables["LichSuRaVao"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                DataRow dr = dt.Rows[i];
                _LichSuRaVao = new LichSuRaVao();
                    _LichSuRaVao.IDLichSu=Convert.ToInt32(dr["IDLichSu"]);
                    _LichSuRaVao.MaThe=Convert.ToString(dr["MaThe"]);
                    _LichSuRaVao.IDLoaiThe=Convert.ToInt32(dr["IDLoaiThe"]);
                    _LichSuRaVao.BienSo=Convert.ToString(dr["BienSo"]);
                    _LichSuRaVao.IDLoaiXe=Convert.ToInt32(dr["IDLoaiXe"]);
                    _LichSuRaVao.GiaTien=Convert.ToDouble(dr["GiaTien"]);
                    _LichSuRaVao.NgayVao=Convert.ToString(dr["NgayVao"]);
                    _LichSuRaVao.NgayRa=Convert.ToString(dr["NgayRa"]);
                    _LichSuRaVao.AnhVao01=Convert.ToString(dr["AnhVao01"]);
                    _LichSuRaVao.AnhVao02=Convert.ToString(dr["AnhVao02"]);
                    _LichSuRaVao.AnhRa01=Convert.ToString(dr["AnhRa01"]);
                    _LichSuRaVao.AnhRa02=Convert.ToString(dr["AnhRa02"]);
                    _LichSuRaVao.TrangThaiHoatDong=Convert.ToInt32(dr["TrangThaiHoatDong"]);

                }
            _conn.Close();
            return _LichSuRaVao;
        }
        public DataTable Get_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strLichSuRaVao = "SELECT * FROM LichSuRaVao WHERE IDLichSu = "+ _iDLichSu;
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strLichSuRaVao, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "LichSuRaVao");
                    dt = ds.Tables["LichSuRaVao"];
                }
            }
            _conn.Close();
            return dt;
        }
        public List<LichSuRaVao> Gets()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List < LichSuRaVao > _lstLichSuRaVao = new List < LichSuRaVao >();
            var _strLichSuRaVao = "SELECT * FROM LichSuRaVao";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strLichSuRaVao, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "LichSuRaVao");
                    dt = ds.Tables["LichSuRaVao"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                DataRow dr = dt.Rows[i];
                var _LichSuRaVao = new LichSuRaVao();
                    _LichSuRaVao.IDLichSu=Convert.ToInt32(dr["IDLichSu"]);
                    _LichSuRaVao.MaThe=Convert.ToString(dr["MaThe"]);
                    _LichSuRaVao.IDLoaiThe=Convert.ToInt32(dr["IDLoaiThe"]);
                    _LichSuRaVao.BienSo=Convert.ToString(dr["BienSo"]);
                    _LichSuRaVao.IDLoaiXe=Convert.ToInt32(dr["IDLoaiXe"]);
                    _LichSuRaVao.GiaTien=Convert.ToDouble(dr["GiaTien"]);
                    _LichSuRaVao.NgayVao=Convert.ToString(dr["NgayVao"]);
                    _LichSuRaVao.NgayRa=Convert.ToString(dr["NgayRa"]);
                    _LichSuRaVao.AnhVao01=Convert.ToString(dr["AnhVao01"]);
                    _LichSuRaVao.AnhVao02=Convert.ToString(dr["AnhVao02"]);
                    _LichSuRaVao.AnhRa01=Convert.ToString(dr["AnhRa01"]);
                    _LichSuRaVao.AnhRa02=Convert.ToString(dr["AnhRa02"]);
                    _LichSuRaVao.TrangThaiHoatDong=Convert.ToInt32(dr["TrangThaiHoatDong"]);

                _lstLichSuRaVao.Add(_LichSuRaVao);
                }
            _conn.Close();
            return _lstLichSuRaVao;
        }
        public DataTable Gets_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strLichSuRaVao = "SELECT * FROM LichSuRaVao";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strLichSuRaVao, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "LichSuRaVao");
                    dt = ds.Tables["LichSuRaVao"];
                }
            }
            _conn.Close();
            return dt;
        }
        #endregion
    }
}