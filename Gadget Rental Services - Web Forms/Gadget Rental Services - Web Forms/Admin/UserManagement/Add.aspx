<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Add.aspx.cs" Inherits="Gadget_Rental_Services___Web_Forms.Admin.UserManagement.Add" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Add New User</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <hr />
    <div class="form-horizontal">

        <asp:Label runat="server" ID="lblGeneralError" CssClass="text-danger"/>

        <div class="form-group">
            <asp:Label runat="server" ID="lblItemName" AssociatedControlID="txtEmail" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtEmail" MaxLength="200" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtEmail" CssClass="text-danger" ErrorMessage="Item Name is required." />
                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ControlToValidate="txtEmail" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" CssClass="text-danger" ErrorMessage="Please enter a valid email." />
            </div>
        </div>    
        
        <div class="form-group">            
            <div class="col-md-offset-2 col-md-10">
                <asp:CheckBox runat="server" ID="chkIsAdmin" />
                <asp:Label runat="server" ID="lblAdmin" AssociatedControlID="chkIsAdmin">Administrator</asp:Label>
            </div>
        </div>        

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtPassword" CssClass="col-md-2 control-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="text-danger" ErrorMessage="The password field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" ID="btnSubmit" Text="Add User" OnClick="btnSubmit_Click" CssClass="btn btn-default" />
            </div>
        </div>

    </div>

</asp:Content>
