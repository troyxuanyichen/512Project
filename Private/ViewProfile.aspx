<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Private/ViewProfile.aspx.cs" Inherits="ViewProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Profile</title>
    <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="avatar.jpg" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-panel">
            <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Email, UsrId, DOB, State, City, Street, Telephone, Gender, Privacy FROM Profile WHERE (Email = @val1)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HiddenField1" Name="val1" PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <br />
            <asp:Panel ID="UserPanel" runat="server">
                <asp:Label ID="DOBLabel" runat="server" Text="Date of Birth"></asp:Label>
                &nbsp;<asp:TextBox ID="DOB" ReadOnly="true" runat="server"></asp:TextBox>
                &nbsp;&nbsp;<br /> <br />
                <asp:Label ID="GenderLabel" runat="server" Text="Gender"></asp:Label>
                &nbsp;<asp:TextBox ID="Gender" ReadOnly="true" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="TelephoneLabel" runat="server" Text="Telephone"></asp:Label>
                <asp:TextBox ID="Telephone" ReadOnly="true" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="StateLabel" runat="server" Text="State"></asp:Label>
                &nbsp;<asp:TextBox ID="State" runat="server" ReadOnly="true"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:Label ID="CityLabel" runat="server" Text="City"></asp:Label>
                &nbsp;<asp:TextBox ID="City" runat="server" ReadOnly="true"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="StreetLabel" runat="server" Text="Street" Style="vertical-align: top;"></asp:Label>
                <asp:TextBox ID="Street" TextMode="MultiLine" ReadOnly="true" runat="server"></asp:TextBox>
            </asp:Panel>
            <br />
            <br />
            <asp:Button ID="RetBut" class="button" runat="server" Text="Return" OnClick="RetBut_Click"/>
            &nbsp;<br />
            &nbsp;<asp:Label ID="Comment" class="warning" runat="server" Text="" Visible="false"></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
