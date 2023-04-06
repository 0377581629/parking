using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data.SQLite;
using Newtonsoft.Json;
using System.Globalization;
using SyncDataModels;

namespace ParkingLib
{
    public class HistoryData
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

        private Int32 _cardId = 0;

        [DisplayName("CardId")]
        [JsonProperty("CardId", NullValueHandling = NullValueHandling.Ignore)]
        public Int32 CardId
        {
            get { return _cardId; }
            set { _cardId = value; }
        }

        private String _cardCode = String.Empty;

        [DisplayName("CardCode")]
        [JsonProperty("CardCode", NullValueHandling = NullValueHandling.Ignore)]
        public String CardCode
        {
            get { return _cardCode; }
            set { _cardCode = value; }
        }

        private String _cardNumber = String.Empty;

        [DisplayName("CardNumber")]
        [JsonProperty("CardNumber", NullValueHandling = NullValueHandling.Ignore)]
        public String CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }

        private String _licensePlate = String.Empty;

        [DisplayName("LicensePlate")]
        [JsonProperty("LicensePlate", NullValueHandling = NullValueHandling.Ignore)]
        public String LicensePlate
        {
            get { return _licensePlate; }
            set { _licensePlate = value; }
        }

        private double _price = 0;

        [DisplayName("Price")]
        [JsonProperty("Price", NullValueHandling = NullValueHandling.Ignore)]
        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private DateTime _time = DateTime.Today;

        [DisplayName("Time")]
        [JsonProperty("Time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        private int _type = 0;

        [DisplayName("Type")]
        [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private String _photo = String.Empty;

        [DisplayName("Photo")]
        [JsonProperty("Photo", NullValueHandling = NullValueHandling.Ignore)]
        public String Photo
        {
            get { return _photo; }
            set { _photo = value; }
        }

        private String _cardTypeName = String.Empty;

        [DisplayName("CardTypeName")]
        [JsonProperty("CardTypeName", NullValueHandling = NullValueHandling.Ignore)]
        public String CardTypeName
        {
            get { return _cardTypeName; }
            set { _cardTypeName = value; }
        }

        private String _vehicleTypeName = String.Empty;

        [DisplayName("VehicleTypeName")]
        [JsonProperty("VehicleTypeName", NullValueHandling = NullValueHandling.Ignore)]
        public String VehicleTypeName
        {
            get { return _vehicleTypeName; }
            set { _vehicleTypeName = value; }
        }

        public HistoryData()
        {
        }

        #endregion

        #region Extend

        public StudentData StudentData { get; set; }

        #endregion

        #region Data

        private SqlConnection _conn = Helper.OpenConnection();
        private SqlCommand _cmd = new SqlCommand();
        private readonly int _tenantId = int.Parse(GlobalConfig.TenantId);

        public void Add()
        {
            var _strSecurityData =
                "INSERT INTO SecurityData([TenantId],[CardId],[CardCode],[CardNumber],[LicensePlate],[Price],[Time],[Type],[Photo],[CardTypeName],[VehicleTypeName]) Values(@TenantId,@CardId,@CardCode,@CardNumber,@LicensePlate,@Price,@Time,@Type,@Photo,@CardTypeName,@VehicleTypeName)";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strSecurityData;

            _cmd.Parameters.Add("@Id", DbType.Int32).Value = _id;
            _cmd.Parameters.Add("@TenantId", DbType.Int32).Value = _tenantId;
            _cmd.Parameters.Add("@CardId", DbType.Int32).Value = _cardId;
            _cmd.Parameters.Add("@CardCode", DbType.String).Value = string.IsNullOrEmpty(_cardCode) ? "" : _cardCode;
            _cmd.Parameters.Add("@CardNumber", DbType.String).Value =
                string.IsNullOrEmpty(_cardNumber) ? "" : _cardNumber;
            _cmd.Parameters.Add("@LicensePlate", DbType.String).Value =
                string.IsNullOrEmpty(_licensePlate) ? "" : _licensePlate;
            _cmd.Parameters.Add("@Price", DbType.Double).Value = _price;
            _cmd.Parameters.Add("@Time", DbType.DateTime).Value = _time;
            _cmd.Parameters.Add("@Type", DbType.Int32).Value = _type;
            _cmd.Parameters.Add("@Photo", DbType.String).Value = string.IsNullOrEmpty(_photo) ? "" : _photo;
            _cmd.Parameters.Add("@CardTypeName", DbType.String).Value =
                string.IsNullOrEmpty(_cardTypeName) ? "" : _cardTypeName;
            _cmd.Parameters.Add("@VehicleTypeName", DbType.String).Value =
                string.IsNullOrEmpty(_vehicleTypeName) ? "" : _vehicleTypeName;

            _cmd.ExecuteNonQuery();
            var _cmdID = new SqlCommand(" SELECT last_insert_rowid() AS  Id FROM SecurityData", _conn);
            var temp = _cmdID.ExecuteScalar();
            _id = Convert.ToInt32(temp);

            _conn.Close();
        }

        public List<HistoryData> Gets()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List<HistoryData> lstHistoryData = new List<HistoryData>();
            var tenantId = GlobalConfig.TenantId;
            var historyDataQuery =
                $"SELECT history.Id, history.CardId, card.Code,card.CardNumber,history.LicensePlate,history.Price,history.Time,history.Type,history.Photo, cardType.Name,vehicleType.Name FROM dbo.Park_History history JOIN dbo.Park_Card_Card card ON card.Id = history.CardId JOIN dbo.Park_Card_CardType cardType ON cardType.Id = card.CardTypeId JOIN dbo.Park_Vehicle_VehicleType vehicleType ON vehicleType.Id = card.VehicleTypeId WHERE history.TenantId = {tenantId}";
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (var da = new SqlDataAdapter(historyDataQuery, _conn))
            {
                using (new SqlCommandBuilder(da))
                {
                    da.Fill(ds, "HistoryData");
                    dt = ds.Tables["HistoryData"];
                }
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                var historyData = new HistoryData();
                historyData.Id = Convert.ToInt32(dr["Id"]);
                historyData.CardId = Convert.ToInt32(dr["CardId"]);
                historyData.CardCode = Convert.ToString(dr["CardCode"]);
                historyData.CardNumber = Convert.ToString(dr["CardNumber"]);
                historyData.LicensePlate = Convert.ToString(dr["LicensePlate"]);
                historyData.Price = Convert.ToDouble(dr["Price"]);
                historyData.Time = Convert.ToDateTime(dr["Time"]);
                historyData.Type = Convert.ToInt32(dr["Type"]);
                historyData.Photo = Convert.ToString(dr["Photo"]);
                historyData.CardTypeName = Convert.ToString(dr["CardTypeName"]);
                historyData.VehicleTypeName = Convert.ToString(dr["VehicleTypeName"]);

                lstHistoryData.Add(historyData);
            }

            _conn.Close();
            return lstHistoryData;
        }

        #endregion
    }
}