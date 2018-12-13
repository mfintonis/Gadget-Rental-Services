using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Custom.Classes
{
    public class UserInfo
    {
        public Guid UserID { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsAdmin { get; set; }
    }
}