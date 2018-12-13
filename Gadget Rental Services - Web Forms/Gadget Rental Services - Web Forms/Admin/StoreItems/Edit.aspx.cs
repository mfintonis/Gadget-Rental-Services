using Custom.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gadget_Rental_Services___Web_Forms.Admin.StoreItems
{
    public partial class Edit : System.Web.UI.Page
    {
        private readonly string ServerFilePath = $"{HttpContext.Current.Request.PhysicalApplicationPath}\\ItemImages";
        private readonly string ImageFilePath = $"/ItemImages";     

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/account/login?returnurl=/admin/storeitems/");
            }

            if (!Context.User.IsInRole("Administrator"))
            {
                Response.Redirect("~/Error?code=403");
            }

            int id = 0;

            if (!Int32.TryParse(Request["id"], out id))
            {
                Response.Redirect("~/admin/StoreItems");
            }

            if (!IsPostBack)
            {
                string exceptionMessage = "";

                var storeItem = StoreItemInfoProvider.GetItem(id, out exceptionMessage);

                if (storeItem != null && String.IsNullOrEmpty(exceptionMessage))
                {
                    txtItemName.Text = storeItem.ItemName;
                    txtItemPrice.Text = storeItem.ItemPrice.ToString();
                    txtItemQuantityAvailable.Text = storeItem.ItemQuantityAvailable.ToString();
                    txtItemSku.Text = storeItem.ItemSku;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {            
            string imageFilePath = $"{ImageFilePath}/{txtItemSku.Text}-{upldImageSelector.FileName}";
            string serverFilePath = $"{ServerFilePath}\\{txtItemSku.Text}-{upldImageSelector.FileName}";

            int id = 0;
            string exceptionMessage;

            try
            {

                if (!Int32.TryParse(Request["id"], out id))
                {
                    lblGeneralError.Text = "Could not retrieve ID for item. Saving failed.";
                    return;
                }

                var storeItem = StoreItemInfoProvider.GetItem(id, out exceptionMessage);

                if (storeItem == null)
                {
                    lblGeneralError.Text = "Could not save data. The result seems to have been deleted before you saved your changes. Please recreate this item";
                    return;
                }

                if (upldImageSelector.HasFile)
                {
                    string originalFilePath = $"{ServerFilePath}\\{storeItem.ItemImagePath.Split('/').Last()}";

                    if (File.Exists(originalFilePath))
                    {
                        File.Delete(originalFilePath);
                    }

                    upldImageSelector.SaveAs(serverFilePath);

                    storeItem.ItemImagePath = imageFilePath;
                }
                string providerErrorMessage = "";

                int itemQuantity;
                double itemPrice;

                if (!Int32.TryParse(txtItemQuantityAvailable.Text, out itemQuantity))
                {
                    lblGeneralError.Text = "Unable to parse value for 'Item Quantity Avaialable' field. Please double check the value entered.";
                    return;
                }

                if (!double.TryParse(txtItemPrice.Text, out itemPrice))
                {
                    lblGeneralError.Text = "Unable to parse value for 'Item Quantity Avaialable' field. Please double check the value entered.";
                    return;
                }

                storeItem.ItemName = txtItemName.Text;
                storeItem.ItemSku = txtItemSku.Text;
                storeItem.ItemQuantityAvailable = itemQuantity;

                storeItem.ItemPrice = itemPrice;

                if (!StoreItemInfoProvider.UpdateItem(storeItem, out exceptionMessage))
                {
                    lblGeneralError.Text = $"An exception occurred while saving the store item. Original Message: '{exceptionMessage}'";
                    return;
                }

                Response.Redirect($"~/Admin/StoreItems/");
            }
            catch (Exception ex)
            {
                lblGeneralError.Text = $"An exception occurred while processing your request. Original message: '{ex.Message}'";

                if (File.Exists(imageFilePath))
                {
                    File.Delete(imageFilePath);
                }
            }
        }
    }
}