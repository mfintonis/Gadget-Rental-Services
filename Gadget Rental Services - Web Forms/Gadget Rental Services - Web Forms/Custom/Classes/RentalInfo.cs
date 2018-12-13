using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Custom.Classes
{
    public class RentalInfo
    {
        public int Id { get; set; }
        public StoreItemInfo StoreItem { get; set; }
        public UserInfo User { get; set; }
        public DateTime RentalDueDate { get; set; }
        public Enums.StatusCode RentalStatus { get; set; }
        public string RentalStatusDisplayText { get; set; }
    }
}