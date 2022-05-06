
<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ValueOrders.Default1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table style = "width:1150px; height: 30px">
        <tr style ="background-color: #5f98f3">
            <td colspan="2" style = "text-align: right">
                <asp:Label ID="labelUsername" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Calibri" style = "text-align:left"></asp:Label>
                <asp:HyperLink ID="HyperLinkLogIn" runat="server" Font-Bold="True" Font-Names="Century" NavigateUrl="~/Login.aspx">Click to login</asp:HyperLink>
                <asp:Button ID="ButtonLogOut" runat="server" Text="Log out" BackColor="#FF5050" Font-Bold="True" ForeColor="White" OnClick="ButtonLogOut_Click" />
            </td>
            <td style ="text-align:right">                
                <asp:DropDownList ID="ProductCategories" runat="server" AutoPostBack="True" OnSelectIndexChanged="ProductCategories_SelectedIndexChanged" BackColor="#5F98F3" Font-Bold="True" Font-Size="Medium" ForeColor = "White">
                </asp:DropDownList>&nbsp;
                <asp:TextBox ID="TextBoxSearchItem" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonSearchItem" runat="server" ImageUrl="~/filter.png" Width="31px" Height="22px" OnClick="ImageButton2_Click" />
            </td>
        </tr>
   </table>
        <asp:DataList ID="DataList1" runat="server" DataKeyField="id" DataSourceID="SqlDataSource1" Height="449px" RepeatColumns="5" RepeatDirection="Horizontal" OnItemCommand="DataList1_ItemCommand" Width="1183px">
            
            <ItemTemplate>
                <table>                                       
                     <tr>
                        <td style="text-align:center; background-color:#5f98f3">
                            <asp:Label ID="CategoryLabel" runat="server" Text='<%# Eval("Category") %>' BackColor="White" BorderWith="1px" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="Blue"/>
                        </td>
                    </tr>
                      <tr>
                        <td style="text-align:center">                           
                               <asp:Image ID="Image2" runat="server" ImageUrl='<%# Eval("ImagePath") %>' Height="171px" Width="221px"/>
                            <div class = "stock">
                                &nbsp;Stock:&nbsp;
                                <asp:Label ID="Label1" runat="server" Text='<%# (int)Eval("WebQuantity") %>' 
                                        BackColor = '<%# (int)Eval("WebQuantity") <= 3 ? System.Drawing.Color.Red : System.Drawing.Color.Green %>' Font-Size="Small"></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center; background-color:#5f98f3">
                             <asp:Label ID="PriceLabel" runat="server" Text="Price: " BackColor="White" BorderWith="1px"/>
                            <asp:Label ID="WebPriceLabel" runat="server" Text='<%# Eval("WebPrice") %>' BackColor="White" />
                        </td>
                    </tr>
                     
                     <tr>
                        <td>
                           <asp:Label ID="QtyLabel" runat="server" Text="Qty: " Font-Size="Small" />
                           <asp:DropDownList ID="DropDownList1" runat="server">
                               <asp:ListItem Value="1"></asp:ListItem>
                               <asp:ListItem>2</asp:ListItem>
                               <asp:ListItem>3</asp:ListItem>
                               <asp:ListItem>4</asp:ListItem>
                               <asp:ListItem>5</asp:ListItem>
                               <asp:ListItem>6</asp:ListItem>
                               <asp:ListItem>7</asp:ListItem>
                               <asp:ListItem>8</asp:ListItem>
                               <asp:ListItem>9</asp:ListItem>
                               <asp:ListItem>10</asp:ListItem>                               
                            </asp:DropDownList>

                        </td>

                    </tr>
                     <tr>
                        <td>                           
                               <asp:ImageButton ID="ImageButton1" runat="server" Height="41px" ImageUrl="~/placeorder.jpg" Width="56px"
                                   CommandArgument='<%# Eval("id") %>' CommandName = "AddToCart"/>
                        </td>
                    </tr>
                     <caption>
                         <br />
                         <br />
                     </caption>
                </table>               
            </ItemTemplate>
        </asp:DataList>
       
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:StocksConnectionString %>" SelectCommand="SELECT [id], [Cod], [Category], [Nivel], [ImagePath], [WebPrice], [WebQuantity] FROM [CatMap] WHERE [Nivel] = 4 AND [VisibleInApps] = 1">

    </asp:SqlDataSource>

</asp:Content>
