<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IP Tracking System</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%; height: 100%">
            <tr>
                <td align="center" valign="middle">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td align="left" style="font-weight: normal; font-size: 16pt; width: 550px; color: white;
                                font-family: 'Comic Sans MS'; height: 40px; border-top: silver 1px solid; background-color: #000000;">
                                &nbsp;
                                IP Tracking System</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-weight: normal; font-size: 12pt; color: black; font-family: 'Comic Sans MS'; height: 38px; width: 550px;" valign="bottom">
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                Sign In as:</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-weight: normal; width: 550px; height: 50px">
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp;&nbsp;
                                <asp:RadioButtonList ID="rblSignInAs" runat="server" Font-Size="10pt" RepeatLayout="Flow"
                                    Width="360px" Font-Names="Arial">
                                    <asp:ListItem Value="Advertiser">Advertiser: Advertising for your company, product, etc.</asp:ListItem>
                                    <asp:ListItem Value="Subscriber">Subscriber: Know your computer's location</asp:ListItem>
                                </asp:RadioButtonList><br />
                                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblSignInAs"
                                    ErrorMessage="You must select if you want to enter like advertiser or subscriber"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="OrangeRed">You must select if you want to enter like advertiser or subscriber</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="center" style="border-top: silver 1px solid; font-weight: normal; font-size: 12pt;
                                width: 550px; color: black; border-bottom: silver 1px solid; font-family: 'Comic Sans MS';
                                height: 50px">
                                <asp:Button ID="cmdEnter" runat="server" BackColor="DimGray" BorderColor="Black"
                                    BorderStyle="Outset" BorderWidth="1px" Font-Bold="False" Font-Names="Arial" Font-Size="9pt"
                                    ForeColor="White" Text="Enter" Width="100px" /></td>
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
