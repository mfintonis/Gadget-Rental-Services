using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gadget_Rental_Services___Web_Forms
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = Request.QueryString["code"];
            switch(code)
            {
                case "403":
                    lblError.Text = "403 - You are not authorized to view this part of the site.";
                    break;
                default:
                    lblError.Text = "An unknown error has occured. Please contact the system administrator.";
                    break;
            }
        }
    }
}