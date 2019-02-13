<%@ Page Language="VB" MasterPageFile="~/frmsSubscriber/MasterPage.master" AutoEventWireup="false" CodeFile="frmSubscriberChangeAccountInformation.aspx.vb" Inherits="frmsSubscriber_frmSubscriberChangeAccountInformation" title="IP Tracking System - Change Account Information" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
        <table cellpadding="0" cellspacing="0" style="padding-right: 0px; padding-left: 0px;
            padding-bottom: 0px; margin: 0px; padding-top: 0px; height: 650px" width="100%">
            <tr>
                <td style="height: 570px; font-size: 9pt; font-family: Arial;" align="center" valign="top">
                    <br />
                    <br />
                    <table style="width: 600px;">
                        <tr>
                            <td align="left">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="Change Account Information" Font-Names="Comic Sans MS"></asp:Label></td>
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
                                            Login:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtLogin" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="50" BackColor="LightYellow" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Computer 
                                            Serial Number:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtSerialNbr" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="50" BackColor="LightYellow" ReadOnly="True"></asp:TextBox></td>
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
                        Width="600px" />
                    <br />
                    <asp:Button ID="cmdOk" runat="server" BackColor="DimGray" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" ForeColor="White" Text="Ok"
                        Width="80px" />&nbsp;
                    <asp:Button ID="cmdReturn" runat="server" BackColor="DimGray" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" Font-Names="Arial"
                        Font-Size="9pt" ForeColor="White" Text="Return" Width="80px" /><br />
                    <cc1:msgbox id="MsgBox" runat="server"></cc1:msgbox>
                                
                    <br />
                   
                </td>
            </tr>
        </table>
</asp:Content>

