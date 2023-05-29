using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParkingLib
{
    public class HistoryData
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

        private int _cardId = 0;

        [DisplayName("CardId")]
        [JsonProperty("CardId", NullValueHandling = NullValueHandling.Ignore)]
        public int CardId
        {
            get { return _cardId; }
            set { _cardId = value; }
        }

        private string _cardCode = String.Empty;

        [DisplayName("CardCode")]
        [JsonProperty("CardCode", NullValueHandling = NullValueHandling.Ignore)]
        public string CardCode
        {
            get { return _cardCode; }
            set { _cardCode = value; }
        }

        private string _cardNumber = string.Empty;

        [DisplayName("CardNumber")]
        [JsonProperty("CardNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }

        private string _licensePlate = string.Empty;

        [DisplayName("LicensePlate")]
        [JsonProperty("LicensePlate", NullValueHandling = NullValueHandling.Ignore)]
        public string LicensePlate
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

        private string _photo = string.Empty;

        [DisplayName("Photo")]
        [JsonProperty("Photo", NullValueHandling = NullValueHandling.Ignore)]
        public string Photo
        {
            get { return _photo; }
            set { _photo = value; }
        }

        private string _cardTypeName = string.Empty;

        [DisplayName("CardTypeName")]
        [JsonProperty("CardTypeName", NullValueHandling = NullValueHandling.Ignore)]
        public string CardTypeName
        {
            get { return _cardTypeName; }
            set { _cardTypeName = value; }
        }

        private string _vehicleTypeName = string.Empty;

        [DisplayName("VehicleTypeName")]
        [JsonProperty("VehicleTypeName", NullValueHandling = NullValueHandling.Ignore)]
        public string VehicleTypeName
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

        public void Add()
        {
            const string tenantId = GlobalConfig.TenantId;
            
            var _strHistoryData =
                "INSERT INTO dbo.Park_History([TenantId],[CardId],[LicensePlate],[Price],[Time],[Type],[Photo],[CreationTime],[IsDeleted]) Values(@TenantId,@CardId,@LicensePlate,@Price,@Time,@Type,@Photo,@CreationTime,@IsDeleted); SELECT SCOPE_IDENTITY();";
            
            if (_conn.State == ConnectionState.Closed) _conn.Open();

            _cmd = new SqlCommand();
            _cmd.Connection = _conn;
            _cmd.CommandText = _strHistoryData;

            if (tenantId != null)
            {
                _cmd.Parameters.Add("@TenantId", DbType.Int32).Value = tenantId;
            }
            else
            {
                _cmd.Parameters.Add("@TenantId", DbType.Int32).Value = DBNull.Value;
            }
            _cmd.Parameters.Add("@CardId", DbType.Int32).Value = _cardId;
            _cmd.Parameters.Add("@LicensePlate", DbType.String).Value =
                string.IsNullOrEmpty(_licensePlate) ? "" : _licensePlate;
            _cmd.Parameters.Add("@Price", DbType.Double).Value = _price;
            _cmd.Parameters.Add("@Time", DbType.DateTime).Value = _time;
            _cmd.Parameters.Add("@Type", DbType.Int32).Value = _type;
            _cmd.Parameters.Add("@Photo", DbType.String).Value = string.IsNullOrEmpty(_photo) ? "" : _photo;
            _cmd.Parameters.Add("@CreationTime", DbType.DateTime).Value = _time;
            _cmd.Parameters.Add("@IsDeleted", DbType.Int32).Value = 0;
            
            _id = Convert.ToInt32(_cmd.ExecuteScalar());
            
            _conn.Close();
        }

        public List<HistoryData> Gets()
        {
            var dt = new DataTable();
            var ds = new DataSet();
            var lstHistoryData = new List<HistoryData>();
            const string tenantId = GlobalConfig.TenantId;

            var historyDataQuery = "";
            if(tenantId !=null)
            {
                historyDataQuery =
                    $"SELECT history.Id, history.CardId, card.Code as CardCode,card.CardNumber as CardNumber,history.LicensePlate,history.Price,history.Time,history.Type,history.Photo, cardType.Name as CardTypeName,vehicleType.Name as VehicleTypeName FROM dbo.Park_History history JOIN dbo.Park_Card_Card card ON card.Id = history.CardId JOIN dbo.Park_Card_CardType cardType ON cardType.Id = card.CardTypeId JOIN dbo.Park_Vehicle_VehicleType vehicleType ON vehicleType.Id = card.VehicleTypeId WHERE history.TenantId = {tenantId}";
            }
            else
            {
                historyDataQuery =
                    $"SELECT history.Id, history.CardId, card.Code as CardCode,card.CardNumber as CardNumber,history.LicensePlate,history.Price,history.Time,history.Type,history.Photo, cardType.Name as CardTypeName,vehicleType.Name as VehicleTypeName FROM dbo.Park_History history JOIN dbo.Park_Card_Card card ON card.Id = history.CardId JOIN dbo.Park_Card_CardType cardType ON cardType.Id = card.CardTypeId JOIN dbo.Park_Vehicle_VehicleType vehicleType ON vehicleType.Id = card.VehicleTypeId WHERE history.TenantId IS NULL";
            }
            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (var da = new SqlDataAdapter(historyDataQuery, _conn))
            {
                using (new SqlCommandBuilder(da))
                {
                    da.Fill(ds, "HistoryData");
                    dt = ds.Tables["HistoryData"];
                }
            }

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
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