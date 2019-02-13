<%@ page language="VB" masterpagefile="~/frmsSubscriber/MasterPage.master" autoeventwireup="false" inherits="frmsSubscriber_frmSubscriberStolenReport, App_Web_isnfrdft" title="IP Tracking System - Stolen Report" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    <br />
    <br />
    <table width="400">
        <tr>
            <td align="left" colspan="2" style="font-weight: bold; font-size: 14pt; width: 400px;
                border-bottom: silver 1px solid; font-family: 'Comic Sans MS'; height: 35px">
                Stolen Report.</td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 150px; font-family: Arial">
                <br />
                Serial Number:&nbsp;
            </td>
            <td align="left" style="width: 250px">
                <br />
                &nbsp;<asp:TextBox ID="txtSerialNbr" runat="server" BackColor="LightYellow" Font-Names="Arial"
                    Font-Size="9pt" ReadOnly="True" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 150px; font-family: Arial; height: 25px">
                Date Missing:&nbsp;
            </td>
            <td align="left" style="width: 250px; height: 25px">
                &nbsp;<asp:TextBox ID="txtDateMissing" runat="server" BackColor="LightYellow" Font-Names="Arial"
                    Font-Size="9pt" ReadOnly="True" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 150px; font-family: Arial; height: 50px">
                Last Known Location:&nbsp;
            </td>
            <td align="left" style="width: 250px; height: 50px">
                &nbsp;<asp:TextBox ID="txtLasKnownLocation" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Height="40px" MaxLength="100" TextMode="MultiLine" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 150px; font-family: Arial">
                Action:<br />
                &nbsp;<br />
            </td>
            <td align="left" style="width: 250px">
                &nbsp;<asp:DropDownList ID="ddlAction" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="205px">
                </asp:DropDownList><br />
                <br />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="border-top: silver 1px solid; font-size: 9pt;
                border-bottom: silver 1px solid; font-family: Arial; height: 69px">
                <asp:RadioButtonList ID="rblAction" runat="server" Enabled="False" Font-Names="Arial"
                    Font-Size="9pt" RepeatDirection="Horizontal">
                    <asp:ListItem Value="ComputerFound">Computer found</asp:ListItem>
                    <asp:ListItem Value="DisactivateAlert">Disactivate alert</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
    </table>
    <br />
    <cc1:AddButton ID="cmdOk" runat="server" AddButtonText="Ok " BackColor="DimGray"
        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CausesValidation="False"
        Font-Names="Arial" Font-Size="9pt" Font-Underline="False" ForeColor="White" SaveButtonText="Ok"
        Text="Ok" Width="90px" />&nbsp;
    <asp:Button ID="cmdReturn" runat="server" BackColor="DimGray" BorderColor="Black"
        BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" Font-Bold="False"
        Font-Names="Arial" Font-Size="9pt" ForeColor="White" Text="Return" Width="90px" />&nbsp;
    <cc1:AddButton ID="cmdApply" runat="server" AddButtonText="Apply " BackColor="DimGray"
        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CausesValidation="False"
        Font-Names="Arial" Font-Size="9pt" Font-Underline="False" ForeColor="White" SaveButtonText="Apply"
        Text="Apply" Width="90px" /><br />
</asp:Content>

