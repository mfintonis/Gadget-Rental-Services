using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Code.Classes
{
    public class RentalInfo
    {
        public int Id { get; set; }
        public StoreItemInfo StoreItem { get; set; }
        public UserInfo User { get; set; }
        public DateTime RentalDueDate { get; set; }
        public int RentalStatus { get; set; }
    }
}