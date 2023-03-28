using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Data.SQLite;
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
        private Int32 _parentStatus = 0;
        [DisplayName("ParentStatus")]
        [JsonProperty("ParentStatus", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 ParentStatus
        {
            get { return _parentStatus; }
            set { _parentStatus = value; }
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
        
        // Extend
        public DateTime SecurityDate { get; set; }
        public StudentData StudentInfo { get; set; }

        #region Data
        private SQLiteConnection _conn = DBExecute.OpenConnection();
        private SQLiteCommand _cmd = new SQLiteCommand();
        public void Add()
        {
            var _strSecurityData = "INSERT INTO SecurityData([StudentId],[CardNumber],[CamCode],[ParentStatus],[SecurityDateStr],[PhotoBase64],[PhotoUrl],[Status]) Values(@StudentId,@CardNumber,@CamCode,@ParentStatus,@SecurityDateStr,@PhotoBase64,@PhotoUrl,@Status)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SQLiteCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strSecurityData;
            _cmd.Parameters.Add("@Id", DbType.Int32).Value = _id;
            _cmd.Parameters.Add("@StudentId", DbType.Int32).Value = _studentId;
            _cmd.Parameters.Add("@CardNumber", DbType.String).Value = String.IsNullOrEmpty(_cardNumber) ? "" : _cardNumber;
            _cmd.Parameters.Add("@ParentStatus", DbType.Int32).Value = _parentStatus;
            _cmd.Parameters.Add("@CamCode", DbType.String).Value = String.IsNullOrEmpty(_camCode) ? "" : _camCode;
            _cmd.Parameters.Add("@SecurityDateStr", DbType.String).Value = String.IsNullOrEmpty(_securityDateStr) ? "" : _securityDateStr;
            _cmd.Parameters.Add("@PhotoBase64", DbType.String).Value = String.IsNullOrEmpty(_photoBase64) ? "" : _photoBase64;
            _cmd.Parameters.Add("@PhotoUrl", DbType.String).Value = String.IsNullOrEmpty(_photoUrl) ? "" : _photoUrl;
            _cmd.Parameters.Add("@Status", DbType.Int32).Value = 0;

            _cmd.ExecuteNonQuery();
            var _cmdID = new SQLiteCommand(" SELECT last_insert_rowid() AS  Id FROM SecurityData", _conn);
            System.Object temp = _cmdID.ExecuteScalar();
            _id = Convert.ToInt32(temp);

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
                _SecurityData.StudentId = Convert.ToInt32(dr["StudentId"]);
                _SecurityData.CardNumber = Convert.ToString(dr["CardNumber"]);
                _SecurityData.ParentStatus = Convert.ToInt32(dr["ParentStatus"]);
                _SecurityData.CamCode = Convert.ToString(dr["CamCode"]);
                _SecurityData.SecurityDateStr = Convert.ToString(dr["SecurityDateStr"]);
                _SecurityData.PhotoBase64 = Convert.ToString(dr["PhotoBase64"]);
                _SecurityData.PhotoUrl = Convert.ToString(dr["PhotoUrl"]);
                _SecurityData.Status = Convert.ToInt32(dr["Status"]);

                _lstSecurityData.Add(_SecurityData);
            }
            _conn.Close();
            return _lstSecurityData;
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
                _SecurityData.StudentId = Convert.ToInt32(dr["StudentId"]);
                _SecurityData.CardNumber = Convert.ToString(dr["CardNumber"]);
                _SecurityData.ParentStatus = Convert.ToInt32(dr["ParentStatus"]);
                _SecurityData.CamCode = Convert.ToString(dr["CamCode"]);
                _SecurityData.SecurityDateStr = Convert.ToString(dr["SecurityDateStr"]);
                _SecurityData.SecurityDate = DateTime.ParseExact(_SecurityData.SecurityDateStr, "dd/MM/yyyy HH:mm:ss", culture);
                _SecurityData.PhotoBase64 = Convert.ToString(dr["PhotoBase64"]);
                _SecurityData.PhotoUrl = Convert.ToString(dr["PhotoUrl"]);
                _SecurityData.Status = Convert.ToInt32(dr["Status"]);

                _lstSecurityData.Add(_SecurityData);
            }
            _conn.Close();
            return _lstSecurityData;
        }
        #endregion
    }
}