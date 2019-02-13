<%@ Page Language="VB" MasterPageFile="~/frmsAdvertiser/MasterPage.master" AutoEventWireup="false" CodeFile="frmAdAccount.aspx.vb" Inherits="frmsAdvertiser_frmAdAccount" title="Advertiser Account Information" %>

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
                            <td align="left" style="width: 300px">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="Account Information" Font-Names="Comic Sans MS"></asp:Label></td>
                            <td align="right" style="width: 300px">
                                <asp:Menu ID="mnuAccount" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
                                    Font-Size="10pt" ForeColor="Gray" Orientation="Horizontal" StaticBottomSeparatorImageUrl="~/Images/Tab.gif"
                                    StaticDisplayLevels="3" StaticSubMenuIndent="10px">
                                    <LevelMenuItemStyles>
                                        <asp:MenuItemStyle Font-Bold="True" Font-Names="Comic Sans MS" Font-Size="10pt" Font-Underline="False"
                                            ForeColor="Black" />
                                    </LevelMenuItemStyles>
                                    <StaticMenuItemStyle Font-Bold="False" Font-Names="Comic Sans MS" ForeColor="#990000"
                                        HorizontalPadding="5px" />
                                    <DynamicHoverStyle BackColor="DarkGray" ForeColor="Black" />
                                    <DynamicMenuStyle BackColor="#FFFBD6" />
                                    <StaticSelectedStyle Font-Bold="True" Font-Italic="False" Font-Names="Comic Sans MS"
                                        Font-Size="10pt" Font-Underline="False" />
                                    <DynamicSelectedStyle BackColor="#FFCC66" />
                                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                                    <Items>
                                        <asp:MenuItem ImageUrl="~/Images/USER16.gif" Text=" Contacts" Value="Contacts"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="~/Images/PROJECT16.BMP" Text=" Projects" Value="Projects"></asp:MenuItem>
                                    </Items>
                                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Comic Sans MS"
                                        Font-Size="10pt" Font-Underline="False" ForeColor="#990000" />
                                </asp:Menu>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid" colspan="2">
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
                                        <td align="left" style="width: 250px" colspan="2">
                                            <asp:TextBox ID="txtAdAccountLogin" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="50" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            </td>
                                        <td align="right">
                                            <asp:Menu ID="mnuChangePassword" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
                                                Font-Size="10pt" ForeColor="Gray" Orientation="Horizontal" StaticDisplayLevels="3"
                                                StaticSubMenuIndent="10px">
                                                <LevelMenuItemStyles>
                                                    <asp:MenuItemStyle Font-Bold="True" Font-Names="Comic Sans MS" Font-Size="10pt" Font-Underline="False"
                                                        ForeColor="Black" />
                                                </LevelMenuItemStyles>
                                                <StaticMenuItemStyle Font-Bold="False" Font-Names="Comic Sans MS" ForeColor="#990000" />
                                                <DynamicHoverStyle BackColor="DarkGray" ForeColor="Black" />
                                                <DynamicMenuStyle BackColor="#FFFBD6" />
                                                <StaticSelectedStyle Font-Bold="True" Font-Italic="False" Font-Names="Comic Sans MS"
                                                    Font-Size="10pt" Font-Underline="False" />
                                                <DynamicSelectedStyle BackColor="#FFCC66" />
                                                <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                                                <Items>
                                                    <asp:MenuItem ImageUrl="~/Images/EDIT16.GIF" NavigateUrl="~/frmsAdvertiser/frmAdChangeWebPassword.aspx"
                                                        Text="Change Password" Value="Change Password"></asp:MenuItem>
                                                </Items>
                                                <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Comic Sans MS"
                                                    Font-Size="10pt" Font-Underline="False" ForeColor="#990000" />
                                            </asp:Menu>
                                        </td>
                                        <td align="left" style="width: 30px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid; border-bottom: silver 1px solid;" colspan="2">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="left" colspan="3" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Company Information</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 150px; font-family: Arial">
                                            Name:
                                        </td>
                                        <td align="left" colspan="2" style="width: 350px">
                                            <asp:TextBox ID="txtAdAccountCompanyName" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="300px" MaxLength="100" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 150px; font-family: Arial">
                                            Note:
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                        <td align="left" colspan="2" style="width: 350px">
                                            <asp:TextBox ID="txtAdAccountCompanyNote" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="300px" MaxLength="1000" Height="50px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 150px; font-family: Arial">
                                        </td>
                                        <td align="right">
                                            <asp:Menu ID="mnuChangeCompanyInformation" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
                                                Font-Size="10pt" ForeColor="Gray" Orientation="Horizontal" StaticDisplayLevels="3"
                                                StaticSubMenuIndent="10px">
                                                <LevelMenuItemStyles>
                                                    <asp:MenuItemStyle Font-Bold="True" Font-Names="Comic Sans MS" Font-Size="10pt" Font-Underline="False"
                                                        ForeColor="Black" />
                                                </LevelMenuItemStyles>
                                                <StaticMenuItemStyle Font-Bold="False" Font-Names="Comic Sans MS" ForeColor="#990000" />
                                                <DynamicHoverStyle BackColor="DarkGray" ForeColor="Black" />
                                                <DynamicMenuStyle BackColor="#FFFBD6" />
                                                <StaticSelectedStyle Font-Bold="True" Font-Italic="False" Font-Names="Comic Sans MS"
                                                    Font-Size="10pt" Font-Underline="False" />
                                                <DynamicSelectedStyle BackColor="#FFCC66" />
                                                <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                                                <Items>
                                                    <asp:MenuItem ImageUrl="~/Images/EDIT16.GIF" NavigateUrl="~/frmsAdvertiser/frmAdChangeAccountInformation.aspx"
                                                        Text="Change Company Information" Value="Change Information"></asp:MenuItem>
                                                </Items>
                                                <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Comic Sans MS"
                                                    Font-Size="10pt" Font-Underline="False" ForeColor="#990000" />
                                            </asp:Menu>
                                        </td>
                                        <td align="left" style="width: 30px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <cc1:msgbox id="MsgBox" runat="server"></cc1:msgbox>
                    
                   
                </td>
            </tr>
        </table>    
</asp:Content>

