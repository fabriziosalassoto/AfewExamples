<%@ page language="VB" masterpagefile="~/frmsAdvertiser/MasterPage.master" autoeventwireup="false" inherits="frmsAdvertiser_frmAdvertiserProjectHistory, App_Web_d4xdeinq" title="Advertiser Project History" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    &nbsp;&nbsp;<cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <br />
    <asp:GridView ID="GridView" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" Caption="Project History" CaptionAlign="Left" CellPadding="2"
        Font-Bold="True" Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="14pt"
        ForeColor="Black" PageSize="15" ShowFooter="True" UseAccessibleHeader="False"
        Width="900px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:ButtonField CommandName="Project" DataTextField="ProjectId" HeaderText="Proj. ID"
                SortExpression="Project" Text="Project ID">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                <HeaderStyle Width="100px" />
            </asp:ButtonField>
            <asp:BoundField DataField="URLDisplay" HeaderText="URL Display">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="DateDisplay" HeaderText="Date">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="125px" />
            </asp:BoundField>
            <asp:BoundField DataField="TimeDisplay" HeaderText="Time">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="URLClickThru" HeaderText="URL Click Thru">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="DateClickThru" HeaderText="Date">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="125px" />
            </asp:BoundField>
            <asp:BoundField DataField="TimeClickThru" HeaderText="Time">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="75px" />
            </asp:BoundField>
        </Columns>
        <RowStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" runat="server" ImageUrl="~/Images/TODO16.BMP" />No Data
            Found to Show.
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
            Wrap="False" />
        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
    <table width="900">
        <tr>
            <td align="left" style="font-size: 9pt; font-family: Arial" valign="top">
                Filter by Project ID:
                <asp:DropDownList ID="ddlProjects" runat="server" AutoPostBack="True" Font-Names="Arial"
                    Font-Size="9pt" Width="206px">
                </asp:DropDownList></td>
        </tr>
    </table>
</asp:Content>

