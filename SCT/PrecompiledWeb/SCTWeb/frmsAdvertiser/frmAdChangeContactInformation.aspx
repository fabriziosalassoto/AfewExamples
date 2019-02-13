<%@ page language="VB" masterpagefile="~/frmsAdvertiser/MasterPage.master" autoeventwireup="false" inherits="frmsAdvertiser_frmAdChangeContactInformation, App_Web_d4xdeinq" title="Contact Information" %>

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
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="Contact Information" Font-Names="Comic Sans MS"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="height: 17px" align="left">*Required fields.</td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid; border-bottom: silver 1px solid;">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            ID:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px; font-size: 9pt; font-family: Arial;">
                                            &nbsp;<asp:TextBox ID="txtAdContactID" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                ReadOnly="True" Width="100px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            *First Name:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px; font-size: 9pt; font-family: Arial;">
                                            &nbsp;<asp:TextBox ID="txtAdContactFirstName" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLogin" runat="server" ControlToValidate="txtAdContactFirstName"
                                                ErrorMessage="*  The First Name is required" Font-Bold="True" Font-Names="Arial" Font-Size="Large"
                                                ForeColor="OrangeRed" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial;">
                                            *Last Name:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px; font-size: 9pt; font-family: Arial;">
                                            &nbsp;<asp:TextBox ID="txtAdContactLastName" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAdContactLastName"
                                                ErrorMessage="*  The Last Name is required" Font-Bold="True" Font-Names="Arial" Font-Size="Large"
                                                ForeColor="OrangeRed" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial;">
                                            *Primary Address:&nbsp;
                                        </td>
                                        <td align="left" style="width: 300px; font-size: 9pt; font-family: Arial;">
                                            &nbsp;<asp:TextBox ID="txtAdContactPrimaryAddress" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="100" Width="250px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAdContactPrimaryAddress"
                                                ErrorMessage="*  The Primary Address is required" Font-Bold="True" Font-Names="Arial" Font-Size="Large"
                                                ForeColor="OrangeRed" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial;">
                                            Secondary Address:&nbsp;
                                        </td>
                                        <td align="left" style="width: 300px; font-size: 9pt; font-family: Arial;">
                                            &nbsp;<asp:TextBox ID="txtAdContactSecondaryAddress" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="100" Width="250px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td align="left" style="width: 300px; font-size: 9pt; font-family: Arial;">
                                            <asp:CheckBox ID="ckbAdContactMainCompanyAddress" runat="server" Text="Main Company Address" Font-Names="Arial" Font-Size="9pt" />
                                            <asp:CustomValidator ID="ctvMainCompanyAddres" runat="server" ErrorMessage="* Main Company Address assigned to another contact"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" Height="18px">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            &nbsp; City:&nbsp;
                                        </td>
                                        <td align="left" style="width: 300px; font-size: 9pt; font-family: Arial;">
                                            &nbsp;<asp:TextBox ID="txtAdContactCity" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="50" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            State:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px; font-size: 9pt; font-family: Arial;">
                                            &nbsp;<asp:DropDownList ID="ddlAdContactState" runat="server" Width="206px">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Country:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px; font-size: 9pt; font-family: Arial;">
                                            &nbsp;<asp:DropDownList ID="ddlAdContactCountry" runat="server" AutoPostBack="True" Width="206px">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Zip Code:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px; font-size: 9pt; font-family: Arial;">
                                            &nbsp;<asp:TextBox ID="txtAdContactZipCode" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="10" Width="100px"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Providence:&nbsp;</td>
                                        <td align="left" colspan="2" style="width: 300px; font-size: 9pt; font-family: Arial;">
                                            &nbsp;<asp:TextBox ID="txtAdContactProvidence" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="50" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            <div id="result_box" dir="ltr">
                                                Department:&nbsp;</div>
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px; font-size: 9pt; font-family: Arial;">
                                            &nbsp;<asp:TextBox ID="txtAdContactDepartment" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="50" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Notes:<br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            &nbsp;</td>
                                        <td align="left" colspan="2" style="width: 300px; font-size: 9pt; font-family: Arial;">
                                            &nbsp;<asp:TextBox ID="txtAdContactNotes" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Height="75px" MaxLength="1000" TextMode="MultiLine" Width="250px"></asp:TextBox></td>
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
                                <cc1:MsgBox ID="MsgBox" runat="server" />
                    <br />
                   
                </td>
            </tr>
        </table>
</asp:Content>

