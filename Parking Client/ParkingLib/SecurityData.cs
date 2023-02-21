using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Data.SQLite;
//using Dapper;
using System.Linq;
using DataAccess;
using System.Globalization;

namespace ParkingLib
{
    public class SecurityData
    {
        #region Structures
        private Int32 _id = 0;
        [DisplayName("Id")]
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private Int32 _personId = 0;
        [DisplayName("PersonId")]
        [JsonProperty("PersonId", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 PersonId
        {
            get { return _personId; }
            set { _personId = value; }
        }
        private Int32 _studentId = 0;
        [DisplayName("StudentId")]
        [JsonProperty("StudentId", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 StudentId
        {
            get { return _studentId; }
            set { _studentId = value; }
        }
        private String _cardNumber = String.Empty;
        [DisplayName("CardNumber")]
        [JsonProperty("CardNumber", NullValueHandling = NullValueHandling.Ignore)]
        public String CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }
        private String _camCode = String.Empty;
        [DisplayName("CamCode")]
        [JsonProperty("CamCode", NullValueHandling = NullValueHandling.Ignore)]
        public String CamCode
        {
            get { return _camCode; }
            set { _camCode = value; }
        }
        private Int32 _parentId = 0;
        [DisplayName("ParentId")]
        [JsonProperty("ParentId", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }
        private String _securityDateStr = String.Empty;
        [DisplayName("SecurityDateStr")]
        [JsonProperty("SecurityDateStr", NullValueHandling = NullValueHandling.Ignore)]
        public String SecurityDateStr
        {
            get { return _securityDateStr; }
            set { _securityDateStr = value; }
        }
        private String _photoBase64 = String.Empty;
        [DisplayName("PhotoBase64")]
        [JsonProperty("PhotoBase64", NullValueHandling = NullValueHandling.Ignore)]
        public String PhotoBase64
        {
            get { return _photoBase64; }
            set { _photoBase64 = value; }
        }
        private String _photoUrl = String.Empty;
        [DisplayName("PhotoUrl")]
        [JsonProperty("PhotoUrl", NullValueHandling = NullValueHandling.Ignore)]
        public String PhotoUrl
        {
            get { return _photoUrl; }
            set { _photoUrl = value; }
        }
        private Int32 _isDeleted = 0;
        [DisplayName("IsDeleted")]
        [JsonProperty("IsDeleted", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        private Int32 _status = 0;
        [DisplayName("Status")]
        [JsonProperty("Status", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public SecurityData() { }
        #endregion

        private String _errorStr = String.Empty;
        public String ErrorStr
        {
            get { return _errorStr; }
            set { _errorStr = value; }
        }
        public bool Valid()
        {
            bool _res = true;
            if (_id == null)
            {
                _res = false;
                _errorStr = "_ID không được bỏ trống!";
            }

            return _res;
        }
        // Extend
        public DateTime SecurityDate { get; set; }
        public StudentData StudentInfo { get; set; }

        #region Data
        private SQLiteConnection _conn = DBExecute.OpenConnection();
        private SQLiteCommand _cmd = new SQLiteCommand();
        public void Add()
        {
            var _strSecurityData = "INSERT INTO SecurityData([PersonId],[StudentId],[CardNumber],[CamCode],[ParentId],[SecurityDateStr],[PhotoBase64],[PhotoUrl],[Status],[IsDeleted]) Values(@PersonId,@StudentId,@CardNumber,@CamCode,@ParentId,@SecurityDateStr,@PhotoBase64,@PhotoUrl,@Status,@IsDeleted)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strSecurityData;
            _cmd.Parameters.Add("@Id", DbType.Int32).Value = _id;
            _cmd.Parameters.Add("@PersonId", DbType.Int32).Value = _personId;
            _cmd.Parameters.Add("@StudentId", DbType.Int32).Value = _studentId;
            _cmd.Parameters.Add("@CardNumber", DbType.String).Value = String.IsNullOrEmpty(_cardNumber) ? "" : _cardNumber;
            _cmd.Parameters.Add("@ParentId", DbType.Int32).Value = _parentId;
            _cmd.Parameters.Add("@CamCode", DbType.String).Value = String.IsNullOrEmpty(_camCode) ? "" : _camCode;
            _cmd.Parameters.Add("@SecurityDateStr", DbType.String).Value = String.IsNullOrEmpty(_securityDateStr) ? "" : _securityDateStr;
            _cmd.Parameters.Add("@PhotoBase64", DbType.String).Value = String.IsNullOrEmpty(_photoBase64) ? "" : _photoBase64;
            _cmd.Parameters.Add("@PhotoUrl", DbType.String).Value = String.IsNullOrEmpty(_photoUrl) ? "" : _photoUrl;
            _cmd.Parameters.Add("@Status", DbType.Int32).Value = 0;
            _cmd.Parameters.Add("@IsDeleted", DbType.Int32).Value = _isDeleted;

            _cmd.ExecuteNonQuery();
            var _cmdID = new SQLiteCommand(" SELECT last_insert_rowid() AS  Id FROM SecurityData", _conn);
            System.Object temp = _cmdID.ExecuteScalar();
            _id = Convert.ToInt32(temp);

            _conn.Close();
        }

        public void Edit()
        {
            var _strSecurityData = "UPDATE SecurityData SET PersonId = @PersonId,StudentId = @StudentId,CardNumber = @CardNumber,CamCode = @CamCode,ParentId = @ParentId,SecurityDateStr = @SecurityDateStr,PhotoBase64 = @PhotoBase64,PhotoUrl = @PhotoUrl,IsDeleted = @IsDeleted WHERE (Id = @Id)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strSecurityData;
            _cmd.Parameters.Add("@Id", DbType.Int32).Value = _id;
            _cmd.Parameters.Add("@PersonId", DbType.Int32).Value = _personId;
            _cmd.Parameters.Add("@StudentId", DbType.Int32).Value = _studentId;
            _cmd.Parameters.Add("@CardNumber", DbType.String).Value = String.IsNullOrEmpty(_cardNumber) ? "" : _cardNumber;
            _cmd.Parameters.Add("@ParentId", DbType.Int32).Value = _parentId;
            _cmd.Parameters.Add("@CamCode", DbType.String).Value = String.IsNullOrEmpty(_camCode) ? "" : _camCode;
            _cmd.Parameters.Add("@SecurityDateStr", DbType.String).Value = String.IsNullOrEmpty(_securityDateStr) ? "" : _securityDateStr;
            _cmd.Parameters.Add("@PhotoBase64", DbType.String).Value = String.IsNullOrEmpty(_photoBase64) ? "" : _photoBase64;
            _cmd.Parameters.Add("@PhotoUrl", DbType.String).Value = String.IsNullOrEmpty(_photoUrl) ? "" : _photoUrl;
            _cmd.Parameters.Add("@Status", DbType.Int32).Value = 0;
            _cmd.Parameters.Add("@IsDeleted", DbType.Int32).Value = _isDeleted;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public void Del()
        {
            var _strSecurityData = "DELETE FROM SecurityData WHERE  (Id = @Id)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strSecurityData;
            _cmd.Parameters.Add("@Id", DbType.Int32).Value = _id;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public bool UpdateStatus(List<int> lstID, int status)
        {
            try
            {
                var strId = string.Join(",", lstID);
                var _strSecurityData = "UPDATE SecurityData SET Status = " + status + " WHERE  Id IN (" + strId + ")";
                if (_conn.State == ConnectionState.Closed) _conn.Open();
                _cmd = new SQLiteCommand();
                _cmd.Connection = _conn;
                _cmd.CommandText = _strSecurityData;
                _cmd.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void DelAll()
        {
            var _strSecurityData = "DELETE FROM SecurityData";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strSecurityData;

            _cmd.ExecuteNonQuery();
            _conn.Close();
        }

        public SecurityData Get()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SecurityData _SecurityData = null;
            var _strSecurityData = "SELECT * FROM SecurityData WHERE Id = " + _id;
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strSecurityData, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "SecurityData");
                    dt = ds.Tables["SecurityData"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                _SecurityData = new SecurityData();
                _SecurityData.Id = Convert.ToInt32(dr["Id"]);
                _SecurityData.PersonId = Convert.ToInt32(dr["PersonId"]);
                _SecurityData.StudentId = Convert.ToInt32(dr["StudentId"]);
                _SecurityData.CardNumber = Convert.ToString(dr["CardNumber"]);
                _SecurityData.ParentId = Convert.ToInt32(dr["ParentId"]);
                _SecurityData.CamCode = Convert.ToString(dr["CamCode"]);
                _SecurityData.SecurityDateStr = Convert.ToString(dr["SecurityDateStr"]);
                _SecurityData.PhotoBase64 = Convert.ToString(dr["PhotoBase64"]);
                _SecurityData.PhotoUrl = Convert.ToString(dr["PhotoUrl"]);
                _SecurityData.Status = Convert.ToInt32(dr["Status"]);
                _SecurityData.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

            }
            _conn.Close();
            return _SecurityData;
        }

        public DataTable Get_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strSecurityData = "SELECT * FROM SecurityData WHERE Id = " + _id;
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strSecurityData, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "SecurityData");
                    dt = ds.Tables["SecurityData"];
                }
            }
            _conn.Close();
            return dt;
        }

        public List<SecurityData> Gets()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List<SecurityData> _lstSecurityData = new List<SecurityData>();
            var _strSecurityData = "SELECT * FROM SecurityData";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strSecurityData, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "SecurityData");
                    dt = ds.Tables["SecurityData"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                var _SecurityData = new SecurityData();
                _SecurityData.Id = Convert.ToInt32(dr["Id"]);
                _SecurityData.PersonId = Convert.ToInt32(dr["PersonId"]);
                _SecurityData.StudentId = Convert.ToInt32(dr["StudentId"]);
                _SecurityData.CardNumber = Convert.ToString(dr["CardNumber"]);
                _SecurityData.ParentId = Convert.ToInt32(dr["ParentId"]);
                _SecurityData.CamCode = Convert.ToString(dr["CamCode"]);
                _SecurityData.SecurityDateStr = Convert.ToString(dr["SecurityDateStr"]);
                _SecurityData.PhotoBase64 = Convert.ToString(dr["PhotoBase64"]);
                _SecurityData.PhotoUrl = Convert.ToString(dr["PhotoUrl"]);
                _SecurityData.Status = Convert.ToInt32(dr["Status"]);
                _SecurityData.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

                _lstSecurityData.Add(_SecurityData);
            }
            _conn.Close();
            return _lstSecurityData;
        }

        public DataTable Gets_Table()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var _strSecurityData = "SELECT * FROM SecurityData";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strSecurityData, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "SecurityData");
                    dt = ds.Tables["SecurityData"];
                }
            }
            _conn.Close();
            return dt;
        }

