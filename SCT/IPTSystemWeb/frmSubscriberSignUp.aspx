<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSubscriberSignUp.aspx.vb" Inherits="WebForms_frmSubscriberSignUp" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Subscriber - Sign Up</title>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="padding-right: 0px; padding-left: 0px;
            padding-bottom: 0px; margin: 0px; padding-top: 0px; height: 650px" width="100%">
            <tr>
                <td bgcolor="#000000" colspan="2" style="padding-right: 0px; padding-left: 0px; font-weight: bold;
                    font-size: 20pt; padding-bottom: 0px; margin: 0px; color: white; padding-top: 0px;
                    font-style: italic; font-family: 'Comic Sans MS'; height: 50px">
                    &nbsp; IP Tracking System</td>
            </tr>
            <tr>
                <td style="height: 570px" align="center" valign="top">
                    <br />
                    <table style="width: 600px;">
                        <tr>
                            <td style="border-top: silver 1px solid">
                                <br />
                                <table style="width: 400px">
                                    <tr>
                                        <td align="left" colspan="2" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Web Login</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Login:&nbsp;</td>
                                        <td align="left" style="width: 200px">
                                            &nbsp;<asp:TextBox ID="txtWebLogin" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*  The Login is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtWebLogin">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtWebLogin"
                                                    Display="Dynamic" ErrorMessage="* Invalid Login Format" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w{1,50}">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Password:&nbsp;</td>
                                        <td align="left" style="width: 200px">
                                            &nbsp;<asp:TextBox ID="txtWebPassword" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" TextMode="Password" MaxLength="12"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*  The Password is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtWebPassword">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtWebPassword"
                                                    Display="Dynamic" ErrorMessage="* Invalid Password Format" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w{4,12}">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Confirm Password:&nbsp;
                                        </td>
                                        <td align="left" style="width: 200px">
                                            &nbsp;<asp:TextBox ID="txtWebConfirm" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" TextMode="Password" MaxLength="12"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*  The Confirm Password is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtWebConfirm">*</asp:RequiredFieldValidator><asp:CompareValidator
                                                    ID="CompareValidator1" runat="server" ControlToCompare="txtWebPassword" ControlToValidate="txtWebConfirm"
                                                    ErrorMessage="* The supplied passwords do not match" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed">*</asp:CompareValidator></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid">
                                <br />
                                <table style="width: 400px">
                                    <tr>
                                        <td align="left" colspan="2" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Computer Login</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Serial Number:&nbsp;</td>
                                        <td align="left" style="width: 200px">
                                            &nbsp;<asp:TextBox ID="txtSerialNbr" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*  The Serial Number is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtSerialNbr">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtSerialNbr"
                                                    Display="Dynamic" ErrorMessage="* Invalid Serial Number Format" Font-Bold="True"
                                                    Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w{1,50}">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Password:&nbsp;</td>
                                        <td align="left" style="width: 200px">
                                            &nbsp;<asp:TextBox ID="txtComputerPassword" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" TextMode="Password" MaxLength="12"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*  The Password is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtComputerPassword">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtComputerPassword"
                                                    Display="Dynamic" ErrorMessage="* Invalid Password Format" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w{4,12}">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Confirm Password:&nbsp;
                                        </td>
                                        <td align="left" style="width: 200px">
                                            &nbsp;<asp:TextBox ID="txtComputerConfirm" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" TextMode="Password" MaxLength="12"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*  The Confirm Password is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtComputerConfirm">*</asp:RequiredFieldValidator><asp:CompareValidator
                                                    ID="CompareValidator2" runat="server" ControlToCompare="txtComputerPassword"
                                                    ControlToValidate="txtComputerConfirm" ErrorMessage="* The supplied passwords do not match"
                                                    Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed">*</asp:CompareValidator></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid">
                                <br />
                                <table style="width: 400px">
                                    <tr>
                                        <td align="left" colspan="2" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Contact Information</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            E-mail:&nbsp;</td>
                                        <td align="left" style="width: 200px">
                                            &nbsp;<asp:TextBox ID="txtEmail" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*  The E-mail field is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtEmail">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEmail"
                                                    Display="Dynamic" ErrorMessage="* Invalid E-mail Format" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid">
                                <br />
                                <table style="width: 400px">
                                    <tr>
                                        <td align="left" colspan="3" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Your Information</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            First Name:&nbsp;
                                        </td>
                                        <td align="left" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtFirstName" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*  The First Name is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtFirstName">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Last Name:&nbsp;
                                        </td>
                                        <td align="left" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtLastName" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*  The Last Name is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtLastName">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial; height: 47px;">
                                            Sexo:&nbsp;
                                        </td>
                                        <td align="left" style="width: 160px; height: 47px;">
                                            <asp:RadioButtonList ID="rblSex" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" RepeatDirection="Horizontal" Width="136px">
                                                <asp:ListItem>Male</asp:ListItem>
                                                <asp:ListItem>Female</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="left" style="width: 39px; height: 47px">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="rblSex"
                                                ErrorMessage="*  The Sex is required" Font-Bold="True" Font-Names="Arial" Font-Size="X-Large"
                                                ForeColor="OrangeRed">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Age:&nbsp;
                                        </td>
                                        <td align="left" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtAge" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" MaxLength="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*  The Age is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtAge">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtAge" Display="Dynamic"
                                                    ErrorMessage="* Invalid Age Format" Font-Bold="True" Font-Names="Arial" Font-Size="X-Large"
                                                    ForeColor="OrangeRed" ValidationExpression="\d{1,3}">*</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Occupation:&nbsp;
                                        </td>
                                        <td align="left" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtOccupation" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*  The Occupation is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtOccupation">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            City:&nbsp;</td>
                                        <td align="left" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtCity" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*  The City is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtCity">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            State:&nbsp;</td>
                                        <td align="left" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtState" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*  The State is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtState">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial; height: 31px;">
                                            Country:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="height: 31px">
                                            &nbsp;<asp:TextBox ID="txtCountry" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="150px" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*  The Country is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtCountry">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid; border-bottom: silver 1px solid">
                                <br />
                                <asp:Button ID="cmdDownload" runat="server" BackColor="DimGray" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="1px" Enabled="False" Font-Names="Arial" Font-Size="9pt"
                                    ForeColor="White" Text="Download IP Tracking System" CausesValidation="False" /><br />
                                <cc1:MsgBox ID="MsgBox" runat="server" />
                                <br />
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed"
                        Width="600px" />
                    <br />
                    <asp:Button ID="cmdOk" runat="server" BackColor="DimGray" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" ForeColor="White" Text="Ok"
                        Width="80px" />
                    &nbsp;
                    <asp:Button ID="cmdCancel" runat="server" BackColor="DimGray" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" ForeColor="White"
                        Text="Cancel" Width="80px" CausesValidation="False" /><br />
                    <br />
                    <br />
                    <br />
                   
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
