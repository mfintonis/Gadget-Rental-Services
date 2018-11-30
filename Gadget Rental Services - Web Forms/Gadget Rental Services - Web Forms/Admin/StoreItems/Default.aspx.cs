using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code.Classes;

namespace Gadget_Rental_Services___Web_Forms.Admin.StoreItems
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/account/login");
            }

            if (!Context.User.IsInRole("Administrator"))
            {
                Response.Redirect("~/Error?code=403");
            }

            string errorMessage = "";
            var items = StoreItemInfoProvider.GetItems(out errorMessage);
            if (String.IsNullOrEmpty(errorMessage))
            {
                rptStoreItems.DataSource = items;
                rptStoreItems.DataBind();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/StoreItems/Add");
        }
    }
}