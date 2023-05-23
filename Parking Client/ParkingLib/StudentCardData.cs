using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace ParkingLib
{
    public class StudentCardData : ICloneable
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

        private int _studentId = 0;

        [DisplayName("StudentId")]
        [JsonProperty("StudentId", NullValueHandling = NullValueHandling.Ignore)]
        public int StudentId
        {
            get { return _studentId; }
            set { _studentId = value; }
        }

        private int _cardId = 0;

        [DisplayName("CardId")]
        [JsonProperty("CardId", NullValueHandling = NullValueHandling.Ignore)]
        public int CardId
        {
            get { return _cardId; }
            set { _cardId = value; }
        }

        public StudentCardData()
        {
        }

        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Data

        private SqlConnection _conn = Helper.OpenConnection();

        public List<StudentCardData> Gets()
        {
            var dt = new DataTable();
            var ds = new DataSet();
            var lstStudentCardData = new List<StudentCardData>();

            var tenantId = GlobalConfig.TenantId;
            var studentCardDataQuery = "";
            if (tenantId != null)
            {
                studentCardDataQuery =
                    $"SELECT * FROM dbo.Parking_Student_StudentCard studentCard WHERE studentCard.TenantId = {tenantId}";
            }
            else
            {
                studentCardDataQuery =
                    $"SELECT * FROM dbo.Parking_Student_StudentCard studentCard WHERE studentCard.TenantId IS NULL";
            }

            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (var da = new SqlDataAdapter(studentCardDataQuery, _conn))
            {
                using (new SqlCommandBuilder(da))
                {
                    da.Fill(ds, "StudentCardData");
                    dt = ds.Tables["StudentCardData"];
                }
            }

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var studentCardData = new StudentCardData();
                studentCardData.Id = Convert.ToInt32(dr["Id"]);
                studentCardData.StudentId = Convert.ToInt32(dr["StudentId"]);
                studentCardData.CardId = Convert.ToInt32(dr["CardId"]);

                lstStudentCardData.Add(studentCardData);
            }

            _conn.Close();
            return lstStudentCardData;
        }

        #endregion
    }
}