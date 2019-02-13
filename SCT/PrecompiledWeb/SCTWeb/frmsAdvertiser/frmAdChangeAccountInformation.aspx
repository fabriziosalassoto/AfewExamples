<%@ page language="VB" masterpagefile="~/frmsAdvertiser/MasterPage.master" autoeventwireup="false" inherits="frmsAdvertiser_frmAdChangeAccountInformation, App_Web_d4xdeinq" title="Change Account Information" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
        <table cellpadding="0" cellspacing="0" style="padding-right: 0px; padding-left: 0px;
            padding-bottom: 0px; margin: 0px; padding-top: 0px;" width="100%">
            <tr>
                <td style="font-size: 9pt; font-family: Arial;" align="center" valign="top">
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
                                            ID:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdAccountID" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="50" ReadOnly="True" Width="100px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Login:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdAccountLogin" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="50" ReadOnly="True"></asp:TextBox>
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
                                                MaxLength="100" Width="300px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAdAccountCompanyName" runat="server" ControlToValidate="txtAdAccountCompanyName"
                                                Display="Dynamic" ErrorMessage="*  The Company Name is required" Font-Bold="True"
                                                Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 150px; font-family: Arial">
                                            Note:&nbsp;<br />
                                            <br />
                                        </td>
                                        <td align="left" colspan="2" style="width: 350px">
                                            &nbsp;<asp:TextBox ID="txtAdAccountCompanyNote" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Height="50px" MaxLength="1000" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="OrangeRed" Width="600px" />
                    <br />
                    <asp:Button ID="cmdOk" runat="server" Font-Names="Verdana" Font-Size="9pt" Text="Ok"
                        Width="80px" />&nbsp;
                    <asp:Button ID="cmdReturn" runat="server" CausesValidation="False" Font-Names="Verdana"
                        Font-Size="9pt" Text="Return" Width="80px" /><br />
                    <cc1:msgbox id="MsgBox" runat="server"></cc1:msgbox>
                                
                    <br />
                   
                </td>
            </tr>
        </table>
</asp:Content>

