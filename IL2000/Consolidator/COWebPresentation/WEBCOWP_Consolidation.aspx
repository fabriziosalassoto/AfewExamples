<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WEBCOWP_Consolidation.aspx.cs" Inherits="COWebPresentation._Default" %>

<%@ Register assembly="Artem.GoogleMap" namespace="Artem.Web.UI.Controls" tagprefix="cc1"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .LstViewDestionations
        {
            height: 68px;
        }
        .style11
        {
            width: 102px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-size: xx-large; font-family: Verdana; color: #000080">    
            Truck Load    
            Consolidation Solution (VRP)<br />
            <asp:Panel ID="Panel1" runat="server" Width="890px" Height="505px">
                <table>
	                <tr>
	                    <td class="style11">	                        
                            <asp:Label ID="Label1" Font-Size="XX-Small" runat="server" Text="Origin Zip:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtOriginPoint" runat="server"></asp:TextBox>
                        </td>                        
                        <td rowspan=6 valign=top>
                            <cc1:GoogleMap ID="GoogleMap1" runat="server" 
                                           AllowBidirectionalLanguages="True" BaseCountryCode="1" BorderStyle="Ridge" 
                                                    EnableContinuousZoom="True" Height="600px" Latitude="38.75408" 
                                                    Longitude="-95.36133" Width="1024px" Zoom="4">                                                                           
                            </cc1:GoogleMap>                            
                        </td>
                    </tr>
                    <tr>
                        <td class="style11">
                            <asp:Label ID="Label3" runat="server" Font-Size="XX-Small" Text="Zips File:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtFileName" runat="server" AutoPostBack="True" BackColor="White"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign=top>
                            <asp:FileUpload ID="FileUpload2" runat="server" 
                                    ToolTip="File structure should be a set of records like this: [O/D],[ZIP],[WEIGHT]. O=Origin, D=Destination. Example: O,23000,500" 
                                    Width="85px" />
                            <asp:Button ID="BtnSend" runat="server" onclick="Button1_Click1" Text="Send" 
                                    ToolTip="File structure should be a set of records like this: [O/D],[ZIP],[WEIGHT]. O=Origin, D=Destination. Example: O,23000,500" />
                            <br/>
                        </td>
                     <tr>
                        <td colspan="2" valign=top>
                            <asp:Button ID="BtnGenerateRandomZips" runat="server" 
                                    onclick="BtnGenerateRandomZips_Click" Text="Generate Random Zips" 
                                    Width="150px" />
                        </td>
                     </tr>
                     <tr>
                        <td class="style11" colspan=2>
                            <asp:Button ID="BtnCalculateSolution" runat="server" 
                                        onclick="BtnCalculateSolution_Click" Text="Calculate Solution" />                                
                        </td>
                     </tr>
                     <tr>
                        <td colspan="2" style="font-size: xx-small; font-family: Verdana;" valign="top">
                            <asp:ListView ID="LstViewDestinations" runat="server" style="margin-right: 0px">
                                <LayoutTemplate>
                                    <table ID="tblProducts" runat="server" border="0" cellpadding="2" 
                                        cellspacing="0" style="border-color : Black" width="160px">
                                        <tr ID="Tr1" runat="server" 
                                            style="background-color : Navy; color : white; font-weight : bold">
                                            <td ID="Td1" runat="server" width="80px">
                                                Zip Code</td>
                                            <td ID="Td2" runat="server" width="80px">
                                                Weight</td>
                                        </tr>
                                        <tr ID="ItemPlaceholder" runat="server">
                                        </tr>
                                        <tr ID="Tr2" runat="server">
                                            <td ID="Td4" runat="server">
                                                <asp:DataPager ID="ContactsDataPager" runat="server" PageSize="50">
                                                </asp:DataPager>
                                            </td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="ZipCode" runat="Server" 
                                                Text='<%#DataBinder.Eval(Container,"DataItem.ZipCode")%>'></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="Weight" runat="Server" 
                                                Text='<%#DataBinder.Eval(Container,"DataItem.Weight")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <div>
                                        No zips loaded yet</div>
                                </EmptyDataTemplate>
                                <EditItemTemplate>
                                    <tr style="background-color: #ADD8E6">
                                        <td>
                                            <asp:TextBox ID="ZipCodeB" runat="server" MaxLength="50" 
                                                Text='<%#DataBinder.Eval(Container,"DataItem.ZipCode")%>' />
                                            <br />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="WeightB" runat="server" MaxLength="50" 
                                                Text='<%#DataBinder.Eval(Container,"DataItem.Weight")%>' />
                                            <br />
                                        </td>
                                    </tr>
                                </EditItemTemplate>
                            </asp:ListView>
                        </td>                        
                    </tr>
                </table>		        		             		                                                                            
            </asp:Panel>    
        </div>
    </form>
</body>
</html>
