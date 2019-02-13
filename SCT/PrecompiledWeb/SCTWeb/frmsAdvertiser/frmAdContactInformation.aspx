<%@ page language="VB" masterpagefile="~/frmsAdvertiser/MasterPage.master" autoeventwireup="false" inherits="frmsAdvertiser_frmAdContactInformation, App_Web_d4xdeinq" title="Advertiser Contact Information" %>

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
                            <td align="left" style="width: 300px">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="Contact Information" Font-Names="Comic Sans MS"></asp:Label></td>
                            <td align="right" style="width: 300px">
                                <asp:Menu ID="mnuContact" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
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
                                        <asp:MenuItem ImageUrl="~/Images/PROJECT16.BMP" Text=" Projects" Value="Projects"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="~/Images/Report.gif" Text=" Notes" Value="Notes"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="~/Images/TODO16.BMP" Text=" To Do" Value="ToDo"></asp:MenuItem>
                                    </Items>
                                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Comic Sans MS"
                                        Font-Size="10pt" Font-Underline="False" ForeColor="#990000" />
                                </asp:Menu>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid; border-bottom: silver 1px solid;" colspan="2">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            ID:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdContactID" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" MaxLength="100" ReadOnly="True" Width="100px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            First Name:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdContactFirstName" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="100" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Last Name:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdContactLastName" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" MaxLength="100" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial;">
                                            Primary Address:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px;">
                                            &nbsp;<asp:TextBox ID="txtAdContactPrimaryAddress" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="100" Width="200px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial;">
                                            Secondary Address:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px;">
                                            &nbsp;<asp:TextBox ID="txtAdContactSecondaryAddress" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="100" Width="200px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:Image ID="imgAdContactMainCompanyAddress" runat="server" ImageAlign="AbsMiddle"
                                                ImageUrl="~/Images/Checked_False.png" />
                                            <asp:Label ID="lblAdContactMainCompanyAddress" runat="server" Text="Main Company Address"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            &nbsp; City:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdContactCity" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="50" Width="200px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            State:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdContactState" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" MaxLength="50" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Country:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdContactCountry" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" MaxLength="50" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Zip Code:&nbsp;
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdContactZipCode" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="10" Width="100px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Providence:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdContactProvidence" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="50" Width="200px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            <div id="result_box" dir="ltr">
                                                Department:&nbsp;</div>
                                        </td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdContactDepartment" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="50" Width="200px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial" valign="top">
                                            Notes:&nbsp;</td>
                                        <td align="left" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdContactNotes" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Height="75px" MaxLength="1000" TextMode="MultiLine" Width="200px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 600px">
                        <tr>
                            <td align="right">
                                            <asp:Menu ID="mnuEditContact" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
                                                Font-Size="10pt" ForeColor="Gray" Orientation="Horizontal" StaticDisplayLevels="3"
                                                StaticSubMenuIndent="10px" StaticBottomSeparatorImageUrl="~/Images/Tab.gif">
                                                <LevelMenuItemStyles>
                                                    <asp:MenuItemStyle Font-Bold="True" Font-Names="Comic Sans MS" Font-Size="10pt" Font-Underline="False"
                                                        ForeColor="Black" />
                                                </LevelMenuItemStyles>
                                                <StaticMenuItemStyle Font-Bold="False" Font-Names="Comic Sans MS" ForeColor="#990000" HorizontalPadding="5px" />
                                                <DynamicHoverStyle BackColor="DarkGray" ForeColor="Black" />
                                                <DynamicMenuStyle BackColor="#FFFBD6" />
                                                <StaticSelectedStyle Font-Bold="True" Font-Italic="False" Font-Names="Comic Sans MS"
                                                    Font-Size="10pt" Font-Underline="False" />
                                                <DynamicSelectedStyle BackColor="#FFCC66" />
                                                <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                                                <Items>
                                                    <asp:MenuItem ImageUrl="~/Images/EDIT16.GIF" Text=" Edit" Value="Edit">
                                                    </asp:MenuItem>
                                                    <asp:MenuItem ImageUrl="~/Images/DELETE16.GIF" Text=" Delete" Value="Delete"></asp:MenuItem>
                                                    <asp:MenuItem Text=" Return" Value="Return" ImageUrl="~/Images/Return.gif"></asp:MenuItem>
                                                </Items>
                                                <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Comic Sans MS"
                                                    Font-Size="10pt" Font-Underline="False" ForeColor="#990000" />
                                            </asp:Menu>
                            </td>
                        </tr>
                    </table>
                                <cc1:MsgBox ID="MsgBox" runat="server" />
                    <br />
                   
                </td>
            </tr>
        </table>
</asp:Content>