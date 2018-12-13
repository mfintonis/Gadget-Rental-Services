using Custom.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gadget_Rental_Services___Web_Forms.Admin.BillingRecords
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


            var records = BillingRecordsInfoProvider.GetBillingRecords();

            if (records != null)
            {
                foreach (var record in records)
                {
                    var expMsg = "";
                    var storeItem = StoreItemInfoProvider.GetItem(record.StoreItemId, out expMsg);

                    if (storeItem != null)
                    {
                        record.ProductName = storeItem.ItemName;
                        record.ProductSku = storeItem.ItemSku;
                    }
                }

                rptBillingRecords.DataSource = records.OrderByDescending(x => x.TransactionDate);
                rptBillingRecords.DataBind();
            }

        }
    }
}