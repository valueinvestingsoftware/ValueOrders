<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ValueOrders.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>  
    <style type="text/css">
        .auto-style3 {
            width: 184px;
        }
        .auto-style5 {
            width: 152px;
        }
        .auto-style6 {
            height: 46px;
        }
        .auto-style7 {
            width: 766px;
        }
        .auto-style8 {
            height: 529px;
        }
    </style>
</head>
<body style="width: 823px; margin-left: 221px">
    <form id="form1" runat="server">
        <div class="auto-style8">
            <table align = "center" style="background-color: #CCFFCC">
                <tr>
                    <td colspan="2" align="center">
                    <h2>Registration Page</h2></td>                    
                </tr>
                <tr>
                    <td class="auto-style3"><b>First Name:</b></td>    
                    <td class="auto-style7">
                    <b><asp:TextBox ID="txtName" runat="server" Width="395px" CssClass="auto-style5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVfirstname" runat="server" ControlToValidate="txtName" ErrorMessage="First name is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </b>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Only characters are allowed" ValidationExpression="^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&amp;*(){}|~&lt;&gt;;:[\]]{2,}$" ForeColor="Red">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"><b>Last Name:</b></td>  
                     <td class="auto-style7">
                         <b><asp:TextBox ID="txtLastName" runat="server" Width="394px" CssClass="auto-style5"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RFVlastname" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last name is empty" ForeColor="#FF0066">*</asp:RequiredFieldValidator>
                        </b>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtLastName" ErrorMessage="Only characters are allowed" ValidationExpression="^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&amp;*(){}|~&lt;&gt;;:[\]]{2,}$" ForeColor="Red">*</asp:RegularExpressionValidator>
                        </td>
                </tr>
                <tr>
                    <td class="auto-style3"><b>Email:</b></td>  
                     <td class="auto-style7">
                         <b><asp:TextBox ID="txtEmail" runat="server" Width="394px" CssClass="auto-style5" TextMode="Email"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RFVemail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is empty" ForeColor="#FF0066">*</asp:RequiredFieldValidator>
                        </b>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail" ErrorMessage="The email you typed is not valid" ValidationExpression="(?:[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*|&quot;(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*&quot;)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])" ForeColor="Red">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"><b>Address:</b></td> 
                     <td class="auto-style7">
                         <b><asp:TextBox ID="txtAddress" runat="server" Width="516px" TextMode="MultiLine"></asp:TextBox>
                        </b>
                         <asp:RequiredFieldValidator ID="RFVaddress" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"><b>Phone Number:</b></td>   
                     <td class="auto-style7">
                         <b><asp:TextBox ID="txtPhoneNumber" runat="server" Width="187px" CssClass="auto-style5"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RFVphonenumber" runat="server" ControlToValidate="txtPhoneNumber" ErrorMessage="Phone number is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </b>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPhoneNumber" ErrorMessage="Only numbers are allowed" ValidationExpression="^[0-9]*$" ForeColor="Red">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"><b>City:</b></td>   
                     <td class="auto-style7">
                         <b><asp:TextBox ID="txtCity" runat="server" Width="187px" CssClass="auto-style5"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCity" ErrorMessage="City is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </b>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCity" ErrorMessage="Only characters are allowed" ValidationExpression="^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&amp;*(){}|~&lt;&gt;;:[\]]{2,}$" ForeColor="Red">*</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"><b>Password:</b></td>  
                     <td class="auto-style7">
                         <b><asp:TextBox ID="txtPassword" runat="server" Width="185px" CssClass="auto-style5" TextMode="Password"></asp:TextBox>
                        </b>
                         <asp:RequiredFieldValidator ID="RFVpassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3"><b>Confirm Password:</b></td>    
                     <td class="auto-style7">
                         <b><asp:TextBox ID="txtConfirmPassword" runat="server" Width="184px" CssClass="auto-style5" TextMode="Password"></asp:TextBox>
                        </b>
                         <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Password is not a match" ForeColor="Red">*</asp:CompareValidator>
                         <asp:RequiredFieldValidator ID="RFVconfirmpassword" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm password is empty" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
              
                  <tr>
                    <td align="right" class="auto-style6">
                        <asp:HyperLink ID="HyperLinkHome" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" NavigateUrl="~/Default.aspx">Home</asp:HyperLink>   
                      </td>

                       <td colspan="2" align="center" class="auto-style6">
                   <asp:Button ID="btnSave" runat="server" Text="Save" Width="190px" OnClick="Button1_Click" />   
                    &nbsp;</td>
                </tr>
               
                <tr>
                <td align="right" class="auto-style6">
                        <asp:HyperLink ID="HyperLinkSignIn" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" NavigateUrl="~/Login.aspx">Sign in</asp:HyperLink>   
                      </td>
                </tr>

                  <tr>
                     <td colspan ="2">
                         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="Fix the following errors:" Width="817px"  Font-Size="Small" />       
                    </td>    
                </tr>
                 <tr>
                    <td align="center" colspan="2">
                        <asp:Label ID="lblRegistration" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Large" ForeColor="Blue"></asp:Label>
                     </td>
                </tr>
                
            </table>
        </div>       
    </form>
</body>
</html>
