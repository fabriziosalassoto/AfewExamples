<%@ page language="VB" autoeventwireup="false" inherits="frmsSystem_frmSubSignIn, App_Web_kmxegdk4" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Notebook Detector - Subscriber Sign In</title>
<style type="text/css">
<!--
body { 
	background-color: #000000; 
}
.style2 {
	color: #FFFFFF;
	font-style: italic;
	font-weight: bold;
}

.style3 {
	color: #FFFFFF;
	font-style: italic;
	font-weight: bold;
}td img {display: block;}td img {display: block;}
.style4 {
	color: #000000;
	font-family: Verdana, Arial, Helvetica, sans-serif;
}
.style6 {
	font-size: 12;
	color: #FFFFFF;
	font-family: Verdana, Arial, Helvetica, sans-serif;
}
.style7 {color: #000000}
.style8 {
	color: #000000
}
.style9 {font-family: Verdana, Arial, Helvetica, sans-serif}
-->
</style>
<script type="text/javascript" src="../JScript/JScript.js"></script>    
</head>
<body onload="MM_preloadImages('../images/top_image2.gif')">
    <form id="form1" runat="server">
    <div>
<table width="881" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td colspan="2"><a href="#" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image5','','../images/top_image2.gif',1)"><img src="../Images/top_image1.gif" name="Image5" width="885" height="125" border="0" id="Image5" /></a></td>
  </tr>
  <tr>
    <td width="357" align="center" valign="top" bgcolor="#FFFFFF"><p>&nbsp;</p>
      <table width="109" height="59" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td bgcolor="#FFFFFF"><p><img src="../images/Logo-300X169.gif" alt="Logo" width="300" height="169" /></p>
            <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Names="Verdana"
                Font-Size="10pt" NavigateUrl="~/frmsSystem/frmSubSignUp.aspx">Sign Up Now</asp:HyperLink></td>
      </tr>
    </table>    
    <p>
        <cc1:msgbox id="MsgBox" runat="server"></cc1:msgbox>
        &nbsp;</p></td>
    <td width="528" valign="top" bgcolor="#FFFFFF">
        <br />
        <br />
        <br />
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="443">
            <tr>
                <td colspan="2">
                    <span style="font-weight: bold; font-family: Verdana">Sign In as Subscriber</span>
                </td>
            </tr>
            <tr>
                <td style="width: 104px; height: 107px; text-align: right">
                    <span class="style6">
                        <label><span class="style7">User name:<br />
                            <br />
                        </span></label>
                        <label ><span class="style7">Password:</span></label>
                    </span>
                </td>
                <td style="width: 339px; height: 107px">
                    <span class="style8">
                    <label>
                        <br />
                        &nbsp;<asp:TextBox ID="txtUserName" runat="server" Font-Names="Verdana" Font-Size="10pt"
                            MaxLength="50" Width="250px"></asp:TextBox><br />
                        <br />
                        &nbsp;<asp:TextBox ID="txtPassword" runat="server" Font-Names="Verdana" Font-Size="10pt"
                            MaxLength="12" TextMode="Password" Width="250px"></asp:TextBox></label></span><br />
                <label>
                    &nbsp;<asp:CustomValidator ID="CustomValidator" runat="server" ErrorMessage="Invalid Password or User Name"
                        Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="OrangeRed" Height="18px"
                        Width="256px">Invalid Password or User Name</asp:CustomValidator></label></td>
        </tr>
        <tr>
            <td style="width: 104px; height: 24px">
            </td>
            <td style="width: 339px; height: 24px">
                &nbsp;<asp:Button ID="cmdSignIn" runat="server" Font-Names="Verdana" Text="Sign In"
                    Width="88px" /></td>
        </tr>
        </TABLE>
        <p class="style6">&nbsp;</p>
   
  </tr>
</table>   
    </div>
    </form>
</body>
</html>
