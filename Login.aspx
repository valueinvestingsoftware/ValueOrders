<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ValueOrders.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>     
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    <table align = "center" style="background-color: #05e9fe">
        <tr style = "color: #ff0000">
            <td colspan="2" style = "text-align: center">
                 <h2>Login Page</h2></td>
        </tr>
        <tr style = "color: #ff0000">
            <td> <b>Email</b></td>
            <td>
                <b><asp:TextBox ID="txtEmail" runat="server" Width="280px" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Group1" ControlToValidate="txtEmail" ErrorMessage="Please type your email" ForeColor="White">*</asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Group2" ControlToValidate="txtEmail" ErrorMessage="Please type your email" ForeColor="White">*</asp:RequiredFieldValidator>
                 </b>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="Group1" ControlToValidate="txtEmail" ErrorMessage="You did not type a valid email address!" ForeColor="White" ValidationExpression="(?:[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*|&quot;(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*&quot;)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])">*</asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="Group2" ControlToValidate="txtEmail" ErrorMessage="You did not type a valid email address!" ForeColor="White" ValidationExpression="(?:[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*|&quot;(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*&quot;)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr style = "color: #ff0000">
            <td class="auto-style8"> <b>Password</b></td>
            <td class="auto-style7">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="281px"></asp:TextBox>           
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Group1" ControlToValidate="txtPassword" ErrorMessage="Please type a password" ForeColor="White">*</asp:RequiredFieldValidator>               
            </td>
        </tr>
        <tr>
             <td style = "text-align:center">
                 <asp:HyperLink ID="HyperLinkSignUp" runat="server" Font-Size="Small" ForeColor="Blue" NavigateUrl="~/Register.aspx">SignUp</asp:HyperLink>
            </td>
            <td style = "text-align:center">
                <asp:Button ID="btnSignIn" runat="server" Text="Sign in" ValidationGroup="Group1" OnClick="btnSignIn_Click" Height="33px" />
            </td>
             <td style = "text-align:center">
                 <asp:LinkButton ID="LinkButtonForgotPassword" Font-Size="Small" ValidationGroup="Group2" ForeColor="Blue" OnClick="LinkButtonForgotPassword_Click" runat="server">Forgot Password</asp:LinkButton>                 
            </td>
        </tr>
         <tr>
                     <td colspan ="2">
                         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" ForeColor="Red" HeaderText="Fix the following errors:" Width="500px"  Font-Size="Small" />   
                         <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group2" ForeColor="Red" HeaderText="Fix the following errors:" Width="500px"  Font-Size="Small" />   
                    </td>    
                </tr>
        <tr>
            <td colspan="2" style = "text-align: center">
                <asp:Label ID="lblSignIn" runat="server"></asp:Label>
            </td>           
        </tr>
    </table>
    </form>
</body>
</html>
