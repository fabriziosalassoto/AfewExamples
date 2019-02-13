<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="frmStatistics.aspx.vb" Inherits="WebForms_frmStatistics" title="Statistics" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <cc1:msgbox id="MsgBox" runat="server"></cc1:msgbox>
    <br />
    <asp:GridView ID="GridView" runat="server" AllowPaging="True"
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
        BorderWidth="1px" CaptionAlign="Top" CellPadding="2" Font-Bold="True" Font-Names="Arial"
        Font-Size="14pt" ForeColor="Black" PageSize="15" ShowFooter="True" Width="600px" Caption="Advertising Statistics" Font-Overline="False">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:BoundField DataField="ProjectID" HeaderText="Project ID">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <HeaderStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="URL" HeaderText="URL">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="450px" />
                <HeaderStyle Width="305px" />
            </asp:BoundField>
            <asp:BoundField DataField="Displays" HeaderText="Displays">
                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="ClickThru" HeaderText="ClickThru">
                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
        </Columns>
        <RowStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" runat="server"/>No Data Found to Show.
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
            Wrap="False" />
        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="#CCCCCC" />
    </asp:GridView>
</asp:Content>

