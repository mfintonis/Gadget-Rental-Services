using Custom.Classes;
using Custom.Helpers;
using AuthorizeNet.Api.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace Gadget_Rental_Services___Web_Forms.Store
{
    public partial class Rent : System.Web.UI.Page
    {
        protected int Id { get; set; } = 0;        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session["StoreItem"] = null;
            }
            int _id;
            Id = int.TryParse(Request["id"], out _id) ? _id : 0;

            if(Id == 0)
            {
                Response.Redirect("~/store");
            }

            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect($"~/account/login?returnurl=/store/rent?id={Id}");
            }

            if (Session["StoreItem"] as StoreItemInfo == null)
            {
                string expMsg = "";
                var storeItem = StoreItemInfoProvider.GetItem(Id, out expMsg);
                if (storeItem.Id == 0)
                {
                    Session["ItemExists"] = false;
                }
                else
                {
                    Session["ItemExists"] = true;
                }

                Session["StoreItem"] = storeItem;

                if(storeItem.ItemQuantityAvailable <= 0)
                {
                    Response.Redirect("~/store");
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var helper = SetUpAuthorization();
            var storeItem = Session["StoreItem"] as StoreItemInfo;

            if (storeItem.ItemQuantityAvailable <= 0)
            {
                Response.Redirect("~/store");
            }

            var total = (decimal)storeItem.ItemPrice;

            var lineItem = new lineItemType
            {
                itemId = storeItem.Id.ToString(),
                quantity = 1,
                name = storeItem.ItemName,
                description = storeItem.ItemName,
                unitPrice = total
            };

            helper.AddItemBeingSold(lineItem);

            var transactionID = helper.ProcessTransaction(total);

            if(!String.IsNullOrEmpty(transactionID))
            {
                var expMsg = "";
                var billingRecord = new BillingRecordsInfo();

                billingRecord.UserId = HttpContext.Current.User.Identity.GetUserId();
                billingRecord.TransactionId = transactionID;
                billingRecord.ProductName = storeItem.ItemName;
                billingRecord.ProductSku = storeItem.ItemSku;
                billingRecord.TotalPrice = Convert.ToDouble(total);
                billingRecord.StoreItemId = storeItem.Id;

                if (BillingRecordsInfoProvider.AddBillingRecord(billingRecord))
                {

                    storeItem.ItemQuantityAvailable--;

                    StoreItemInfoProvider.UpdateItem(storeItem, out expMsg);

                    Response.Redirect("~/store/thankyou");
                }

                lblGeneralError.Text = "A server error occurred while processing your transaction. Please try again later or contact us.";
            }
            else
            {
                lblGeneralError.Text = "An error occurred while processing your payment. Please ensure that the details entered are correct.";
            }
        }

        private AuthorizePaymentHelper SetUpAuthorization()
        {
            string cardMonth = txtCCMonth.Text;
            string cardYear = txtCCYear.Text;
            string cardSecurityCode = txtCCSecurityCode.Text;
            string cardNumber = txtCCNumber.Text;

            var authorization = new AuthorizePaymentHelper();
            authorization.SetBillingAddress(txtBillingFirstName.Text, txtBillingLastName.Text, txtBillingAddress.Text, txtBillingCity.Text, txtBillingZipCode.Text, HttpContext.Current.User.Identity.Name, "", "USA", txtBillingState.Text);
            authorization.SetCardInfo(cardNumber, String.Format("{0}{1}", cardMonth, cardYear), cardSecurityCode);
            return authorization;            
        }
    }
}