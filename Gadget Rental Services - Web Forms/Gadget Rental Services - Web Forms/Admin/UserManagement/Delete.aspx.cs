﻿using App_Code.Classes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gadget_Rental_Services___Web_Forms.Admin.UserManagement
{
    public partial class Delete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid id = Guid.Empty;

            if (Guid.TryParse(Request["id"], out id))
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = manager.FindById(id.ToString());

                if(user.Email == Context.User.Identity.GetUserName())
                {
                    Response.Redirect("~/admin/usermanagement?error=1");
                }

                manager.DeleteAsync(user).ConfigureAwait(false).GetAwaiter().GetResult();
               
            }

            Response.Redirect("~/admin/usermanagement");
        }
    }
}