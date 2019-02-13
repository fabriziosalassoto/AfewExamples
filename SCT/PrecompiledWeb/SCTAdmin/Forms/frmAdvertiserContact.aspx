<%@ page language="VB" masterpagefile="~/Forms/MasterPage.master" autoeventwireup="false" inherits="Forms_frmAdvertiserContact, App_Web_qbwzvytd" title="IP Tracking Admin System - Advertiser Contact" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <table style="width: 600px">
        <tr>
            <td align="right" colspan="3" style="font-size: 14pt; vertical-align: middle; border-bottom: silver 1px solid;
                font-family: 'Comic Sans MS'; text-align: left">
                Contact Information</td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <br />
                <asp:Label ID="lblAdContactID" runat="server" Text="ID:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <br />
                <asp:TextBox ID="txtAdContactID" runat="server" Font-Names="Arial" Font-Size="9pt"
                    ReadOnly="True" Width="100px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdContactAdvertiser" runat="server" Text="Advertiser:"></asp:Label></td>
            <td align="left" colspan="2" style="width: 400px">
                <asp:DropDownList ID="ddlAdContactAdvertiser" runat="server" Width="206px">
                </asp:DropDownList>
                <asp:CompareValidator ID="cvContactName" runat="server" ControlToValidate="ddlAdContactAdvertiser"
                    ErrorMessage="*  The Advertiser is required." Font-Bold="True" Font-Names="Arial" Font-Size="Large"
                    ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True" ValueToCompare="0">*</asp:CompareValidator><asp:CustomValidator
                        ID="cvAdContactAdvertiser" runat="server" ControlToValidate="ddlAdContactAdvertiser"
                        Display="Dynamic" ErrorMessage="* Contact assigned to another projects advertiser"
                        Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:CustomValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdContactFirstName" runat="server" Text="First name:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdContactFirstName" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="100" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLogin" runat="server" ControlToValidate="txtAdContactFirstName"
                    Display="Dynamic" ErrorMessage="*  The First Name is required" Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr style="color: #000000">
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdContactLastName" runat="server" Text="Last Name:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdContactLastName" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="100" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAdContactLastName"
                    Display="Dynamic" ErrorMessage="*  The Last Name is required" Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr style="font-size: 12pt; color: #000000">
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdContactPrimaryAddress" runat="server" Text="Primary Address:"></asp:Label></td>
            <td align="left" colspan="2" style="width: 400px">
                <asp:TextBox ID="txtAdContactPrimaryAddress" runat="server" Font-Names="Arial"
                    Font-Size="9pt" MaxLength="100" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAdContactPrimaryAddress"
                    Display="Dynamic" ErrorMessage="*  The Primary Address is required" Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr style="color: #000000">
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdContactSecondaryAddress" runat="server" Text="Secondary Address:"></asp:Label></td>
            <td align="left" colspan="2" style="width: 400px">
                <asp:TextBox ID="txtAdContactSecondaryAddress" runat="server" Font-Names="Arial"
                    Font-Size="9pt" MaxLength="100" Width="300px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
            </td>
            <td align="left" colspan="2" style="width: 400px">
                <asp:CheckBox ID="ckbAdContactMainCompanyAddress" runat="server" Font-Names="Arial"
                    Font-Size="9pt" Text="Main Company Address" />
                <asp:CustomValidator ID="ctvMainCompanyAddres" runat="server" ErrorMessage="* Main Company Address assigned to another contact"
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" Height="18px" Display="Dynamic">*</asp:CustomValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdContactCity" runat="server" Text="City:"></asp:Label></td>
            <td align="left" colspan="2" style="width: 400px">
                <asp:TextBox ID="txtAdContactCity" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="50" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdContactState" runat="server" Text="State:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:DropDownList ID="ddlAdContactState" runat="server" Width="206px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdContactCountry" runat="server" Text="Country:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:DropDownList ID="ddlAdContactCountry" runat="server" AutoPostBack="True"
                    Width="206px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdContactZipCode" runat="server" Text="Zip Code:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdContactZipCode" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="10" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdContactProvidence" runat="server" Text="Providence:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdContactProvidence" runat="server" Font-Names="Arial"
                    Font-Size="9pt" MaxLength="50" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <div id="result_box" dir="ltr">
                    <asp:Label ID="lblAdContactDepartment" runat="server" Text="Department:"></asp:Label>&nbsp;</div>
            </td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdContactDepartment" runat="server" Font-Names="Arial"
                    Font-Size="9pt" MaxLength="50" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial; vertical-align: top;">
                <asp:Label ID="lblAdContactNotes" runat="server" Text="Notes:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdContactNotes" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Height="75px" MaxLength="1000" TextMode="MultiLine" Width="300px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
            </td>
            <td align="right">
                <asp:Menu ID="mnuItem" runat="server" DynamicHorizontalOffset="2" Font-Names="Arial"
                    Font-Size="9pt" ForeColor="Gray" Orientation="Horizontal" StaticBottomSeparatorImageUrl="~/Images/Tab.gif"
                    StaticDisplayLevels="3" StaticSubMenuIndent="10px">
                    <StaticSelectedStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" />
                    <LevelMenuItemStyles>
                        <asp:MenuItemStyle Font-Bold="True" Font-Names="Arial" Font-Size="9pt" Font-Underline="False"
                            ForeColor="Black" />
                    </LevelMenuItemStyles>
                    <StaticMenuItemStyle Font-Bold="False" Font-Names="Arial" ForeColor="#990000" ItemSpacing="5px" />
                    <DynamicHoverStyle BackColor="DarkGray" ForeColor="Black" />
                    <DynamicMenuStyle BackColor="#FFFBD6" />
                    <DynamicSelectedStyle BackColor="#FFCC66" />
                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" ForeColor="#990000" />
                    <Items>
                        <asp:MenuItem ImageUrl="~/Images/Profile.png" Text=" Advertiser" Value="Advertiser">
                        </asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/DELETE16.GIF" Text=" Delete" Value="Delete"></asp:MenuItem>
                    </Items>
                </asp:Menu>
                <br />
            </td>
            <td align="left" style="width: 38px">
            </td>
        </tr>
        <tr>
            <td colspan="3" style="font-size: 9pt; font-family: Arial; border-top: silver 1px solid;">
                <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" Font-Bold="True"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="576px" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="font-size: 9pt; font-family: Arial; border-top: silver 1px solid;">
                <asp:Menu ID="mnuSave" runat="server" DynamicHorizontalOffset="2" Font-Names="Arial"
                    Font-Size="9pt" ForeColor="Gray" Orientation="Horizontal" StaticBottomSeparatorImageUrl="~/Images/Tab.gif"
                    StaticDisplayLevels="3" StaticSubMenuIndent="10px">
                    <LevelMenuItemStyles>
                        <asp:MenuItemStyle Font-Bold="True" Font-Names="Arial" Font-Size="9pt" Font-Underline="False"
                            ForeColor="Black" />
                    </LevelMenuItemStyles>
                    <StaticMenuItemStyle Font-Bold="False" Font-Names="Arial" ForeColor="#990000" ItemSpacing="5px" />
                    <DynamicHoverStyle BackColor="DarkGray" ForeColor="Black" />
                    <DynamicMenuStyle BackColor="#FFFBD6" />
                    <StaticSelectedStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" />
                    <DynamicSelectedStyle BackColor="#FFCC66" />
                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <Items>
                        <asp:MenuItem ImageUrl="~/Images/OK.bmp" Text=" Ok" Value="Ok"></asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/CANCEL.bmp" Text=" Cancel" Value="Cancel"></asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/SAVE.png" Text=" Save" Value="Save"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" ForeColor="#990000" />
                </asp:Menu>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

