<%@ page language="VB" masterpagefile="~/frmsAdvertiser/MasterPage.master" autoeventwireup="false" inherits="frmsAdvertiser_frmAdChangeWebPassword, App_Web_d4xdeinq" title="Change Web Password" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
        <table cellpadding="0" cellspacing="0" style="padding-right: 0px; padding-left: 0px;
            padding-bottom: 0px; margin: 0px; padding-top: 0px;" width="100%">
            <tr>
                <td style="font-size: 9pt; font-family: Arial;" align="center" valign="top">
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
                            <td style="border-top: silver 1px solid; border-bottom-color: silver;">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="left" colspan="3" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Account</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            ID:</td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            <asp:TextBox ID="txtAdAccountID" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="50" ReadOnly="True" Width="100px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Login:
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            <asp:TextBox ID="txtAdAccountLogin" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="50" ReadOnly="True" Width="200px"></asp:TextBox>
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
                                        <td align="left" style="font-size: 10pt; font-family: Arial; font-weight: bold;" colspan="2">
                                            Web Password</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            *Current Password:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdAccountCurrentPassword" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="12" TextMode="Password" Width="200px"></asp:TextBox>
                                            <asp:CustomValidator ID="ctvCurrentPassword" runat="server" ControlToValidate="txtAdAccountCurrentPassword"
                                                Display="Dynamic" ErrorMessage="* Invalid Password" Font-Bold="True" Font-Names="Arial"
                                                Font-Size="X-Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            *New Password:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdAccountNewWebPassword" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" TextMode="Password" MaxLength="12"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvWebPassword" runat="server" ErrorMessage="* A password writes and, next, it returns to write it to confirm it."
                                                Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtAdAccountNewWebPassword" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="revWebPassword" runat="server" ControlToValidate="txtAdAccountNewWebPassword"
                                                    Display="Dynamic" ErrorMessage="* Invalid Password Format" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w{6,}" SetFocusOnError="True">*</asp:RegularExpressionValidator><asp:CompareValidator
                                                    ID="cpvWebConfirm" runat="server" ControlToCompare="txtAdAccountWebConfirm" ControlToValidate="txtAdAccountNewWebPassword"
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
                                            &nbsp;<asp:TextBox ID="txtAdAccountWebConfirm" runat="server" Font-Names="Arial" Font-Size="9pt"
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
                    <asp:Button ID="cmdOk" runat="server" Font-Names="Verdana" Text="Ok"
                        Width="80px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="cmdReturn" runat="server" Font-Names="Verdana"
                        Text="Return" Width="80px" CausesValidation="False" /><br />
                    <cc1:msgbox id="MsgBox" runat="server"></cc1:msgbox>
                                
                    <br />
                   
                </td>
            </tr>
        </table>
</asp:Content>

