using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gadget_Rental_Services___Web_Forms.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/account/login");
            }

            if(!Context.User.IsInRole("Administrator"))
            {
                Response.Redirect("~/Error?code=403");
            }
        }
    }
}