<%@ Page Language="VB" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="false" CodeFile="frmForms.aspx.vb" Inherits="Forms_frmForms" title="IP Tracking Admin System - Forms" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <br />
    <asp:GridView ID="GridView" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" Caption="Forms" CaptionAlign="Left" CellPadding="0" Font-Bold="True"
        Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="14pt" ForeColor="Black" ShowFooter="True" UseAccessibleHeader="False" Width="600px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:TemplateField HeaderText="ID" SortExpression="ID">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkFormID" runat="server" Text='<%# Eval("ID") %>' OnClick="lnkFormID_Click" CausesValidation="False" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton><asp:Label ID="lblFormID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="LogDescription" HeaderText="Log" SortExpression="LogDescription">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Permissions" ShowHeader="False">
                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdPermissions" runat="server" AlternateText="Permissions" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/Permissions.png" OnClick="cmdPermissions_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Groups" ShowHeader="False">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdGroups" runat="server" AlternateText="Groups" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/GROUPS.BMP" OnClick="cmdGroups_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdEdit" runat="server" AlternateText="Edit" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/EDIT16.GIF" OnClick="cmdEdit_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Del" ShowHeader="False">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdDelete" runat="server" AlternateText="Delete" CausesValidation="False" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/DELETE16.GIF" OnClick="cmdDelete_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" runat="server" ImageUrl="~/Images/Form.png"/> No Data Found to Show.
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
            Wrap="False" />
        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Height="20px" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <table width="600">
        <tr>
            <td align="left" style="font-size: 9pt; font-family: Arial">
                <asp:Label ID="lblLogs" runat="server" Text="Filter by Log:"></asp:Label>&nbsp;<asp:DropDownList
                    ID="ddlLogs" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="9pt"
                    Width="208px">
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
                        <asp:MenuItem ImageUrl="~/Images/New.PNG" Text=" Add New Form" Value="AddNew"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" ForeColor="#990000" />
                </asp:Menu>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlAddForm" runat="server" Width="600px" Visible="False">
        <br />
        <table style="width: 600px" id="TABLE1">
            <tr>
                <td align="left" colspan="4" style="font-weight: bold; font-size: 11pt; border-bottom: silver 1px solid;
                    font-family: 'Comic Sans MS';">
                    Form Information</td>
            </tr>
            <tr>
                <td style="font-size: 9pt; width: 90px; font-family: Arial" colspan="1">
                    <asp:Label ID="lblFormID" runat="server" Text="ID:"></asp:Label>
                    <asp:TextBox ID="txtFormID" runat="server"
                        Width="50px" Enabled="False"></asp:TextBox></td>
                <td colspan="1" style="font-size: 9pt; width: 190px; font-family: Arial">
                    <asp:Label ID="lblFormLog" runat="server" Text="Log:"></asp:Label>
                    <asp:DropDownList ID="ddlFormLog" runat="server" Font-Names="Arial" Font-Size="9pt"
                        Width="150px">
                    </asp:DropDownList>
                    </td>
                <td style="font-size: 9pt; font-family: Arial; width: 320px;" colspan="2">
                    <asp:Label ID="lblFormDescription" runat="server" Text="Description:"></asp:Label>
                    <asp:TextBox ID="txtFormDescription" runat="server" MaxLength="100" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="rfvFormDescription" runat="server" ControlToValidate="txtFormDescription"
                        ErrorMessage="* Description is required" Font-Bold="True" Font-Names="Arial"
                        Font-Size="X-Large" ForeColor="OrangeRed" Display="Dynamic">*</asp:RequiredFieldValidator><asp:CustomValidator
                            ID="cvExistsFormDescription" runat="server" ErrorMessage="* Description already assigned to another Form" Font-Bold="True" Font-Names="Arial"
                            Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtFormDescription" Display="Dynamic">*</asp:CustomValidator></td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; font-family: Arial">
                </td>
                <td colspan="1" style="font-size: 9pt; font-family: Arial">
                </td>
                <td align="right" colspan="1" style="font-size: 9pt; font-family: Arial">
                    <asp:Menu ID="mnuItem" runat="server" DynamicHorizontalOffset="2" Font-Names="Arial"
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
                            <asp:MenuItem ImageUrl="~/Images/Permissions.png" Text=" Permissions" Value="Permissions"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="~/Images/GROUPS.BMP" Text=" Groups" Value="Groups"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="~/Images/DELETE16.GIF" Text=" Delete" Value="Delete"></asp:MenuItem>
                        </Items>
                        <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                            Font-Underline="False" ForeColor="#990000" />
                    </asp:Menu>
                    <br />
                </td>
                <td colspan="1" style="font-size: 9pt; font-family: Arial; width: 16px;">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-top: silver 1px solid; font-size: 9pt; font-family: Arial;">
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List"
                        Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="568px" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="4" style="font-size: 9pt; font-family: Arial; border-top: silver 1px solid;">
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
                    </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

