<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewProfile.aspx.cs" Inherits="ViewProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Profile</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="avatar.jpg" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT ProfileId, Email, UsrId, Name, DOB, Address, Telephone, Gender, Privacy FROM Profile WHERE (Email = @val1)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HiddenField1" Name="val1" PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <br />
            <asp:Panel ID="UserPanel" runat="server">
                <asp:Label ID="DOBLabel" runat="server" Text="Date of Birth"></asp:Label>
                &nbsp;<asp:TextBox ID="DOB" ReadOnly="true" runat="server"></asp:TextBox>
                &nbsp;&nbsp;<br />
                <asp:Label ID="GenderLabel" runat="server" Text="Gender"></asp:Label>
                &nbsp;<asp:TextBox ID="Gender" ReadOnly="true" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="TelephoneLabel" runat="server" Text="Telephone"></asp:Label>
                <asp:TextBox ID="Telephone" ReadOnly="true" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="AddressLabel" runat="server" Text="Address" Style="vertical-align: top;"></asp:Label>
                <asp:TextBox ID="Address" TextMode="MultiLine" ReadOnly="true" runat="server"></asp:TextBox>
            </asp:Panel>
            <br />
            <br />
            <asp:Button ID="RetBut" runat="server" Text="Return" OnClick="RetBut_Click" Style="height: 26px" />
            &nbsp;<br />
            &nbsp;<asp:Label ID="Comment" runat="server" Text="" Visible="false"></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
