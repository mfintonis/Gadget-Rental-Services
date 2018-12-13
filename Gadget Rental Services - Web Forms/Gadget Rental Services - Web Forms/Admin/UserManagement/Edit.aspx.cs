using Custom.Classes;
using Gadget_Rental_Services___Web_Forms.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gadget_Rental_Services___Web_Forms.Admin.UserManagement
{
    public partial class Edit : System.Web.UI.Page
    {
        private readonly string ServerFilePath = $"{HttpContext.Current.Request.PhysicalApplicationPath}\\ItemImages";
        private readonly string ImageFilePath = $"/ItemImages";
        private static Guid _userGuid;

        private Guid userGuid
        {
            get
            {
                try
                {
                    if (!IsPostBack || _userGuid == null || _userGuid == Guid.Empty)
                    {
                        _userGuid = Guid.Parse(Request["id"]);
                    }

                    return _userGuid;
                }
                catch
                {
                    return Guid.Empty;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/account/login?returnurl=/admin/usermanagement/");
            }

            if (!Context.User.IsInRole("Administrator"))
            {
                Response.Redirect("~/Error?code=403");
            }

            if(userGuid == Guid.Empty)
            {
                Response.Redirect("~/admin/usermanagement");
            }

            if(!IsPostBack)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

                var user = manager.FindById(userGuid.ToString());
                if(user != null)
                {
                    txtEmail.Text = user.Email;
                    chkIsAdmin.Checked = manager.IsInRole(user.Id, "Administrator");
                    chkIsAdmin.Enabled = Context.User.Identity.GetUserName() != user.UserName;

                    txtPassword.Visible = false;
                    lblPassword.Visible = false;
                    txtConfirmPassword.Visible = false;
                    lblConfirmPasword.Visible = false;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            var user = manager.FindById(userGuid.ToString());
            user.Email = txtEmail.Text;
            if (Context.User.Identity.GetUserName() != user.UserName)
            {
                if (manager.IsInRole(user.Id, "Administrator") && !chkIsAdmin.Checked)
                {
                    manager.RemoveFromRole(user.Id, "Administrator");
                }
                else if (!manager.IsInRole(user.Id, "Administrator") && chkIsAdmin.Checked)
                {
                    manager.AddToRole(user.Id, "Administrator");
                }
            }

            var result = manager.Update(user);

            if(result.Succeeded)
            {
                if (chkResetPassword.Checked)
                {
                    var resetToken = manager.GeneratePasswordResetToken(user.Id);
                    manager.ResetPassword(user.Id, resetToken, txtPassword.Text);

                    if(result.Succeeded)
                    {
                        Response.Redirect("~/admin/usermanagement");
                    }
                    ErrorMessage.Text = result.Errors.FirstOrDefault();
                    return;
                }
                Response.Redirect("~/admin/usermanagement");
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        protected void chkResetPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(chkResetPassword.Checked)
            {
                txtPassword.Visible = true;
                lblPassword.Visible = true;
                txtConfirmPassword.Visible = true;
                lblConfirmPasword.Visible = true;
            }
            else
            {
                txtPassword.Visible = false;
                lblPassword.Visible = false;
                txtConfirmPassword.Visible = false;
                lblConfirmPasword.Visible = false;
            }
        }
    }
}