        public List<SecurityData> GetByCardNumbers()
        {
            IFormatProvider culture = new CultureInfo("en-US", true);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List<SecurityData> _lstSecurityData = new List<SecurityData>();
            var _strSecurityData = "SELECT * FROM SecurityData WHERE CardNumber = '" + _cardNumber + "'";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (SQLiteDataAdapter da = new SQLiteDataAdapter(_strSecurityData, _conn))
            {
                using (new SQLiteCommandBuilder(da))
                {
                    da.Fill(ds, "SecurityData");
                    dt = ds.Tables["SecurityData"];
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                var _SecurityData = new SecurityData();
                _SecurityData.Id = Convert.ToInt32(dr["Id"]);
                _SecurityData.PersonId = Convert.ToInt32(dr["PersonId"]);
                _SecurityData.StudentId = Convert.ToInt32(dr["StudentId"]);
                _SecurityData.CardNumber = Convert.ToString(dr["CardNumber"]);
                _SecurityData.ParentId = Convert.ToInt32(dr["ParentId"]);
                _SecurityData.CamCode = Convert.ToString(dr["CamCode"]);
                _SecurityData.SecurityDateStr = Convert.ToString(dr["SecurityDateStr"]);
                _SecurityData.SecurityDate = DateTime.ParseExact(_SecurityData.SecurityDateStr, "dd/MM/yyyy HH:mm:ss", culture);
                _SecurityData.PhotoBase64 = Convert.ToString(dr["PhotoBase64"]);
                _SecurityData.PhotoUrl = Convert.ToString(dr["PhotoUrl"]);
                _SecurityData.Status = Convert.ToInt32(dr["Status"]);
                _SecurityData.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

                _lstSecurityData.Add(_SecurityData);
            }
            _conn.Close();
            return _lstSecurityData;
        }
        #endregion
    }
}