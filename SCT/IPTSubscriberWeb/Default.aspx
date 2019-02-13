<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>System Login Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%; height: 100%">
            <tr>
                <td align="center" valign="middle" style="height: 477px">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <table style="width: 100%; height: 200pt">
                        <tr>
                            <td align="right" style="border-right: silver 1px solid; font-weight: bold; font-size: 15pt; color: black; font-family: 'Comic Sans MS'">
                                IP Tracking System &nbsp; &nbsp; &nbsp;&nbsp;<br />
                                <br />
                                &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<asp:Button ID="cmdSignUp" runat="server" Text="Sign Up Now" BackColor="DimGray" BorderColor="Black" BorderStyle="Outset" BorderWidth="1px" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" ForeColor="White" Width="120px" CausesValidation="False" />
                                &nbsp; &nbsp; &nbsp;&nbsp;</td>
                            <td align="left" style="font-weight: normal; font-size: 12pt; color: black; font-family: 'Comic Sans MS'">
                                &nbsp;&nbsp; Sign In as Subscriber<br />
                                <table>
                                    <tr>
                                        <td align="right" style="width: 75px; height: 35px">
                                            <asp:Label ID="lblUserName" runat="server" Font-Names="Arial" Font-Size="10pt" Font-Strikeout="False"
                                                Text="User Name:"></asp:Label></td>
                                        <td style="width: 250px; height: 35px">
                                            <asp:TextBox ID="txtUserName" runat="server" Font-Names="Arial" Font-Size="10pt" MaxLength="50"
                                                Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 75px; height: 40px">
                                            <asp:Label ID="lblPassword" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Password:"></asp:Label></td>
                                        <td style="width: 250px; height: 40px">
                                            <asp:TextBox ID="txtPassword" runat="server" Font-Names="Arial" Font-Size="10pt"
                                                MaxLength="12" TextMode="Password" Width="200px"></asp:TextBox></td>
                                    </tr>
                                </table>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:CustomValidator ID="CustomValidator" runat="server" ErrorMessage="Invalid Password or User Name"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="OrangeRed" Height="18px"
                                    Width="184px">Invalid Password or User Name</asp:CustomValidator><br />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:Button ID="cmdSignIn" runat="server" BackColor="DimGray" BorderColor="Black"
                                    BorderStyle="Outset" BorderWidth="1px" Font-Bold="False" Font-Names="Arial" Font-Size="9pt"
                                    ForeColor="White" Text="Sign In" Width="120px" /><cc1:MsgBox ID="MsgBox" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
        </table>      
    </div>
    </form>
</body>
</html>
