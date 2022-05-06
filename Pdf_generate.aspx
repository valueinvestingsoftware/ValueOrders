<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pdf_generate.aspx.cs" Inherits="ValueOrders.Pdf_generate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    </head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnDownload" runat="server" Text="Download Invoice" Font-Bold="True" Font-Size="Larger" OnClick="btnDownload_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#3366FF" NavigateUrl="~/Default.aspx">Go to Home page</asp:HyperLink>
            <asp:Panel ID="Panel1" runat="server">
            <table border ="1">
                <tr>
                    <td style = "text-align:center">
                        <h2 style = "text-align:center">Retail invoice</h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        Order No:
                        <asp:Label ID="lblOrderId" runat="server" Text="Label"></asp:Label>
                        <br /><br />
                         Order Date:
                        <asp:Label ID="lblOrderDate" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                     Buyer address: <br />
                                    <asp:Label ID="lblBuyerAddress" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Seller address: <br /><br />
                                    Here goes the address of my company
                                </td>
                            </tr>                           
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="1000px">
                            <Columns>
                                <asp:BoundField DataField="pSec" HeaderText="Sec">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="id" HeaderText="Id">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Category" HeaderText="Item">
                                <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="pQuantity" HeaderText="Quantity">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Pprice" HeaderText="Price">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="pTotalPrice" HeaderText="Total Price">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style ="text-align: right">
                        Grand Total:
                        <asp:Label ID="lblGrandTotal" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style = "text-align: center">
                        This is a computer generated invoice.
                    </td>
                </tr>
            </table>
          </asp:Panel>
        </div>
    </form>
</body>
</html>
