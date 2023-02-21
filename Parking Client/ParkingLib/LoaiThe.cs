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
    public class LoaiThe
    {
        #region Structures
        private Int32 _iDLoaiThe=0;
        [DisplayName("IDLoaiThe")]
        [JsonProperty("IDLoaiThe", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 IDLoaiThe
        {
            get { return _iDLoaiThe; }
            set { _iDLoaiThe = value; }
        }
        private String _tenLoaiThe=String.Empty;
        [DisplayName("TenLoaiThe")]
        [JsonProperty("TenLoaiThe", NullValueHandling = NullValueHandling.Ignore)]
        public String TenLoaiThe
        {
            get { return _tenLoaiThe; }
            set { _tenLoaiThe = value; }
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
        public LoaiThe(){}
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
            if (_iDLoaiThe == null) 
            {
                _res = false; 
                _errorStr = "_IDLOAITHE không được bỏ trống!";
            }

            return _res;
        }

        #region Data
        private SQLiteConnection _conn = DBExecute.OpenConnection();
        private SQLiteCommand _cmd=new SQLiteCommand();
        public void Add()
        {
            var _strLoaiThe = "INSERT INTO LoaiThe([TenLoaiThe],[GhiChu],[TrangThaiHoatDong]) Values(@TenLoaiThe,@GhiChu,@TrangThaiHoatDong)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strLoaiThe; 
            _cmd.Parameters.Add("@IDLoaiThe",DbType.Int32).Value = _iDLoaiThe;
            _cmd.Parameters.Add("@TenLoaiThe",DbType.String).Value = String.IsNullOrEmpty(_tenLoaiThe) ? "" : _tenLoaiThe;
            _cmd.Parameters.Add("@GhiChu",DbType.String).Value = String.IsNullOrEmpty(_ghiChu) ? "" : _ghiChu;
            _cmd.Parameters.Add("@TrangThaiHoatDong",DbType.Int32).Value = _trangThaiHoatDong;

            _cmd.ExecuteNonQuery();
                        var _cmdID = new SQLiteCommand( " SELECT last_insert_rowid() AS  IDLoaiThe FROM LoaiThe", _conn);
            System.Object temp = _cmdID.ExecuteScalar();
            _iDLoaiThe = Convert.ToInt32(temp);

            _conn.Close();
        }
        public void Edit()
        {
            var _strLoaiThe = "UPDATE LoaiThe SET TenLoaiThe = @TenLoaiThe,GhiChu = @GhiChu,TrangThaiHoatDong = @TrangThaiHoatDong WHERE (IDLoaiThe = @IDLoaiThe)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strLoaiThe; 
            _cmd.Parameters.Add("@IDLoaiThe",DbType.Int32).Value = _iDLoaiThe;
            _cmd.Parameters.Add("@TenLoaiThe",DbType.String).Value = String.IsNullOrEmpty(_tenLoaiThe) ? "" : _tenLoaiThe;
            _cmd.Parameters.Add("@GhiChu",DbType.String).Value = String.IsNullOrEmpty(_ghiChu) ? "" : _ghiChu;
            _cmd.Parameters.Add("@TrangThaiHoatDong",DbType.Int32).Value = _trangThaiHoatDong;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }
        public void Del()
        {
            var _strLoaiThe = "DELETE FROM LoaiThe WHERE  (IDLoaiThe = @IDLoaiThe)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strLoaiThe; 
            _cmd.Parameters.Add("@IDLoaiThe",DbType.Int32).Value = _iDLoaiThe;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }
        public LoaiThe Get()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            LoaiThe _LoaiThe = null;
            var _strLoaiThe = "SELECT * FROM LoaiThe WHERE IDLoaiThe = "+ _iDLoaiThe;
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strLoaiThe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "LoaiThe");
                    dt = ds.Tables["LoaiThe"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                DataRow dr = dt.Rows[i];
                _LoaiThe = new LoaiThe();
                    _LoaiThe.IDLoaiThe=Convert.ToInt32(dr["IDLoaiThe"]);
                    _LoaiThe.TenLoaiThe=Convert.ToString(dr["TenLoaiThe"]);
                    _LoaiThe.GhiChu=Convert.ToString(dr["GhiChu"]);
                    _LoaiThe.TrangThaiHoatDong=Convert.ToInt32(dr["TrangThaiHoatDong"]);

                }
            _conn.Close();
            return _LoaiThe;
        }
        public DataTable Get_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strLoaiThe = "SELECT * FROM LoaiThe WHERE IDLoaiThe = "+ _iDLoaiThe;
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strLoaiThe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "LoaiThe");
                    dt = ds.Tables["LoaiThe"];
                }
            }
            _conn.Close();
            return dt;
        }
        public List<LoaiThe> Gets()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List < LoaiThe > _lstLoaiThe = new List < LoaiThe >();
            var _strLoaiThe = "SELECT * FROM LoaiThe";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strLoaiThe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "LoaiThe");
                    dt = ds.Tables["LoaiThe"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                DataRow dr = dt.Rows[i];
                var _LoaiThe = new LoaiThe();
                    _LoaiThe.IDLoaiThe=Convert.ToInt32(dr["IDLoaiThe"]);
                    _LoaiThe.TenLoaiThe=Convert.ToString(dr["TenLoaiThe"]);
                    _LoaiThe.GhiChu=Convert.ToString(dr["GhiChu"]);
                    _LoaiThe.TrangThaiHoatDong=Convert.ToInt32(dr["TrangThaiHoatDong"]);

                _lstLoaiThe.Add(_LoaiThe);
                }
            _conn.Close();
            return _lstLoaiThe;
        }
        public DataTable Gets_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strLoaiThe = "SELECT * FROM LoaiThe";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strLoaiThe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "LoaiThe");
                    dt = ds.Tables["LoaiThe"];
                }
            }
            _conn.Close();
            return dt;
        }
        #endregion
    }
}