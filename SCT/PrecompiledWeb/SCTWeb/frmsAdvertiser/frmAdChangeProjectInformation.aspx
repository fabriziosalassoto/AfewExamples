<%@ page language="VB" masterpagefile="~/frmsAdvertiser/MasterPage.master" autoeventwireup="false" inherits="frmsAdvertiser_frmAdChangeProjectInformation, App_Web_d4xdeinq" title="Advertiser Information" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
        <table cellpadding="0" cellspacing="0" style="padding-right: 0px; padding-left: 0px;
            padding-bottom: 0px; margin: 0px; padding-top: 0px; height: 650px" width="100%">
            <tr>
                <td style="height: 570px; font-size: 9pt; font-family: Arial;" align="center" valign="top">
                    <br />
                    <br />
                    <table style="width: 600px;" id="TABLE1">
                        <tr>
                            <td align="left" colspan="2">
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Size="14pt" Text="Project Information" Font-Names="Comic Sans MS"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                *Required fields.</td>
                        </tr>
                        <tr>
                            <td style="border-top: silver 1px solid;" colspan="2">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="left" colspan="3" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Generic Information<br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            ID:</td>
                                        <td align="left" style="width: 300px">
                                            <asp:TextBox ID="txtAdProjectID" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                ReadOnly="True" Width="100px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            *Contact Name:
                                        </td>
                                        <td align="left" style="width: 300px">
                                            <asp:DropDownList ID="ddlAdProjectContact" runat="server" Width="255px">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="cmvAdProjectContact" runat="server" ControlToValidate="ddlAdProjectContact"
                                                ErrorMessage="*  The Contact is required." Font-Bold="True" Font-Names="Arial"
                                                Font-Size="Large" ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True"
                                                ValueToCompare="0">*</asp:CompareValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            *Advertising Url:
                                        </td>
                                        <td align="left" style="width: 300px">
                                            <asp:TextBox ID="txtAdProjectADUrl" runat="server" Font-Names="Arial" Font-Size="9pt" Width="250px" MaxLength="200"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqvAdProjectAdUrl" runat="server" ControlToValidate="txtAdProjectADUrl"
                                                ErrorMessage="*  The Advertising URL is required." Font-Bold="True" Font-Size="Large"
                                                ForeColor="OrangeRed" Font-Names="Arial" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial" valign="top">
                                            Description:
                                        </td>
                                        <td align="left" style="width: 300px">
                                            <asp:TextBox ID="txtAdProjectDescription" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Height="75px" TextMode="MultiLine" Width="250px" MaxLength="1000"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial; padding-right: 0px; padding-left: 0px; padding-bottom: 0px; padding-top: 8px;">
                                            Size:</td>
                                        <td align="left" style="width: 300px" valign="top">
                                            Height: <asp:TextBox ID="txtAdProjectHeight" runat="server" Width="70px" MaxLength="3"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rgvAdProjectHeight" runat="server" ControlToValidate="txtAdProjectHeight"
                                                ErrorMessage="* Invalid height value" Font-Bold="True" Font-Names="Arial" Font-Size="Large"
                                                ForeColor="OrangeRed" ValidationExpression="\d+" Display="Dynamic" SetFocusOnError="True">*</asp:RegularExpressionValidator><asp:CompareValidator
                                                    ID="cmvAdProjectHeight" runat="server" ControlToValidate="txtAdProjectHeight" ErrorMessage="* The heidth value can not be less than 100"
                                                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed"
                                                    Operator="GreaterThanEqual" SetFocusOnError="True" Type="Integer" ValueToCompare="100" Display="Dynamic">*</asp:CompareValidator>
                                            Width: <asp:TextBox ID="txtAdProjectWidth" runat="server" Width="70px" MaxLength="3"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rgvAdProjectWidth" runat="server" ControlToValidate="txtAdProjectWidth"
                                                ErrorMessage="* Invalid width value" Font-Bold="True" Font-Names="Arial" Font-Size="Large"
                                                ForeColor="OrangeRed" ValidationExpression="\d+" Display="Dynamic" SetFocusOnError="True">*</asp:RegularExpressionValidator><asp:CompareValidator
                                                    ID="cmvAdProjectWidth" runat="server" ControlToValidate="txtAdProjectWidth" ErrorMessage="* The width value can not be less than 100"
                                                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed"
                                                    Operator="GreaterThanEqual" SetFocusOnError="True" Type="Integer" ValueToCompare="100" Display="Dynamic">*</asp:CompareValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial;">
                                            Start Date:
                                        </td>
                                        <td align="left" style="width: 300px;">
                                            <asp:DropDownList ID="ddlAdProjectStartMonth" runat="server" Width="116px">
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
                                            <asp:DropDownList ID="ddlAdProjectStartDay" runat="server" Width="50px">
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
                                            <asp:DropDownList ID="ddlAdProjectStartYear" runat="server" Width="78px">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvAdProjectStartDate" runat="server"
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" ErrorMessage="* Invalid Start Date." Display="Dynamic" SetFocusOnError="True">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            End Date:
                                        </td>
                                        <td align="left" style="width: 300px">
                                            <asp:DropDownList ID="ddlAdProjectEndMonth" runat="server" Width="116px">
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
                                            <asp:DropDownList ID="ddlAdProjectEndDay" runat="server" Width="50px">
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
                                            <asp:DropDownList ID="ddlAdProjectEndYear" runat="server" Width="78px">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvAdProjectEndDate" runat="server" Font-Bold="True" Font-Names="Arial"
                                                Font-Size="Large" ForeColor="OrangeRed" ErrorMessage="* Invalid End Date." Display="Dynamic" SetFocusOnError="True">*</asp:CustomValidator><asp:CustomValidator
                                                    ID="csvAdProjectStartDateGTEndDate" runat="server" ErrorMessage="* Start date can't be after end date."
                                                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" Display="Dynamic" SetFocusOnError="True">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Start Time:
                                        </td>
                                        <td align="left" style="width: 300px">
                                            <asp:DropDownList ID="ddlAdProjectStartHour" runat="server" Width="78px">
                                                <asp:ListItem>Hour</asp:ListItem>
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
                                            </asp:DropDownList>
                                            :
                                            <asp:DropDownList ID="ddlAdProjectStartMinute" runat="server" Width="80px">
                                                <asp:ListItem>Minute</asp:ListItem>
                                                <asp:ListItem Value="00">00</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                            </asp:DropDownList>
                                            :
                                            <asp:DropDownList ID="ddlAdProjectStartAMPM" runat="server" Width="80px">
                                                <asp:ListItem>a.m./p.m.</asp:ListItem>
                                                <asp:ListItem Value="a.m.">a.m.</asp:ListItem>
                                                <asp:ListItem Value="p.m.">p.m.</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvAdProjectStartTime" runat="server" ErrorMessage="* Invalid Start Time. "
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" Display="Dynamic" SetFocusOnError="True">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            End Time:
                                        </td>
                                        <td align="left" style="width: 300px">
                                            <asp:DropDownList ID="ddlAdProjectEndHour" runat="server" Width="78px">
                                                <asp:ListItem>Hour</asp:ListItem>
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
                                            </asp:DropDownList>
                                            :
                                            <asp:DropDownList ID="ddlAdProjectEndMinute" runat="server" Width="80px">
                                                <asp:ListItem>Minute</asp:ListItem>
                                                <asp:ListItem>00</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>55</asp:ListItem>
                                            </asp:DropDownList>
                                            :
                                            <asp:DropDownList ID="ddlAdProjectEndAMPM" runat="server" Width="80px">
                                                <asp:ListItem>a.m./p.m.</asp:ListItem>
                                                <asp:ListItem Value="a.m.">a.m.</asp:ListItem>
                                                <asp:ListItem Value="p.m.">p.m.</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvAdProjectEndTime" runat="server" ErrorMessage="* Invalid End Time."
                                                Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" Display="Dynamic" SetFocusOnError="True">*</asp:CustomValidator><asp:CustomValidator
                                                    ID="csvAdProjectStartTimeGTEndTime" runat="server" ErrorMessage="* Start time can't be after end time."
                                                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" Display="Dynamic" SetFocusOnError="True">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Min. Display:
                                        </td>
                                        <td align="left" style="width: 300px">
                                            <asp:TextBox ID="txtAdProjectMinDisplay" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" Width="75px" MaxLength="10"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rgvAdProjectMinDisplay" runat="server" ErrorMessage="* Invalid Min. Display value" Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" ControlToValidate="txtAdProjectMinDisplay" ValidationExpression="\d+" Display="Dynamic" SetFocusOnError="True">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Max. Display:
                                        </td>
                                        <td align="left" style="width: 300px">
                                            <asp:TextBox ID="txtAdProjectMaxDisplay" runat="server" Font-Names="Arial"
                                                Font-Size="9pt" Width="75px" MaxLength="10"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rgvAdProjectMaxDisplay" runat="server" ControlToValidate="txtAdProjectMaxDisplay"
                                                ErrorMessage="* Invalid Max. Display value" Font-Bold="True" Font-Names="Arial" Font-Size="Large"
                                                ForeColor="OrangeRed" ValidationExpression="\d+" Display="Dynamic" SetFocusOnError="True">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Min. Per Day:
                                        </td>
                                        <td align="left" style="width: 300px;">
                                            <asp:TextBox ID="txtAdProjectMinPerDay" runat="server" Font-Names="Arial" Font-Size="9pt" Width="75px" MaxLength="10"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rgvAdProjectMinPerDay" runat="server" ControlToValidate="txtAdProjectMinPerDay"
                                                ErrorMessage="* Invalid Min. Per Day value" Font-Bold="True" Font-Names="Arial" Font-Size="Large"
                                                ForeColor="OrangeRed" ValidationExpression="\d+" Display="Dynamic" SetFocusOnError="True">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                                            Max. Per Day:
                                        </td>
                                        <td align="left" style="width: 300px">
                                            <asp:TextBox ID="txtAdProjectMaxPerDay" runat="server" Font-Names="Arial" Font-Size="9pt" Width="75px" MaxLength="10"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rgvAdProjectMaxPerDay" runat="server" ControlToValidate="txtAdProjectMaxPerDay"
                                                ErrorMessage="* Invalid Max. Per Day value" Font-Bold="True" Font-Names="Arial" Font-Size="Large"
                                                ForeColor="OrangeRed" ValidationExpression="\d+" Display="Dynamic" SetFocusOnError="True">*</asp:RegularExpressionValidator></td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="border-top: silver 1px solid; border-bottom: silver 1px solid">
                                <br />
                                <table style="width: 500px">
                                    <tr>
                                        <td align="left" colspan="3" style="font-weight: bold; font-size: 10pt; font-family: Arial">
                                            Demographic Requirement<br />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            Sex:
                                        </td>
                                        <td align="left" style="width: 300px">
                                            <asp:DropDownList ID="ddlAdProjectSex" runat="server" Width="100px">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            Min. Age:
                                        </td>
                                        <td align="left" style="width: 300px">
                                            <asp:DropDownList ID="ddlAdProjectMinAge" runat="server" Width="50px">
                                            </asp:DropDownList>
                                            Years.</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            Max. Age:
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            <asp:DropDownList ID="ddlAdProjectMaxAge" runat="server" Width="50px">
                                            </asp:DropDownList>
                                            Years.</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            Occupation:
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            <asp:DropDownList ID="ddlAdProjectOccupation" runat="server" Width="206px">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            Country or Region:
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            <asp:DropDownList ID="ddlAdProjectCountry" runat="server" AutoPostBack="True" Width="206px">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <div id="Div1" dir="ltr">
                                                State or Province:
                                            </div>
                                        </td>
                                        <td align="left" colspan="2" style="width: 300px">
                                            <asp:DropDownList ID="ddlAdProjectState" runat="server" Width="206px">
                                            </asp:DropDownList></td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="OrangeRed" Width="600px" />
                    <br />
                    <asp:Button ID="cmdOk" runat="server" Font-Names="Verdana" Text="Ok"
                        Width="80px" />
                    <asp:Button ID="cmdReturn" runat="server" CausesValidation="False" Font-Names="Verdana" Text="Return" Width="80px" /><br />
                                <cc1:MsgBox ID="MsgBox" runat="server" />
                    <br />
                   
                </td>
            </tr>
        </table>
</asp:Content>

