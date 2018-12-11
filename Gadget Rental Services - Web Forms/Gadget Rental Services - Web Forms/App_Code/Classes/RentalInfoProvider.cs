using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace App_Code.Classes
{
    public static class RentalInfoProvider
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static bool AddRental(RentalInfo info)
        {
            var procName = "Proc_Insert_Rental";

            if(info == null)
            {
                throw new NullReferenceException("Rental info was null");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StoreItemId", info.StoreItem.Id);
                    cmd.Parameters.AddWithValue("@@UserId", info.User.UserID);
                    cmd.Parameters.AddWithValue("@RentalDueDate", DateTime.Now.AddDays(14));
                    cmd.Parameters.AddWithValue("@RentalStatus", info.RentalStatus);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch
                    {
                        conn.Close();
                        return false;
                    }
                }
            }
        }
    }
}