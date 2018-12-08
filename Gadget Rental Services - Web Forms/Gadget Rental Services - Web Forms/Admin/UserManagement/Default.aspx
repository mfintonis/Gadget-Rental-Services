<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="Gadget_Rental_Services___Web_Forms.Admin.UserManagement.Default" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <asp:Literal ID="ltrlError" runat="server"></asp:Literal>
    <br />
    <table style="width: 100%;">
        <tr>
            <th></th>
            <th><strong>Email</strong></th>
            <th><strong>Role</strong></th>
        </tr>
        <asp:Repeater runat="server" ID="rptUsers">
            <ItemTemplate>
                <tr>
                    <td style="max-width: 35px;">
                        <a href="/admin/usermanagement/edit?id=<%# Eval("UserID") %>">Edit</a> |
                        <a href="/admin/usermanagement/delete?id=<%# Eval("UserID") %>">Delete</a>
                    </td>
                    <td><%# Eval("Email") %></td>
                    <td><%# Eval("RoleName") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <br />
    <br />
    <asp:Button runat="server" ID="btnNew" OnClick="btnNew_Click" Text="Add User" CssClass="btn btn-default" />
</asp:Content>