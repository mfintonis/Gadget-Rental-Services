<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="Gadget_Rental_Services___Web_Forms.Admin.RentalManagement.Default" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <table style="width: 100%;">
        <tr>
            <th></th>
            <th><strong>User Name</strong></th>
            <th><strong>Rented Store Item</strong></th>
            <th><strong>Due Date</strong></th>
            <th><strong>Rental Status</strong></th>
        </tr>
        <asp:Repeater runat="server" ID="rptRentals">
            <ItemTemplate>
                <tr>
                    <td><%# Eval("ActionResult") %></td>
                    <td><%# (Eval("User") as Custom.Classes.UserInfo).Email %></td>
                    <td><%# (Eval("StoreItem") as Custom.Classes.StoreItemInfo).ItemName %></td>
                    <td><%# Eval("RentalDueDate") %></td>
                    <td><%# Eval("RentalStatusDisplayText") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <br />
    <br />
</asp:Content>