<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WEBCOWP_Test.aspx.cs" Inherits="COWebPresentation.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">

        .style1
        {
            width: 77px;
        }
        .style2
        {
            width: 223px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <div>
    
    <asp:Panel ID="Panel1" runat="server" Font-Bold="True" Height="124px" 
        style="margin-left: 236px" Width="482px">
        Rating Engine<br />
        <table bgcolor="#006699" style="width:100%;">
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LblZipCode" runat="server" Font-Bold="True" ForeColor="#CCFFFF" 
                        Text="Zip Code:"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="TxtZipCode" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="BtnConvert" runat="server" Text="Calculate Lat, Long" 
                        onclick="BtnConvert_Click" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LblLatitude" runat="server" Font-Bold="True" ForeColor="#CCFFFF" 
                        Text="Latitude:"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="TxtLatitude" runat="server" Width="217px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="LblLongitude" runat="server" Font-Bold="True" 
                        ForeColor="#CCFFFF" Text="Longitude:"></asp:Label>
                </td>
                <td class="style2">
                    <asp:TextBox ID="TxtLongitude" runat="server" Width="216px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    
    </div>
    </form>
</body>
</html>
