<%@ Page Language="VB" MasterPageFile="~/frmsAdvertiser/MasterPage.master" AutoEventWireup="false" CodeFile="frmAdProjectInformation.aspx.vb" Inherits="frmsAdvertiser_frmAdProjectInformation" title="Advertiser Project Information" %>

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
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="Project Information" Font-Names="Comic Sans MS"></asp:Label>&nbsp;</td>
                            <td align="right">
                                <asp:Menu ID="mnuProject" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
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
                                        <asp:MenuItem ImageUrl="~/Images/Report.gif" Text=" Project History" Value="ProjectHistory">
                                        </asp:MenuItem>
                                    </Items>
                                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Comic Sans MS"
                                        Font-Size="10pt" Font-Underline="False" ForeColor="#990000" />
                                </asp:Menu>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid;" colspan="2">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="left" colspan="3" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Generic Information<br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            ID:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectID" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                ReadOnly="True" Width="100px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial; height: 25px;">
                                            Contact Name:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px; height: 25px;">
                                            &nbsp;<asp:TextBox ID="txtAdProjectContact" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Width="250px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
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
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Url:&nbsp;</td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectADUrl" runat="server" Font-Names="Arial" Font-Size="9pt" Width="250px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial" valign="top">
                                            Description:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectDescription" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Height="75px" TextMode="MultiLine" Width="250px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; padding-top: 8px;">
                                            Size:&nbsp;</td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;Height: &nbsp;<asp:TextBox ID="txtAdProjectHeight" runat="server"
                                                ReadOnly="True" Width="50px" Font-Names="Arial" Font-Size="9pt"></asp:TextBox>&nbsp; &nbsp;Width: &nbsp;<asp:TextBox
                                                    ID="txtAdProjectWidth" runat="server" ReadOnly="True" Width="50px" Font-Names="Arial" Font-Size="9pt"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial;">
                                            Start Date:&nbsp;
                                        </td>
                                        <td align="left" style="width: 300px;" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtAdProjectRunStartDate" runat="server" Font-Names="Arial" Font-Size="9pt" Width="200px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            End Date:&nbsp;
                                        </td>
                                        <td align="left" style="width: 300px" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtAdProjectRunEndDate" runat="server" Font-Names="Arial" Font-Size="9pt" Width="200px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Start Time:&nbsp;</td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectStartTime" runat="server" Font-Names="Arial" Font-Size="9pt" Width="100px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            End Time:&nbsp;</td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectEndTime" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" ReadOnly="True" Width="100px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Min. Display:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectMinDisplay" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" ReadOnly="True" Width="75px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Max. Display:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectMaxDisplay" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" ReadOnly="True" Width="75px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Min. Per Day:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px;">
                                            &nbsp;<asp:TextBox ID="txtAdProjectMinPerDay" runat="server" Font-Names="Arial" Font-Size="9pt" Width="75px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Max. Per Day:&nbsp;</td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectMaxPerDay" runat="server" Font-Names="Arial" Font-Size="9pt" Width="75px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Count Displayed:&nbsp;</td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectCountDisplayed" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" ReadOnly="True" Width="75px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Verified Date:&nbsp;</td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectVerifiedDate" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Online Date:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectOnlineDate" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" ReadOnly="True" Width="200px" CausesValidation="True"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="border-top: silver 1px solid; border-bottom: silver 1px solid">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="left" colspan="3" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Demographic Requirement<br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            Sex:&nbsp;
                                        </td>
                                        <td align="left" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectSex" runat="server" ReadOnly="True" Width="50px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            Min. Age:&nbsp;
                                        </td>
                                        <td align="left" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectMinAge" runat="server" ReadOnly="True"
                                                Width="25px"></asp:TextBox>
                                            Years.</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            &nbsp;Max. Age:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectMaxAge" runat="server" ReadOnly="True" Width="25px"></asp:TextBox>
                                            Years.</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            Occupation:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectOccupation" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            Country or Region:&nbsp;
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectCountry" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <div id="Div1" dir="ltr">
                                                State or Province:&nbsp;
                                            </div>
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            &nbsp;<asp:TextBox ID="txtAdProjectState" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 600px">
                        <tr>
                            <td align="right" style="height: 36px">
                                            <asp:Menu ID="mnuEditProject" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
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

