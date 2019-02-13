<%@ Page Language="VB" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="false" CodeFile="frmFields.aspx.vb" Inherits="Forms_frmFields" title="IP Tracking Admin System - Fields" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <br />
    <asp:GridView ID="GridView" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" Caption="Fields" CaptionAlign="Left" CellPadding="0" Font-Bold="True"
        Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="14pt" ForeColor="Black" ShowFooter="True" UseAccessibleHeader="False" Width="700px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:TemplateField HeaderText="ID" ShowHeader="False" SortExpression="ID">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:LinkButton ID="lnkFieldID" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>'
                        OnClick="lnkFieldID_Click" Text='<%# Eval("ID") %>'></asp:LinkButton><asp:Label ID="lblFieldID"
                            runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Form" ShowHeader="False" SortExpression="Form">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="175px" />
                <ItemTemplate>
                    <asp:LinkButton ID="lnkFieldForm" runat="server" CausesValidation="False" CommandArgument='<%# Eval("IDForm") %>'
                        OnClick="lnkFieldForm_Click" Text='<%# Eval("Form") %>'></asp:LinkButton><asp:Label
                            ID="lblFieldForm" runat="server" Text='<%# Eval("Form") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Group" ShowHeader="False" SortExpression="Group">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="175px" />
                <ItemTemplate>
                    <asp:LinkButton ID="lnkFieldGroup" runat="server" CausesValidation="False" CommandArgument='<%# Eval("IDGroup") %>'
                        CommandName='<%# Eval("IDForm") %>' OnClick="lnkFieldGroup_Click" Text='<%# Eval("Group") %>'></asp:LinkButton><asp:Label
                            ID="lblFieldGroup" runat="server" Text='<%# Eval("Group") %>'></asp:Label>
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
            <asp:Image ID="NoDataImage" runat="server" ImageUrl="~/Images/FIELDS.BMP"/> No Data
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
    <table width="700">
        <tr>
            <td align="left" style="font-size: 9pt; font-family: Arial;">
                <asp:Label ID="lblForms" runat="server" Text="Filter by Form:"></asp:Label>
                <asp:DropDownList ID="ddlForms" runat="server" AutoPostBack="True"
                    Font-Names="Arial" Font-Size="9pt" Width="150px">
                </asp:DropDownList>
                &nbsp;&nbsp;<asp:Label ID="lblGroups" runat="server" Text="Group:"></asp:Label>
                <asp:DropDownList ID="ddlGroups" runat="server" AutoPostBack="True"
                    Font-Names="Arial" Font-Size="9pt" Width="150px">
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
                        <asp:MenuItem ImageUrl="~/Images/New.PNG" Text=" Add New Field" Value="AddNew"></asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/Return.png" Text=" Return" Value="Return"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" ForeColor="#990000" />
                </asp:Menu>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlAddForm" runat="server" Width="700px" Visible="False">
        <br />
        <table style="width: 700px" id="TABLE1">
            <tr>
                <td align="left" colspan="4" style="font-weight: bold; font-size: 11pt; border-bottom: silver 1px solid;
                    font-family: 'Comic Sans MS'">
                    Field Information</td>
            </tr>
            <tr>
                <td style="font-size: 9pt; width: 75px; font-family: Arial; border-bottom: silver 1px solid;" colspan="" align="center">
                    <asp:Label ID="lblFieldID" runat="server" Text="ID"></asp:Label></td>
                <td colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial; border-bottom: silver 1px solid;" align="center">
                    <asp:Label ID="lblFieldForm" runat="server" Text="Form"></asp:Label></td>
                <td align="center" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial; border-bottom: silver 1px solid;">
                    <asp:Label ID="lblFieldGroup" runat="server" Text="Group"></asp:Label></td>
                <td style="font-size: 9pt; width: 225px; font-family: Arial; border-bottom: silver 1px solid;" align="center">
                    <asp:Label ID="lblFieldDescription" runat="server" Text="Description"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" colspan="1" style="font-size: 9pt; width: 75px; font-family: Arial">
                    <asp:TextBox ID="txtFieldID" runat="server"
                        Width="50px" Enabled="False"></asp:TextBox></td>
                <td align="center" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial">
                    <asp:DropDownList ID="ddlFieldForm" runat="server" AutoPostBack="True" Width="175px">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cvContactName" runat="server" ControlToValidate="ddlFieldForm"
                        ErrorMessage="*  The Form is required." Font-Bold="True" Font-Names="Arial" Font-Size="X-Large"
                        ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True" ValueToCompare="0">*</asp:CompareValidator></td>
                <td align="center" colspan="1" style="font-size: 9pt; width: 200px; font-family: Arial">
                    <asp:DropDownList ID="ddlFieldGroup" runat="server" Font-Names="Arial"
                        Font-Size="9pt" Width="175px">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlFieldGroup"
                        ErrorMessage="*  The Group is required." Font-Bold="True" Font-Names="Arial"
                        Font-Size="X-Large" ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True"
                        ValueToCompare="0">*</asp:CompareValidator></td>
                <td align="center" style="font-size: 9pt; width: 225px; font-family: Arial">
                    <asp:TextBox ID="txtFieldDescription" runat="server" MaxLength="100" Width="175px"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="rfvDescription" runat="server" ControlToValidate="txtFieldDescription" ErrorMessage="* Description is required"
                        Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" Display="Dynamic">*</asp:RequiredFieldValidator><asp:CustomValidator
                            ID="cvExistsDescriptionInGroup" runat="server" ControlToValidate="txtFieldDescription"
                            ErrorMessage="* Description already assigned to another Field in Group."
                            Font-Bold="True" Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" Display="Dynamic">*</asp:CustomValidator><asp:CustomValidator ID="cvExistsDescriptionInForm" runat="server" ControlToValidate="txtFieldDescription"
                        ErrorMessage="* Description already assigned to another Field in another Group in Form." Font-Bold="True"
                        Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed" Display="Dynamic">*</asp:CustomValidator></td>
            </tr>
            <tr>
                <td colspan="4" style="font-size: 9pt; font-family: Arial" align="right">
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
                            <asp:MenuItem ImageUrl="~/Images/Permissions.png" Text=" Group" Value="Group"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="~/Images/DELETE16.GIF" Text=" Delete" Value="Delete"></asp:MenuItem>
                        </Items>
                        <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                            Font-Underline="False" ForeColor="#990000" />
                    </asp:Menu>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="font-size: 9pt; font-family: Arial; border-top: silver 1px solid;">
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" Font-Bold="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="688px" />
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
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

