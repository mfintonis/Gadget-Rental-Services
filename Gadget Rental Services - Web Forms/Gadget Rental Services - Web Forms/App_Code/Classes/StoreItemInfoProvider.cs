using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace App_Code.Classes
{
    public static class StoreItemInfoProvider
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static bool AddStoreItem(StoreItemInfo info, out string exceptionMessage, out int itemID)
        {
            itemID = 0;
            if(info == null)
            {
                exceptionMessage = "StoreItemInfo object was null.";
                return false;
            }

            string commandString = "INSERT INTO StoreItem (ItemName, ItemSku, ItemQuantityAvailable, ItemImagePath, ItemPrice) OUTPUT INSERTED.ID VALUES (@ItemName, @ItemSku, @ItemQuantityAvailable, @ItemImagePath, @ItemPrice)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(commandString, conn))
                {
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
            string commandString = $"SELECT * FROM StoreItem WHERE Id = {id}";
            StoreItemInfo info = null;

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
                                while (dr.Read())
                                {
                                    info = new StoreItemInfo();
                                    info.Id = Convert.ToInt32(dr["Id"]);
                                    info.ItemName = dr["ItemName"].ToString();
                                    info.ItemSku = dr["ItemSku"].ToString();
                                    info.ItemQuantityAvailable = Convert.ToInt32(dr["ItemQuantityAvailable"]);
                                    info.ItemImagePath = dr["ItemImagePath"].ToString();
                                    info.ItemPrice = Convert.ToDecimal(dr["ItemPrice"]);
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
                                    info.ItemPrice = Convert.ToDecimal(dr["ItemPrice"]);
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
    }
}