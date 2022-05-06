<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlaceOrder.aspx.cs" Inherits="ValueOrders.PlaceOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>   
   
    <style type="text/css">
        .auto-style1 {
            width: 620px;
            background-color:aliceblue;
        }
        .auto-style2 {
            width: 807px;
        }
        .auto-style3 {
            width: 897px;
        }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table align = "center" class="auto-style1">
                <tr>
                    <td colspan ="2">
                        <asp:Label ID="Label1" runat="server" Text="Card details" Font-Bold="True" Font-Size="Larger" ForeColor="Green"></asp:Label>
                    </td>                                 
                </tr>                
                <tr>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtFirstName" runat="server" BorderColor="Black" BorderWidth="2px" Font-Bold="True" Font-Size="Small" placeholder ="First Name" Width="200px"></asp:TextBox>
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
                        <asp:Image ID="Image1" runat="server" Height="194px" ImageUrl="~/creditcards.png" Width="652px"/></td>                               
                </tr>               
                <tr>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtCard" runat="server" BorderColor="Black" BorderWidth="2px" Font-Bold="True" Font-Size="Small" placeholder ="Card Number: 16 Digits" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Card field is empty" ControlToValidate="txtCard" ForeColor="White"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="The card number you typed is not correc!" ControlToValidate="txtCard" ForeColor="White" ValidationExpression = "(^4[0-9]{12}(?:[0-9]{3})?$)|(^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$)|(3[47][0-9]{13})|(^3(?:0[0-5]|[68][0-9])[0-9]{11}$)|(^6(?:011|5[0-9]{2})[0-9]{12}$)|(^(?:2131|1800|35\d{3})\d{11}$)"></asp:RegularExpressionValidator>
                    </td>  
                     
                </tr>                 
                <tr>
                     <td class="auto-style3">
                        <asp:TextBox ID="txtExpiryDate" runat="server" BorderColor="Black" BorderWidth="2px" Font-Bold="True" Font-Size="Small" placeholder ="Expire Date: MM/YY(Eg.062021)" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Expiry date is empty!" ControlToValidate="txtExpiryDate" ForeColor="White"></asp:RequiredFieldValidator>                        
                    </td>      

                    <td class="auto-style2">
                        <asp:TextBox ID="txtCVV" runat="server" BorderColor="Black" BorderWidth="2px" Font-Bold="True" Font-Size="Small" placeholder ="CVV: 3 Digits" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="CVV field is empty" ControlToValidate="txtCVV" ForeColor="White"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="CVV number must have 3 digits!" ControlToValidate="txtCVV" ForeColor="White" ValidationExpression = "^[0-9]{3,4}$"></asp:RegularExpressionValidator>
                    </td>   
                </tr>                                 
                <tr>
                     <td class="auto-style3"; colspan ="2">
                        <asp:TextBox ID="txtBillingAddress" runat="server" BorderColor="Black" BorderWidth="2px" Font-Bold="True" Font-Size="Small" placeholder ="Billing Address" TextMode="MultiLine" Width="631px" Height="34px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Address is empty!" ControlToValidate="txtBillingAddress" ForeColor="White">*</asp:RequiredFieldValidator>                        
                    </td>    
                </tr>
                 <tr>
                     <td colspan ="2" style = "text-align: center">
                         <asp:Button ID="btnPayment" runat="server" Text="Make Payment" OnClick="Button1_Click" style="height: 29px" />                  
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
                     <td class="auto-style2">                     
                         <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Default.aspx">Home Page</asp:HyperLink>     
                    </td>  
                </tr>

            </table>

        </div>
    </form>
</body>
</html>
