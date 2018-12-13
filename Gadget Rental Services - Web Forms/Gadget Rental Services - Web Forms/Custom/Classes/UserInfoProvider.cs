using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Custom.Classes
{
    public class UserInfoProvider
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<UserInfo> GetAllUsers(out string exceptionMessage)
        {
            string queryText = "SELECT * FROM View_User_Role_Joined";
            var users = new List<UserInfo>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryText, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr != null)
                            {
                                while (dr.Read())
                                {
                                    var info = new UserInfo();
                                    info.UserID = Guid.Parse(dr["UserID"].ToString());
                                    info.Email = dr["Email"].ToString();
                                    info.RoleID = Convert.ToInt32(dr["RoleID"]);
                                    info.RoleName = dr["RoleName"].ToString();
                                    users.Add(info);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        exceptionMessage = e.Message;
                        conn.Close();
                        return null;
                    }
                    conn.Close();
                }
            }
            exceptionMessage = null;
            return users;
        }
    }
}