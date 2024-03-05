using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ParkingLib
{
    public class Helper
    {
        //Connect database
        public static SqlConnection OpenConnection()
        {
            var connectionString = ConfigurationManager.AppSettings["connectionStringSqlServer"];
            var sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sqlConnection;
        }
    }
}