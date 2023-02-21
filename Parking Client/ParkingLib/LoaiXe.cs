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
    public class LoaiXe
    {
        #region Structures
        private Int32 _iDLoaiXe=0;
        [DisplayName("IDLoaiXe")]
        [JsonProperty("IDLoaiXe", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 IDLoaiXe
        {
            get { return _iDLoaiXe; }
            set { _iDLoaiXe = value; }
        }
        private String _tenLoaiXe=String.Empty;
        [DisplayName("TenLoaiXe")]
        [JsonProperty("TenLoaiXe", NullValueHandling = NullValueHandling.Ignore)]
        public String TenLoaiXe
        {
            get { return _tenLoaiXe; }
            set { _tenLoaiXe = value; }
        }
        private String _ghiChu=String.Empty;
        [DisplayName("GhiChu")]
        [JsonProperty("GhiChu", NullValueHandling = NullValueHandling.Ignore)]
        public String GhiChu
        {
            get { return _ghiChu; }
            set { _ghiChu = value; }
        }
        private Int32 _trangThaiHoatDong=0;
        [DisplayName("TrangThaiHoatDong")]
        [JsonProperty("TrangThaiHoatDong", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 TrangThaiHoatDong
        {
            get { return _trangThaiHoatDong; }
            set { _trangThaiHoatDong = value; }
        }
        public LoaiXe(){}
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
            if (_iDLoaiXe == null) 
            {
                _res = false; 
                _errorStr = "_IDLOAIXE không được bỏ trống!";
            }

            return _res;
        }

        #region Data
        private SQLiteConnection _conn = DBExecute.OpenConnection();
        private SQLiteCommand _cmd=new SQLiteCommand();
        public void Add()
        {
            var _strLoaiXe = "INSERT INTO LoaiXe([TenLoaiXe],[GhiChu],[TrangThaiHoatDong]) Values(@TenLoaiXe,@GhiChu,@TrangThaiHoatDong)";
           if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strLoaiXe; 
            _cmd.Parameters.Add("@IDLoaiXe",DbType.Int32).Value = _iDLoaiXe;
            _cmd.Parameters.Add("@TenLoaiXe",DbType.String).Value = String.IsNullOrEmpty(_tenLoaiXe) ? "" : _tenLoaiXe;
            _cmd.Parameters.Add("@GhiChu",DbType.String).Value = String.IsNullOrEmpty(_ghiChu) ? "" : _ghiChu;
            _cmd.Parameters.Add("@TrangThaiHoatDong",DbType.Int32).Value = _trangThaiHoatDong;

            _cmd.ExecuteNonQuery();
                        var _cmdID = new SQLiteCommand( " SELECT last_insert_rowid() AS  IDLoaiXe FROM LoaiXe", _conn);
            System.Object temp = _cmdID.ExecuteScalar();
            _iDLoaiXe = Convert.ToInt32(temp);

            _conn.Close();
        }
        public void Edit()
        {
            var _strLoaiXe = "UPDATE LoaiXe SET TenLoaiXe = @TenLoaiXe,GhiChu = @GhiChu,TrangThaiHoatDong = @TrangThaiHoatDong WHERE (IDLoaiXe = @IDLoaiXe)";
           if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strLoaiXe; 
            _cmd.Parameters.Add("@IDLoaiXe",DbType.Int32).Value = _iDLoaiXe;
            _cmd.Parameters.Add("@TenLoaiXe",DbType.String).Value = String.IsNullOrEmpty(_tenLoaiXe) ? "" : _tenLoaiXe;
            _cmd.Parameters.Add("@GhiChu",DbType.String).Value = String.IsNullOrEmpty(_ghiChu) ? "" : _ghiChu;
            _cmd.Parameters.Add("@TrangThaiHoatDong",DbType.Int32).Value = _trangThaiHoatDong;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }
        public void Del()
        {
            var _strLoaiXe = "DELETE FROM LoaiXe WHERE  (IDLoaiXe = @IDLoaiXe)";
           if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strLoaiXe; 
            _cmd.Parameters.Add("@IDLoaiXe",DbType.Int32).Value = _iDLoaiXe;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }
        public LoaiXe Get()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            LoaiXe _LoaiXe = null;
            var _strLoaiXe = "SELECT * FROM LoaiXe WHERE IDLoaiXe = "+ _iDLoaiXe;
           if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strLoaiXe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "LoaiXe");
                    dt = ds.Tables["LoaiXe"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                DataRow dr = dt.Rows[i];
                _LoaiXe = new LoaiXe();
                    _LoaiXe.IDLoaiXe=Convert.ToInt32(dr["IDLoaiXe"]);
                    _LoaiXe.TenLoaiXe=Convert.ToString(dr["TenLoaiXe"]);
                    _LoaiXe.GhiChu=Convert.ToString(dr["GhiChu"]);
                    _LoaiXe.TrangThaiHoatDong=Convert.ToInt32(dr["TrangThaiHoatDong"]);

                }
            _conn.Close();
            return _LoaiXe;
        }
        public DataTable Get_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strLoaiXe = "SELECT * FROM LoaiXe WHERE IDLoaiXe = "+ _iDLoaiXe;
           if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strLoaiXe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "LoaiXe");
                    dt = ds.Tables["LoaiXe"];
                }
            }
            _conn.Close();
            return dt;
        }
        public List<LoaiXe> Gets()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List < LoaiXe > _lstLoaiXe = new List < LoaiXe >();
            var _strLoaiXe = "SELECT * FROM LoaiXe";
           if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strLoaiXe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "LoaiXe");
                    dt = ds.Tables["LoaiXe"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                DataRow dr = dt.Rows[i];
                var _LoaiXe = new LoaiXe();
                    _LoaiXe.IDLoaiXe=Convert.ToInt32(dr["IDLoaiXe"]);
                    _LoaiXe.TenLoaiXe=Convert.ToString(dr["TenLoaiXe"]);
                    _LoaiXe.GhiChu=Convert.ToString(dr["GhiChu"]);
                    _LoaiXe.TrangThaiHoatDong=Convert.ToInt32(dr["TrangThaiHoatDong"]);

                _lstLoaiXe.Add(_LoaiXe);
                }
            _conn.Close();
            return _lstLoaiXe;
        }
        public DataTable Gets_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strLoaiXe = "SELECT * FROM LoaiXe";
           if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strLoaiXe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "LoaiXe");
                    dt = ds.Tables["LoaiXe"];
                }
            }
            _conn.Close();
            return dt;
        }
        #endregion
    }
}