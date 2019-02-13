<%@ page language="VB" masterpagefile="~/Forms/MasterPage.master" autoeventwireup="false" inherits="Forms_frmAdvertiserAccount, App_Web_qbwzvytd" title="IP Tracking Admin System - Advertiser Account" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <table id="TABLE1" style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px;
        margin: 0px; width: 600px; padding-top: 0px">
        <tr>
            <td align="left" colspan="4" style="font-size: 14pt; border-bottom: silver 1px solid;
                font-family: 'Comic Sans MS'">
                Account Information</td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial">
                <br />
                <asp:Label ID="lblAdAccountID" runat="server" Text="ID:"></asp:Label></td>
            <td align="left" colspan="2" style="font-size: 9pt; width: 400px; font-family: Arial">
                <br />
                <asp:TextBox ID="txtAdAccountID" runat="server" Enabled="False" Width="75px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdAccountLogin" runat="server" Text="Login:"></asp:Label></td>
            <td align="left" colspan="2" style="font-size: 9pt; width: 400px; font-family: Arial">
                <asp:TextBox ID="txtAdAccountLogin" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="50" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAdAccountLogin" runat="server" ControlToValidate="txtAdAccountLogin"
                    Display="Dynamic" ErrorMessage="*  The Login is required" Font-Bold="True" Font-Names="Arial"
                    Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                        ID="rgvAdaccountLogin" runat="server" ControlToValidate="txtAdAccountLogin" Display="Dynamic"
                        ErrorMessage="* Invalid Login Format" Font-Bold="True" Font-Names="Arial" Font-Size="Large"
                        ForeColor="OrangeRed" SetFocusOnError="True" ValidationExpression="\w+">*</asp:RegularExpressionValidator><asp:CustomValidator
                            ID="cvAdAccountLogin" runat="server" ControlToValidate="txtAdAccountLogin" Display="Dynamic"
                            ErrorMessage="* Login is assigned to another" Font-Bold="True" Font-Names="Arial"
                            Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:CustomValidator></td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdAccountPassword" runat="server" Text="Password:"></asp:Label></td>
            <td align="left" colspan="2" style="font-size: 9pt; width: 400px; font-family: Arial">
                <asp:TextBox ID="txtAdAccountPassword" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="50" Width="200px" TextMode="Password"></asp:TextBox>
                <asp:CustomValidator ID="cvAdAccountPassword" runat="server"
                    Display="Dynamic" ErrorMessage="*  The Password is required" Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:CustomValidator><asp:RegularExpressionValidator
                        ID="rgvAdAccountWebPassword" runat="server" ControlToValidate="txtAdAccountPassword"
                        Display="Dynamic" ErrorMessage="* Invalid Password Format" Font-Bold="True" Font-Names="Arial"
                        Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True" ValidationExpression="\w{6,}">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdAccountCompanyName" runat="server" Text="Company Name:"></asp:Label></td>
            <td align="left" colspan="2" style="font-size: 9pt; width: 400px; font-family: Arial">
                <asp:TextBox ID="txtAdAccountCompanyName" runat="server" MaxLength="100" Width="300px" Font-Names="Arial" Font-Size="9pt"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtAdAccountCompanyName"
                    Display="Dynamic" ErrorMessage="*  The Company Name is required." Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial; padding-top: 4px;" valign="top">
                <asp:Label ID="lblAdAccountCompanyNotes" runat="server" Text="Company Notes:"></asp:Label></td>
            <td align="left" colspan="2" style="font-size: 9pt; width: 400px; font-family: Arial">
                <asp:TextBox ID="txtAdAccountCompanyNotes" runat="server" Font-Names="Arial"
                    Font-Size="9pt" Height="50px" MaxLength="1000" TextMode="MultiLine" Width="300px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial">
            </td>
            <td align="right" colspan="1" style="font-size: 9pt; font-family: Arial">
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
                        <asp:MenuItem ImageUrl="~/Images/DELETE16.GIF" Text=" Delete" Value="Delete"></asp:MenuItem>
                    </Items>
                </asp:Menu>
                <br />
            </td>
            <td colspan="1" style="font-size: 9pt; width: 17px; font-family: Arial">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="border-top: silver 1px solid; font-size: 9pt; font-family: Arial">
                <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" Font-Bold="True"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="576px" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="4" style="border-top: silver 1px solid; font-size: 9pt;
                font-family: Arial">
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
            </td>
        </tr>
    </table>
</asp:Content>

