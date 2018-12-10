<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="Default.aspx.cs" Inherits="Gadget_Rental_Services___Web_Forms.Store.Default" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <h2>Store</h2>
    <br />
    <br />
    <table style="width: 100%;">
        <tr>
            <th></th>
            <th><strong>Name</strong></th>
            <th><strong>Quantity Available</strong></th>
            <th><strong>Price</strong></th>
            <th><strong></strong></th>
        </tr>
        <asp:Repeater runat="server" ID="rptStoreItems">
            <ItemTemplate>
                <tr>
                    <td style="max-width: 45px;">
                        <a style="<%# Int32.Parse(Eval("ItemQuantityAvailable").ToString()) <= 0 ? "display: none;" : "" %>" href="/store/rent?id=<%# Eval("Id") %>">Rent</a>
                        <p style="<%# Int32.Parse(Eval("ItemQuantityAvailable").ToString()) <= 0 ? "" : "display: none;" %> margin: 0;">Not Available</p>
                    </td>
                    <td><%# Eval("ItemName") %></td>
                    <td><%# Eval("ItemQuantityAvailable") %></td>
                    <td>$<%# Eval("ItemPrice") %></td>
                    <td>
                        <img src="<%# Eval("ItemImagePath") %>" style="max-width: 150px; max-height: 150px; padding-top: 10px" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>