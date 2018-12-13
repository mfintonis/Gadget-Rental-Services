using Custom.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gadget_Rental_Services___Web_Forms.Admin.StoreItems
{
    public partial class Return : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/account/login?returnurl=/admin/rentalmanagement");
            }

            if (!Context.User.IsInRole("Administrator"))
            {
                Response.Redirect("~/Error?code=403");
            }

            int id = 0;

            if (Int32.TryParse(Request["id"], out id))
            {
                RentalInfoProvider.Return(id);
            }
            
            Response.Redirect("~/admin/rentalmanagement");
        }
    }
}