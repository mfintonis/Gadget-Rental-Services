<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Edit.aspx.cs" Inherits="Gadget_Rental_Services___Web_Forms.Admin.StoreItems.Edit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit Store Item</h2>
    <hr />
    <div class="form-horizontal">

        <asp:Label runat="server" ID="lblGeneralError" CssClass="text-danger"/>

        <div class="form-group">
            <asp:Label runat="server" ID="lblItemName" AssociatedControlID="txtItemName" CssClass="col-md-2 control-label">Item Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtItemName" MaxLength="200" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtItemName" CssClass="text-danger" ErrorMessage="Item Name is required." />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="lblItemSku" AssociatedControlID="txtItemSku" CssClass="col-md-2 control-label">Item Sku</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtItemSku" MaxLength="200" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtItemSku" CssClass="text-danger" ErrorMessage="Item Sku is required." />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="lblItemQuantityAvailable" AssociatedControlID="txtItemQuantityAvailable" CssClass="col-md-2 control-label">Item Quantity</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtItemQuantityAvailable" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtItemQuantityAvailable" CssClass="text-danger" ErrorMessage="Item Quantity is required." />
                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ControlToValidate="txtItemQuantityAvailable" CssClass="text-danger" ValidationExpression="^\d+$" ErrorMessage="Please enter a valid quantity."></asp:RegularExpressionValidator>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="lblItemPrice" AssociatedControlID="txtItemPrice" CssClass="col-md-2 control-label">Item Price</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtItemPrice" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtItemPrice" CssClass="text-danger" ErrorMessage="Item Price is required." />
                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ControlToValidate="txtItemPrice" CssClass="text-danger" ValidationExpression="^\$?(\d{1,3},?(\d{3},?)*\d{3}(.\d{0,3})?|\d{1,3}(.\d{2})?)$" ErrorMessage="Please enter a valid currency (ex: 24.99)"></asp:RegularExpressionValidator>
            </div>
        </div>

        <%--<div class="form-group">
            <asp:Label runat="server" ID="lblImage" AssociatedControlID="upldImageSelector" CssClass="col-md-2 control-label">Item Image</asp:Label>
            <div class="col-md-10">
                <asp:FileUpload runat="server" ID="upldImageSelector" AllowMultiple="false" />
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="upldImageSelector" CssClass="text-danger" ErrorMessage="Item Image is required." />
                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ControlToValidate="upldImageSelector" CssClass="text-danger" ValidationExpression="(.*\.([Jj][Pp][Gg])|.*\.([Bb][Mm][Pp])|.*\.([pP][nN][gG])|.*\.([Jj][Pp][Ee][Gg])$)" ErrorMessage="Only .png, .jpg, .jpeg, and .bmp file types are allowed." />
            </div>
        </div>--%>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" ID="btnSubmit" Text="Add Item" OnClick="btnSubmit_Click" CssClass="btn btn-default" />
            </div>
        </div>

    </div>

</asp:Content>