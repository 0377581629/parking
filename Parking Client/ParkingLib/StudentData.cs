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
    public class StudentData : ICloneable
    {
        #region Structures
        private Int32 _id=0;
        [DisplayName("Id")]
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private String _code=String.Empty;
        [DisplayName("Code")]
        [JsonProperty("Code", NullValueHandling = NullValueHandling.Ignore)]
        public String Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private String _name=String.Empty;
        [DisplayName("Name")]
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        private String _avatar=String.Empty;
        [DisplayName("Avatar")]
        [JsonProperty("Avatar", NullValueHandling = NullValueHandling.Ignore)]
        public String Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
        private String _avatarBase64=String.Empty;
        [DisplayName("AvatarBase64")]
        [JsonProperty("AvatarBase64", NullValueHandling = NullValueHandling.Ignore)]
        public String AvatarBase64
        {
            get { return _avatarBase64; }
            set { _avatarBase64 = value; }
        }
        private Int32 _male=0;
        [DisplayName("Male")]
        [JsonProperty("Male", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 Male
        {
            get { return _male; }
            set { _male = value; }
        }
        private String _doBStr=String.Empty;
        [DisplayName("DoBStr")]
        [JsonProperty("DoBStr", NullValueHandling = NullValueHandling.Ignore)]
        public String DoBStr
        {
            get { return _doBStr; }
            set { _doBStr = value; }
        }
        private String _cardNumber=String.Empty;
        [DisplayName("CardNumber")]
        [JsonProperty("CardNumber", NullValueHandling = NullValueHandling.Ignore)]
        public String CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }
        
        public StudentData(){}
        #endregion
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Data
        private SQLiteConnection _conn = DBExecute.OpenConnection();
        private SQLiteCommand _cmd = new SQLiteCommand();
        public void Add()
        {
            var _strStudentData = "INSERT INTO StudentData([Id],[Code],[Name],[Avatar],[AvatarBase64],[Male],[DoBStr],[CardNumber]) Values(@Id,@Code,@Name,@Avatar,@AvatarBase64,@Male,@DoBStr,@CardNumber)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strStudentData; 
            _cmd.Parameters.Add("@Id",DbType.Int32).Value = _id;
            _cmd.Parameters.Add("@Code",DbType.String).Value = String.IsNullOrEmpty(_code) ? "" : _code;
            _cmd.Parameters.Add("@Name",DbType.String).Value = String.IsNullOrEmpty(_name) ? "" : _name;
            _cmd.Parameters.Add("@Avatar",DbType.String).Value = String.IsNullOrEmpty(_avatar) ? "" : _avatar;
            _cmd.Parameters.Add("@AvatarBase64",DbType.String).Value = String.IsNullOrEmpty(_avatarBase64) ? "" : _avatarBase64;
            _cmd.Parameters.Add("@Male",DbType.Int32).Value = _male;
            _cmd.Parameters.Add("@DoBStr",DbType.String).Value = String.IsNullOrEmpty(_doBStr) ? "" : _doBStr;
            _cmd.Parameters.Add("@CardNumber",DbType.String).Value = String.IsNullOrEmpty(_cardNumber) ? "" : _cardNumber;

            _cmd.ExecuteNonQuery();
                        var _cmdID = new SQLiteCommand( " SELECT last_insert_rowid() AS  Id FROM StudentData", _conn);
            System.Object temp = _cmdID.ExecuteScalar();
            _id = Convert.ToInt32(temp);

            _conn.Close();
        }

        public void DelAll()
        {
            var _strStudentData = "DELETE FROM StudentData";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strStudentData;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public List<StudentData> Gets()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List < StudentData > _lstStudentData = new List < StudentData >();
            var _strStudentData = "SELECT * FROM StudentData";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strStudentData, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "StudentData");
                    dt = ds.Tables["StudentData"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
                {
                DataRow dr = dt.Rows[i];
                var _StudentData = new StudentData();
                    _StudentData.Id=Convert.ToInt32(dr["Id"]);
                    _StudentData.Code=Convert.ToString(dr["Code"]);
                    _StudentData.Name=Convert.ToString(dr["Name"]);
                    _StudentData.Avatar=Convert.ToString(dr["Avatar"]);
                    _StudentData.AvatarBase64=Convert.ToString(dr["AvatarBase64"]);
                    _StudentData.Male=Convert.ToInt32(dr["Male"]);
                    _StudentData.DoBStr=Convert.ToString(dr["DoBStr"]);
                    _StudentData.CardNumber=Convert.ToString(dr["CardNumber"]);

                _lstStudentData.Add(_StudentData);
                }
            _conn.Close();
            return _lstStudentData;
        }

        #endregion
    }
}