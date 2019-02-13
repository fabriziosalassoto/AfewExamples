<%@ Page Language="VB" MasterPageFile="~/frmsAdvertiser/MasterPage.master" AutoEventWireup="false" CodeFile="frmAdToDoInformation.aspx.vb" Inherits="frmsAdvertiser_frmAdToDoInformation" title="Advertiser To Do Information" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
        <table cellpadding="0" cellspacing="0" style="padding-right: 0px; padding-left: 0px;
            padding-bottom: 0px; margin: 0px; padding-top: 0px; height: 650px" width="100%">
            <tr>
                <td style="height: 570px; font-size: 9pt; font-family: Arial;" align="center" valign="top">
                    <br />
                    <br />
                    <table style="width: 600px;" id="TABLE">
                        <tr>
                            <td align="left" colspan="2">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="To Do Information" Font-Names="Comic Sans MS"></asp:Label>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid; border-bottom: silver 1px solid;" colspan="2">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            ID:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdToDoID" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                ReadOnly="True" Width="100px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Contact Name:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdToDoContact" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="200px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                        </td>
                                        <td align="right" colspan="">
                                <asp:Menu ID="mnuContact" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
                                    Font-Size="10pt" ForeColor="Gray" Orientation="Horizontal"
                                    StaticDisplayLevels="3" StaticSubMenuIndent="10px">
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
                                        <asp:MenuItem ImageUrl="~/Images/USER16.gif" Text=" Contact" Value="Contact"></asp:MenuItem>
                                    </Items>
                                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Comic Sans MS"
                                        Font-Size="10pt" Font-Underline="False" ForeColor="#990000" />
                                </asp:Menu>
                                        </td>
                                        <td align="right" style="width: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Date Entered:&nbsp;</td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdToDoDateEntered" runat="server" Font-Names="Arial" Font-Size="9pt" Width="200px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Date Due:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdToDoDateDue" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                                            Date Completed:&nbsp;</td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdToDoDateCompleted" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial" valign="top">
                                            Task Notes:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 250px">
                                            &nbsp;<asp:TextBox ID="txtAdToDoTaskNotes" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Height="75px" TextMode="MultiLine" Width="200px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial;">
                                            &nbsp;</td>
                                        <td align="left" style="width: 300px;" colspan="2">
                                            &nbsp;<asp:Image ID="imgAdToDoCallBackRecord" runat="server" ImageAlign="AbsMiddle"
                                                ImageUrl="~/Images/Checked_False.png" />
                                            <asp:Label ID="lblAdToDoCallBackRecord" runat="server" Text="Call Back Record"></asp:Label></td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 600px">
                        <tr>
                            <td align="right" style="height: 36px">
                                            <asp:Menu ID="mnuEditToDo" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
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

