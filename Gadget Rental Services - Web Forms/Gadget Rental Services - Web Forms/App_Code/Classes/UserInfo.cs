using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gadget_Rental_Services___Web_Forms.App_Code.Classes
{
    public class UserInfo
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}