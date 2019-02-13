<%@ page language="VB" masterpagefile="~/Forms/MasterPage.master" autoeventwireup="false" inherits="Forms_frmAdvertiserTransactions, App_Web_qbwzvytd" title="IP Tracking Admin System - Transactions" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <table style="width: 750px">
        <tr>
            <td colspan="5" style="font-weight: bold; font-size: 14pt; border-bottom: silver 1px solid;
                font-family: 'Comic Sans MS'; text-align: left">
                Transaction of Account.</td>
        </tr>
    </table>
    <table style="width: 750px">
        <tr>
            <td style="font-weight: normal; font-size: 10pt; vertical-align: top;
                font-family: Arial; text-align: right; width: 90px;">
                <br />
                <asp:RequiredFieldValidator ID="rfvAdvertisers" runat="server" ControlToValidate="lstAdvertisers"
                    Display="Dynamic" ErrorMessage="* Must select at least one option in the filter of Advertisers."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator>Advetiser:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial;
                text-align: left; width: 230px;">
                <br />
                <asp:ListBox ID="lstAdvertisers" runat="server" AutoPostBack="True" Height="100px"
                    SelectionMode="Multiple" Width="225px"></asp:ListBox></td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial;
                text-align: right; width: 81px;">
                <br />
                <asp:RequiredFieldValidator ID="rfvProjects" runat="server" ControlToValidate="lstProjects"
                    Display="Dynamic" ErrorMessage="* Must select at least one option in the filter of Projects."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator>Project:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial;
                text-align: left;">
                <br />
                <asp:ListBox ID="lstProjects" runat="server" Height="100px" SelectionMode="Multiple"
                    Width="320px"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td style="font-size: 10pt; font-family: Arial; text-align: right; width: 90px; vertical-align: top;">
                <asp:RequiredFieldValidator ID="rfvTransactions" runat="server" ControlToValidate="lstTransactions"
                    Display="Dynamic" ErrorMessage="* Must select at least one option in the filter of Transactions."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator>Transaction:</td>
            <td contenteditable="true" rowspan="2" style="font-size: 10pt; vertical-align: top;
                width: 230px; font-family: Arial; text-align: left">
                <asp:ListBox ID="lstTransactions" runat="server" Height="60px"
                    SelectionMode="Multiple" Width="225px">
                </asp:ListBox></td>
            <td style="font-size: 10pt; font-family: Arial; text-align: right; width: 81px;">
                <asp:CustomValidator ID="vldStartDate" runat="server" ErrorMessage="* Invalid Start Date."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator>Start Date:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
            <asp:DropDownList ID="ddlStartMonth" runat="server" Width="104px">
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
                </asp:DropDownList>,&nbsp;<asp:DropDownList ID="ddlStartYear" runat="server" Width="70px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="font-size: 10pt; font-family: Arial; text-align: right; width: 90px;">
                </td>
            <td style="font-size: 10pt; font-family: Arial; text-align: right; width: 81px;">
                <asp:CustomValidator ID="vldEndDate" runat="server" ErrorMessage="* Invalid End Date."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator>End Date:</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial;
                text-align: left;">
                <asp:DropDownList ID="ddlEndMonth" runat="server" Width="104px">
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
                </asp:DropDownList>,&nbsp;<asp:DropDownList ID="ddlEndYear" runat="server" Width="70px">
                </asp:DropDownList></td>
        </tr>
    </table>
    <table style="width: 750px">
        <tr>
            <td colspan="2" style="border-top: silver 1px solid;">
                <asp:ValidationSummary ID="vldSummary" runat="server" DisplayMode="List" Font-Bold="True"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="728px" />
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
                        <asp:MenuItem ImageUrl="~/Images/New.PNG" Text=" Submit" Value="Submit">
                        </asp:MenuItem>
                    </Items>
                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" ForeColor="#990000" />
                </asp:Menu>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="grdAdvertiserTransactions" runat="server" AllowSorting="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" CaptionAlign="Left" CellPadding="0" Font-Bold="True" Font-Names="Comic Sans MS"
        Font-Overline="False" Font-Size="10pt" ForeColor="Black" PageSize="25" ShowFooter="True"
        UseAccessibleHeader="False" Width="750px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" FirstPageText="First" LastPageImageUrl="~/Images/STEPFORW.GIF"
            LastPageText="Last" Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF"
            NextPageText="Next" PreviousPageImageUrl="~/Images/REWIND.GIF" PreviousPageText="Previous" />
        <FooterStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" ForeColor="White" Font-Size="9pt" HorizontalAlign="Right" VerticalAlign="Middle" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Height="20px" />
        <Columns>
            <asp:TemplateField HeaderText="Advertiser" SortExpression="Advertiser">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:LinkButton ID="lnkAdvertiser" runat="server" CausesValidation="False" CommandArgument='<%# Eval("IDAdvertiser") %>'
                        Text='<%# Eval("Advertiser") %>' OnClick="lnkAdvertiser_Click"></asp:LinkButton><asp:Label ID="lblAdvertiser" runat="server"
                            Text='<%# Eval("Advertiser") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Project" SortExpression="Project">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:LinkButton ID="lnkProject" runat="server" CausesValidation="False" CommandArgument='<%# Eval("IDProject") %>'
                        Text='<%# Eval("Project") %>' OnClick="lnkProject_Click"></asp:LinkButton><asp:Label ID="lblProject" runat="server"
                            Text='<%# Eval("Project") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HtmlEncode="False" DataField="TransactionDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date" SortExpression="TransactionDate">
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField FooterText="Totals:" HeaderText="Number" SortExpression="TransactionNumber">
                <EditItemTemplate>
                    &nbsp;
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkNumber" runat="server" CommandArgument='<%# Eval("IDTransaction") %>'
                        CommandName='<%# Eval("Transaction") %>' Text='<%# Eval("TransactionNumber") %>' OnClick="lnkNumber_Click" CausesValidation="False"></asp:LinkButton><asp:Label ID="lblNumber" runat="server" Text='<%# Eval("TransactionNumber") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
                <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:BoundField DataField="InvoiceAmount" HeaderText="Invoice">
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="ReceiptAmount" HeaderText="Receipt">
                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
            </asp:BoundField>
        </Columns>
        <RowStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Size="9pt" ForeColor="Black" Height="18px" />
        <EmptyDataTemplate>
            &nbsp;No Transaction Found to Show.
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
            Wrap="False" />
        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    
</asp:Content>

