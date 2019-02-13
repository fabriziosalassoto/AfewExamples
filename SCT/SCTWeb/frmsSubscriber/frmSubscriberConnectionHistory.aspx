<%@ Page Language="VB" MasterPageFile="~/frmsSubscriber/MasterPage.master" AutoEventWireup="false" CodeFile="frmSubscriberConnectionHistory.aspx.vb" Inherits="frmsSubscriber_frmSubscriberConnectionHistory" title="IP Tracking System - Connection History" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <br />
    <asp:GridView ID="GridView" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
        Caption="Connection History" CaptionAlign="Left" CellPadding="2" Font-Bold="True"
        Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="14pt" ForeColor="Black"
        PageSize="15" ShowFooter="True" Width="800px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="3pt" Font-Underline="True" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="IPAddress" HeaderText="IP Address">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="Date" HeaderText="Date">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="Time" HeaderText="Time">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="DNS" HeaderText="DNS">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="State" HeaderText="State">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="City" HeaderText="City">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="Activity" HeaderText="Activity">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
        </Columns>
        <RowStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" runat="server" ImageUrl="~/Images/Report.gif" />No Data
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
</asp:Content>

