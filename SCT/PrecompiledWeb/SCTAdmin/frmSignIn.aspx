<%@ page language="VB" autoeventwireup="false" inherits="Forms_frmSignIn, App_Web_60tvvty-" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Notebook Detector - Sign In</title>
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
	font-size: 9;
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
<script type="text/javascript">
<!--
function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}
function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}
function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_nbGroup(event, grpName) { //v6.0
  var i,img,nbArr,args=MM_nbGroup.arguments;
  if (event == "init" && args.length > 2) {
    if ((img = MM_findObj(args[2])) != null && !img.MM_init) {
      img.MM_init = true; img.MM_up = args[3]; img.MM_dn = img.src;
      if ((nbArr = document[grpName]) == null) nbArr = document[grpName] = new Array();
      nbArr[nbArr.length] = img;
      for (i=4; i < args.length-1; i+=2) if ((img = MM_findObj(args[i])) != null) {
        if (!img.MM_up) img.MM_up = img.src;
        img.src = img.MM_dn = args[i+1];
        nbArr[nbArr.length] = img;
    } }
  } else if (event == "over") {
    document.MM_nbOver = nbArr = new Array();
    for (i=1; i < args.length-1; i+=3) if ((img = MM_findObj(args[i])) != null) {
      if (!img.MM_up) img.MM_up = img.src;
      img.src = (img.MM_dn && args[i+2]) ? args[i+2] : ((args[i+1])? args[i+1] : img.MM_up);
      nbArr[nbArr.length] = img;
    }
  } else if (event == "out" ) {
    for (i=0; i < document.MM_nbOver.length; i++) {
      img = document.MM_nbOver[i]; img.src = (img.MM_dn) ? img.MM_dn : img.MM_up; }
  } else if (event == "down") {
    nbArr = document[grpName];
    if (nbArr)
      for (i=0; i < nbArr.length; i++) { img=nbArr[i]; img.src = img.MM_up; img.MM_dn = 0; }
    document[grpName] = nbArr = new Array();
    for (i=2; i < args.length-1; i+=2) if ((img = MM_findObj(args[i])) != null) {
      if (!img.MM_up) img.MM_up = img.src;
      img.src = img.MM_dn = (args[i+1])? args[i+1] : img.MM_up;
      nbArr[nbArr.length] = img;
  } }
}
function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
//-->
</script>
</head>
<body onload="MM_preloadImages('images/top_image2.gif')">
<form id="form1" runat="server">
<table width="881" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td colspan="2" style="height: 144px"><a href="#" onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image5','','images/top_image2.gif',1)"><img src="images/top_image1.gif" name="Image5" width="885" height="125" border="0" id="Image" /></a></td>
  </tr>
  <tr>
    <td width="357" align="center" valign="top" bgcolor="#FFFFFF"><p>&nbsp;</p>
      <table width="109" height="59" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td bgcolor="#FFFFFF" style="height: 206px"><p><img src="Images/Logo-300X169.gif" alt="Logo" width="300" height="169" /></p>
          </td>
      </tr>
    </table>    
        <br />
        <br />
    <cc1:msgbox id="MsgBox" runat="server"></cc1:msgbox>
    <td width="528" valign="top" bgcolor="#FFFFFF">      
        <br />
        <br />
        <br />
      <table width="443" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
              <td colspan="2">
                  <span style="font-weight: bold; font-family: Verdana">Sign In</span></td>
          </tr>
        <tr>
          <td style="width: 104px; height: 107px; text-align: right;"><span class="style6">
          <label><span class="style7">User name:</span></label></span><p class="style6"><span class="style8">P</span><span class="style8">assword:</span></p>
          </td>
            <td style="height: 107px; width: 339px;">
          <span class="style8">
          <label>
              <br />
              &nbsp;<asp:TextBox ID="txtUserName" runat="server" Font-Names="Verdana" Font-Size="10pt"
                  MaxLength="50" Width="250px"></asp:TextBox><br />
              <br />
              &nbsp;<asp:TextBox ID="txtPassword" runat="server" Font-Names="Verdana" Font-Size="10pt"
                  MaxLength="12" TextMode="Password" Width="250px"></asp:TextBox></label></span><br />
            <label>
                &nbsp;<asp:CustomValidator ID="cvUser" runat="server" ErrorMessage="Invalid Password or User Name"
                    Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="OrangeRed" Height="18px"
                    Width="256px">Invalid Password or User Name</asp:CustomValidator></label></td>
        </tr>
          <tr>
              <td style="width: 104px; height: 24px;">
                  </td>
              <td style="width: 339px; height: 24px;">
                  &nbsp;<asp:Button ID="cmdSignIn" runat="server" Text="Sign In" Font-Names="Verdana" Width="88px" /></td>
          </tr>
      </table></td>
  </tr>
</table>
</form> 
</body>
</html>
