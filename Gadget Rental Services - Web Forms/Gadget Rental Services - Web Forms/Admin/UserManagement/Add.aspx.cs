using App_Code.Classes;
using Gadget_Rental_Services___Web_Forms.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gadget_Rental_Services___Web_Forms.Admin.UserManagement
{
    public partial class Add : System.Web.UI.Page
    {
        private readonly string ServerFilePath = $"{HttpContext.Current.Request.PhysicalApplicationPath}\\ItemImages";
        private readonly string ImageFilePath = $"/ItemImages";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/account/login?returnurl=/admin/usermanagement/add");
            }

            if (!Context.User.IsInRole("Administrator"))
            {
                Response.Redirect("~/Error?code=403");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = txtEmail.Text, Email = txtEmail.Text };
            IdentityResult result = manager.Create(user, txtPassword.Text);
            if (result.Succeeded)
            {
                if (chkIsAdmin.Checked)
                {
                    var newUser = manager.FindByName(txtEmail.Text);
                    manager.AddToRole(user.Id, "Administrator");
                }

                Response.Redirect("~/admin/usermanagement");
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}