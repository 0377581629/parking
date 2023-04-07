using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using Newtonsoft.Json;
using SyncDataModels;

namespace ParkingLib
{
    public class StudentData : ICloneable
    {
        #region Structures

        private int _id = 0;

        [DisplayName("Id")]
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _code = string.Empty;

        [DisplayName("Code")]
        [JsonProperty("Code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _name = string.Empty;

        [DisplayName("Name")]
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _phoneNumber = string.Empty;

        [DisplayName("PhoneNumber")]
        [JsonProperty("PhoneNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        private string _avatar = string.Empty;

        [DisplayName("Avatar")]
        [JsonProperty("Avatar", NullValueHandling = NullValueHandling.Ignore)]
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
        
        private string _email = string.Empty;

        [DisplayName("Email")]
        [JsonProperty("Email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private bool _gender = true;

        [DisplayName("Gender")]
        [JsonProperty("Gender", NullValueHandling = NullValueHandling.Ignore)]
        public bool Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        private DateTime _dob = DateTime.Today;

        [DisplayName("Dob")]
        [JsonProperty("Dob", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Dob
        {
            get { return _dob; }
            set { _dob = value; }
        }
        
        private bool _isActive = true;

        [DisplayName("IsActive")]
        [JsonProperty("IsActive", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public StudentData()
        {
        }

        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Data

        private SqlConnection _conn = Helper.OpenConnection();

        public List<StudentData> Gets()
        {
            var dt = new DataTable();
            var ds = new DataSet();
            var lstStudentData = new List<StudentData>();
            var tenantId = GlobalConfig.TenantId;
            var studentDataQuery = $"SELECT * FROM dbo.Parking_Student_Student student WHERE student.TenantId = {tenantId}";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (var da = new SqlDataAdapter(studentDataQuery, _conn))
            {
                using (new SqlCommandBuilder(da))
                {
                    da.Fill(ds, "StudentData");
                    dt = ds.Tables["StudentData"];
                }
            }

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var studentData = new StudentData();
                studentData.Id = Convert.ToInt32(dr["Id"]);
                studentData.Code = Convert.ToString(dr["Code"]);
                studentData.Name = Convert.ToString(dr["Name"]);
                studentData.PhoneNumber = Convert.ToString(dr["PhoneNumber"]);
                studentData.Avatar = $"{GlobalConfig.TargetDomain}{Convert.ToString(dr["Avatar"])}";
                studentData.Email = Convert.ToString(dr["Email"]);
                studentData.Gender = Convert.ToBoolean(dr["Gender"]);
                studentData.Dob = Convert.ToDateTime(dr["Dob"]);
                studentData.IsActive = Convert.ToBoolean(dr["IsActive"]);

                lstStudentData.Add(studentData);
            }

            _conn.Close();
            return lstStudentData;
        }

        #endregion
    }
}