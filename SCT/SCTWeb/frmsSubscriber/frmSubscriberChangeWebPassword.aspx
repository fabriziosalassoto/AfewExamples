<%@ Page Language="VB" MasterPageFile="~/frmsSubscriber/MasterPage.master" AutoEventWireup="false" CodeFile="frmSubscriberChangeWebPassword.aspx.vb" Inherits="frmsSubscriber_frmSubscriberChangeWebPassword" title="IP Tracking System - Change Password" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
        <table cellpadding="0" cellspacing="0" style="padding-right: 0px; padding-left: 0px;
            padding-bottom: 0px; margin: 0px; padding-top: 0px; height: 650px" width="100%">
            <tr>
                <td style="height: 570px; font-size: 9pt; font-family: Arial;" align="center" valign="top">
                    <br />
                    <br />
                    <table style="width: 600px;" id="TABLE1">
                        <tr>
                            <td align="left">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="Change Web Password" Font-Names="Comic Sans MS"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>*Required fields. &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid; border-bottom: silver 1px solid;">
                                <br />
                                <table style="width: 500px">
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
                                            *Current Password:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtCurrentPassword" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="12" TextMode="Password" Width="200px"></asp:TextBox>
                                            <asp:CustomValidator ID="ctvCurrentPassword" runat="server" ControlToValidate="txtCurrentPassword"
                                                Display="Dynamic" ErrorMessage="* Invalid Password" Font-Bold="True" Font-Names="Arial"
                                                Font-Size="X-Large" ForeColor="OrangeRed">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            *New Password:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtNewWebPassword" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" TextMode="Password" MaxLength="12"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvWebPassword" runat="server" ErrorMessage="* A password writes and, next, it returns to write it to confirm it."
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtNewWebPassword">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="revWebPassword" runat="server" ControlToValidate="txtNewWebPassword"
                                                    Display="Dynamic" ErrorMessage="* Invalid Password Format" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w{6,}">*</asp:RegularExpressionValidator><asp:CompareValidator
                                                    ID="cpvWebConfirm" runat="server" ControlToCompare="txtWebConfirm" ControlToValidate="txtNewWebPassword"
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
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed"
                        Width="600px" />
                    <br />
                    <asp:Button ID="cmdOk" runat="server" BackColor="DimGray" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" ForeColor="White" Text="Ok"
                        Width="80px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="cmdReturn" runat="server" BackColor="DimGray" BorderColor="Black"
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" ForeColor="White"
                        Text="Return" Width="80px" CausesValidation="False" /><br />
                    <cc1:msgbox id="MsgBox" runat="server"></cc1:msgbox>
                                
                    <br />
                   
                </td>
            </tr>
        </table>
</asp:Content>

