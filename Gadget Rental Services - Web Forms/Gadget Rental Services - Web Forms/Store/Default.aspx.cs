﻿using Custom.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gadget_Rental_Services___Web_Forms.Store
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string errorMessage = "";
            var items = StoreItemInfoProvider.GetItems(out errorMessage);
            if (String.IsNullOrEmpty(errorMessage))
            {
                rptStoreItems.DataSource = items;
                rptStoreItems.DataBind();
            }
        }
    }
}