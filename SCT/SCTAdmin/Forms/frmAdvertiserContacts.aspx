<%@ Page Language="VB" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="false" CodeFile="frmAdvertiserContacts.aspx.vb" Inherits="Forms_frmAdvertiserContacts" title="IP Tracking Admin System - Advertiser Contacts" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    &nbsp;<br />
    <table style="width: 750px">
        <tr>
            <td colspan="5" style="font-weight: bold; font-size: 14pt; border-bottom: silver 1px solid;
                font-family: 'Comic Sans MS'; text-align: left">
                Advertiser Contacts.</td>
        </tr>
    </table>
    <table style="width: 450px">
        <tr>
            <td style="font-weight: normal; font-size: 10pt; vertical-align: top; width: 130px;
                font-family: Arial; text-align: right">
                <br />
                Filter by
                <asp:RequiredFieldValidator ID="rvAdvertisers" runat="server" ControlToValidate="lstAdAccounts"
                    Display="Dynamic" ErrorMessage="* Must select at least one option in the filter of Advertisers."
                    Font-Bold="True" Font-Size="Large" ForeColor="OrangeRed">*</asp:RequiredFieldValidator><asp:Label
                        ID="lblAdvertisers" runat="server" Text="Advertiser:"></asp:Label>&nbsp;</td>
            <td style="font-size: 10pt; vertical-align: top; font-family: Arial; text-align: left">
                <br />
                <asp:ListBox ID="lstAdAccounts" runat="server" Height="100px"
                    SelectionMode="Multiple" Width="300px"></asp:ListBox></td>
        </tr>
    </table>
    <table style="width: 450px">
        <tr>
            <td style="border-top: silver 1px solid">
                <asp:ValidationSummary ID="vldSummary" runat="server" DisplayMode="List" Font-Bold="True"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="425px" />
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
    <asp:GridView ID="grdAdContacts" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" CaptionAlign="Left" CellPadding="2"
        Font-Bold="True" Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="14pt"
        ForeColor="Black" PageSize="15" ShowFooter="True" Width="750px">
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
                    <asp:LinkButton ID="lnkView" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' Text='<%# Eval("ID") %>' OnClick="lnkView_Click">
                    </asp:LinkButton><asp:Label ID="lblView" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Advertiser" SortExpression="Advertiser">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkAdvertiser" runat="server" CommandArgument='<%# Eval("AdvertiserID") %>'
                        Text='<%# Eval("Advertiser") %>' OnClick="lnkAdvertiser_Click"></asp:LinkButton><asp:Label ID="lblAdvertiser" runat="server"
                            Text='<%# Eval("Advertiser") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
            </asp:TemplateField>
            <asp:BoundField DataField="Address" HeaderText="Address">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:ImageField DataImageUrlField="MCA" HeaderText="*MCA">
                <ItemStyle Width="30px" />
            </asp:ImageField>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:ImageButton ID="cmdEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>'
                        ImageUrl="~/Images/EDIT16.GIF" OnClick="cmdEdit_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Del">
                <ItemTemplate>
                    <asp:ImageButton ID="cmdDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>'
                        ImageUrl="~/Images/DELETE16.GIF" OnClick="cmdDelete_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
            Wrap="False" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" runat="server" ImageUrl="~/Images/USER16.gif" />No Data
            Found to Show.
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <table width="750">
        <tr>
            <td align="left" style="font-size: 8pt; color: orangered; font-family: Arial">
                * Main Company Addres</td>
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

