<%@ page language="VB" autoeventwireup="false" inherits="Forms_frmBinnacleFormEntry, App_Web_pvziqsfz" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IP Tracking Admin System - Log Entry Detail</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <cc1:MsgBox ID="MsgBox" runat="server" />
        <br />
        <table id="TABLE1" style="padding-right: 0px; padding-left: 0px; padding-bottom: 0px;
            margin: 0px; width: 450px; padding-top: 0px">
            <tr>
                <td align="left" colspan="2" style="font-size: 14pt; border-bottom: silver 1px solid;
                    font-family: 'Comic Sans MS'">
                    Log Entry Detail</td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 175px; font-family: Arial; text-align: right">
                    <br />
                    <asp:Label ID="lblBinnacleFormEntryID" runat="server" Text="ID:"></asp:Label></td>
                <td style="font-size: 9pt; width: 275px; font-family: Arial; text-align: left">
                    <br />
                    <asp:TextBox ID="txtBinnacleFormEntryID" runat="server" MaxLength="100" ReadOnly="True"
                        Width="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 175px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblBinnacleFormEntryLog" runat="server" Text="Log:"></asp:Label></td>
                <td style="font-size: 9pt; width: 275px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtBinnacleFormEntryLog" runat="server" MaxLength="100" ReadOnly="True"
                        Width="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 175px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblBinnacleFormEntryDate" runat="server" Text="Date:"></asp:Label></td>
                <td style="font-size: 9pt; width: 275px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtBinnacleFormEntryDate" runat="server" MaxLength="100" ReadOnly="True"
                        Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 175px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblBinnacleFormEntryHour" runat="server" Text="Hour:"></asp:Label></td>
                <td style="font-size: 9pt; width: 275px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtBinnacleFormEntryHour" runat="server" MaxLength="100" ReadOnly="True"
                        Width="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 175px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblBinnacleFormEntryUser" runat="server" Text="User:"></asp:Label></td>
                <td style="font-size: 9pt; width: 275px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtBinnacleFormEntryUser" runat="server" MaxLength="100" ReadOnly="True"
                        Width="250px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 175px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblBinnacleFormEntryForm" runat="server" Text="Form:"></asp:Label></td>
                <td style="font-size: 9pt; width: 275px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtBinnacleFormEntryForm" runat="server" MaxLength="50" ReadOnly="True"
                        Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="1" style="font-size: 9pt; width: 175px; font-family: Arial; text-align: right">
                    <asp:Label ID="lblBinnacleFormEntryOperation" runat="server" Text="Operation:"></asp:Label><br />
                    <br />
                </td>
                <td style="font-size: 9pt; width: 275px; font-family: Arial; text-align: left">
                    <asp:TextBox ID="txtBinnacleFormEntryOperation" runat="server" MaxLength="12" ReadOnly="True"
                        Width="250px"></asp:TextBox>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border-top: silver 1px solid; font-size: 9pt; font-family: Arial">
                    <br />
                    &nbsp;<asp:GridView ID="grdBinnacleFormEntryFields" runat="server" AutoGenerateColumns="False" BorderColor="DarkGray" BorderStyle="Solid"
                        BorderWidth="1px" Caption="Fields:" CaptionAlign="Left" Font-Bold="True" Font-Italic="False"
                        Font-Overline="False" Font-Size="11pt" Font-Strikeout="False"
                        UseAccessibleHeader="False" Width="450px">
                        <PagerSettings FirstPageImageUrl="~/Images/STEPBACK.GIF" FirstPageText="First" LastPageImageUrl="~/Images/STEPFORW.GIF"
                            LastPageText="Last" Mode="NextPreviousFirstLast" NextPageImageUrl="~/Images/FASTFORW.GIF"
                            NextPageText="Next" PreviousPageImageUrl="~/Images/REWIND.GIF" PreviousPageText="Previous"
                            Visible="False" />
                        <FooterStyle Font-Size="4pt" Font-Underline="True" VerticalAlign="Middle" />
                        <EmptyDataRowStyle BackColor="Black" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:BoundField DataField="Description" HeaderText="Description">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OldValue" HeaderText="OldValue">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NewValue" HeaderText="New Value">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Size="9pt" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <PagerStyle BackColor="#CCCCCC" BorderStyle="None" Font-Bold="False" HorizontalAlign="Center"
                            Wrap="False" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" Font-Overline="False" Font-Size="9pt" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
