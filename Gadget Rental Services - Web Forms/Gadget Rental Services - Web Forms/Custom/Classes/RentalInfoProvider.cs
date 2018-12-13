using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Custom.Classes
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
                    cmd.Parameters.AddWithValue("@UserId", info.User.UserID);
                    cmd.Parameters.AddWithValue("@RentalDueDate", DateTime.Now.AddDays(14));
                    cmd.Parameters.AddWithValue("@RentalStatus", info.RentalStatus);

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

        public static bool UpdateRental(RentalInfo info)
        {
            var procName = "Proc_Update_Rental";

            if(info == null)
            {
                throw new NullReferenceException("Rental info was null");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", info.Id);
                    cmd.Parameters.AddWithValue("@StoreItemId", info.StoreItem.Id);
                    cmd.Parameters.AddWithValue("@UserId", info.User.UserID);
                    cmd.Parameters.AddWithValue("@RentalStatus", info.RentalStatus);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }
                    catch (Exception e)
                    {
                        conn.Close();
                        return false;
                    }
                }
            }
        }

        public static bool Return(int id)
        {
            //TODO: implement
        }

        public static List<RentalInfo> GetRentals()
        {
            var queryText = "SELECT * FROM View_Rental_User_StoreItem_Joined";
            List<RentalInfo> rentals;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryText, conn))
                {
                    try
                    {
                        conn.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            var expMsg = "";
                            rentals = new List<RentalInfo>();
                            while (dr.Read())
                            {
                                var info = new RentalInfo()
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    StoreItem = StoreItemInfoProvider.GetItem(Convert.ToInt32(dr["StoreItemId"]), out expMsg),
                                    User = new UserInfo()
                                    {
                                        UserID = Guid.Parse(dr["UserId"].ToString()),
                                        Email = dr["Email"].ToString()
                                    },
                                    RentalDueDate = Convert.ToDateTime(dr["RentalDueDate"]),
                                    RentalStatus = (Enums.StatusCode)dr["RentalStatus"]
                                };

                                rentals.Add(info);
                            }
                        }

                        conn.Close();

                        return rentals;
                    }
                    catch (Exception e)
                    {
                        conn.Close();
                        return null;
                    }
                }
            }
        }
    }
}