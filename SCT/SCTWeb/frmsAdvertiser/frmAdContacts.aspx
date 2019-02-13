<%@ Page Language="VB" MasterPageFile="~/frmsAdvertiser/MasterPage.master" AutoEventWireup="false" CodeFile="frmAdContacts.aspx.vb" Inherits="frmsAdvertiser_frmAdContacts" title="Advertiser Contacts" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <table style="width: 800px">
        <tr>
            <td colspan="5" style="font-weight: bold; font-size: 14pt; border-bottom: silver 1px solid;
                font-family: 'Comic Sans MS'; text-align: left">
                Contacts</td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="grdAdContacts" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CaptionAlign="Left" CellPadding="2" Font-Bold="True"
        Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="14pt" ForeColor="Black"
        PageSize="15" ShowFooter="True" Width="800px" AllowSorting="True">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:TemplateField HeaderText="ID" SortExpression="ID">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkView" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' OnClick="lnkView_Click" Text='<%# Eval("ID") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="Address" HeaderText="Address">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:ImageField DataImageUrlField="MCA" HeaderText="*MCA">
                <ItemStyle Width="40px" />
            </asp:ImageField>
            <asp:TemplateField HeaderText="Projs">
                <ItemTemplate>
                    <asp:ImageButton ID="cmdProjects" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/PROJECT16.BMP" OnClick="cmdProjects_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes">
                <ItemTemplate>
                    <asp:ImageButton ID="cmdNotes" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/Report.gif" OnClick="cmdNotes_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To Do">
                <ItemTemplate>
                    <asp:ImageButton ID="cmdToDos" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/TODO16.BMP" OnClick="cmdToDos_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:ImageButton ID="cmdEdit" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/EDIT16.GIF" OnClick="cmdEdit_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Del">
                <ItemTemplate>
                    <asp:ImageButton ID="cmdDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/DELETE16.GIF" OnClick="cmdDelete_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
            </asp:TemplateField>
        </Columns>
        <RowStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" runat="server" ImageUrl="~/Images/USER16.gif" />No
            Data Found to Show.
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
            Wrap="False" />
        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <table width="800">
        <tr>
            <td align="left" style="font-size: 8pt; color: orangered; font-family: Arial" valign="top">
                * Main Company Addres</td>
            <td align="right">
                <asp:Menu ID="cmdAddNew" runat="server" DynamicHorizontalOffset="2" Font-Names="Comic Sans MS"
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

