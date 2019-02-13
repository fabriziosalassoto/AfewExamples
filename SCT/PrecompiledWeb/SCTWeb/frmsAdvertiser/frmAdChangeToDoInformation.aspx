<%@ page language="VB" masterpagefile="~/frmsAdvertiser/MasterPage.master" autoeventwireup="false" inherits="frmsAdvertiser_frmAdChangeToDoInformation, App_Web_d4xdeinq" title="Advertiser To Do Information" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    &nbsp;<table cellpadding="0" cellspacing="0" style="padding-right: 0px; padding-left: 0px;
        padding-bottom: 0px; margin: 0px; padding-top: 0px; height: 650px" width="100%">
        <tr>
            <td align="center" style="font-size: 9pt; font-family: Arial; height: 570px" valign="top">
                <br />
                <br />
                <table id="TABLE1" style="width: 600px">
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Names="Comic Sans MS"
                                Font-Size="14pt" Text="To Do Information"></asp:Label>&nbsp;</td>
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
                                        *Contact Name:&nbsp;
                                    </td>
                                    <td align="left" style="width: 300px">
                                        &nbsp;<asp:DropDownList ID="ddlContactName" runat="server" Width="255px">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="cvContactName" runat="server" ControlToValidate="ddlContactName"
                                            ErrorMessage="*  The Contact is required." Font-Bold="True" Font-Names="Arial"
                                            Font-Size="Large" ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True"
                                            ValueToCompare="0">*</asp:CompareValidator></td>
                                </tr>
                                <tr style="font-family: Arial">
                                    <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                        *Date Entered:&nbsp;</td>
                                    <td align="left" style="width: 300px">
                                        &nbsp;<asp:DropDownList ID="ddlEnteredMonth" runat="server" Width="116px">
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
                                        <asp:DropDownList ID="ddlEnteredDay" runat="server" Width="50px">
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
                                        <asp:DropDownList ID="ddlEnteredYear" runat="server" Width="75px">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="cvEnteredDate" runat="server" ErrorMessage="* Invalid Date."
                                            Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator></td>
                                </tr>
                                <tr style="font-family: Arial">
                                    <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                        Date Due:&nbsp;
                                    </td>
                                    <td align="left" style="width: 300px">
                                        &nbsp;<asp:DropDownList ID="ddlDueMonth" runat="server" Width="116px">
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
                                        <asp:DropDownList ID="ddlDueDay" runat="server" Width="50px">
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
                                        <asp:DropDownList ID="ddlDueYear" runat="server" Width="75px">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="cvDueDate" runat="server" ErrorMessage="* Invalid Date."
                                            Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator><asp:CustomValidator
                                                ID="cvEnteredDateGTDueDate" runat="server" ErrorMessage="* Start date can't be after end date."
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator></td>
                                </tr>
                                <tr style="font-family: Arial">
                                    <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                        Date Completed:&nbsp;</td>
                                    <td align="left" style="width: 300px">
                                        &nbsp;<asp:DropDownList ID="ddlCompletedMonth" runat="server" Width="116px">
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
                                        <asp:DropDownList ID="ddlCompletedDay" runat="server" Width="50px">
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
                                        <asp:DropDownList ID="ddlCompletedYear" runat="server" Width="75px">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="cvCompletedDate" runat="server" ErrorMessage="* Invalid Date."
                                            Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator><asp:CustomValidator
                                                ID="cvEnteredDateGTCompletedDate" runat="server" ErrorMessage="* Start date can't be after end date."
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed">*</asp:CustomValidator></td>
                                </tr>
                                <tr style="font-family: Arial">
                                    <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial" valign="top">
                                        Task Notes:&nbsp;
                                    </td>
                                    <td align="left" style="width: 300px">
                                        &nbsp;<asp:TextBox ID="txtTaskNotes" runat="server" Font-Names="Arial" Font-Size="9pt"
                                            Height="75px" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="font-family: Arial">
                                    <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                        &nbsp;</td>
                                    <td align="left" style="width: 300px">
                                        &nbsp;<asp:Image ID="imgAdToDoCallBackRecord" runat="server" ImageAlign="AbsMiddle"
                                            ImageUrl="~/Images/Checked_False.png" />
                                        <asp:Label ID="lblAdContactCallBackRecord" runat="server" Text="Call Back Record"></asp:Label></td>
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

