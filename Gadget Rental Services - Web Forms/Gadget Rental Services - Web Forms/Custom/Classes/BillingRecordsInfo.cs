﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Custom.Classes
{
    public class BillingRecordsInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int StoreItemId { get; set; }
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ProductName { get; set; }
        public string ProductSku { get; set; }
        public double TotalPrice { get; set; }
    }
}