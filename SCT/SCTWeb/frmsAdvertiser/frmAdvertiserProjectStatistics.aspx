<%@ Page Language="VB" MasterPageFile="~/frmsAdvertiser/MasterPage.master" AutoEventWireup="false" CodeFile="frmAdvertiserProjectStatistics.aspx.vb" Inherits="frmsAdvertiser_frmAdvertiserProjectStatistics" title="Project Statistics" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <br />
    <asp:GridView ID="GridView" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
        Caption="Projects Statistics" CaptionAlign="Left" CellPadding="2" Font-Bold="True"
        Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="14pt" ForeColor="Black" PageSize="15"
        ShowFooter="True" Width="800px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="5pt" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:ButtonField CommandName="Project" DataTextField="ID" HeaderText="ID" Text="View Project">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25px" />
            </asp:ButtonField>
            <asp:BoundField DataField="URL" HeaderText="URL">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="Description" HeaderText="Description">
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px" />
            </asp:BoundField>
            <asp:BoundField DataField="Displays" HeaderText="Displays">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="ClickThru" HeaderText="Click Thru">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:ButtonField ButtonType="Image" CommandName="Project" HeaderText="View" ImageUrl="~/Images/PROJECT16.BMP"
                Text="View Project">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:ButtonField>
        </Columns>
        <RowStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" />
        <EmptyDataTemplate>
            <asp:Image ID="NoDataImage" ImageUrl="~/Images/PROJECT16.BMP" runat="server" />No Data Found to Show.
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

