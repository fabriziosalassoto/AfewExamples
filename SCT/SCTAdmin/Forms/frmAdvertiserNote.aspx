<%@ Page Language="VB" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="false" CodeFile="frmAdvertiserNote.aspx.vb" Inherits="Forms_frmAdvertiserNote" title="IP Tracking Admin System - Note" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <table style="font-size: 9pt; width: 600px; font-family: Arial">
        <tr>
            <td align="left" colspan="3" style="font-size: 14pt; font-family: 'Comic Sans MS'">
                Note Information</td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <br />
                <asp:Label ID="lblAdNoteID" runat="server" Text="ID:"></asp:Label></td>
            <td align="left" colspan="2" style="width: 400px">
                <br />
                <asp:TextBox ID="txtAdNoteID" runat="server" Font-Names="Arial" Font-Size="9pt"
                    ReadOnly="True" Width="100px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdNoteAdvertiser" runat="server" Text="Advertiser:"></asp:Label></td>
            <td align="left" colspan="2" style="width: 400px">
                <asp:DropDownList ID="ddlAdNoteAdvertiser" runat="server" AutoPostBack="True"
                    Width="255px">
                </asp:DropDownList>
                <asp:CompareValidator ID="cmvAdNoteAdvertiser" runat="server" ControlToValidate="ddlAdNoteAdvertiser"
                    ErrorMessage="*  The Advertiser is required." Font-Bold="True" Font-Names="Arial"
                    Font-Size="Large" ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True"
                    ValueToCompare="0" Display="Dynamic">*</asp:CompareValidator></td>
        </tr>
        <tr style="font-size: 9pt; color: #000000">
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdNoteContact" runat="server" Text="Contact:"></asp:Label></td>
            <td align="left" colspan="2" style="width: 400px">
                <asp:DropDownList ID="ddlAdNoteContact" runat="server" Width="255px">
                </asp:DropDownList>
                <asp:CompareValidator ID="cmvAdNoteContact" runat="server" ControlToValidate="ddlAdNoteContact"
                    ErrorMessage="*  The Contact is required." Font-Bold="True" Font-Names="Arial"
                    Font-Size="Large" ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True"
                    ValueToCompare="0" Display="Dynamic">*</asp:CompareValidator></td>
        </tr>
        <tr style="font-size: 9pt; color: #000000">
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdNoteEnteredDate" runat="server" Text="Date Entered:"></asp:Label></td>
            <td align="left" colspan="2" style="width: 400px">
                <asp:DropDownList ID="ddlAdNoteEnteredMonth" runat="server" Width="115px">
                    <asp:ListItem>Month</asp:ListItem>
                    <asp:ListItem Value="01">January</asp:ListItem>
                    <asp:ListItem Value="02">February </asp:ListItem>
                    <asp:ListItem Value="03">March</asp:ListItem>
                    <asp:ListItem Value="04">April</asp:ListItem>
                    <asp:ListItem Value="05">May</asp:ListItem>
                    <asp:ListItem Value="06">June</asp:ListItem>
                    <asp:ListItem Value="07">July</asp:ListItem>
                    <asp:ListItem Value="08">August</asp:ListItem>
                    <asp:ListItem Value="09">September</asp:ListItem>
                    <asp:ListItem Value="10">October</asp:ListItem>
                    <asp:ListItem Value="11">November</asp:ListItem>
                    <asp:ListItem Value="12">December</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlAdNoteEnteredDay" runat="server"
                    Width="54px">
                    <asp:ListItem>Day</asp:ListItem>
                    <asp:ListItem>01</asp:ListItem>
                    <asp:ListItem>02</asp:ListItem>
                    <asp:ListItem>03</asp:ListItem>
                    <asp:ListItem>04</asp:ListItem>
                    <asp:ListItem>05</asp:ListItem>
                    <asp:ListItem>06</asp:ListItem>
                    <asp:ListItem>07</asp:ListItem>
                    <asp:ListItem>08</asp:ListItem>
                    <asp:ListItem>09</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>13</asp:ListItem>
                    <asp:ListItem>14</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>16</asp:ListItem>
                    <asp:ListItem>17</asp:ListItem>
                    <asp:ListItem>18</asp:ListItem>
                    <asp:ListItem>19</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>21</asp:ListItem>
                    <asp:ListItem>22</asp:ListItem>
                    <asp:ListItem>23</asp:ListItem>
                    <asp:ListItem>24</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>26</asp:ListItem>
                    <asp:ListItem>27</asp:ListItem>
                    <asp:ListItem>28</asp:ListItem>
                    <asp:ListItem>29</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>31</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlAdNoteEnteredYear" runat="server"
                    Width="80px">
                </asp:DropDownList>
                <asp:CustomValidator ID="csvAdNoteEnteredDateValid" runat="server" ErrorMessage="* Invalid Date Entered."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" Display="Dynamic" SetFocusOnError="True">*</asp:CustomValidator><asp:CustomValidator
                        ID="csvAdNoteDateEnteredRequired" runat="server" Display="Dynamic" ErrorMessage="*  The Date Entered is required."
                        Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:CustomValidator></td>
        </tr>
        <tr style="font-size: 9pt; color: #000000">
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial" valign="top">
                <asp:Label ID="lblAdNoteDescription" runat="server" Text="Description:"></asp:Label></td>
            <td align="left">
                <asp:TextBox ID="txtAdNoteDescription" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Height="75px" TextMode="MultiLine" Width="246px"></asp:TextBox></td>
            <td align="left" colspan="1" style="width: 140px" valign="top">
                <asp:RequiredFieldValidator ID="rqvAdNoteDescription" runat="server" ControlToValidate="txtAdNoteDescription"
                    Display="Dynamic" ErrorMessage="*  The Task Notes are required." Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
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
                        <asp:MenuItem ImageUrl="~/Images/USER16.gif" Text=" Contact" Value="Contact"></asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/DELETE16.GIF" Text=" Delete" Value="Delete"></asp:MenuItem>
                    </Items>
                </asp:Menu>
                <br />
            </td>
            <td align="left" style="width: 140px">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" style="border-top: silver 1px solid; font-size: 9pt;
                font-family: Arial;">
                <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" Font-Bold="True"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="576px" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="border-top: silver 1px solid; font-size: 9pt;
                font-family: Arial;">
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
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

