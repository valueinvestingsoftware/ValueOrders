<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProductList.aspx.cs" Inherits="ValueOrders.UserProductList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style = "background-image: url(/Images/order.png); background-size:contain; background-position:center">
    <form id="form1" runat="server">
        <div align ="center">
            <br />
            <br />
            <asp:GridView ID="GridViewProductList" runat="server" OnRowDataBound = "GridView1_RowDataBound" 
                BackColor ="#FF6699" BorderColod ="333333" BorderWith ="5px" OnDataBound="GridViewProductList_DataBound" OnRowCommand="GridViewProductList_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText ="ItemImage">
                        <ItemTemplate>
                          <%--  <asp:Image ID ="Image1" runat="server" ImageUrl='<%# Eval("Image") %>' Height ="105px" Width="105px"/>--%>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("Image") %>' Height ="105px" Width="105px" CommandName = "ViewPDF" CommandArgument = '<%# Eval("OrderId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="DarkOrchid" ForeColor="White" />
            </asp:GridView>
            <br />
            <br />
            <div align="center">
            <asp:HyperLink ID ="HyperLink1" runat="server" NavigateUrl="~/Default.aspx" BackColor = "DarkOrchid" Fond-Bold="True" Font-Size="Large"
   ForeColor       = "WhiteSmoke" BorderColor = "White" >Back to HOME Page</asp:HyperLink>

        </div>
    </form>
</body>
</html>
