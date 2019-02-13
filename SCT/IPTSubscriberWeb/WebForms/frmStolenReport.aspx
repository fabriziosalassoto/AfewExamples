<%@ Page Language="VB" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="false" CodeFile="frmStolenReport.aspx.vb" Inherits="WebForms_frmStolenReport" title="Stolen Report" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <table>
        <tr>
            <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                ID: &nbsp;
            </td>
            <td align="left" style="width: 250px">
                &nbsp;<asp:TextBox ID="txtID" runat="server" BackColor="#FFFFC0" Font-Names="Arial"
                    Font-Size="9pt" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                Serial Number: &nbsp;
            </td>
            <td align="left" style="width: 250px">
                &nbsp;<asp:TextBox ID="txtSerialNbr" runat="server" BackColor="#FFFFC0" Font-Names="Arial"
                    Font-Size="9pt" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                Date Missing: &nbsp;
            </td>
            <td align="left" style="width: 250px">
                &nbsp;<asp:TextBox ID="txtDateMissing" runat="server" Font-Names="Arial" Font-Size="9pt" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                <asp:Label ID="lblDateFound" runat="server" Text="Date Found:"></asp:Label>
                &nbsp;</td>
            <td align="left" style="width: 250px">
                &nbsp;<asp:TextBox ID="txtDateFound" runat="server" Font-Names="Arial" Font-Size="9pt" BackColor="#FFFFC0" ReadOnly="True"></asp:TextBox>&nbsp;
                <asp:Button ID="cmdFound" runat="server" BackColor="DimGray" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" ForeColor="White"
                    Text="Found" Visible="False" CausesValidation="False" /></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                Last Known Location: &nbsp;&nbsp;
            </td>
            <td align="left" style="width: 250px">
                &nbsp;<asp:TextBox ID="txtLasKnownLocation" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 250px; font-family: Arial">
                Action: &nbsp;</td>
            <td align="left" style="width: 250px">
                &nbsp;<asp:DropDownList ID="ddlAction" runat="server" Font-Names="Arial" Font-Size="9pt">
                    <asp:ListItem Selected="True" Value="1">Shut Down</asp:ListItem>
                    <asp:ListItem Value="3">Delete File</asp:ListItem>
                    <asp:ListItem Value="4">Get File</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
    </table>
    <br />
    &nbsp;<asp:Button ID="cmdOk" runat="server" BackColor="DimGray" BorderColor="Black"
        BorderStyle="Solid" BorderWidth="1px" Font-Bold="False" Font-Names="Arial" Font-Size="9pt"
        ForeColor="White" Text="Ok" Width="100px" />
    &nbsp;
    <asp:Button ID="Button2" runat="server" BackColor="DimGray" BorderColor="Black" BorderStyle="Solid"
        BorderWidth="1px" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" ForeColor="White"
        Text="Cancel" Width="100px" CausesValidation="False" />
</asp:Content>

