<%@ Page Language="VB" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="false" CodeFile="frmGroups.aspx.vb" Inherits="Forms_frmGroups" title="IP Tracking Admin System - Groups" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <br />
    <asp:GridView ID="GridView" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" Caption="Groups" CaptionAlign="Left" CellPadding="0" Font-Bold="True"
        Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="14pt" ForeColor="Black" ShowFooter="True" UseAccessibleHeader="False" Width="600px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:TemplateField HeaderText="ID" ShowHeader="False" SortExpression="ID">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:LinkButton ID="lnkGroupID" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>'
                        Text='<%# Eval("ID") %>' OnClick="lnkGroupID_Click"></asp:LinkButton><asp:Label ID="lblGroupID" runat="server"
                            Text='<%# Eval("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Form" ShowHeader="False" SortExpression="Form">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                <ItemTemplate>
                    <asp:LinkButton ID="lnkGroupForm" runat="server" CausesValidation="False" CommandArgument='<%# Eval("IDForm") %>'
                        Text='<%# Eval("Form") %>' OnClick="lnkGroupForm_Click"></asp:LinkButton><asp:Label ID="lblGroupForm" runat="server"
                            Text='<%# Eval("Form") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Permissions" ShowHeader="False">
                <ItemStyle Width="100px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdPermissions" runat="server" AlternateText="Permissions" CausesValidation="False"
                        ImageUrl="~/Images/Permissions.png" CommandArgument='<%# Eval("ID") %>' CommandName='<%# Eval("IDForm") %>' OnClick="cmdPermissions_Click"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fields" ShowHeader="False">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdFields" runat="server" AlternateText="Fields" CausesValidation="False"
                        CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/FIELDS.BMP" CommandName='<%# Eval("IDForm") %>' OnClick="cmdFields_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdEdit" runat="server" AlternateText="Edit" CausesValidation="False"
                        CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/EDIT16.GIF" OnClick="cmdEdit_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Del" ShowHeader="False">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdDelete" runat="server" AlternateText="Delete" CausesValidation="False"
                        CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Images/DELETE16.GIF" OnClick="cmdDelete_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" runat="server" ImageUrl="~/Images/GROUPS.BMP"/> No Data
            Found to Show.
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
                <asp:Label ID="lblForms" runat="server" Text="Filter by Form:"></asp:Label>&nbsp;<asp:DropDownList ID="ddlForms" runat="server" AutoPostBack="True"
                    Font-Names="Arial" Font-Size="9pt" Width="208px">
                </asp:DropDownList><br />
            </td>
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
                        <asp:MenuItem ImageUrl="~/Images/New.PNG" Text=" Add New Group" Value="AddNew"></asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/Return.png" Text=" Return" Value="Return"></asp:MenuItem>
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
                    font-family: 'Comic Sans MS'">
                    Group Information</td>
            </tr>
            <tr>
                <td style="font-size: 9pt; width: 80px; font-family: Arial" colspan="1">
                    <asp:Label ID="lblGroupID" runat="server" Text="ID:"></asp:Label>
                    <asp:TextBox ID="txtGroupID" runat="server"
                        Width="50px" Enabled="False"></asp:TextBox></td>
                <td colspan="1" style="font-size: 9pt; width: 205px; font-family: Arial">
                    <asp:Label ID="lblGroupForm" runat="server" Text="Form:"></asp:Label>&nbsp;<asp:DropDownList ID="ddlGroupForm" runat="server" Width="150px">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cvContactName" runat="server" ControlToValidate="ddlGroupForm"
                        ErrorMessage="*  The Form is required." Font-Bold="True" Font-Names="Arial"
                        Font-Size="X-Large" ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True"
                        ValueToCompare="0">*</asp:CompareValidator></td>
                <td style="font-size: 9pt; width: 315px; font-family: Arial" colspan="2">
                    <asp:Label ID="lblGroupDescription" runat="server" Text="Description:"></asp:Label>
                    <asp:TextBox ID="txtGroupDescription" runat="server" MaxLength="100" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" Font-Bold="True"
                        Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ControlToValidate="txtGroupDescription" ErrorMessage="* Description is required">*</asp:RequiredFieldValidator><asp:CustomValidator
                            ID="cvExistsDescription" runat="server" ControlToValidate="txtGroupDescription"
                            Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" ErrorMessage="* Description already assigned to another Group in Form.">*</asp:CustomValidator></td>
            </tr>
            <tr>
                <td colspan="2" style="font-size: 9pt; font-family: Arial">
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
                            <asp:MenuItem ImageUrl="~/Images/Form.png" Text=" Form" Value="Form"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="~/Images/Permissions.png" Text=" Permissions" Value="Permissions">
                            </asp:MenuItem>
                            <asp:MenuItem ImageUrl="~/Images/FIELDS.BMP" Text=" Fields" Value="Fields"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="~/Images/DELETE16.GIF" Text=" Delete" Value="Delete"></asp:MenuItem>
                        </Items>
                        <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                            Font-Underline="False" ForeColor="#990000" />
                    </asp:Menu>
                    <br />
                </td>
                <td colspan="1" style="font-size: 9pt; font-family: Arial; width: 18px;">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-top: silver 1px solid; font-size: 9pt; font-family: Arial">
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" Font-Bold="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="576px" />
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

