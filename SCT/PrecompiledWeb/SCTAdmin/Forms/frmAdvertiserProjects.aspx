<%@ page language="VB" masterpagefile="~/Forms/MasterPage.master" autoeventwireup="false" inherits="Forms_frmAdvertiserProjects, App_Web_qbwzvytd" title="IP Tracking Admin System - Projects" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    &nbsp;<br />
    <table style="width: 750px">
        <tr>
            <td colspan="5" style="font-weight: bold; font-size: 14pt; border-bottom: silver 1px solid;
                font-family: 'Comic Sans MS'; text-align: left">
                Advertiser Projects.</td>
        </tr>
    </table>
    <table style="width: 750px">
        <tr>
            <td style="font-weight: normal; font-size: 10pt; vertical-align: top; width: 130px;
                font-family: Arial; text-align: right">
                <br />
                Filter by
                <asp:RequiredFieldValidator ID="rqvAdvertisers" runat="server" ControlToValidate="lstAdvertisers"
                    Display="Dynamic" ErrorMessage="* Must select at least one option in the filter of Advertisers."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator><asp:Label
                        ID="lblAdvertisers" runat="server" Text="Advertiser:"></asp:Label>&nbsp;</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left; width: 255px;">
                <br />
                <asp:ListBox ID="lstAdvertisers" runat="server" Height="100px" SelectionMode="Multiple"
                    Width="250px" AutoPostBack="True"></asp:ListBox></td>
            <td style="font-size: 10pt; vertical-align: top; width: 90px; font-family: Arial;
                text-align: right">
                <br />
                <asp:RequiredFieldValidator ID="rqvContacts" runat="server" ControlToValidate="lstContacts"
                    Display="Dynamic" ErrorMessage="* Must select at least one option in the filter of Contacts."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator><asp:Label
                        ID="lblContacts" runat="server" Text="Contact:"></asp:Label></td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
                <br />
                <asp:ListBox ID="lstContacts" runat="server" Height="100px" SelectionMode="Multiple"
                    Width="250px"></asp:ListBox></td>
        </tr>
    </table>
    <table style="width: 750px">
        <tr>
            <td style="border-top: silver 1px solid">
                <asp:ValidationSummary ID="vldSummary" runat="server" DisplayMode="List" Font-Bold="True"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="712px" />
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
    <asp:GridView ID="grdAdProjects" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" CaptionAlign="Left" CellPadding="2" Font-Bold="True" Font-Names="Comic Sans MS"
        Font-Overline="False" Font-Size="14pt" ForeColor="Black" PageSize="15" ShowFooter="True"
        UseAccessibleHeader="False" Width="750px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <RowStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:TemplateField HeaderText="ID" SortExpression="ID">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkID" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' Text='<%# Eval("ID") %>' OnClick="lnkID_Click"></asp:LinkButton><asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="URL" HeaderText="URL" SortExpression="URL">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Advertiser" SortExpression="Advertiser">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkAdvertiser" runat="server" CommandArgument='<%# Eval("AdvertiserID") %>'
                        Text='<%# Eval("Advertiser") %>' OnClick="lnkAdvertiser_Click"></asp:LinkButton><asp:Label ID="lblAdvertiser" runat="server"
                            Text='<%# Eval("AdvertiserID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contact" SortExpression="Contact">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkContact" runat="server" CommandArgument='<%# Eval("ContactID") %>'
                         Text='<%# Eval("Contact") %>' OnClick="lnkContact_Click"></asp:LinkButton><asp:Label
                            ID="lblContact" runat="server" Text='<%# Eval("Contact") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:ImageButton ID="cmdEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>'
                        ImageUrl="~/Images/EDIT16.GIF" OnClick="cmdEdit_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Del">
                <ItemTemplate>
                    <asp:ImageButton ID="cmdDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>'
                        ImageUrl="~/Images/DELETE16.GIF" OnClick="cmdDelete_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25px" />
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
            Wrap="False" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" runat="server" ImageUrl="~/Images/PROJECT16.BMP" />No
            Data Found to Show.
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <table width="750">
        <tr>
            <td align="right">
                <asp:Menu ID="mnuAddNew" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
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
                        <asp:MenuItem ImageUrl="~/Images/USER16.gif" Text=" Add New Contact" Value="AddNew">
                        </asp:MenuItem>
                    </Items>
                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Comic Sans MS"
                        Font-Size="10pt" Font-Underline="False" ForeColor="#990000" />
                </asp:Menu>
            </td>
        </tr>
    </table>
</asp:Content>

