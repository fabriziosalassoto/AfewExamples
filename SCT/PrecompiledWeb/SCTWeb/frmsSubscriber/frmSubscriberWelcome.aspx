<%@ page language="VB" masterpagefile="~/frmsSubscriber/MasterPage.master" autoeventwireup="false" inherits="frmsSubscriber_frmSubscriberWelcome, App_Web_isnfrdft" title="IP Tracking System - Welcome" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <br />
    <br />
    <br />
    <table width="550">
        <tr>
            <td align="left" style="border-top: silver 1px solid; font-weight: normal; font-size: 16pt;
                width: 550px; color: black; font-family: 'Comic Sans MS'; height: 40px;">
                &nbsp;<asp:Label ID="lblWelcome" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-weight: normal; font-size: 12pt; width: 550px; color: black;
                font-family: 'Comic Sans MS'; height: 38px" valign="bottom">
                <br />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Wants download
                IP tracking system?</td>
        </tr>
        <tr>
            <td align="center" style="font-weight: normal; font-size: 12pt;
                width: 550px; color: black; border-bottom: silver 1px solid; font-family: 'Comic Sans MS';
                height: 50px">
                <asp:Button ID="cmdOk" runat="server" BackColor="DimGray" BorderColor="Black"
                    BorderStyle="Outset" BorderWidth="1px" Font-Bold="False" Font-Names="Arial" Font-Size="9pt"
                    ForeColor="White" Text="Ok" Width="100px" />&nbsp;
                <asp:Button ID="cmdCancel" runat="server" BackColor="DimGray" BorderColor="Black"
                    BorderStyle="Outset" BorderWidth="1px" Font-Bold="False" Font-Names="Arial" Font-Size="9pt"
                    ForeColor="White" Text="Cancel" Width="100px" /></td>
        </tr>
    </table>
</asp:Content>

