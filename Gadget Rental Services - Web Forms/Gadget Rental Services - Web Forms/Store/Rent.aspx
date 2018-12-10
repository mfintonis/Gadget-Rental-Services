<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Rent.aspx.cs" Inherits="Gadget_Rental_Services___Web_Forms.Store.Rent" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Rent</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <hr />
    <div class="form-horizontal">
        <h3>Shipping Address</h3>
        <asp:Label runat="server" ID="lblGeneralError" CssClass="text-danger"/>
        <div class="form-group">
            <asp:Label runat="server" ID="lblItemName" AssociatedControlID="txtShippingFirstName" CssClass="col-md-2 control-label">First Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtShippingFirstName" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtShippingFirstName" CssClass="text-danger" ErrorMessage="This field is required." />                
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="lblLastName" AssociatedControlID="txtShippingLastName" CssClass="col-md-2 control-label">Last Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtShippingLastName" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtShippingLastName" CssClass="text-danger" ErrorMessage="This field is required." />                
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="lblAddress" AssociatedControlID="txtShippingAddress" CssClass="col-md-2 control-label">Address</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtShippingAddress" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtShippingAddress" CssClass="text-danger" ErrorMessage="This field is required." />                
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="lblCity" AssociatedControlID="txtShippingCity" CssClass="col-md-2 control-label">City</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtShippingCity" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtShippingCity" CssClass="text-danger" ErrorMessage="This field is required." />                
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="lblState" AssociatedControlID="txtShippingState" CssClass="col-md-2 control-label">State</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtShippingState" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtShippingState" CssClass="text-danger" ErrorMessage="This field is required." />                
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="lblZipCode" AssociatedControlID="txtShippingZipCode" CssClass="col-md-2 control-label">Zip Code</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtShippingZipCode" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtShippingZipCode" CssClass="text-danger" ErrorMessage="This field is required." />              
                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ControlToValidate="txtShippingZipCode" CssClass="text-danger" ErrorMessage="Please enter a valid US zip code." ValidationExpression="^[0-9]{5}(?:-[0-9]{4})?$"></asp:RegularExpressionValidator>
            </div>
        </div>

        <hr />

        <h3>Billing Address</h3>

        <div class="form-group">
            <asp:Label runat="server" ID="Label1" AssociatedControlID="txtBillingFirstName" CssClass="col-md-2 control-label">First Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtBillingFirstName" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtBillingFirstName" CssClass="text-danger" ErrorMessage="This field is required." />                
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="Label2" AssociatedControlID="txtBillingLastName" CssClass="col-md-2 control-label">Last Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtBillingLastName" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtBillingLastName" CssClass="text-danger" ErrorMessage="This field is required." />                
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="Label3" AssociatedControlID="txtBillingAddress" CssClass="col-md-2 control-label">Address</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtBillingAddress" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtBillingAddress" CssClass="text-danger" ErrorMessage="This field is required." />                
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="Label4" AssociatedControlID="txtBillingCity" CssClass="col-md-2 control-label">City</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtBillingCity" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtBillingCity" CssClass="text-danger" ErrorMessage="This field is required." />                
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="Label5" AssociatedControlID="txtBillingState" CssClass="col-md-2 control-label">State</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtBillingState" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtBillingState" CssClass="text-danger" ErrorMessage="This field is required." />                
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="Label6" AssociatedControlID="txtBillingZipCode" CssClass="col-md-2 control-label">Zip Code</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtBillingZipCode" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtBillingZipCode" CssClass="text-danger" ErrorMessage="This field is required." />              
                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ControlToValidate="txtBillingZipCode" CssClass="text-danger" ErrorMessage="Please enter a valid US zip code." ValidationExpression="^[0-9]{5}(?:-[0-9]{4})?$"></asp:RegularExpressionValidator>
            </div>
        </div>

        <hr />

        <h3>Credit Card Information</h3>
        <div class="form-group">
            <asp:Label runat="server" ID="Label7" AssociatedControlID="txtCCFirstName" CssClass="col-md-2 control-label">First Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtCCFirstName" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtCCFirstName" CssClass="text-danger" ErrorMessage="This field is required." />                
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="Label8" AssociatedControlID="txtCCLastName" CssClass="col-md-2 control-label">Last Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtCCLastName" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtCCLastName" CssClass="text-danger" ErrorMessage="This field is required." />                
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="Label9" AssociatedControlID="txtCCNumber" CssClass="col-md-2 control-label">Credit Card Number</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtCCNumber" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtCCNumber" CssClass="text-danger" ErrorMessage="This field is required." />  
                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ControlToValidate="txtCCNumber" ValidationExpression="^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$" CssClass="text-danger" ErrorMessage="Please enter a valid credit card number" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="Label10" AssociatedControlID="txtCCMonth" CssClass="col-md-2 control-label">Expiration Month</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtCCMonth" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtCCMonth" CssClass="text-danger" ErrorMessage="This field is required." />    
                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ControlToValidate="txtCCMonth" ValidationExpression="^0*(?:[0-1][0-2]?|12)$" CssClass="text-danger" ErrorMessage="Please enter a valid month (01-12)" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="Label11" AssociatedControlID="txtCCYear" CssClass="col-md-2 control-label">Expiration Year</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtCCYear" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtCCYear" CssClass="text-danger" ErrorMessage="This field is required." />    
                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ControlToValidate="txtCCYear" ValidationExpression="^0*(?:[2][0][1-4][0-9]?|2040)$" CssClass="text-danger" ErrorMessage="Please enter a valid 4 digit year" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" ID="Label12" AssociatedControlID="txtCCSecurityCode" CssClass="col-md-2 control-label">Security Code</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtCCSecurityCode" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="txtCCSecurityCode" CssClass="text-danger" ErrorMessage="This field is required." />              
                <asp:RegularExpressionValidator runat="server" Display="Dynamic" ControlToValidate="txtCCSecurityCode" ValidationExpression="^[0-9]{3,4}$" CssClass="text-danger" ErrorMessage="Please enter a valid 3 or 4 digit security code"></asp:RegularExpressionValidator>
            </div>
        </div>

        <hr />

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" ID="btnSubmit" Text="Purchase" OnClick="btnSubmit_Click" CssClass="btn btn-default" />
            </div>
        </div>

    </div>

</asp:Content>