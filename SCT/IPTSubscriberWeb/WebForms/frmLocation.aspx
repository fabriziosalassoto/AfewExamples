<%@ Page Language="VB" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="false" CodeFile="frmLocation.aspx.vb" Inherits="WebForms_frmLocation" title="Location" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <asp:GridView ID="GridView" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
        Caption="Computer Location" CaptionAlign="Top" CellPadding="2" Font-Bold="True"
        Font-Names="Arial" Font-Overline="False" Font-Size="14pt" ForeColor="Black" PageSize="15"
        ShowFooter="True" Width="550px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                <HeaderStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="SerialNbr" HeaderText="Serial Nbr">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                <HeaderStyle Width="305px" />
            </asp:BoundField>
            <asp:BoundField DataField="Status" HeaderText="Status">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="IPAddress" HeaderText="IP Address">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="StolenReport" HeaderText="Stolen Report">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" Wrap="True" />
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

