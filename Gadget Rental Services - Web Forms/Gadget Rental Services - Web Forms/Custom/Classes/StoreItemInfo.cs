using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Custom.Classes
{
    public class StoreItemInfo
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemSku { get; set; }
        public int ItemQuantityAvailable { get; set; }
        public string ItemImagePath { get; set; }
        public double ItemPrice { get; set; }
    }
}