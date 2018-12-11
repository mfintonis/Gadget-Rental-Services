using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace App_Code.Classes
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
                    cmd.Parameters.AddWithValue("@ProductName", info.ProductName);
                    cmd.Parameters.AddWithValue("@ProductSku", info.ProductSku);
                    cmd.Parameters.AddWithValue("@TotalPrice", info.TotalPrice);
                    cmd.Parameters.AddWithValue("@TransactionDate", DateTime.Now);

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
    }
}