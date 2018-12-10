using App_Code.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gadget_Rental_Services___Web_Forms.Store
{
    public partial class Rent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = int.TryParse(Request["id"], out id) ? id : 0;
            
            if(id == 0)
            {
                Response.Redirect("~/store");
            }

            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect($"~/account/login?returnurl=/store/rent?id=");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var helper = SetUpAuthorization();
        }

        private AuthorizePaymentHelper SetUpAuthorization()
        {
            string cardMonth = txtCCMonth.Text;
            string cardYear = txtCCYear.Text;
            string cardSecurityCode = txtCCSecurityCode.Text;
            string cardNumber = txtCCNumber.Text;

            var ccname = txtNameCardCC.Text.Split(' ');
            var fname = ccname.Count() > 0 ? ccname[0] : "";
            var lname = ccname.Count() > 1 ? ccname[1] : "";
            var authorization = new AuthorizePaymentHelper();
            authorization.SetBillingAddress(fname, lname, txtAddressCC.Text, txtCityCC.Text, txtZipCC.Text, txtEmailCC.Text, txtPhoneCC.Text, ddlCountryCC.SelectedValue, ddlStateCC.SelectedValue);
            authorization.SetCardInfo(cardNumber, String.Format("{0}{1}", cardMonth, cardYear), cardSecurityCode);
            return authorization;
        }
    }
}