<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewProfile.aspx.cs" Inherits="ViewProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Profile</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Profile]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT UsrId AS UsrIdAcc, UsrName AS UsrNameAcc, Email AS EmailAcc FROM [User] WHERE (UsrId = @val1)"></asp:SqlDataSource>
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
            <asp:Label ID="GenderLabel" runat="server" Text="Gender"></asp:Label>
            &nbsp;<asp:DropDownList ID="GenderList" runat="server">
                <asp:ListItem Value="M">Male</asp:ListItem>
                <asp:ListItem Value="F">Female</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Label ID="TelephoneLabel" runat="server" Text="Telephone"></asp:Label>
            <asp:TextBox ID="TelephoneInput" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="AddressLabel" runat="server" Text="Address" Style="vertical-align: top;"></asp:Label>
            <asp:TextBox ID="AddressInput" TextMode="MultiLine" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="SaveBut" runat="server" Text="Save" OnClick="SavBut_Click" />
            &nbsp;<br />
            &nbsp;<asp:Label ID="Comment" runat="server" Text=""></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
