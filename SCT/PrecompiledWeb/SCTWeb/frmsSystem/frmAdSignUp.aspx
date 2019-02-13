<%@ page language="VB" autoeventwireup="false" inherits="frmsSystem_frmAdSignUp, App_Web_kmxegdk4" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IP Tracking System - Advertiser Sign Up</title>
<script type="text/javascript" src="../JScript/JScript.js"></script>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" onload="MM_preloadImages('../images/top_image2.gif')">
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
                                <asp:HyperLink ID="lnkAdvertiserSignIn" runat="server" NavigateUrl="~/frmsSystem/frmAdSignIn.aspx">Sign In.</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="left" colspan="2" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Create account</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            *Login:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdAccountLogin" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="50"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAdAccountLogin" runat="server" ErrorMessage="*  The Login is required"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtAdAccountLogin" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="rvAdaccountLogin" runat="server" ControlToValidate="txtAdAccountLogin"
                                                    Display="Dynamic" ErrorMessage="* Invalid Login Format" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w+" SetFocusOnError="True">*</asp:RegularExpressionValidator><asp:CustomValidator ID="ctvAdAccountLogin" runat="server" ErrorMessage="* Login is assigned to another" ControlToValidate="txtAdAccountLogin" Display="Dynamic" Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            *Password:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdAccountWebPassword" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" TextMode="Password" MaxLength="12"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAdAccountWebPassword" runat="server" ErrorMessage="* A password writes and, next, it returns to write it to confirm it."
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtAdAccountWebPassword" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="rvAdAccountWebPassword" runat="server" ControlToValidate="txtAdAccountWebPassword"
                                                    Display="Dynamic" ErrorMessage="* Invalid Password Format" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w{6,}" SetFocusOnError="True">*</asp:RegularExpressionValidator><asp:CompareValidator
                                                    ID="cpvAdAccountWebConfirm" runat="server" ControlToCompare="txtAdAccountWebPasswordConfirm" ControlToValidate="txtAdAccountWebPassword"
                                                    ErrorMessage="* The supplied passwords do not match" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed" Display="Dynamic" SetFocusOnError="True">*</asp:CompareValidator></td>
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
                                            &nbsp;<asp:TextBox ID="txtAdAccountWebPasswordConfirm" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" TextMode="Password" MaxLength="12"></asp:TextBox>
                                            </td>
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
                                            Company Information</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 150px; font-family: Arial">
                                            Name:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 350px">
                                            &nbsp;<asp:TextBox ID="txtAdAccountCompanyName" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="300px" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAdAccountCompanyName" runat="server" ControlToValidate="txtAdAccountCompanyName"
                                                Display="Dynamic" ErrorMessage="*  The Company Name is required" Font-Bold="True"
                                                Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 150px; font-family: Arial">
                                            Note:&nbsp;<br />
                                            <br />
                                        </td>
                                        <td align="left" colspan="2" style="width: 350px">
                                            &nbsp;<asp:TextBox ID="txtAdAccountCompanyNote" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="300px" MaxLength="1000" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed"
                        Width="600px" Font-Bold="True" />
                    &nbsp;<br />
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
