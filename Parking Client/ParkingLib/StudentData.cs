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
        private Int32 _personId=0;
        [DisplayName("PersonId")]
        [JsonProperty("PersonId", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 PersonId
        {
            get { return _personId; }
            set { _personId = value; }
        }
        private String _firstName=String.Empty;
        [DisplayName("FirstName")]
        [JsonProperty("FirstName", NullValueHandling = NullValueHandling.Ignore)]
        public String FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        private String _lastName=String.Empty;
        [DisplayName("LastName")]
        [JsonProperty("LastName", NullValueHandling = NullValueHandling.Ignore)]
        public String LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        private String _peopleId=String.Empty;
        [DisplayName("PeopleId")]
        [JsonProperty("PeopleId", NullValueHandling = NullValueHandling.Ignore)]
        public String PeopleId
        {
            get { return _peopleId; }
            set { _peopleId = value; }
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
        private String _cardDateStr=String.Empty;
        [DisplayName("CardDateStr")]
        [JsonProperty("CardDateStr", NullValueHandling = NullValueHandling.Ignore)]
        public String CardDateStr
        {
            get { return _cardDateStr; }
            set { _cardDateStr = value; }
        }
        private String _className=String.Empty;
        [DisplayName("ClassName")]
        [JsonProperty("ClassName", NullValueHandling = NullValueHandling.Ignore)]
        public String ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        private Int32 _isDeleted=0;
        [DisplayName("IsDeleted")]
        [JsonProperty("IsDeleted", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        public StudentData(){}
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
            if (_id == null) 
            {
                _res = false; 
                _errorStr = "_ID không được bỏ trống!";
            }

            return _res;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Data
        private SQLiteConnection _conn = DBExecute.OpenConnection();
        private SQLiteCommand _cmd = new SQLiteCommand();
        public void Add()
        {
            var _strStudentData = "INSERT INTO StudentData([Id],[Code],[PersonId],[FirstName],[LastName],[PeopleId],[Avatar],[AvatarBase64],[Male],[DoBStr],[CardNumber],[CardDateStr],[ClassName],[IsDeleted]) Values(@Id,@Code,@PersonId,@FirstName,@LastName,@PeopleId,@Avatar,@AvatarBase64,@Male,@DoBStr,@CardNumber,@CardDateStr,@ClassName,@IsDeleted)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strStudentData; 
            _cmd.Parameters.Add("@Id",DbType.Int32).Value = _id;
            _cmd.Parameters.Add("@Code",DbType.String).Value = String.IsNullOrEmpty(_code) ? "" : _code;
            _cmd.Parameters.Add("@PersonId",DbType.Int32).Value = _personId;
            _cmd.Parameters.Add("@FirstName",DbType.String).Value = String.IsNullOrEmpty(_firstName) ? "" : _firstName;
            _cmd.Parameters.Add("@LastName",DbType.String).Value = String.IsNullOrEmpty(_lastName) ? "" : _lastName;
            _cmd.Parameters.Add("@PeopleId",DbType.String).Value = String.IsNullOrEmpty(_peopleId) ? "" : _peopleId;
            _cmd.Parameters.Add("@Avatar",DbType.String).Value = String.IsNullOrEmpty(_avatar) ? "" : _avatar;
            _cmd.Parameters.Add("@AvatarBase64",DbType.String).Value = String.IsNullOrEmpty(_avatarBase64) ? "" : _avatarBase64;
            _cmd.Parameters.Add("@Male",DbType.Int32).Value = _male;
            _cmd.Parameters.Add("@DoBStr",DbType.String).Value = String.IsNullOrEmpty(_doBStr) ? "" : _doBStr;
            _cmd.Parameters.Add("@CardNumber",DbType.String).Value = String.IsNullOrEmpty(_cardNumber) ? "" : _cardNumber;
            _cmd.Parameters.Add("@CardDateStr",DbType.String).Value = String.IsNullOrEmpty(_cardDateStr) ? "" : _cardDateStr;
            _cmd.Parameters.Add("@ClassName",DbType.String).Value = String.IsNullOrEmpty(_className) ? "" : _className;
            _cmd.Parameters.Add("@IsDeleted",DbType.Int32).Value = _isDeleted;

            _cmd.ExecuteNonQuery();
                        var _cmdID = new SQLiteCommand( " SELECT last_insert_rowid() AS  Id FROM StudentData", _conn);
            System.Object temp = _cmdID.ExecuteScalar();
            _id = Convert.ToInt32(temp);

            _conn.Close();
        }

        public void Edit()
        {
            var _strStudentData = "UPDATE StudentData SET Code = @Code,PersonId = @PersonId,FirstName = @FirstName,LastName = @LastName,PeopleId = @PeopleId,Avatar = @Avatar,AvatarBase64 = @AvatarBase64,Male = @Male,DoBStr = @DoBStr,CardNumber = @CardNumber,CardDateStr = @CardDateStr,ClassName = @ClassName,IsDeleted = @IsDeleted WHERE (Id = @Id)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strStudentData; 
            _cmd.Parameters.Add("@Id",DbType.Int32).Value = _id;
            _cmd.Parameters.Add("@Code",DbType.String).Value = String.IsNullOrEmpty(_code) ? "" : _code;
            _cmd.Parameters.Add("@PersonId",DbType.Int32).Value = _personId;
            _cmd.Parameters.Add("@FirstName",DbType.String).Value = String.IsNullOrEmpty(_firstName) ? "" : _firstName;
            _cmd.Parameters.Add("@LastName",DbType.String).Value = String.IsNullOrEmpty(_lastName) ? "" : _lastName;
            _cmd.Parameters.Add("@PeopleId",DbType.String).Value = String.IsNullOrEmpty(_peopleId) ? "" : _peopleId;
            _cmd.Parameters.Add("@Avatar",DbType.String).Value = String.IsNullOrEmpty(_avatar) ? "" : _avatar;
            _cmd.Parameters.Add("@AvatarBase64",DbType.String).Value = String.IsNullOrEmpty(_avatarBase64) ? "" : _avatarBase64;
            _cmd.Parameters.Add("@Male",DbType.Int32).Value = _male;
            _cmd.Parameters.Add("@DoBStr",DbType.String).Value = String.IsNullOrEmpty(_doBStr) ? "" : _doBStr;
            _cmd.Parameters.Add("@CardNumber",DbType.String).Value = String.IsNullOrEmpty(_cardNumber) ? "" : _cardNumber;
            _cmd.Parameters.Add("@CardDateStr",DbType.String).Value = String.IsNullOrEmpty(_cardDateStr) ? "" : _cardDateStr;
            _cmd.Parameters.Add("@ClassName",DbType.String).Value = String.IsNullOrEmpty(_className) ? "" : _className;
            _cmd.Parameters.Add("@IsDeleted",DbType.Int32).Value = _isDeleted;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public void Del()
        {
            var _strStudentData = "DELETE FROM StudentData WHERE  (Id = @Id)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strStudentData; 
            _cmd.Parameters.Add("@Id",DbType.Int32).Value = _id;

            _cmd.ExecuteNonQuery();
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

        public StudentData Get()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            StudentData _StudentData = null;
            var _strStudentData = "SELECT * FROM StudentData WHERE Id = "+ _id;
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
                _StudentData = new StudentData();
                    _StudentData.Id=Convert.ToInt32(dr["Id"]);
                    _StudentData.Code=Convert.ToString(dr["Code"]);
                    _StudentData.PersonId=Convert.ToInt32(dr["PersonId"]);
                    _StudentData.FirstName=Convert.ToString(dr["FirstName"]);
                    _StudentData.LastName=Convert.ToString(dr["LastName"]);
                    _StudentData.PeopleId=Convert.ToString(dr["PeopleId"]);
                    _StudentData.Avatar=Convert.ToString(dr["Avatar"]);
                    _StudentData.AvatarBase64=Convert.ToString(dr["AvatarBase64"]);
                    _StudentData.Male=Convert.ToInt32(dr["Male"]);
                    _StudentData.DoBStr=Convert.ToString(dr["DoBStr"]);
                    _StudentData.CardNumber=Convert.ToString(dr["CardNumber"]);
                    _StudentData.CardDateStr=Convert.ToString(dr["CardDateStr"]);
                    _StudentData.ClassName=Convert.ToString(dr["ClassName"]);
                    _StudentData.IsDeleted=Convert.ToInt32(dr["IsDeleted"]);

                }
            _conn.Close();
            return _StudentData;
        }

        public DataTable Get_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strStudentData = "SELECT * FROM StudentData WHERE Id = "+ _id;
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strStudentData, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "StudentData");
                    dt = ds.Tables["StudentData"];
                }
            }
            _conn.Close();
            return dt;
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
                    _StudentData.PersonId=Convert.ToInt32(dr["PersonId"]);
                    _StudentData.FirstName=Convert.ToString(dr["FirstName"]);
                    _StudentData.LastName=Convert.ToString(dr["LastName"]);
                    _StudentData.PeopleId=Convert.ToString(dr["PeopleId"]);
                    _StudentData.Avatar=Convert.ToString(dr["Avatar"]);
                    _StudentData.AvatarBase64=Convert.ToString(dr["AvatarBase64"]);
                    _StudentData.Male=Convert.ToInt32(dr["Male"]);
                    _StudentData.DoBStr=Convert.ToString(dr["DoBStr"]);
                    _StudentData.CardNumber=Convert.ToString(dr["CardNumber"]);
                    _StudentData.CardDateStr=Convert.ToString(dr["CardDateStr"]);
                    _StudentData.ClassName=Convert.ToString(dr["ClassName"]);
                    _StudentData.IsDeleted=Convert.ToInt32(dr["IsDeleted"]);

                _lstStudentData.Add(_StudentData);
                }
            _conn.Close();
            return _lstStudentData;
        }

        public DataTable Gets_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
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
            _conn.Close();
            return dt;
        }

        #endregion
    }
}