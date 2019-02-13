<%@ page language="VB" masterpagefile="~/Forms/MasterPage.master" autoeventwireup="false" inherits="Forms_frmUser, App_Web_qbwzvytd" title="IP Tracking Admin System - User Information" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <br />
    <table id="TABLE1" style="width: 500px; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; margin: 0px; padding-top: 0px;">
        <tr>
            <td align="left" colspan="4" style="font-size: 14pt; border-bottom: silver 1px solid;
                font-family: 'Comic Sans MS'">
                User Information</td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial">
                <br />
                <asp:Label ID="lblUserID" runat="server" Text="ID:"></asp:Label></td>
            <td align="left" colspan="2" style="font-size: 9pt; width: 300px; font-family: Arial">
                <br />
                <asp:TextBox ID="txtUserID" runat="server"
                    Width="75px" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial;">
                <asp:Label ID="lblUserFirstName" runat="server" Text="First Name:"></asp:Label></td>
            <td align="left" colspan="2" style="font-size: 9pt; width: 300px; font-family: Arial;">
                <asp:TextBox ID="txtUserFirstName" runat="server" MaxLength="100" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="*  The First Name is required."
                    Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtUserFirstName" Display="Dynamic">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblUserLastName" runat="server" Text="Last Name:"></asp:Label></td>
            <td align="left" colspan="2" style="font-size: 9pt; width: 300px; font-family: Arial">
                <asp:TextBox ID="txtUserLastName" runat="server" MaxLength="100" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtUserLastName"
                    Display="Dynamic" ErrorMessage="*  The Last Name is required." Font-Bold="True"
                    Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblUserProfile" runat="server" Text="Profile:"></asp:Label></td>
            <td align="left" colspan="2" style="font-size: 9pt; width: 300px; font-family: Arial">
                <asp:DropDownList ID="ddlUserProfile" runat="server" Width="205px">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator ID="cvProfile" runat="server" ControlToValidate="ddlUserProfile"
                    Display="Dynamic" ErrorMessage="*  The Profile is required." Font-Bold="True" Font-Names="Arial"
                    Font-Size="X-Large" ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True"
                    ValueToCompare="0">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial;">
                <asp:Label ID="lblUserLogin" runat="server" Text="Login:"></asp:Label></td>
            <td align="left" colspan="2" style="font-size: 9pt; width: 300px; font-family: Arial;">
                <asp:TextBox ID="txtUserLogin" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                <asp:CustomValidator ID="cvExistsUserLogin" runat="server" ControlToValidate="txtUserLogin"
                    Display="Dynamic" ErrorMessage="* Login already assigned to another user." Font-Bold="True"
                    Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed">*</asp:CustomValidator></td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblUserPassword" runat="server" Text="Password:"></asp:Label></td>
            <td align="left" colspan="2" style="font-size: 9pt; width: 300px; font-family: Arial">
                <asp:TextBox ID="txtUserPassword" runat="server" MaxLength="12" TextMode="Password"
                    Width="200px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revWebPassword" runat="server" ControlToValidate="txtUserPassword"
                    Display="Dynamic" ErrorMessage="* Invalid Password Format" Font-Bold="True" Font-Names="Arial"
                    Font-Size="X-Large" ForeColor="OrangeRed" ValidationExpression="\w{6,}">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial">
            </td>
            <td align="right" colspan="1" style="font-size: 9pt; font-family: Arial">
                <asp:Menu ID="mnuItem" runat="server" DynamicHorizontalOffset="2" Font-Names="Arial"
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
                        <asp:MenuItem ImageUrl="~/Images/Profile.png" Text=" Profile" Value="Profile"></asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/DELETE16.GIF" Text=" Delete" Value="Delete"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" ForeColor="#990000" />
                </asp:Menu>
                <br />
            </td>
            <td colspan="1" style="font-size: 9pt; width: 47px; font-family: Arial">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="border-top: silver 1px solid; font-size: 9pt; font-family: Arial;">
                <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" Font-Bold="True"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="480px" />
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

