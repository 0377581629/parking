using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace ParkingLib
{
    public class FareData : ICloneable
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

        private double _price = 0;

        [DisplayName("Price")]
        [JsonProperty("Price", NullValueHandling = NullValueHandling.Ignore)]
        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public FareData()
        {
        }

        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Data

        private SqlConnection _conn = Helper.OpenConnection();

        public List<FareData> GetFaresByCardTypeAndVehicleType(int cardTypeId, int vehicleTypeId)
        {
            var dt = new DataTable();
            var ds = new DataSet();
            var lstFareData = new List<FareData>();
            const string tenantId = GlobalConfig.TenantId;
            var fareDataQuery = "";
            if (tenantId != null)
            {
                fareDataQuery =
                    $"SELECT * FROM dbo.Park_Fare fare WHERE fare.TenantId = {tenantId} AND fare.CardTypeId = {cardTypeId} AND fare.VehicleTypeId = {vehicleTypeId}";
            }
            else
            {
                fareDataQuery = $"SELECT * FROM dbo.Park_Fare fare WHERE fare.TenantId IS NULL AND fare.CardTypeId = {cardTypeId} AND fare.VehicleTypeId = {vehicleTypeId}";
            }

            if (_conn.State == ConnectionState.Closed) _conn.Open();
            using (var da = new SqlDataAdapter(fareDataQuery, _conn))
            {
                using (new SqlCommandBuilder(da))
                {
                    da.Fill(ds, "FareData");
                    dt = ds.Tables["FareData"];
                }
            }

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var fareData = new FareData();
                fareData.Id = Convert.ToInt32(dr["Id"]);
                fareData.Price = Convert.ToDouble(dr["Price"]);

                lstFareData.Add(fareData);
            }

            _conn.Close();
            return lstFareData;
        }

        #endregion
    }
}