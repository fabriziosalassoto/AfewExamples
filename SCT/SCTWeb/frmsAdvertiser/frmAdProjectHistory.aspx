<%@ Page Language="VB" MasterPageFile="~/frmsAdvertiser/MasterPage.master" AutoEventWireup="false" CodeFile="frmAdProjectHistory.aspx.vb" Inherits="frmsAdvertiser_frmAdProjectHistory" title="Advertiser Project History" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    &nbsp;&nbsp;<cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <table style="width: 800px">
        <tr>
            <td colspan="5" style="font-weight: bold; font-size: 14pt; border-bottom: silver 1px solid;
                font-family: 'Comic Sans MS'; text-align: left">
                Project History</td>
        </tr>
    </table>
    <table style="width: 800px">
        <tr>
            <td style="font-weight: normal; font-size: 10pt; vertical-align: top;
                font-family: Arial; text-align: right" rowspan="4">
                <br />
                Filter by
                <asp:RequiredFieldValidator ID="rvAdProjects" runat="server" ControlToValidate="lstAdProjects"
                    Display="Dynamic" ErrorMessage="* Must select at least one option in the filter of Projects."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator>Projects:
            </td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left;" rowspan="4">
                <br />
                <asp:ListBox ID="lstAdProjects" runat="server" Height="100px" SelectionMode="Multiple"
                    Width="200px"></asp:ListBox></td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: right" align="right">
                <br />
                <asp:CustomValidator ID="csvStartDate" runat="server" ErrorMessage="* Invalid Start Date."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator>Start
                Date:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
                <br />
                <asp:DropDownList ID="ddlStartMonth" runat="server" AppendDataBoundItems="True" Width="116px">
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
                ,&nbsp;<asp:DropDownList ID="ddlStartYear" runat="server" Width="78px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: right" align="right">
                <asp:CustomValidator ID="csvEndDate" runat="server" ErrorMessage="* Invalid End Date."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator>End
                Date:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
                <asp:DropDownList ID="ddlEndMonth" runat="server" AppendDataBoundItems="True" Width="116px">
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
                ,&nbsp;<asp:DropDownList ID="ddlEndYear" runat="server" Width="78px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: right" align="right">
                <asp:CustomValidator ID="csvStartTime" runat="server" ErrorMessage="* Invalid Start Time."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator>Start Time:
            </td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
                <asp:DropDownList ID="ddlStartHour" runat="server" Width="78px">
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
                <asp:DropDownList ID="ddlStartMinute" runat="server" Width="80px">
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
                <asp:DropDownList ID="ddlStartAMPM" runat="server" Width="79px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="AM">AM</asp:ListItem>
                    <asp:ListItem Value="PM">PM</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" style="font-size: 10pt; vertical-align: top; font-family: Arial;
                text-align: right">
                <asp:CustomValidator ID="csvEndTime" runat="server" ErrorMessage="* Invalid End Time."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator>End Time:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left"><asp:DropDownList ID="ddlEndHour" runat="server" Width="78px">
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
                <asp:DropDownList ID="ddlEndMinute" runat="server" Width="80px">
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
                <asp:DropDownList ID="ddlEndAMPM" runat="server" Width="79px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="AM">AM</asp:ListItem>
                    <asp:ListItem Value="PM">PM</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table style="width: 800px">
        <tr>
            <td style="border-top: silver 1px solid">
                <asp:ValidationSummary ID="vldSummary" runat="server" DisplayMode="List" Font-Bold="True"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="784px" />
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
    <br />
    <asp:GridView ID="grdProjectHistory" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" CellPadding="2"
        Font-Bold="True" Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="14pt"
        ForeColor="Black" PageSize="15" ShowFooter="True" UseAccessibleHeader="False"
        Width="800px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:TemplateField HeaderText="Proj." SortExpression="ProjectID">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkID" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ProjectID") %>'
                        Text='<%# Eval("ProjectID") %>' OnClick="lnkID_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="URLDisplay" HeaderText="URL Display" SortExpression="URLDisplay">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="DateDisplay" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" SortExpression="DateDisplay">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
            </asp:BoundField>
            <asp:BoundField DataField="TimeDisplay" HeaderText="Time" DataFormatString="{0:hh:mm:tt}" SortExpression="TimeDisplay">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="URLClickThru" HeaderText="URL Click Thru" SortExpression="URLClickThru">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="DateClickThru" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date"
                SortExpression="DateClickThru">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
            </asp:BoundField>
            <asp:BoundField DataField="TimeClickThru" HeaderText="Time" DataFormatString="{0:hh:mm:tt}" SortExpression="TimeClickThru">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
            </asp:BoundField>
        </Columns>
        <RowStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" runat="server" ImageUrl="~/Images/TODO16.BMP" />No Data
            Found to Show.
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
            Wrap="False" />
        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <br />
</asp:Content>

