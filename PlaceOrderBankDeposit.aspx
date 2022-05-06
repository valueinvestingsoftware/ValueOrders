<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlaceOrderBankDeposit.aspx.cs" Inherits="ValueOrders.PlaceOrderBankDeposit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

             <table align = "center" class="auto-style1">
                <tr>
                    <td colspan ="2">
                        <asp:Label ID="Label1" runat="server" Text="Billing details" Font-Bold="True" Font-Size="Larger" ForeColor="Green"></asp:Label>
                    </td>                                 
                </tr>                
                <tr>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtFirstName" runat="server" BorderColor="Black" BorderWidth="2px" Font-Bold="True" Font-Size="Small" placeholder ="First Name" Width="177px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="First name is empty" ControlToValidate="txtFirstName" ForeColor="White"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="First name needs to contain only letters!" ControlToValidate="txtFirstName" ForeColor="White" ValidationExpression="^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&amp;*(){}|~&lt;&gt;;:[\]]{2,}$"></asp:RegularExpressionValidator>
                    </td>      
                     <td class="auto-style2">
                        <asp:TextBox ID="txtLastName" runat="server" BorderColor="Black" BorderWidth="2px" Font-Bold="True" Font-Size="Small" placeholder ="Last Name" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Last name is empty" ControlToValidate="txtLastName" ForeColor="White"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Last name needs to contain only letters!" ControlToValidate="txtLastName" ForeColor="White" ValidationExpression="^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&amp;*(){}|~&lt;&gt;;:[\]]{2,}$"></asp:RegularExpressionValidator>
                    </td>      
                </tr>
                <tr>
                    <td colspan ="2" style ="align-items: center">
                        <asp:Image ID="Image1" runat="server" Height="194px" ImageUrl="~/ImagesWebSite/jep.jpg" Width="652px"/></td>                               
                </tr>  
                  <tr> 

                  </tr>
                  <tr> 
                      <td>
                           <asp:Label ID="lblBankName" runat="server" style="font-weight:bold" Font-Size="Small" Text="Bank name:"></asp:Label>  
                           <asp:Label ID="lblBankNameAnswer" runat="server" Font-Size="Small" Text=" Cooperativa JEP"></asp:Label>  
                      </td>
                 </tr>
                 <tr> 
                      <td>
                           <asp:Label ID="lblAccountType" runat="server" style="font-weight:bold" Font-Size="Small" Text="Account type:"></asp:Label> 
                          <asp:Label ID="lblAccountTypeAnswer" runat="server" Font-Size="Small" Text=" Savings"></asp:Label> 
                      </td>
                  </tr>
                  <tr> 
                      <td>                          
                           <asp:Label ID="lblAccountNumber" runat="server" style="font-weight:bold" Font-Size="Small" Text="Account number:"></asp:Label>
                          <asp:Label ID="lblAccountNumberAnswer" runat="server" Font-Size="Small" Text=" 123456789"></asp:Label>
                      </td>
                  </tr>   
                  <tr> 
                      <td>                          
                           <asp:Label ID="lblAccountOwnerName" runat="server" style="font-weight:bold" Font-Size="Small" Text="Account owner:"></asp:Label>
                          <asp:Label ID="lblAccountOwnerNameAnswer" runat="server" Font-Size="Small" Text=" Ciro Patricio Romero Romero"></asp:Label>
                      </td>
                  </tr> 
                  <tr> 
                      <td>                          
                           <asp:Label ID="lblAccountOwnerId" runat="server" style="font-weight:bold" Font-Size="Small" Text="Account owner id:"></asp:Label>
                           <asp:Label ID="lblAccountOwnerIdAnswer" runat="server" Font-Size="Small" Text=" 0703619346"></asp:Label>
                      </td>
                  </tr> 
                  <tr> 

                  </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="HyperLinkPayPal" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="#3366FF" NavigateUrl="https://www.paypal.me/StockMarketManage" Target="_blank">Go to PayPal</asp:HyperLink>
                    </td>   
                </tr>                          
                <tr>
                     <td class="auto-style3"; colspan ="2">
                        <asp:TextBox ID="txtBillingAddress" runat="server" BorderColor="Black" BorderWidth="2px" Font-Bold="True" Font-Size="Small" placeholder ="Billing Address" TextMode="MultiLine" Width="631px" Height="34px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Address is empty!" ControlToValidate="txtBillingAddress" ForeColor="White">*</asp:RequiredFieldValidator>                        
                    </td>    
                </tr>                
                <tr>
                     <td colspan ="2">
                         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="Fix the following errors:" Width="652px" CssClass="auto-style5" Font-Size="Medium" />       
                    </td>    
                </tr>
                 <tr>
                     <td class="auto-style3">
                         <asp:HyperLink ID="HLPreviousPage" runat="server" NavigateUrl="~/AddToCart.aspx">Previous Page</asp:HyperLink>                         
                    </td>  
                     <td class="auto-style3">                     
                         <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Default.aspx">Home Page</asp:HyperLink>     
                    </td>  
                </tr>

                 <tr>
                     <td colspan ="2" style = "text-align:center">                      
                                                   
                    </td>  
                 </tr>

                 <tr>
                     <td style = "text-align:center">                      
                           Upload payment proof(image or pdf): <asp:FileUpload ID="FileImageSave" runat="server" /> <br />    
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Upload payment proof please!" ControlToValidate="FileImageSave" ForeColor="White">*</asp:RequiredFieldValidator> 
                         <asp:RegularExpressionValidator id="regpdf" text="*" errormessage="Please upload either a pdf or a jpg image" ControlToValidate="FileImageSave" ValidationExpression="^.*\.(pdf|PDF|png|PNG|gif|GIF|jpg|JPG|jpeg|JPEG|tiff|TIFF|eps|EPS)$" runat="server" />
                      </td>  
                 </tr>

                 <tr>
                     <td colspan ="2" style = "text-align:center">                      
                                                   
                    </td>  
                 </tr>

                 <tr>
                     <td style = "text-align:center">                      
                          <asp:Button ID="btnPlaceOrder" runat="server" Text="Place order" OnClick="btnPlaceOrder_Click" />                           
                    </td>    
                </tr>
            </table>

           
        </div>

            
            <p>
                 <asp:Literal ID="LitMsg" runat="server"></asp:Literal>
            </p>  



    </form>
</body>
</html>
