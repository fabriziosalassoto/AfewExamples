<%@ Page Language="VB" MasterPageFile="~/frmsSubscriber/MasterPage.master" AutoEventWireup="false" CodeFile="frmSubscriberAccount.aspx.vb" Inherits="frmsSubscriber_frmSubscriberAccount" title="IP Tracking System - Subscriber Account Information" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
         <table cellpadding="0" cellspacing="0" style="padding-right: 0px; padding-left: 0px;
            padding-bottom: 0px; margin: 0px; padding-top: 0px;">
            <tr>
                <td style="font-size: 9pt; font-family: Arial;" align="center" valign="top">
                    <br />
                    <br />
                    <table style="width: 600px;">
                        <tr>
                            <td align="left" style="height: 29px">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="Account Information" Font-Names="Comic Sans MS"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="left" colspan="2" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Account</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Login:
                                        </td>
                                        <td align="left" style="width: 250px">
                                            <asp:TextBox ID="txtLogin" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="50" BackColor="LightYellow" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            <asp:HyperLink ID="lnkChageWebPassword" runat="server" NavigateUrl="~/frmsSubscriber/frmSubscriberChangeWebPassword.aspx">Change Password</asp:HyperLink></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Computer 
                                            Serial Number:
                                        </td>
                                        <td align="left" style="width: 250px">
                                            <asp:TextBox ID="txtSerialNbr" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="50" BackColor="LightYellow" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            <asp:HyperLink ID="lnkChangeComputerPassword" runat="server" NavigateUrl="~/frmsSubscriber/frmSubscriberChangeComputerPassword.aspx">Change Password</asp:HyperLink></td>
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
                                            E-mail:
                                        </td>
                                        <td align="left" style="width: 250px">
                                            <asp:TextBox ID="txtEmail" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="100" BackColor="LightYellow" ReadOnly="True"></asp:TextBox>
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
                                            Your information</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            First Name:
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            <asp:TextBox ID="txtFirstName" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="100" BackColor="LightYellow" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Last Name:
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            <asp:TextBox ID="txtLastName" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="100" BackColor="LightYellow" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial;">
                                            Sex:
                                        </td>
                                        <td align="left" style="width: 250px;">
                                            <asp:TextBox ID="txtSex" runat="server" BackColor="LightYellow" ReadOnly="True"
                                                Width="50px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Age:
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            <asp:TextBox ID="txtAge" runat="server" BackColor="LightYellow" ReadOnly="True"
                                                Width="25px"></asp:TextBox> Years.</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Occupation:
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            <asp:TextBox ID="txtOccupation" runat="server" BackColor="LightYellow" Font-Names="Arial"
                                                Font-Size="9pt" MaxLength="100" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Country or Region:
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            <asp:TextBox ID="txtCountry" runat="server" BackColor="LightYellow" Font-Names="Arial"
                                                Font-Size="9pt" MaxLength="100" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            <div id="result_box" dir="ltr">
                                                State or Province:
                                            </div>
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            <asp:TextBox ID="txtState" runat="server" BackColor="LightYellow" Font-Names="Arial"
                                                Font-Size="9pt" MaxLength="100" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp;
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/frmsSubscriber/frmSubscriberChangeAccountInformation.aspx">Change Information</asp:HyperLink></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                     
                    <cc1:msgbox id="MsgBox" runat="server"></cc1:msgbox>
                                
                    <br />
                   
                </td>
            </tr>
        </table>
 </asp:Content>

