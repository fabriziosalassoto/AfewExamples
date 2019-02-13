<%@ page language="VB" masterpagefile="~/frmsSubscriber/MasterPage.master" autoeventwireup="false" inherits="frmsSubscriber_frmSubscriberLocation, App_Web_isnfrdft" title="IP Tracking System - Subscriber Computer Location" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <br />
    <cc1:msgbox id="MsgBox" runat="server"></cc1:msgbox>
    
    <br />
    <asp:GridView ID="GridView" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
        Caption="Computer Status." CaptionAlign="Left" CellPadding="2" Font-Bold="True"
        Font-Names="Comic Sans MS" Font-Overline="False" Font-Size="14pt" ForeColor="Black" PageSize="15"
        ShowFooter="True" Width="650px">
        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" LastPageImageUrl="~/Images/STEPFORW.GIF"
            Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF" PreviousPageImageUrl="~/Images/REWIND.GIF" />
        <FooterStyle BackColor="Black" Font-Size="3pt" Font-Underline="True" />
        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
        <Columns>
            <asp:BoundField DataField="SerialNbr" HeaderText="Serial Nbr">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="Status" HeaderText="Status">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="IPAddress" HeaderText="IP Address">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="Alert" HeaderText="Alert">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Wrap="True" />
            </asp:BoundField>
            <asp:ButtonField ButtonType="Image" CommandName="Location" HeaderText="Location"
                ImageUrl="~/Images/Location.png" Text="Location">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
            </asp:ButtonField>
            <asp:ButtonField ButtonType="Image" HeaderText="Stolen Rep" ImageUrl="~/Images/Report.gif"
                Text="Stolen Rep" CommandName="StolenReport">
                <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:ButtonField>
            <asp:ButtonField ButtonType="Image" CommandName="Uninstall" HeaderText="Uninstall"
                ImageUrl="~/Images/Uninstall.png" Text="Uninstall" >
                <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:ButtonField>
            <asp:ButtonField ButtonType="Image" CommandName="Install" HeaderText="Install" ImageUrl="~/Images/Install.png"
                Text="Install">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
            </asp:ButtonField>
        </Columns>
        <RowStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
            ForeColor="Black" />
        <EmptyDataTemplate>
            <asp:image ImageUrl="~/Images/Report.gif" ID="NoDataImage" runat="server"/>No Data Found to Show.
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

