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

            rentalRecords = rentalRecords.OrderBy(x => x.RentalDueDate).ToList();

            foreach(var rental in rentalRecords)
            {
                if (rental != null)
                {
                    if(rental.RentalStatus != StatusCode.Overdue && DateTime.Now > rental.RentalDueDate)
                    {
                        rental.RentalStatus = StatusCode.Overdue;
                        RentalInfoProvider.UpdateRental(rental);
                    }

                    if (rental.RentalStatus == StatusCode.Rented)
                    {
                        rental.RentalStatusDisplayText = "Rented";
                        rental.ActionResult = $"<a href=\"/admin/rentalmanagement/return?id={rental.Id}\">Return</a>";
                    }

                    if (rental.RentalStatus == StatusCode.Returned)
                    {
                        rental.RentalStatusDisplayText = "<p style=\"color: green; margin: 0;\">Returned</p>";
                    }

                    if (rental.RentalStatus == StatusCode.Overdue)
                    {
                        rental.RentalStatusDisplayText = "<p style=\"color: red; margin: 0;\">Overdue</p>";
                        rental.ActionResult = $"<a href=\"/admin/rentalmanagement/return?id={rental.Id}\">Return</a>";
                    }
                }
            }

            rptRentals.DataSource = rentalRecords;
            rptRentals.DataBind();
        }
    }
}