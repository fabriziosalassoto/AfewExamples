<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAdvertiserTransactionInvoice.aspx.vb" Inherits="Forms_frmAdvertiserTransactionInvoice" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IP Tracking Admin System - Invoice</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <cc1:MsgBox ID="MsgBox" runat="server" />
        <br />
        <table id="Table" style="width: 600px">
            <tr>
                <td align="left" colspan="6" style="font-size: 14pt; border-bottom: silver 1px solid;
                    font-family: 'Comic Sans MS'">
                    Invoice &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 107px; font-family: Arial; text-align: right">
                    <br />
                    <asp:Label ID="lblInvoiceNumber" runat="server" Text="Number:"></asp:Label></td>
                <td style="font-size: 9pt; width: 213px; font-family: Arial; text-align: left">
                    <br />
                    <asp:TextBox ID="txtInvoiceNumber" runat="server" ReadOnly="True" Width="100px"></asp:TextBox></td>
                <td style="font-size: 9pt; width: 105px; font-family: Arial; text-align: right">
                    <br />
                    <asp:Label ID="lblInvoiceSequence" runat="server" Text="Sequence:"></asp:Label></td>
                <td style="font-size: 9pt; width: 159px; font-family: Arial; text-align: left">
                    <br />
                    <asp:TextBox ID="txtInvoiceSequence" runat="server" ReadOnly="True" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 107px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblAdvertiser" runat="server" Text="Advertiser:"></asp:Label></td>
                <td style="font-size: 9pt; width: 213px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtAdvertiser" runat="server" ReadOnly="True" Width="200px"></asp:TextBox></td>
                <td style="font-size: 9pt; width: 105px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblInvoiceDate" runat="server" Text="Date:"></asp:Label></td>
                <td colspan="" style="font-size: 9pt; width: 159px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtInvoiceDate" runat="server" ReadOnly="True" Width="150px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 107px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblProject" runat="server" Text="Project:"></asp:Label></td>
                <td colspan="3" style="font-size: 9pt; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtProject" runat="server" ReadOnly="True" Width="475px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 107px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblChargedToDate" runat="server" Text="Charged to Date"></asp:Label></td>
                <td style="font-size: 9pt; width: 213px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtChargedToDate" runat="server" ReadOnly="True" Width="150px"></asp:TextBox></td>
                <td style="font-size: 9pt; width: 105px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblPaidToDate" runat="server" Text="Paid to Date:"></asp:Label></td>
                <td colspan="" style="font-size: 9pt; width: 159px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtPaidToDate" runat="server" ReadOnly="True" Width="150px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 107px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblPreviousDisplays" runat="server" Text="Previous Displays:"></asp:Label></td>
                <td style="font-size: 9pt; width: 213px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtPreviousDisplays" runat="server" ReadOnly="True" Width="150px"></asp:TextBox></td>
                <td style="font-size: 9pt; width: 105px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblPreviousClicks" runat="server" Text="Previous Clicks:"></asp:Label></td>
                <td colspan="" style="font-size: 9pt; width: 159px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtPreviousClicks" runat="server" ReadOnly="True" Width="150px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 107px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblPreviousBalance" runat="server" Text="Previous Balance:"></asp:Label></td>
                <td style="font-size: 9pt; width: 213px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtPreviousBalance" runat="server" ReadOnly="True" Width="150px"></asp:TextBox></td>
                <td style="font-size: 9pt; width: 105px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblTotalAmountDue" runat="server" Text="Total Amount Due:"></asp:Label></td>
                <td colspan="" style="font-size: 9pt; width: 159px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtTotalAmountDue" runat="server" ReadOnly="True" Width="150px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" style="font-size: 9pt; font-family: Arial; text-align: center">
                    <br />
                    <asp:GridView ID="grdDetails" runat="server"
                        AutoGenerateColumns="False" CaptionAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Width="592px" ShowFooter="True" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px" Caption="Details:" Font-Names="Arial" Font-Size="11pt">
                        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" FirstPageText="First" LastPageImageUrl="~/Images/STEPFORW.GIF"
                            LastPageText="Last" Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF"
                            NextPageText="Next" PreviousPageImageUrl="~/Images/REWIND.GIF" PreviousPageText="Previous" />
                        <FooterStyle Font-Size="9pt" Font-Underline="False" BackColor="Black" Font-Bold="True" Font-Names="Arial" ForeColor="White" />
                        <RowStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Size="9pt" ForeColor="Black" />
                        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:BoundField DataField="Displays" HeaderText="Displays">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Clicks" HeaderText="Clicks">
                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DisplayCost" HeaderText="Display Cost" DataFormatString="{0:#,###,##0.00}">
                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="125px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ClickCost" HeaderText="Click Cost" DataFormatString="{0:#,###,##0.00}" FooterText="Total Amount Due:">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="125px" />
                                <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AmountDue" DataFormatString="{0:#,###,##0.00}" HeaderText="Amount Due">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="125px" />
                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
                            Wrap="False" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Overline="False" Font-Size="9pt" Font-Names="Arial" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
