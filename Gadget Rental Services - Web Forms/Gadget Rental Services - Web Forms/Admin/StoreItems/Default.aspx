<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="Gadget_Rental_Services___Web_Forms.Admin.StoreItems.Default" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <table style="width: 100%;">
        <tr>
            <th></th>
            <th><strong>Name</strong></th>
            <th><strong>SKU</strong></th>
            <th><strong>Quantity Available</strong></th>
            <th><strong>Price</strong></th>
            <th><strong>Image</strong></th>
        </tr>
        <asp:Repeater runat="server" ID="rptStoreItems">
            <ItemTemplate>
                <tr>
                    <td style="max-width: 35px;">
                        <a>Edit</a> |
                        <a>Delete</a>
                    </td>
                    <td><%# Eval("ItemName") %></td>
                    <td><%# Eval("ItemSku") %></td>
                    <td><%# Eval("ItemQuantityAvailable") %></td>
                    <td>$<%# Eval("ItemPrice") %></td>
                    <td>
                        <img src="<%# Eval("ItemImagePath") %>" style="max-width: 100px; max-height: 100px;" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <br />
    <br />
    <asp:Button runat="server" ID="btnNew" OnClick="btnNew_Click" Text="Add Item" CssClass="btn btn-default" />
</asp:Content>