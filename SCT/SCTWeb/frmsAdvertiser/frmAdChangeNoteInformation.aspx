<%@ Page Language="VB" MasterPageFile="~/frmsAdvertiser/MasterPage.master" AutoEventWireup="false" CodeFile="frmAdChangeNoteInformation.aspx.vb" Inherits="frmsAdvertiser_frmAdChangeNoteInformation" title="Untitled Page" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    &nbsp;<table cellpadding="0" cellspacing="0" style="padding-right: 0px; padding-left: 0px;
        padding-bottom: 0px; margin: 0px; padding-top: 0px; height: 650px" width="100%">
        <tr>
            <td align="center" style="font-size: 9pt; font-family: Arial; height: 570px" valign="top">
                <br />
                <br />
                <table id="TABLE" style="width: 600px">
                    <tr>
                        <td align="left" colspan="2" style="height: 29px">
                            <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Names="Comic Sans MS"
                                Font-Size="14pt" Text="Note Information"></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            *Required fields.</td>
                    </tr>
                    <tr style="font-family: Arial">
                        <td colspan="2" style="border-top: silver 1px solid; border-bottom: silver 1px solid">
                            <br />
                            <table style="width: 500px">
                                <tr>
                                    <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                        ID:&nbsp;
                                    </td>
                                    <td align="left" colspan="2" style="width: 300px">
                                        &nbsp;<asp:TextBox ID="txtAdNoteID" runat="server" Font-Names="Arial" Font-Size="9pt"
                                            ReadOnly="True" Width="100px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                        *Contact Name:&nbsp;
                                    </td>
                                    <td align="left" style="width: 300px" colspan="2">
                                        &nbsp;<asp:DropDownList ID="ddlAdNoteContact" runat="server" Width="252px">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="cmvAdNoteContact" runat="server" ControlToValidate="ddlAdNoteContact"
                                            ErrorMessage="*  The Contact is required." Font-Bold="True" Font-Names="Arial"
                                            Font-Size="Large" ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True"
                                            ValueToCompare="0" Display="Dynamic">*</asp:CompareValidator></td>
                                </tr>
                                <tr style="font-family: Arial">
                                    <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                        *Date Entered:&nbsp;</td>
                                    <td align="left" style="width: 300px" colspan="2">
                                        &nbsp;<asp:DropDownList ID="ddlAdNoteEnteredMonth" runat="server" Width="116px">
                                            <asp:ListItem>Month</asp:ListItem>
                                            <asp:ListItem Value="01">January</asp:ListItem>
                                            <asp:ListItem Value="02">February </asp:ListItem>
                                            <asp:ListItem Value="03">March</asp:ListItem>
                                            <asp:ListItem Value="04">April</asp:ListItem>
                                            <asp:ListItem Value="05">May</asp:ListItem>
                                            <asp:ListItem Value="06">June</asp:ListItem>
                                            <asp:ListItem Value="07">July</asp:ListItem>
                                            <asp:ListItem Value="08">August</asp:ListItem>
                                            <asp:ListItem Value="09">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlAdNoteEnteredDay" runat="server" Width="50px">
                                            <asp:ListItem>Day</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                        </asp:DropDownList>
                                        ,
                                        <asp:DropDownList ID="ddlAdNoteEnteredYear" runat="server" Width="75px">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="csvAdNoteEnteredDateValid" runat="server" ErrorMessage="* Invalid Date Entered."
                                            Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator><asp:CustomValidator
                                                ID="csvAdNoteDateEnteredRequired" runat="server" Display="Dynamic" ErrorMessage="*  The Date Entered is required."
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:CustomValidator></td>
                                </tr>
                                <tr style="font-family: Arial">
                                    <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial" valign="top">
                                        *Description:&nbsp;
                                    </td>
                                    <td align="left" valign="top">
                                        &nbsp;<asp:TextBox ID="txtAdNoteDescription" runat="server" Font-Names="Arial" Font-Size="9pt"
                                            Height="75px" TextMode="MultiLine" Width="246px"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" style="width: 29px">
                                        <asp:RequiredFieldValidator ID="rqvAdNoteDescription" runat="server" Display="Dynamic"
                                            Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True" ErrorMessage="*  The Task Notes are required." ControlToValidate="txtAdNoteDescription">*</asp:RequiredFieldValidator></td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="OrangeRed" Width="600px" Font-Bold="True" />
                <br />
                <asp:Button ID="cmdOk" runat="server" Font-Names="Verdana" Text="Ok"
                    Width="80px" />&nbsp;
                <asp:Button ID="cmdReturn" runat="server" CausesValidation="False" Font-Names="Verdana" Text="Return" Width="80px" /><br />
                <cc1:MsgBox ID="MsgBox" runat="server" />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

