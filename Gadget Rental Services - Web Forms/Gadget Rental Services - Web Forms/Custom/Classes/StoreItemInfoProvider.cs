using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace Custom.Classes
{
    public static class StoreItemInfoProvider
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private static readonly string ServerFilePath = $"{HttpContext.Current.Request.PhysicalApplicationPath}\\ItemImages";

        public static bool AddStoreItem(StoreItemInfo info, out string exceptionMessage, out int itemID)
        {
            itemID = 0;
            if(info == null)
            {
                exceptionMessage = "StoreItemInfo object was null.";
                return false;
            }
            
            string storedProcedure = "Proc_Insert_Store_Item";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ItemName", info.ItemName);
                    cmd.Parameters.AddWithValue("@ItemSku", info.ItemSku);
                    cmd.Parameters.AddWithValue("@ItemQuantityAvailable", info.ItemQuantityAvailable);
                    cmd.Parameters.AddWithValue("@ItemImagePath", info.ItemImagePath);
                    cmd.Parameters.AddWithValue("@ItemPrice", info.ItemPrice);                    

                    try
                    {
                        conn.Open();
                        itemID = (int)cmd.ExecuteScalar();
                    }
                    catch(Exception e)
                    {
                        conn.Close();
                        exceptionMessage = e.Message;
                        return false;
                    }
                    conn.Close();
                }
            }
            exceptionMessage = null;
            return true;
        }

        public static StoreItemInfo GetItem(int id, out string exceptionMessage)
        {
            string storedProcedure = $"Proc_Get_Store_Item";
            var info = new StoreItemInfo();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    try
                    {
                        conn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr != null)
                            {
                                while (dr.Read())
                                {                                    
                                    info.Id = Convert.ToInt32(dr["Id"]);
                                    info.ItemName = dr["ItemName"].ToString();
                                    info.ItemSku = dr["ItemSku"].ToString();
                                    info.ItemQuantityAvailable = Convert.ToInt32(dr["ItemQuantityAvailable"]);
                                    info.ItemImagePath = dr["ItemImagePath"].ToString();
                                    info.ItemPrice = Convert.ToDouble(dr["ItemPrice"]);
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
            return info;
        }

        public static bool UpdateItem(StoreItemInfo info, out string exceptionMessage)
        {
            string storedProcedure = $"Proc_Update_Store_Item";

            if (info != null)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", info.Id);
                        cmd.Parameters.AddWithValue("@ItemName", info.ItemName);
                        cmd.Parameters.AddWithValue("@ItemSku", info.ItemSku);
                        cmd.Parameters.AddWithValue("@ItemQuantityAvailable", info.ItemQuantityAvailable);
                        cmd.Parameters.AddWithValue("@ItemImagePath", info.ItemImagePath);
                        cmd.Parameters.AddWithValue("@ItemPrice", info.ItemPrice);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            conn.Close();
                            exceptionMessage = e.Message;
                            return false;
                        }
                        conn.Close();
                    }
                }
            }
            else
            {
                throw new NullReferenceException("StoreItemInfo 'info' was null");
            }
            exceptionMessage = null;
            return true;
        }

        public static List<StoreItemInfo> GetItems(out string exceptionMessage)
        {
            string commandString = $"SELECT * FROM StoreItem";
            List<StoreItemInfo> infos = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandString, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr != null)
                            {
                                infos = new List<StoreItemInfo>();
                                while (dr.Read())
                                {
                                    var info = new StoreItemInfo();
                                    info.Id = Convert.ToInt32(dr["Id"]);
                                    info.ItemName = dr["ItemName"].ToString();
                                    info.ItemSku = dr["ItemSku"].ToString();
                                    info.ItemQuantityAvailable = Convert.ToInt32(dr["ItemQuantityAvailable"]);
                                    info.ItemImagePath = dr["ItemImagePath"].ToString();
                                    info.ItemPrice = Convert.ToDouble(dr["ItemPrice"]);
                                    infos.Add(info);
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
            return infos;
        }

        public static void DeleteItem(int id)
        {
            string commandString = $"DELETE FROM StoreItem WHERE Id = {id}";

            string message;
            var item = GetItem(id, out message);

            string serverFilePath = $"{ServerFilePath}\\{item.ItemImagePath.Split('/').Last()}";

            if (File.Exists(serverFilePath))
            {
                File.Delete(serverFilePath);
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandString, conn))
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}