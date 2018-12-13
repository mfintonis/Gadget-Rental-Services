using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Custom.Classes
{
    public static class BillingRecordsInfoProvider
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static bool AddBillingRecord(BillingRecordsInfo info)
        {
            var procName = "Proc_Insert_BillingRecord";

            if (info == null)
            {
                throw new NullReferenceException("Billing Records Info was null.");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", info.UserId);
                    cmd.Parameters.AddWithValue("@TransactionId", info.TransactionId);
                    cmd.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@StoreItemId", info.StoreItemId);
                    cmd.Parameters.AddWithValue("@TotalPrice", info.TotalPrice);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch(Exception e)
                    {
                        conn.Close();
                        return false;
                    }
                }
            }
        }

        public static List<BillingRecordsInfo> GetBillingRecords()
        {
            var query = "SELECT * FROM BillingRecords";
            List<BillingRecordsInfo> records;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            records = new List<BillingRecordsInfo>();
                            while(dr.Read())
                            {
                                var record = new BillingRecordsInfo()
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    UserId = dr["UserId"].ToString(),
                                    TransactionId = dr["TransactionId"].ToString(),
                                    TransactionDate = Convert.ToDateTime(dr["TransactionDate"]),
                                    StoreItemId = Convert.ToInt32(dr["StoreItemId"]),
                                    TotalPrice = Convert.ToDouble(dr["TotalPrice"])
                                };

                                records.Add(record);
                            }
                        }
                    }
                    catch(Exception e)
                    {
                        conn.Close();
                        return null;
                    }
                }
                conn.Close();
            }

            return records;
        }
    }
}