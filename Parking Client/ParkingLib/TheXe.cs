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
    public class TheXe
    {
        #region Structures
        private Int32 _iDTheXe=0;
        [DisplayName("IDTheXe")]
        [JsonProperty("IDTheXe", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 IDTheXe
        {
            get { return _iDTheXe; }
            set { _iDTheXe = value; }
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
        private Int32 _maTheXe=0;
        [DisplayName("MaTheXe")]
        [JsonProperty("MaTheXe", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 MaTheXe
        {
            get { return _maTheXe; }
            set { _maTheXe = value; }
        }
        private Int32 _soTheXe=0;
        [DisplayName("SoTheXe")]
        [JsonProperty("SoTheXe", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 SoTheXe
        {
            get { return _soTheXe; }
            set { _soTheXe = value; }
        }
        private String _ghiChu=String.Empty;
        [DisplayName("GhiChu")]
        [JsonProperty("GhiChu", NullValueHandling = NullValueHandling.Ignore)]
        public String GhiChu
        {
            get { return _ghiChu; }
            set { _ghiChu = value; }
        }
        private Int32 _trangThaiThe=0;
        [DisplayName("TrangThaiThe")]
        [JsonProperty("TrangThaiThe", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 TrangThaiThe
        {
            get { return _trangThaiThe; }
            set { _trangThaiThe = value; }
        }
        private Int32 _trangThaiHoatDong=0;
        [DisplayName("TrangThaiHoatDong")]
        [JsonProperty("TrangThaiHoatDong", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 TrangThaiHoatDong
        {
            get { return _trangThaiHoatDong; }
            set { _trangThaiHoatDong = value; }
        }
        public TheXe(){}
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
            if (_iDTheXe == null) 
            {
                _res = false; 
                _errorStr = "_IDTHEXE không được bỏ trống!";
            }

            return _res;
        }

        #region Data
        private SQLiteConnection _conn = DBExecute.OpenConnection();
        private SQLiteCommand _cmd=new SQLiteCommand();
        public void Add()
        {
            var _strTheXe = "INSERT INTO TheXe([IDLoaiThe],[IDLoaiXe],[MaTheXe],[SoTheXe],[GhiChu],[TrangThaiThe],[TrangThaiHoatDong]) Values(@IDLoaiThe,@IDLoaiXe,@MaTheXe,@SoTheXe,@GhiChu,@TrangThaiThe,@TrangThaiHoatDong)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strTheXe; 
            _cmd.Parameters.Add("@IDTheXe",DbType.Int32).Value = _iDTheXe;
            _cmd.Parameters.Add("@IDLoaiThe",DbType.Int32).Value = _iDLoaiThe;
            _cmd.Parameters.Add("@IDLoaiXe",DbType.Int32).Value = _iDLoaiXe;
            _cmd.Parameters.Add("@MaTheXe",DbType.Int32).Value = _maTheXe;
            _cmd.Parameters.Add("@SoTheXe",DbType.Int32).Value = _soTheXe;
            _cmd.Parameters.Add("@GhiChu",DbType.String).Value = String.IsNullOrEmpty(_ghiChu) ? "" : _ghiChu;
            _cmd.Parameters.Add("@TrangThaiThe",DbType.Int32).Value = _trangThaiThe;
            _cmd.Parameters.Add("@TrangThaiHoatDong",DbType.Int32).Value = _trangThaiHoatDong;

            _cmd.ExecuteNonQuery();
                        var _cmdID = new SQLiteCommand( " SELECT last_insert_rowid() AS  IDTheXe FROM TheXe", _conn);
            System.Object temp = _cmdID.ExecuteScalar();
            _iDTheXe = Convert.ToInt32(temp);

            _conn.Close();
        }
        public void Edit()
        {
            var _strTheXe = "UPDATE TheXe SET IDLoaiThe = @IDLoaiThe,IDLoaiXe = @IDLoaiXe,MaTheXe = @MaTheXe,SoTheXe = @SoTheXe,GhiChu = @GhiChu,TrangThaiThe = @TrangThaiThe,TrangThaiHoatDong = @TrangThaiHoatDong WHERE (IDTheXe = @IDTheXe)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strTheXe; 
            _cmd.Parameters.Add("@IDTheXe",DbType.Int32).Value = _iDTheXe;
            _cmd.Parameters.Add("@IDLoaiThe",DbType.Int32).Value = _iDLoaiThe;
            _cmd.Parameters.Add("@IDLoaiXe",DbType.Int32).Value = _iDLoaiXe;
            _cmd.Parameters.Add("@MaTheXe",DbType.Int32).Value = _maTheXe;
            _cmd.Parameters.Add("@SoTheXe",DbType.Int32).Value = _soTheXe;
            _cmd.Parameters.Add("@GhiChu",DbType.String).Value = String.IsNullOrEmpty(_ghiChu) ? "" : _ghiChu;
            _cmd.Parameters.Add("@TrangThaiThe",DbType.Int32).Value = _trangThaiThe;
            _cmd.Parameters.Add("@TrangThaiHoatDong",DbType.Int32).Value = _trangThaiHoatDong;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }
        public void Del()
        {
            var _strTheXe = "DELETE FROM TheXe WHERE  (IDTheXe = @IDTheXe)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strTheXe; 
            _cmd.Parameters.Add("@IDTheXe",DbType.Int32).Value = _iDTheXe;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }
        public TheXe Get()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            TheXe _TheXe = null;
            var _strTheXe = "SELECT * FROM TheXe WHERE IDTheXe = "+ _iDTheXe;
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strTheXe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "TheXe");
                    dt = ds.Tables["TheXe"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                DataRow dr = dt.Rows[i];
                _TheXe = new TheXe();
                    _TheXe.IDTheXe=Convert.ToInt32(dr["IDTheXe"]);
                    _TheXe.IDLoaiThe=Convert.ToInt32(dr["IDLoaiThe"]);
                    _TheXe.IDLoaiXe=Convert.ToInt32(dr["IDLoaiXe"]);
                    _TheXe.MaTheXe=Convert.ToInt32(dr["MaTheXe"]);
                    _TheXe.SoTheXe=Convert.ToInt32(dr["SoTheXe"]);
                    _TheXe.GhiChu=Convert.ToString(dr["GhiChu"]);
                    _TheXe.TrangThaiThe=Convert.ToInt32(dr["TrangThaiThe"]);
                    _TheXe.TrangThaiHoatDong=Convert.ToInt32(dr["TrangThaiHoatDong"]);

                }
            _conn.Close();
            return _TheXe;
        }
        public DataTable Get_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strTheXe = "SELECT * FROM TheXe WHERE IDTheXe = "+ _iDTheXe;
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strTheXe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "TheXe");
                    dt = ds.Tables["TheXe"];
                }
            }
            _conn.Close();
            return dt;
        }
        public List<TheXe> Gets()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List < TheXe > _lstTheXe = new List < TheXe >();
            var _strTheXe = "SELECT * FROM TheXe";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strTheXe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "TheXe");
                    dt = ds.Tables["TheXe"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                DataRow dr = dt.Rows[i];
                var _TheXe = new TheXe();
                    _TheXe.IDTheXe=Convert.ToInt32(dr["IDTheXe"]);
                    _TheXe.IDLoaiThe=Convert.ToInt32(dr["IDLoaiThe"]);
                    _TheXe.IDLoaiXe=Convert.ToInt32(dr["IDLoaiXe"]);
                    _TheXe.MaTheXe=Convert.ToInt32(dr["MaTheXe"]);
                    _TheXe.SoTheXe=Convert.ToInt32(dr["SoTheXe"]);
                    _TheXe.GhiChu=Convert.ToString(dr["GhiChu"]);
                    _TheXe.TrangThaiThe=Convert.ToInt32(dr["TrangThaiThe"]);
                    _TheXe.TrangThaiHoatDong=Convert.ToInt32(dr["TrangThaiHoatDong"]);

                _lstTheXe.Add(_TheXe);
                }
            _conn.Close();
            return _lstTheXe;
        }
        public DataTable Gets_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strTheXe = "SELECT * FROM TheXe";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strTheXe, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "TheXe");
                    dt = ds.Tables["TheXe"];
                }
            }
            _conn.Close();
            return dt;
        }
        #endregion
    }
}