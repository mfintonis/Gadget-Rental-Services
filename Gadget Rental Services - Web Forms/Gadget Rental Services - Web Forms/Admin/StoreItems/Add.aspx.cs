using App_Code.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gadget_Rental_Services___Web_Forms.Admin.StoreItems
{
    public partial class Add : System.Web.UI.Page
    {
        private readonly string ServerFilePath = $"{HttpContext.Current.Request.PhysicalApplicationPath}\\ItemImages";
        private readonly string ImageFilePath = $"/ItemImages";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/account/login?returnurl=/admin/storeitems/add");
            }

            if (!Context.User.IsInRole("Administrator"))
            {
                Response.Redirect("~/Error?code=403");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string imageFilePath = $"{ImageFilePath}/{txtItemSku.Text}-{upldImageSelector.FileName}";
            string serverFilePath = $"{ServerFilePath}\\{txtItemSku.Text}-{upldImageSelector.FileName}";
            int itemID = 0;
            try
            {                
                string providerErrorMessage = "";

                StoreItemInfo itemInfo = new StoreItemInfo();

                int itemQuantity;
                double itemPrice;

                if(!Int32.TryParse(txtItemQuantityAvailable.Text, out itemQuantity))
                {
                    lblGeneralError.Text = "Unable to parse value for 'Item Quantity Avaialable' field. Please double check the value entered.";
                    return;
                }

                if(!double.TryParse(txtItemPrice.Text, out itemPrice))
                {
                    lblGeneralError.Text = "Unable to parse value for 'Item Quantity Avaialable' field. Please double check the value entered.";
                    return;
                }

                itemInfo.ItemName = txtItemName.Text;
                itemInfo.ItemSku = txtItemSku.Text;
                itemInfo.ItemQuantityAvailable = itemQuantity;
                itemInfo.ItemImagePath = imageFilePath;
                itemInfo.ItemPrice = itemPrice;

                upldImageSelector.SaveAs(serverFilePath);

                if(!StoreItemInfoProvider.AddStoreItem(itemInfo, out providerErrorMessage, out itemID))
                {
                    throw new Exception($"An exception occurred while saving the store item. Original Message: '{providerErrorMessage}'");
                }

                Response.Redirect($"~/Admin/StoreItems/");
            }
            catch (Exception ex)
            {
                lblGeneralError.Text = $"An exception occurred while processing your request. Original message: '{ex.Message}'";

                if(File.Exists(imageFilePath))
                {
                    File.Delete(imageFilePath);
                }
            }
        }
    }
}