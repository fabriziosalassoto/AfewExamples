<%@ page language="VB" autoeventwireup="false" inherits="Forms_frmAdvertiserTransactionReceipt, App_Web_pvziqsfz" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IP Tracking Admin System - Receipt</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <cc1:MsgBox ID="MsgBox" runat="server" />
        <br />
        <table id="Table" style="width: 750px;">
            <tr>
                <td align="left" colspan="4" style="font-size: 14pt; border-bottom: silver 1px solid;
                    font-family: 'Comic Sans MS'">
                    Receipt&nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 109px; font-family: Arial; text-align: right">
                    <br />
                    <asp:Label ID="lblReceiptNumber" runat="server" Text="Number:"></asp:Label></td>
                <td style="font-size: 9pt; font-family: Arial; text-align: left; width: 244px;">
                    <br />
                    <asp:TextBox ID="txtReceiptNumber" runat="server" MaxLength="100" ReadOnly="True"
                        Width="100px"></asp:TextBox></td>
                <td style="font-size: 9pt; width: 160px; font-family: Arial; text-align: right">
                    <br />
                    <asp:Label ID="lblPaymentDate" runat="server" Text="Date:"></asp:Label></td>
                <td style="font-size: 9pt; width: 261px; font-family: Arial; text-align: left">
                    <br />
                    <asp:TextBox ID="txtPaymentDate" runat="server" MaxLength="100" ReadOnly="True" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 109px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblPaymentType" runat="server" Text="Payment Type:"></asp:Label></td>
                <td style="font-size: 9pt; width: 244px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtPaymentType" runat="server" MaxLength="100" ReadOnly="True" Width="200px"></asp:TextBox></td>
                <td style="font-size: 9pt; width: 160px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblPaymentNumber" runat="server" Text="Payment Nbr:"></asp:Label></td>
                <td style="font-size: 9pt; width: 261px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtPaymentNunber" runat="server" MaxLength="100" ReadOnly="True"
                        Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 109px; font-family: Arial; text-align: right">
                    </td>
                <td style="font-size: 9pt; font-family: Arial; text-align: left; width: 244px;">
                    </td>
                <td style="font-size: 9pt; width: 160px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblPaymentAmount" runat="server" Text="Payment Amount:"></asp:Label></td>
                <td style="font-size: 9pt; vertical-align: top; width: 261px; font-family: Arial;
                    text-align: left">
                    <asp:TextBox ID="txtPaymentAmount" runat="server" MaxLength="100" ReadOnly="True" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" style="font-size: 9pt; font-family: Arial">
                    <br />
                    <asp:GridView ID="grdDetails" runat="server"
                        AutoGenerateColumns="False" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px"
                        Caption="Details:" CaptionAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                        Font-Size="11pt" Font-Strikeout="False" ShowFooter="True" Width="750px">
                        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" FirstPageText="First" LastPageImageUrl="~/Images/STEPFORW.GIF"
                            LastPageText="Last" Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF"
                            NextPageText="Next" PreviousPageImageUrl="~/Images/REWIND.GIF" PreviousPageText="Previous" />
                        <FooterStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                            Font-Underline="False" ForeColor="White" />
                        <RowStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Size="9pt" ForeColor="Black" />
                        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:BoundField DataField="Advertiser" HeaderText="Advertiser">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="125px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Project" HeaderText="Project" FooterText="Totals:">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <FooterStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PaidByDisplay" DataFormatString="{0:#,###,##0.00}" HeaderText="Paid by Displays">
                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PaidByClick" DataFormatString="{0:#,###,##0.00}" HeaderText="Paid by Clicks">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="100px" />
                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotalPaid" DataFormatString="{0:#,###,##0.00}" HeaderText="Total Paid">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="75px" />
                                <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
                            Wrap="False" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Overline="False"
                            Font-Size="9pt" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
