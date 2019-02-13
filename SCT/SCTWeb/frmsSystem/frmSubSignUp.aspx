<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSubSignUp.aspx.vb" Inherits="frmsSystem_frmSubSignUp" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IP Tracking System - Subscriber Sign Up</title>
<script type="text/javascript" src="../JScript/JScript.js"></script>    
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0"  onload="MM_preloadImages('../images/top_image2.gif')">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="padding-right: 0px; padding-left: 0px;
            padding-bottom: 0px; margin: 0px; padding-top: 0px; height: 650px" width="100%">
            <tr>
                <td bgcolor="#000000" colspan="2" style="padding-right: 0px; padding-left: 0px; font-weight: bold;
                    font-size: 20pt; padding-bottom: 0px; margin: 0px; color: white; padding-top: 0px;
                    font-style: italic; font-family: 'Comic Sans MS'; height: 50px"><a href="#" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image5','','../images/top_image2.gif',1)"><img src="../Images/top_image1.gif" name="Image5" height="125" border="0" id="Image5" style="width: 100%" /></a>
                    </td>
            </tr>
            <tr>
                <td style="height: 570px; font-size: 9pt; font-family: Arial;" align="center" valign="top">
                    <br />
                    <br />
                    <table style="width: 600px;" id="TABLE1">
                        <tr>
                            <td align="left">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="Sign up in IP Tracking System" Font-Names="Comic Sans MS"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>*Required fields. &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:HyperLink ID="lnkSubscriberSignIn" runat="server" NavigateUrl="~/frmsSystem/frmSubSignIn.aspx">Sign In.</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="left" colspan="2" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Create your account</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            *Login:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtLogin" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLogin" runat="server" ErrorMessage="*  The Login is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtLogin">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="revLogin" runat="server" ControlToValidate="txtLogin"
                                                    Display="Dynamic" ErrorMessage="* Invalid Login Format" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w+">*</asp:RegularExpressionValidator><asp:CustomValidator ID="ctvLogin" runat="server" ErrorMessage="* Login is assigned to another" ControlToValidate="txtLogin" Display="Dynamic" Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            *Password:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtWebPassword" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" TextMode="Password" MaxLength="12"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvWebPassword" runat="server" ErrorMessage="* A password writes and, next, it returns to write it to confirm it."
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtWebPassword">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="revWebPassword" runat="server" ControlToValidate="txtWebPassword"
                                                    Display="Dynamic" ErrorMessage="* Invalid Password Format" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w{6,}">*</asp:RegularExpressionValidator><asp:CompareValidator
                                                    ID="cpvWebConfirm" runat="server" ControlToCompare="txtWebConfirm" ControlToValidate="txtWebPassword"
                                                    ErrorMessage="* The supplied passwords do not match" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed">*</asp:CompareValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 8pt;">
                                            &nbsp;&nbsp; Six characters minimum; distinguishes<br />
                                            &nbsp;&nbsp; between capital letters and small letters.</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            *Confirm Password:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtWebConfirm" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" TextMode="Password" MaxLength="12"></asp:TextBox>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="left" colspan="2" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Computer information</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            *Serial Number:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtSerialNbr" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSerialNbr" runat="server" ErrorMessage="*  The Serial Number is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtSerialNbr">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="revSerialNbr" runat="server" ControlToValidate="txtSerialNbr"
                                                    Display="Dynamic" ErrorMessage="* Invalid Serial Number Format" Font-Bold="True"
                                                    Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w+">*</asp:RegularExpressionValidator><asp:CustomValidator
                                                        ID="ctvSerialNbr" runat="server" ControlToValidate="txtSerialNbr" Display="Dynamic"
                                                        ErrorMessage="* Computer Serial Number is assigned to another" Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            *Password:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtComputerPassword" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" TextMode="Password" MaxLength="12"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvComputerPassword" runat="server" ControlToValidate="txtComputerPassword"
                                                ErrorMessage="* A password writes and, next, it returns to write it to confirm it."
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="revComputerPassword" runat="server" ControlToValidate="txtComputerPassword"
                                                    Display="Dynamic" ErrorMessage="* Invalid Password Format" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w{6,}">*</asp:RegularExpressionValidator><asp:CompareValidator
                                                    ID="cpvComputerConfirm" runat="server" ControlToCompare="txtComputerConfirm"
                                                    ControlToValidate="txtComputerPassword" ErrorMessage="* The supplied passwords do not match"
                                                    Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed">*</asp:CompareValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                        </td>
                                        <td align="left" style="width: 250px; font-size: 8pt;">
                                            &nbsp;&nbsp; &nbsp;Six characters minimum; distinguishes<br />
                                            &nbsp; &nbsp; between capital letters and small letters.</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            *Confirm Password:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtComputerConfirm" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" TextMode="Password" MaxLength="12"></asp:TextBox>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="left" colspan="2" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Contact information</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            E-mail:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtEmail" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="100"></asp:TextBox>
                                            <asp:RegularExpressionValidator
                                                    ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                                    Display="Dynamic" ErrorMessage="* Invalid E-mail Format" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid; border-bottom: silver 1px solid;">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="left" colspan="3" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Your information</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            First Name:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtFirstName" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="100"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Last Name:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtLastName" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="100"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial;">
                                            Sex:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px;">
                                            &nbsp;<asp:DropDownList ID="ddlSex" runat="server" Width="100px">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Age:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            &nbsp;<asp:DropDownList ID="ddlAge" runat="server" Width="50px">
                                            </asp:DropDownList>&nbsp;&nbsp;&nbsp; Years.</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Occupation:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            &nbsp;<asp:DropDownList ID="ddlOccupation" runat="server" Width="206px">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Country or Region:&nbsp;</td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            &nbsp;<asp:DropDownList ID="ddlCountry" runat="server" Width="206px" AutoPostBack="True">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            <div id="result_box" dir="ltr">
                                                State or Province:&nbsp;</div>
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            &nbsp;<asp:DropDownList ID="ddlState" runat="server" Width="206px">
                                            </asp:DropDownList></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed"
                        Width="600px" Font-Bold="True" />
                    <br />
                    <asp:Button ID="cmdOk" runat="server" Font-Names="Verdana" Text="Ok"
                        Width="80px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="cmdCancel" runat="server" Font-Names="Verdana"
                        Text="Cancel" Width="80px" CausesValidation="False" /><br />
                                <cc1:MsgBox ID="MsgBox" runat="server" />
                    <br />
                   
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
