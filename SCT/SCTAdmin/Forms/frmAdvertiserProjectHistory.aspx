<%@ Page Language="VB" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="false" CodeFile="frmAdvertiserProjectHistory.aspx.vb" Inherits="Forms_frmAdvertiserProjectHistory" title="IP Tracking Admin System - Project History" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    &nbsp;<br />
    <table style="width: 850px">
        <tr>
            <td colspan="5" style="font-weight: bold; font-size: 14pt; border-bottom: silver 1px solid;
                font-family: 'Comic Sans MS'; text-align: left">
                Advertiser Project Hostory.</td>
        </tr>
    </table>
    <table style="width: 850px">
        <tr>
            <td style="font-weight: normal; font-size: 10pt; vertical-align: top; font-family: Arial;
                text-align: right">
                <br />
                <asp:RequiredFieldValidator ID="rqvAdvertisers" runat="server" ControlToValidate="lstAdAccounts"
                    Display="Dynamic" ErrorMessage="* Must select at least one option in the filter of Advertisers."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator><asp:Label
                        ID="lblAdvertisers" runat="server" Text="Advertiser:"></asp:Label>&nbsp;</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
                <br />
                <asp:ListBox ID="lstAdAccounts" runat="server" AutoPostBack="True" Height="100px"
                    SelectionMode="Multiple" Width="250px"></asp:ListBox></td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: right">
                <br />
                <asp:RequiredFieldValidator ID="rqvProjects" runat="server" ControlToValidate="lstAdProjects"
                    Display="Dynamic" ErrorMessage="* Must select at least one option in the filter of Projects."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator><asp:Label
                        ID="lblProjects" runat="server" Text="Project:"></asp:Label></td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
                <br />
                <asp:ListBox ID="lstAdProjects" runat="server" Height="100px" SelectionMode="Multiple"
                    Width="250px"></asp:ListBox></td>
        </tr>
        <tr>
            <td style="font-weight: normal; font-size: 10pt; vertical-align: top; font-family: Arial;
                text-align: right">
                <asp:CustomValidator ID="csvStartDate" runat="server" ErrorMessage="* Invalid Start Date."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator>Start
                Date:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
                <asp:DropDownList ID="ddlStartMonth" runat="server" AppendDataBoundItems="True" Width="110px">
                    <asp:ListItem></asp:ListItem>
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
                <asp:DropDownList ID="ddlStartDay" runat="server" Width="50px">
                    <asp:ListItem></asp:ListItem>
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
                ,&nbsp;<asp:DropDownList ID="ddlStartYear" runat="server" Width="74px">
                </asp:DropDownList></td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: right">
                <asp:CustomValidator ID="csvEndDate" runat="server" ErrorMessage="* Invalid End Date."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator>End
                Date:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
                <asp:DropDownList ID="ddlEndMonth" runat="server" AppendDataBoundItems="True" Width="110px">
                    <asp:ListItem></asp:ListItem>
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
                <asp:DropDownList ID="ddlEndDay" runat="server" Width="50px">
                    <asp:ListItem></asp:ListItem>
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
                ,&nbsp;<asp:DropDownList ID="ddlEndYear" runat="server" Width="74px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="font-weight: normal; font-size: 10pt; vertical-align: top; font-family: Arial;
                text-align: right">
                <asp:CustomValidator ID="csvStartTime" runat="server" ErrorMessage="* Invalid Start Time."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator>Start
                Time:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
                <asp:DropDownList ID="ddlStartHour" runat="server" Width="75px">
                    <asp:ListItem></asp:ListItem>
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
                </asp:DropDownList>
                :
                <asp:DropDownList ID="ddlStartMinute" runat="server" Width="75px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="00">00</asp:ListItem>
                    <asp:ListItem>05</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>35</asp:ListItem>
                    <asp:ListItem>40</asp:ListItem>
                    <asp:ListItem>45</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>55</asp:ListItem>
                </asp:DropDownList>
                :
                <asp:DropDownList ID="ddlStartAMPM" runat="server" Width="76px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="AM">AM</asp:ListItem>
                    <asp:ListItem Value="PM">PM</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: right">
                <asp:CustomValidator ID="csvEndTime" runat="server" ErrorMessage="* Invalid End Time."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator>End
                Time:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
                <asp:DropDownList ID="ddlEndHour" runat="server" Width="75px">
                    <asp:ListItem></asp:ListItem>
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
                </asp:DropDownList>
                :
                <asp:DropDownList ID="ddlEndMinute" runat="server" Width="75px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="00">00</asp:ListItem>
                    <asp:ListItem>05</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>35</asp:ListItem>
                    <asp:ListItem>40</asp:ListItem>
                    <asp:ListItem>45</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>55</asp:ListItem>
                </asp:DropDownList>
                :
                <asp:DropDownList ID="ddlEndAMPM" runat="server" Width="76px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="AM">AM</asp:ListItem>
                    <asp:ListItem Value="PM">PM</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table style="width: 850px">
        <tr>
            <td style="border-top: silver 1px solid">
                <asp:ValidationSummary ID="vldSummary" runat="server" DisplayMode="List" Font-Bold="True"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="792px" />
            </td>
        </tr>
        <tr>
            <td style="border-top: silver 1px solid; text-align: right">
                <asp:Menu ID="mnuSubmit" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
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
                        <asp:MenuItem ImageUrl="~/Images/New.PNG" Text=" Submit" Value="Submit"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" ForeColor="#990000" />
                </asp:Menu>
            </td>
        </tr>
    </table>
    &nbsp;<br />
    <asp:GridView ID="grdProjectHistory" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" CellPadding="2" Font-Bold="True" Font-Names="Comic Sans MS"
        Font-Overline="False" Font-Size="14pt" ForeColor="Black" PageSize="15" ShowFooter="True"
        UseAccessibleHeader="False" Width="850px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <RowStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:TemplateField HeaderText="Advertiser" SortExpression="Advertiser">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkAdvertiser" runat="server" CommandArgument='<%# Eval("AdvertiserID") %>'
                        Text='<%# Eval("Advertiser") %>' OnClick="lnkAdvertiser_Click"></asp:LinkButton><asp:Label ID="lblAdvertiser" runat="server"
                            Text='<%# Eval("AdvertiserID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Proj." SortExpression="Project">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkProject" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Project") %>' Text='<%# Eval("Project") %>' OnClick="lnkProject_Click"></asp:LinkButton><asp:Label ID="lblProject" runat="server" Text='<%# Eval("Project") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="URLDisplay" HeaderText="URL Display" SortExpression="URLDisplay">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="DateDisplay" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date"
                SortExpression="DateDisplay">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
            </asp:BoundField>
            <asp:BoundField DataField="TimeDisplay" DataFormatString="{0:hh:mm:tt}" HeaderText="Time"
                SortExpression="TimeDisplay">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="URLClickThru" HeaderText="URL Click Thru" SortExpression="URLClickThru">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="DateClickThru" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date"
                SortExpression="DateClickThru">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
            </asp:BoundField>
            <asp:BoundField DataField="TimeClickThru" DataFormatString="{0:hh:mm:tt}" HeaderText="Time"
                SortExpression="TimeClickThru">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
            </asp:BoundField>
        </Columns>
        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
            Wrap="False" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" runat="server" ImageUrl="~/Images/TODO16.BMP" />No Data
            Found to Show.
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <br />
</asp:Content>

