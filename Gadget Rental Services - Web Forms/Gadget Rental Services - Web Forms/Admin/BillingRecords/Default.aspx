<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Gadget_Rental_Services___Web_Forms.Admin.BillingRecords.Default" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <br />
    <table style="width: 100%;">
        <tr>
            <th><strong>Trnasaction ID</strong></th>
            <th><strong>Date</strong></th>
            <th><strong>Product Name</strong></th>
            <th><strong>Product SKU</strong></th>
            <th><strong>Price</strong></th>
        </tr>
        <asp:Repeater runat="server" ID="rptBillingRecords">
            <ItemTemplate>
                <tr>
                    <td><%# Eval("TransactionId") %></td>
                    <td><%# Eval("TransactionDate") %></td>
                    <td><%# Eval("ProductName") %></td>
                    <td><%# Eval("ProductSku") %></td>
                    <td>$<%# Eval("TotalPrice") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <br />
    <br />
</asp:Content>