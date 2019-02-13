<%@ Page Language="VB" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="false" CodeFile="frmProfileForms.aspx.vb" Inherits="Forms_frmProfileForms" title="IP Tracking Admin System - Forms Permissions" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <br />
    <asp:GridView ID="GridView" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" Caption="Profile Forms Permissions" CaptionAlign="Left" CellPadding="0" Font-Bold="True"
        Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="14pt" ForeColor="Black" ShowFooter="True" UseAccessibleHeader="False" Width="750px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:TemplateField HeaderText="Profile" SortExpression="Profile">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="140px" />
                <ItemTemplate>
                    <asp:LinkButton ID="lnkProfile" runat="server" CausesValidation="False" CommandArgument='<%# Eval("IDProfile") %>'
                        Text='<%# Eval("Profile") %>' OnClick="lnkProfile_Click"></asp:LinkButton><asp:Label ID="lblProfile" runat="server"
                            Text='<%# Eval("Profile") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Form" SortExpression="Form">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
                <ItemTemplate>
                    <asp:LinkButton ID="lnkForm" runat="server" CausesValidation="False" CommandName="Form" Text='<%# Eval("Form") %>' CommandArgument='<%# Eval("IDForm") %>' OnClick="lnkForm_Click"></asp:LinkButton><asp:Label ID="lblForm" runat="server" Text='<%# Eval("Form") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ImageField HeaderText="Select" DataImageUrlField="Select">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
            </asp:ImageField>
            <asp:ImageField HeaderText="Insert" DataImageUrlField="Insert">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
            </asp:ImageField>
            <asp:ImageField HeaderText="Update" DataImageUrlField="Update">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
            </asp:ImageField>
            <asp:ImageField HeaderText="Delete" DataImageUrlField="Delete">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
            </asp:ImageField>
            <asp:TemplateField HeaderText="Groups">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdGroups" runat="server" AlternateText="Groups" CausesValidation="False"
                        CommandArgument='<%# Eval("IDProfile") %>' ImageUrl="~/Images/GROUPS.BMP" OnClick="cmdGroups_Click" CommandName='<%# Eval("IDForm") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdEdit" runat="server" AlternateText="Edit" CausesValidation="False"
                        CommandArgument='<%# Eval("IDProfile") %>' ImageUrl="~/Images/EDIT16.GIF" OnClick="cmdEdit_Click" CommandName='<%# Eval("IDForm") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Del">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <ItemTemplate>
                    <asp:ImageButton ID="cmdDelete" runat="server" AlternateText="Delete" CausesValidation="False"
                        CommandArgument='<%# Eval("IDProfile") %>' ImageUrl="~/Images/DELETE16.GIF" OnClick="cmdDelete_Click" CommandName='<%# Eval("IDForm") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" runat="server" ImageUrl="~/Images/Permissions.png"/> No Data
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
    <table width="750">
        <tr>
            <td align="left" style="font-size: 9pt; font-family: Arial;">
                <asp:Label ID="lblProfiles" runat="server" Text="Filter by Profile:"></asp:Label>&nbsp;<asp:DropDownList
                    ID="ddlProfiles" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="9pt"
                    Width="200px">
                </asp:DropDownList>&nbsp;<br />
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
                        <asp:MenuItem ImageUrl="~/Images/New.PNG" Text=" Add New" Value="AddNew"></asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/Return.png" Text=" Return" Value="Return"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" ForeColor="#990000" />
                </asp:Menu>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlAddForm" runat="server" Height="50px" Width="750px" Visible="False">
        <br />
        <table style="width: 750px" id="TABLE1">
            <tr>
                <td align="left" colspan="7" style="font-weight: bold; font-size: 11pt; border-bottom: silver 1px solid;
                    font-family: 'Comic Sans MS'; height: 24px;">
                    Profile
                    Form Permissions</td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; font-family: Arial; width: 205px;">
                    <asp:Label ID="lblProfile" runat="server" Text="Profile:"></asp:Label>
                    <asp:DropDownList ID="ddlProfile" runat="server" Width="150px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cvProfile" runat="server" ControlToValidate="ddlProfile"
                        Display="Dynamic" ErrorMessage="*  The Profile is required." Font-Bold="True" Font-Names="Arial"
                        Font-Size="X-Large" ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True"
                        ValueToCompare="0">*</asp:CompareValidator></td>
                <td colspan="1" style="font-size: 9pt; font-family: Arial; width: 260px;">
                    <asp:Label ID="lblForm" runat="server" Text="Form:"></asp:Label>&nbsp;<asp:DropDownList ID="ddlForm" runat="server" Width="200px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cvForm" runat="server" ControlToValidate="ddlForm"
                        ErrorMessage="*  The Form is required." Font-Bold="True" Font-Names="Arial" Font-Size="X-Large"
                        ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True" ValueToCompare="0" Display="Dynamic">*</asp:CompareValidator><asp:CustomValidator ID="cvValidateAssign" runat="server" ControlToValidate="ddlForm"
                        Display="Dynamic" ErrorMessage="* Form already assigned to Profile" Font-Bold="True"
                        Font-Names="Arial" Font-Size="X-Large" ForeColor="OrangeRed">*</asp:CustomValidator></td>
                <td style="font-size: 9pt; font-family: Arial; border-bottom: silver 1px solid; width: 65px;">
                    <asp:CheckBox ID="ckbSelect" runat="server" Text="Select" AutoPostBack="True" /></td>
                <td colspan="1" style="font-size: 9pt; font-family: Arial; border-bottom: silver 1px solid; width: 65px;">
                    <asp:CheckBox ID="ckbInsert" runat="server" Text="Insert" /></td>
                <td colspan="1" style="font-size: 9pt; font-family: Arial; border-bottom: silver 1px solid; width: 65px;">
                    <asp:CheckBox ID="ckbUpdate" runat="server" Text="Update" /></td>
                <td colspan="1" style="font-size: 9pt; font-family: Arial; border-bottom: silver 1px solid; width: 65px;">
                    <asp:CheckBox ID="ckbDelete" runat="server" Text="Delete" /></td>
            </tr>
            <tr>
                <td align="right" colspan="6" style="font-size: 9pt; font-family: Arial">
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
                            <asp:MenuItem ImageUrl="~/Images/Profile.png" Text=" Profile" Value="Profile"></asp:MenuItem>
                            <asp:MenuItem ImageUrl="~/Images/GROUPS.BMP" Text=" Groups" Value="Groups">
                            </asp:MenuItem>
                            <asp:MenuItem ImageUrl="~/Images/DELETE16.GIF" Text=" Delete" Value="Delete"></asp:MenuItem>
                        </Items>
                        <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                            Font-Underline="False" ForeColor="#990000" />
                    </asp:Menu>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="6" style="border-top: silver 1px solid; font-size: 9pt; font-family: Arial">
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" Font-Bold="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="728px" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="6" style="font-size: 9pt; font-family: Arial; border-top: silver 1px solid;" contenteditable="true">
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

