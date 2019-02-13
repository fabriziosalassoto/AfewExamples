<%@ Page Language="VB" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="false" CodeFile="frmUsers.aspx.vb" Inherits="Forms_frmUsers" title="IP Tracking Admin System - Users" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    &nbsp;<cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <br />
    <asp:GridView ID="GridView" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" Caption="Users" CaptionAlign="Left" CellPadding="0" Font-Bold="True"
        Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="14pt" ForeColor="Black"
        ShowFooter="True" Width="500px" PageSize="15">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:TemplateField HeaderText="ID" ShowHeader="False" SortExpression="ID">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:LinkButton ID="lnkUserID" runat="server" CausesValidation="False" Text='<%# Eval("ID") %>' OnClick="lnkUserID_Click" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton><asp:Label ID="lblUserID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Profile" ShowHeader="False" SortExpression="Profile">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                <ItemTemplate>
                    <asp:LinkButton ID="lnkUserProfile" runat="server" CausesValidation="False" Text='<%# Eval("Profile") %>' OnClick="lnkUserProfile_Click" CommandArgument='<%# Eval("ProfileID") %>'></asp:LinkButton><asp:Label ID="lblUserProfile" runat="server" Text='<%# Eval("Profile") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdEdit" runat="server" AlternateText="Edit" CausesValidation="False"
                        CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/EDIT16.GIF" OnClick="cmdEdit_Click"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Del" ShowHeader="False">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdDelete" runat="server" AlternateText="Delete" CausesValidation="False"
                        CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/DELETE16.GIF" OnClick="cmdDelete_Click"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" BackColor="White" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" runat="server" ImageUrl="~/Images/User.PNG" />
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
    <table width="500">
        <tr>
            <td align="left" style="font-size: 9pt; font-family: Arial;">
                <asp:Label ID="lblProfiles" runat="server" Text="Filter by Profile:"></asp:Label>&nbsp;<asp:DropDownList ID="ddlProfiles" runat="server" AutoPostBack="True"
                    Font-Names="Arial" Font-Size="9pt" Width="208px">
                </asp:DropDownList></td>
            <td align="right">
                <asp:Menu ID="mnuAddNew" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
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
                        <asp:MenuItem ImageUrl="~/Images/New.PNG" Text=" Add New User" Value="AddNew"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" ForeColor="#990000" />
                </asp:Menu>
            </td>
        </tr>
    </table>
</asp:Content>

