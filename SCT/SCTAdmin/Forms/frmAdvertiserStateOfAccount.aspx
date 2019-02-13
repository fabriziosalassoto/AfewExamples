<%@ Page Language="VB" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="false" CodeFile="frmAdvertiserStateOfAccount.aspx.vb" Inherits="Forms_frmAdvertiserStateOfAccount" title="IP Tracking Admin System - State of Account" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <table style="width: 750px">
        <tr>
            <td colspan="5" style="font-weight: bold; font-size: 14pt; border-bottom: silver 1px solid;
                font-family: 'Comic Sans MS'; text-align: left">
                State of Account.</td>
        </tr>
    </table>
    <table style="width: 750px">
        <tr>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: right">
                <br />
                <asp:RequiredFieldValidator ID="rfvAdvertisers" runat="server" ControlToValidate="lstAdvertisers"
                    Display="Dynamic" ErrorMessage="* Must select at least one option in the filter of Advertisers."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator>Advertiser:</td>
            <td contenteditable="true" rowspan="1" style="font-size: 10pt; vertical-align: top;
                font-family: Arial; text-align: left">
                <br />
                <asp:ListBox ID="lstAdvertisers" runat="server" AutoPostBack="True" Height="100px"
                    SelectionMode="Multiple" Width="245px"></asp:ListBox></td>
            <td style="font-size: 10pt; font-family: Arial; text-align: right; vertical-align: top;">
                <br />
                <asp:RequiredFieldValidator ID="rfvProjects" runat="server" ControlToValidate="lstProjects"
                    Display="Dynamic" ErrorMessage="* Must select at least one option in the filter of Projects."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator>Project:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
                <br />
                <asp:ListBox ID="lstProjects" runat="server" Height="100px" SelectionMode="Multiple"
                    Width="245px"></asp:ListBox></td>
        </tr>
        <tr>
            <td style="font-size: 10pt; font-family: Arial; text-align: right">
                <asp:CustomValidator ID="vldStartDate" runat="server" ErrorMessage="* Invalid Start Date."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" Display="Dynamic">*</asp:CustomValidator>Start
                Date:</td>
            <td contenteditable="true" rowspan="1" style="font-size: 10pt; vertical-align: top;
                font-family: Arial; text-align: left">
                <asp:DropDownList ID="ddlStartMonth" runat="server" Width="104px">
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
                </asp:DropDownList>,&nbsp;<asp:DropDownList ID="ddlStartYear" runat="server" Width="80px">
                </asp:DropDownList></td>
            <td style="font-size: 10pt; font-family: Arial; text-align: right">
                <asp:CustomValidator ID="vldEndDate" runat="server" ErrorMessage="* Invalid End Date."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" Display="Dynamic">*</asp:CustomValidator>End
                Date:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
                <asp:DropDownList ID="ddlEndMonth" runat="server" Width="104px">
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
                </asp:DropDownList>,&nbsp;<asp:DropDownList ID="ddlEndYear" runat="server" Width="80px">
                </asp:DropDownList></td>
        </tr>
    </table>
    <table style="width: 750px">
        <tr>
            <td colspan="2" style="border-top: silver 1px solid">
                <asp:ValidationSummary ID="vldSummary" runat="server" DisplayMode="List" Font-Bold="True"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="736px" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border-top: silver 1px solid; text-align: right">
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
    <asp:GridView ID="grdTransactions" runat="server" AllowSorting="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" CaptionAlign="Left" CellPadding="0" Font-Bold="True" Font-Names="Comic Sans MS"
        Font-Overline="False" Font-Size="11pt" PageSize="25" ShowFooter="True"
        UseAccessibleHeader="False" Width="750px" Caption="Transactions">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" FirstPageText="First" LastPageImageUrl="~/Images/STEPFORW.GIF"
            LastPageText="Last" Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF"
            NextPageText="Next" PreviousPageImageUrl="~/Images/REWIND.GIF" PreviousPageText="Previous" />
        <FooterStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="XX-Small"
            ForeColor="White" />
        <RowStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Size="9pt" ForeColor="Black" Height="18px" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" BorderStyle="None" Height="18px" />
        <Columns>
            <asp:TemplateField HeaderText="Advertiser" SortExpression="Advertiser">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkAdvertiser" runat="server" CommandArgument='<%# Eval("IDAdvertiser") %>'
                        Text='<%# Eval("Advertiser") %>' CausesValidation="False" OnClick="lnkAdvertiser_Click"></asp:LinkButton><asp:Label ID="lblAdvertiser" runat="server"
                            Text='<%# Eval("Advertiser") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Project" SortExpression="Project">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkProject" runat="server" CausesValidation="False" CommandArgument='<%# Eval("IDProject") %>'
                        Text='<%# Eval("Project") %>' OnClick="lnkProject_Click"></asp:LinkButton><asp:Label ID="lblProject" runat="server"
                            Text='<%# Eval("Project") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:BoundField DataField="TransactionDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date"
                HtmlEncode="False" SortExpression="TransactionDate">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Trans. Nbr" SortExpression="TransactionNumber">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkNumber" runat="server" CommandArgument='<%# Eval("IDTransaction") %>'
                        Text='<%# Eval("TransactionNumber") %>' CommandName='<%# Eval("Transaction") %>' CausesValidation="False" OnClick="lnkNumber_Click"></asp:LinkButton><asp:Label ID="lblNumber" runat="server"
                            Text='<%# Eval("TransactionNumber") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
            </asp:TemplateField>
            <asp:BoundField DataField="InvoiceAmount" DataFormatString="{0:#,###,##0.00}" HeaderText="Invoice">
                <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="ReceiptAmount" DataFormatString="{0:#,###,##0.00}" HeaderText="Receipt">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
        </Columns>
        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
            Wrap="False" />
        <EmptyDataTemplate>
            No Transaction Found to Show.
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
            ForeColor="White" Height="18px" HorizontalAlign="Center" VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <br />
    <asp:Table ID="tblBalance" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both"
        Width="750px" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Caption="Balance" CaptionAlign="Left" Font-Bold="True" Font-Names="Comic Sans MS" Font-Size="11pt">
        <asp:TableRow ID="trwHeader" runat="server" BackColor="Black" Font-Bold="True" Font-Names="Arial"
            Font-Size="9pt" ForeColor="White" Height="18px" HorizontalAlign="Center" TableSection="TableHeader"
            VerticalAlign="Middle">
            <asp:TableCell ID="hclStartBalance" runat="server" Width="187px">Start Balance</asp:TableCell>
            <asp:TableCell ID="hclInvoices" runat="server" Width="187px">Invoices</asp:TableCell>
            <asp:TableCell ID="hclReceipts" runat="server" Width="187px">Receipts</asp:TableCell>
            <asp:TableCell ID="hclEndBalance" runat="server" Width="187px">End Balance</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="trwBody" runat="server" BackColor="White" Font-Names="Arial" Font-Size="9pt"
            HorizontalAlign="Center" VerticalAlign="Middle">
            <asp:TableCell ID="bclStartBalance" runat="server"></asp:TableCell>
            <asp:TableCell ID="bclInvoices" runat="server"></asp:TableCell>
            <asp:TableCell ID="bclReceipts" runat="server"></asp:TableCell>
            <asp:TableCell ID="bclEndBalance" runat="server"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="trwFooter" runat="server" BackColor="Black" Font-Names="Arial"
            Font-Size="XX-Small" HorizontalAlign="Center" TableSection="TableFooter" VerticalAlign="Middle">
            <asp:TableCell ID="fclBalance" runat="server" ColumnSpan="4">Balance</asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

