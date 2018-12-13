using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Custom.Classes;

namespace Gadget_Rental_Services___Web_Forms.Admin.UserManagement
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/account/login?returnurl=/admin/usermanagement");
            }

            if (!Context.User.IsInRole("Administrator"))
            {
                Response.Redirect("~/Error?code=403");
            }
            int errorCode = 0;

            if (Int32.TryParse(Request["error"], out errorCode) && errorCode == 1)
            {
                ltrlError.Text = "<p style=\"color: red;\">You cannot delete your own user. If this is needed, another administrator must login and delete your account.</p>";
            }

            string errorMessage = "";
            var users = UserInfoProvider.GetAllUsers(out errorMessage);
            if (String.IsNullOrEmpty(errorMessage))
            {
                rptUsers.DataSource = users;
                rptUsers.DataBind();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/UserManagement/Add");
        }
    }
}