using Custom.Classes;
using Custom.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gadget_Rental_Services___Web_Forms.Admin.RentalManagement
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/account/login?returnurl=/admin/billingrecords");
            }

            if (!Context.User.IsInRole("Administrator"))
            {
                Response.Redirect("~/Error?code=403");
            }

            var rentalRecords = RentalInfoProvider.GetRentals();

            rptRentals.DataSource = rentalRecords;
            rptRentals.DataBind();
        }

        protected void rptRentals_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var rental = e.Item.DataItem as RentalInfo;

            if(rental != null)
            {
                if(rental.RentalStatus == StatusCode.Rented)
                {
                    rental.RentalStatusDisplayText = "Rented";                  
                }

                if(rental.RentalStatus == StatusCode.Returned)
                {
                    rental.RentalStatusDisplayText = "Returned";
                }

                if(rental.RentalStatus == StatusCode.Overdue)
                {
                    rental.RentalStatusDisplayText = "<p style=\"
                }
            }
        }
    }
}