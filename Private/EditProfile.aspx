<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="EditProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Profile</title>
    <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="profile.png" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-panel">
            <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT ProfileId, Email, UsrId, Name, DOB, Address, Telephone, Gender, Privacy FROM Profile WHERE (Email = @TempEmail)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HiddenEmail" Name="TempEmail" PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT UsrId AS UsrIdAcc, UsrName AS UsrNameAcc, Email AS EmailAcc FROM [User] WHERE (Email = @val1)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HiddenEmail" Name="val1" PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:HiddenField ID="HiddenEmail" runat="server" />
            <br />
            <asp:Label ID="DOBLabel" runat="server" Text="Date of Birth"></asp:Label>
            <asp:DropDownList ID="YearList" runat="server" AutoPostBack="true">
            </asp:DropDownList>
            &nbsp;
        <asp:DropDownList ID="MonthList" runat="server" AutoPostBack="true">
        </asp:DropDownList>
            &nbsp;<asp:DropDownList ID="DayList" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="GenderLabel" runat="server" Text="Gender"></asp:Label>
            &nbsp;<asp:DropDownList ID="GenderList" runat="server" AutoPostBack="true">
                <asp:ListItem Value="M">Male</asp:ListItem>
                <asp:ListItem Value="F">Female</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="TelephoneLabel" runat="server" Text="Telephone"></asp:Label>
            <asp:TextBox ID="TelephoneInput" runat="server" TextMode="Phone"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="AddressLabel" runat="server" Text="Address" Style="vertical-align: top;"></asp:Label>
            <asp:TextBox ID="AddressInput" TextMode="MultiLine" runat="server"></asp:TextBox>
            <br />
            <asp:CheckBox ID="PrivacyInput" runat="server" Text="Prevent others from seeing my profile" />
            <br />
            <br />
            <asp:Button ID="SaveBut" class="button" runat="server" Text="Save" OnClick="SavBut_Click" />
            &nbsp;<br />
            <asp:Button ID="RetBut" class="button" runat="server" Text="Return" OnClick="RetBut_Click" />
            <br />
            &nbsp;<asp:Label ID="Comment" class="warning" runat="server" Text=""></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
