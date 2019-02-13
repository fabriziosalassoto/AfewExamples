<%@ Page Language="VB" MasterPageFile="~/Forms/MasterPage.master" AutoEventWireup="false" CodeFile="frmAdvertiserProject.aspx.vb" Inherits="Forms_frmAdvertiserProject" title="IP Tracking Admin System - Project" %>

<%@ Register Assembly="ITC_Web_Controls" Namespace="ITC_Web_Controls" TagPrefix="cc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <cc1:MsgBox ID="MsgBox" runat="server" />
    &nbsp;<br />
    <table style="width: 600px; font-size: 9pt; font-family: Arial;">
        <tr>
            <td align="left" colspan="3" style="font-size: 14pt; font-family: 'Comic Sans MS'">
                Project Information</td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="font-weight: bold; font-size: 10pt; vertical-align: middle;
                font-family: Arial; text-align: left; border-top: silver 1px solid; border-bottom: silver 1px solid;">
                <br />
                &nbsp; &nbsp;Generic Information</td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <br />
                <asp:Label ID="lblAdProjectID" runat="server" Text="ID:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <br />
                <asp:TextBox ID="txtAdProjectID" runat="server" Font-Names="Arial" Font-Size="9pt"
                    ReadOnly="True" Width="100px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial;">
                <asp:Label ID="lblAdProjectAdvertiser" runat="server" Text="Advertiser:"></asp:Label></td>
            <td align="left" style="width: 400px;" colspan="2">
                <asp:DropDownList ID="ddlAdProjectAdvertiser" runat="server" Width="255px" AutoPostBack="True">
                </asp:DropDownList>
                <asp:CompareValidator ID="cmvAdProjectAdvertiser" runat="server" ControlToValidate="ddlAdProjectAdvertiser"
                    ErrorMessage="*  The Advertiser is required." Font-Bold="True" Font-Names="Arial"
                    Font-Size="Large" ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True"
                    ValueToCompare="0">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectContact" runat="server" Text="Contact:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:DropDownList ID="ddlAdProjectContact" runat="server" Width="255px">
                </asp:DropDownList>
                <asp:CompareValidator ID="cmvAdProjectContact" runat="server" ControlToValidate="ddlAdProjectContact"
                    ErrorMessage="*  The Contact is required." Font-Bold="True" Font-Names="Arial"
                    Font-Size="Large" ForeColor="OrangeRed" Operator="NotEqual" SetFocusOnError="True"
                    ValueToCompare="0">*</asp:CompareValidator></td>
        </tr>
        <tr style="color: #000000">
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectADUrl" runat="server" Text="Url:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdProjectADUrl" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="200" Width="250px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rqvAdProjectAdUrl" runat="server" ControlToValidate="txtAdProjectADUrl"
                    Display="Dynamic" ErrorMessage="*  The Advertising URL is required." Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr style="font-size: 12pt; color: #000000">
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial" valign="top">
                <asp:Label ID="lblAdProjectDescription" runat="server" Text="Description:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdProjectDescription" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Height="75px" MaxLength="1000" TextMode="MultiLine" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt;
                width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectHeight" runat="server" Text="Height:"></asp:Label></td>
            <td align="left" style="width: 400px" valign="top" colspan="2">
                <asp:TextBox ID="txtAdProjectHeight" runat="server" MaxLength="3" Width="70px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rgvAdProjectHeight" runat="server" ControlToValidate="txtAdProjectHeight"
                    Display="Dynamic" ErrorMessage="* Invalid height value" Font-Bold="True" Font-Names="Arial"
                    Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True" ValidationExpression="\d+">*</asp:RegularExpressionValidator><asp:CompareValidator
                        ID="cmvAdProjectHeight" runat="server" ControlToValidate="txtAdProjectHeight"
                        Display="Dynamic" ErrorMessage="* The heidth value can not be less than 100"
                        Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" Operator="GreaterThanEqual"
                        SetFocusOnError="True" Type="Integer" ValueToCompare="100">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectWidth" runat="server" Text="Width:"></asp:Label></td>
            <td align="left" style="width: 400px" valign="top" colspan="2">
                <asp:TextBox ID="txtAdProjectWidth" runat="server" MaxLength="3" Width="70px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rgvAdProjectWidth" runat="server" ControlToValidate="txtAdProjectWidth"
                    Display="Dynamic" ErrorMessage="* Invalid width value" Font-Bold="True" Font-Names="Arial"
                    Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True" ValidationExpression="\d+">*</asp:RegularExpressionValidator><asp:CompareValidator
                        ID="cmvAdProjectWidth" runat="server" ControlToValidate="txtAdProjectWidth" Display="Dynamic"
                        ErrorMessage="* The width value can not be less than 100" Font-Bold="True" Font-Names="Arial"
                        Font-Size="Large" ForeColor="OrangeRed" Operator="GreaterThanEqual" SetFocusOnError="True"
                        Type="Integer" ValueToCompare="100">*</asp:CompareValidator></td>
        </tr>
        <tr style="font-weight: bold; font-size: 12pt; color: #000000">
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial; font-weight: normal;">
                <asp:Label ID="lblAdProjectStartDate" runat="server" Text="Start Date:"></asp:Label></td>
            <td align="left" style="width: 400px; font-weight: normal; font-size: 9pt; font-family: Arial;" colspan="2">
                <asp:DropDownList ID="ddlAdProjectStartMonth" runat="server" Width="115px">
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
                <asp:DropDownList ID="ddlAdProjectStartDay" runat="server" Width="54px">
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
                <asp:DropDownList ID="ddlAdProjectStartYear" runat="server" Width="80px">
                </asp:DropDownList>
                <asp:CustomValidator ID="csvAdProjectStartDate" runat="server" Display="Dynamic"
                    ErrorMessage="* Invalid Start Date." Font-Bold="True" Font-Names="Arial" Font-Size="Large"
                    ForeColor="OrangeRed" SetFocusOnError="True">*</asp:CustomValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectEndDate" runat="server" Text="End Date:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:DropDownList ID="ddlAdProjectEndMonth" runat="server" Width="115px">
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
                <asp:DropDownList ID="ddlAdProjectEndDay" runat="server" Width="54px">
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
                <asp:DropDownList ID="ddlAdProjectEndYear" runat="server" Width="80px">
                </asp:DropDownList>
                <asp:CustomValidator ID="csvAdProjectEndDate" runat="server" Display="Dynamic" ErrorMessage="* Invalid End Date."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed"
                    SetFocusOnError="True">*</asp:CustomValidator><asp:CustomValidator ID="csvAdProjectStartDateGTEndDate"
                        runat="server" Display="Dynamic" ErrorMessage="* Start date can't be after end date."
                        Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed"
                        SetFocusOnError="True">*</asp:CustomValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectStartTime" runat="server" Text="Start Time:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:DropDownList ID="ddlAdProjectStartHour" runat="server" Width="84px">
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
                <asp:DropDownList ID="ddlAdProjectStartMinute" runat="server" Width="85px">
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
                <asp:DropDownList ID="ddlAdProjectStartAMPM" runat="server" Width="80px">
                    <asp:ListItem>a.m./p.m.</asp:ListItem>
                    <asp:ListItem Value="a.m.">a.m.</asp:ListItem>
                    <asp:ListItem Value="p.m.">p.m.</asp:ListItem>
                </asp:DropDownList>
                <asp:CustomValidator ID="csvAdProjectStartTime" runat="server" Display="Dynamic"
                    ErrorMessage="* Invalid Start Time. " Font-Bold="True" Font-Names="Arial" Font-Size="Large"
                    ForeColor="OrangeRed" SetFocusOnError="True">*</asp:CustomValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectEndTime" runat="server" Text="End Time:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:DropDownList ID="ddlAdProjectEndHour" runat="server" Width="84px">
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
                <asp:DropDownList ID="ddlAdProjectEndMinute" runat="server" Width="85px">
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
                <asp:DropDownList ID="ddlAdProjectEndAMPM" runat="server" Width="80px">
                    <asp:ListItem>a.m./p.m.</asp:ListItem>
                    <asp:ListItem Value="a.m.">a.m.</asp:ListItem>
                    <asp:ListItem Value="p.m.">p.m.</asp:ListItem>
                </asp:DropDownList>
                <asp:CustomValidator ID="csvAdProjectEndTime" runat="server" Display="Dynamic" ErrorMessage="* Invalid End Time."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed"
                    SetFocusOnError="True">*</asp:CustomValidator><asp:CustomValidator ID="csvAdProjectStartTimeGTEndTime"
                        runat="server" Display="Dynamic" ErrorMessage="* Start time can't be after end time."
                        Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed"
                        SetFocusOnError="True">*</asp:CustomValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectMinDisplay" runat="server" Text="Min. Display:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdProjectMinDisplay" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="10" Width="75px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rgvAdProjectMinDisplay" runat="server" ControlToValidate="txtAdProjectMinDisplay"
                    Display="Dynamic" ErrorMessage="* Invalid Min. Display value" Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True"
                    ValidationExpression="\d+">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectMaxDisplay" runat="server" Text="Max. Display:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdProjectMaxDisplay" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="10" Width="75px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rgvAdProjectMaxDisplay" runat="server" ControlToValidate="txtAdProjectMaxDisplay"
                    Display="Dynamic" ErrorMessage="* Invalid Max. Display value" Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True"
                    ValidationExpression="\d+">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectMinPerDay" runat="server" Text="Min. Per Day:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdProjectMinPerDay" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="10" Width="75px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rgvAdProjectMinPerDay" runat="server" ControlToValidate="txtAdProjectMinPerDay"
                    Display="Dynamic" ErrorMessage="* Invalid Min. Per Day value" Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True"
                    ValidationExpression="\d+">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectMaxPerDay" runat="server" Text="Max. Per Day:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdProjectMaxPerDay" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="10" Width="75px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rgvAdProjectMaxPerDay" runat="server" ControlToValidate="txtAdProjectMaxPerDay"
                    Display="Dynamic" ErrorMessage="* Invalid Max. Per Day value" Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True"
                    ValidationExpression="\d+">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectCountDisplayed" runat="server" Text="Count Displayed"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdProjectCountDisplayed" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                    Width="75px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rgvAdProjectCountDisplayed" runat="server" ControlToValidate="txtAdProjectCountDisplayed"
                    Display="Dynamic" ErrorMessage="* Invalid Count Displayed value" Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True"
                    ValidationExpression="\d+">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectVerifiedDate" runat="server" Text="Verified Date"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2"><asp:DropDownList ID="ddlAdProjectVerifiedMonth" runat="server" Width="115px">
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
            <asp:DropDownList ID="ddlAdProjectVerifiedDay" runat="server" Width="54px">
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
                <asp:DropDownList ID="ddlAdProjectVerifiedYear" runat="server" Width="80px">
                </asp:DropDownList>
                <asp:CustomValidator ID="csvAdProjectVerifiedDate" runat="server" Display="Dynamic" ErrorMessage="* Invalid Verified Date."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:CustomValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectOnlineDate" runat="server" Text="Online Date"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2"><asp:DropDownList ID="ddlAdProjectOnlineMonth" runat="server" Width="115px">
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
            <asp:DropDownList ID="ddlAdProjectOnlineDay" runat="server" Width="54px">
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
                <asp:DropDownList ID="ddlAdProjectOnlineYear" runat="server" Width="80px">
                </asp:DropDownList>
                <asp:CustomValidator ID="csvAdProjectOnlineDate" runat="server" Display="Dynamic" ErrorMessage="* Invalid Online Date."
                    Font-Bold="True" Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True">*</asp:CustomValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectPromoCode" runat="server" Text="Promo Code"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdProjectPromoCode" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                    Width="75px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rgvAdProjectPromoCode" runat="server" ControlToValidate="txtAdProjectPromoCode"
                    Display="Dynamic" ErrorMessage="* Invalid Promo Code value" Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True"
                    ValidationExpression="\d+">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectComissionCode" runat="server" Text="Comission Code"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:TextBox ID="txtAdProjectComissionCode" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                    Width="75px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rgvAdProjectComissionCode" runat="server" ControlToValidate="txtAdProjectComissionCode"
                    Display="Dynamic" ErrorMessage="* Invalid Comission Code value" Font-Bold="True"
                    Font-Names="Arial" Font-Size="Large" ForeColor="OrangeRed" SetFocusOnError="True"
                    ValidationExpression="\d+">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="left" colspan="3" style="font-size: 10pt; font-family: Arial; font-weight: bold; border-top: silver 1px solid; border-bottom: silver 1px solid;">
                <br />
                &nbsp;&nbsp;Demographic Requirement</td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <br />
                <asp:Label ID="lblAdProjectSex" runat="server" Text="Sex:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <br />
                <asp:DropDownList ID="ddlAdProjectSex" runat="server" Width="125px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectMinAge" runat="server" Text="Min. Age:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:DropDownList ID="ddlAdProjectMinAge" runat="server" Width="75px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectMaxAge" runat="server" Text="Max. Age:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:DropDownList ID="ddlAdProjectMaxAge" runat="server" Width="75px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectOccupation" runat="server" Text="Occupation:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:DropDownList ID="ddlAdProjectOccupation" runat="server" Width="250px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectCountry" runat="server" Text="Country:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:DropDownList ID="ddlAdProjectCountry" runat="server" AutoPostBack="True" Width="250px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
                <asp:Label ID="lblAdProjectState" runat="server" Text="State:"></asp:Label></td>
            <td align="left" style="width: 400px" colspan="2">
                <asp:DropDownList ID="ddlAdProjectState" runat="server" Width="250px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right" style="font-size: 9pt; width: 200px; font-family: Arial">
            </td>
            <td align="right">
                <asp:Menu ID="mnuItem" runat="server" DynamicHorizontalOffset="2" Font-Names="Arial"
                    Font-Size="9pt" ForeColor="Gray" Orientation="Horizontal" StaticBottomSeparatorImageUrl="~/Images/Tab.gif"
                    StaticDisplayLevels="3" StaticSubMenuIndent="10px">
                    <StaticSelectedStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" />
                    <LevelMenuItemStyles>
                        <asp:MenuItemStyle Font-Bold="True" Font-Names="Arial" Font-Size="9pt" Font-Underline="False"
                            ForeColor="Black" />
                    </LevelMenuItemStyles>
                    <StaticMenuItemStyle Font-Bold="False" Font-Names="Arial" ForeColor="#990000" ItemSpacing="5px" />
                    <DynamicHoverStyle BackColor="DarkGray" ForeColor="Black" />
                    <DynamicMenuStyle BackColor="#FFFBD6" />
                    <DynamicSelectedStyle BackColor="#FFCC66" />
                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" ForeColor="#990000" />
                    <Items>
                        <asp:MenuItem ImageUrl="~/Images/Profile.png" Text=" Advertiser" Value="Advertiser">
                        </asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/USER16.gif" Text=" Contact" Value="Contact"></asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/DELETE16.GIF" Text=" Delete" Value="Delete"></asp:MenuItem>
                    </Items>
                </asp:Menu>
                <br />
            </td>
            <td align="left" style="width: 123px">
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" style="border-top: silver 1px solid; font-size: 9pt;
                font-family: Arial;">
                <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" Font-Bold="True"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="OrangeRed" Width="576px" />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3" style="border-top: silver 1px solid; font-size: 9pt;
                font-family: Arial;">
                <asp:Menu ID="mnuSave" runat="server" DynamicHorizontalOffset="2" Font-Names="Arial"
                    Font-Size="9pt" ForeColor="Gray" Orientation="Horizontal" StaticBottomSeparatorImageUrl="~/Images/Tab.gif"
                    StaticDisplayLevels="3" StaticSubMenuIndent="10px">
                    <LevelMenuItemStyles>
                        <asp:MenuItemStyle Font-Bold="True" Font-Names="Arial" Font-Size="9pt" Font-Underline="False"
                            ForeColor="Black" />
                    </LevelMenuItemStyles>
                    <StaticMenuItemStyle Font-Bold="False" Font-Names="Arial" ForeColor="#990000" ItemSpacing="5px" />
                    <DynamicHoverStyle BackColor="DarkGray" ForeColor="Black" />
                    <DynamicMenuStyle BackColor="#FFFBD6" />
                    <StaticSelectedStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" />
                    <DynamicSelectedStyle BackColor="#FFCC66" />
                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <Items>
                        <asp:MenuItem ImageUrl="~/Images/OK.bmp" Text=" Ok" Value="Ok"></asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/CANCEL.bmp" Text=" Cancel" Value="Cancel"></asp:MenuItem>
                        <asp:MenuItem ImageUrl="~/Images/SAVE.png" Text=" Save" Value="Save"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="9pt"
                        Font-Underline="False" ForeColor="#990000" />
                </asp:Menu>
                <br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

