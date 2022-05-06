<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddToCart.aspx.cs" Inherits="ValueOrders.AddToCart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div align ="center">
            <h2 style ="text-decoration: underline overline blink; color: #0026ff">You have the following products in your cart</h2>
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="Comic Sans MS" Font-Size="Large" ForeColor="#6600FF" NavigateUrl="~/Default.aspx">Continue shopping</asp:HyperLink><br />
            <br />
            <asp:GridView ID="GridViewAddToCart" runat="server" AutoGenerateColumns="False" BackColor="#FFFFCC" BorderColor="#FFCC99" BorderWidth="4px" EmptyDataText="No items in shoping cart" Height="250px" ShowFooter="True" Width="1100px" OnRowDeleting="GridViewAddToCart_RowDeleting" OnSelectedIndexChanged="GridViewAddToCart_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="pSec" HeaderText="Sec">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="id" HeaderText="Item id">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Category" HeaderText="Item name">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:ImageField DataImageUrlField="ImagePath" HeaderText="Image">
                        <ControlStyle Height="300px" Width="340px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:ImageField>
                    <asp:BoundField DataField="Pprice" HeaderText="Price">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PQuantity" HeaderText="Qty">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="pTotalPrice" HeaderText="TotalPrice">
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
                </Columns>
                <FooterStyle BackColor="#FFCC99" Font-Bold="True" Font-Italic="True" ForeColor="#CC00FF" />
                <HeaderStyle BackColor="#FF9933" Font-Bold="True" ForeColor="#0033CC" />
            </asp:GridView>
            <br />
            <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" Font-Bold="True" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
