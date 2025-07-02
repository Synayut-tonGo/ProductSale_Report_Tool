using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using C_Forintern.Dto;

namespace C_Forintern.Repository
{
    public class SaleRepository
    {
        private readonly string _connectionString = "server=DESKTOP-CLBSF3Q\\MSSQLSERVERYUT;database=ForTest;user id=sa;password=123";

        public List<SaleDto> GetSales(DateTime startDate, DateTime endDate, string productNameFilter = "")
        {
            var sales = new List<SaleDto>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_GetProductSales", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@STARTDATE", startDate);
                        cmd.Parameters.AddWithValue("@ENDDATE", endDate);
                        cmd.Parameters.AddWithValue("@PRODUCTNAME", productNameFilter);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                sales.Add(new SaleDto
                                {
                                    ProductCode = reader.GetString(0),
                                    ProductName = reader.GetString(1),
                                    Quantity = reader.GetInt32(2),
                                    UnitPrice = reader.GetDecimal(3),
                                    SaleDate = reader.GetDateTime(4)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }

            return sales;
        }
    }

    public static class Logger
    {
        public static void LogError(string message)
        {
            try
            {
                var folder = "logs";
                var path = Path.Combine(folder, "errors.txt");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                File.AppendAllText(path, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}");
            }
            catch
            {
                // prevent crash if logging fails
            }
        }
    }
}
