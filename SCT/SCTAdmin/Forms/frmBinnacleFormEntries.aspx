<%@ Page Language="VB" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="false" CodeFile="frmBinnacleFormEntries.aspx.vb" Inherits="Forms_frmBinnacleFormEntries" title="IP Tracking Admin System - Log Entries" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
                <table style="width: 700px">
                    <tr>
                        <td colspan="5" style="font-weight: bold; font-size: 14pt; border-bottom: silver 1px solid;
                            font-family: 'Comic Sans MS'; text-align: left">
                Query Log Entries</td>
                    </tr>
                    <tr>
                        <td style="font-size: 10pt; width: 76px; font-family: Arial; text-align: right">
                            <br />
                Query and
                <br />
                Sort by:</td>
                        <td style="font-size: 10pt; width: 343px; font-family: Arial; text-align: left;">
                            <br />
                <asp:CheckBoxList ID="cklFilter" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Width="336px" Font-Names="Arial" Font-Size="10pt">
                    <asp:ListItem Value="Log">Log</asp:ListItem>
                    <asp:ListItem Value="BDate">Date</asp:ListItem>
                    <asp:ListItem Value="User">User</asp:ListItem>
                    <asp:ListItem Value="Form">Form</asp:ListItem>
                    <asp:ListItem Value="Operation">Operation</asp:ListItem>
                </asp:CheckBoxList></td>
                        <td style="font-size: 10pt; font-family: Arial; text-align: right;">
                            <br />
                            Order:</td>
                        <td style="font-size: 10pt; font-family: Arial; text-align: left;">
                            <br />
                <asp:RadioButtonList ID="rdlSortDirection" runat="server" RepeatDirection="Horizontal" Width="184px" Font-Names="Arial" Font-Size="10pt">
                    <asp:ListItem Selected="True" Value="ASC">Ascending </asp:ListItem>
                    <asp:ListItem Value="DESC">Descending </asp:ListItem>
                </asp:RadioButtonList></td>
                    </tr>
                </table>
    <table style="width: 700px">
        <tr>
            <td style="font-weight: normal; font-size: 10pt; vertical-align: top; font-family: Arial;
                text-align: right;">
                <br />
                <asp:RequiredFieldValidator ID="rfvLogs" runat="server" ErrorMessage="* Must select at least one option."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed" ControlToValidate="lstLogs" Display="Dynamic" Enabled="False">*</asp:RequiredFieldValidator>Log:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left;">
                <br />
                <asp:ListBox ID="lstLogs" runat="server" Enabled="False" Height="80px" SelectionMode="Multiple"
                    Width="250px" AutoPostBack="True"></asp:ListBox></td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: right;">
                <br />
                <asp:RequiredFieldValidator ID="rfvOperations" runat="server" ErrorMessage="* Must select at least one option."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed" ControlToValidate="lstOperations" Display="Dynamic" Enabled="False">*</asp:RequiredFieldValidator>Operation:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left;">
                <br />
                <asp:ListBox ID="lstOperations" runat="server" Enabled="False" Height="80px" SelectionMode="Multiple"
                    Width="250px"></asp:ListBox>
                </td>
        </tr>
        <tr>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: right;">
                <asp:RequiredFieldValidator ID="rfvUsers" runat="server" ErrorMessage="* Must select at least one option."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed" ControlToValidate="lstUsers" Display="Dynamic" Enabled="False">*</asp:RequiredFieldValidator>User:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left;">
                <asp:ListBox ID="lstUsers" runat="server" Enabled="False" Height="80px" SelectionMode="Multiple"
                    Width="250px"></asp:ListBox></td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: right;">
                <asp:RequiredFieldValidator ID="rfvForms" runat="server" ErrorMessage="* Must select at least one option."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed" ControlToValidate="lstForms" Display="Dynamic" Enabled="False">*</asp:RequiredFieldValidator>Form:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left;">
                <asp:ListBox ID="lstForms" runat="server" Enabled="False" Height="80px" SelectionMode="Multiple"
                    Width="250px"></asp:ListBox></td>
        </tr>
        <tr>
            <td style="font-size: 10pt; font-family: Arial; text-align: right;">
                <asp:CustomValidator ID="vldStartDate" runat="server" Enabled="False" ErrorMessage="* Invalid Start Date."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator>Start Date:</td>
            <td contenteditable="true" rowspan="1" style="font-size: 10pt; vertical-align: top;
                font-family: Arial; text-align: left;" colspan="1">
                <asp:DropDownList ID="ddlStartMonth" runat="server" Enabled="False" Width="104px">
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
                <asp:DropDownList ID="ddlStartDay" runat="server" Enabled="False" Width="50px">
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
                ,&nbsp;<asp:DropDownList ID="ddlStartYear" runat="server" Width="80px" Enabled="False">
                </asp:DropDownList>
                </td>
            <td style="font-size: 10pt; font-family: Arial; text-align: right;">
                <asp:CustomValidator ID="vldEndDate" runat="server" Enabled="False" ErrorMessage="* Invalid End Date."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator>End Date:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left;" colspan="1">
                <asp:DropDownList ID="ddlEndMonth" runat="server" Enabled="False" Width="104px">
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
                <asp:DropDownList ID="ddlEndDay" runat="server" Enabled="False" Width="50px">
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
                ,&nbsp;<asp:DropDownList ID="ddlEndYear" runat="server" Width="80px" Enabled="False">
                </asp:DropDownList>
                </td>
        </tr>
    </table>
    <table style="width: 700px">
        <tr>
            <td colspan="2" style="border-top: silver 1px solid">
                <asp:ValidationSummary ID="vldSummary" runat="server" DisplayMode="List" Font-Bold="True"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="688px" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border-top: silver 1px solid; text-align: right">
                <asp:Menu ID="mnuSubmitQuery" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
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
                        <asp:MenuItem ImageUrl="~/Images/New.PNG" Text=" Submit Query" Value="Submit Query">
                        </asp:MenuItem>
                    </Items>
                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" ForeColor="#990000" />
                </asp:Menu>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:GridView ID="grdBinnacleFormEntries" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" CaptionAlign="Left" CellPadding="0" Font-Bold="True"
        Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="10pt" ForeColor="Black"
        PageSize="25" ShowFooter="True" UseAccessibleHeader="False" Width="700px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:BoundField HtmlEncode="False">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField HtmlEncode="False">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField HtmlEncode="False">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField HtmlEncode="False">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField HtmlEncode="False" >
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Detail" ShowHeader="False">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdDetail" runat="server" AlternateText="Detail" CausesValidation="False"
                        CommandArgument='<%# Eval("BinnacleFormID") %>' CommandName='<%# Eval("LogValue") %>'
                        ImageUrl="~/Images/Detail.gif" OnClick="cmdDetail_Click"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Size="9pt" ForeColor="Black" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" runat="server" ImageUrl="~/Images/Binnacle.BMP" />
            No Data Found to Show.
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
            Wrap="False" />
        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Height="20px" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
</asp:Content>

