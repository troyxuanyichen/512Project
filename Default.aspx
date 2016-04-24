<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main Page</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="avatar.jpg" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="ControlPanel">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM Profile WHERE (UsrId = @val2)" SelectCommand="SELECT UsrId, UsrName, Password, Email FROM [User] WHERE (UsrId = @uid)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HiddenField1" Name="uid" PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [User]" DeleteCommand="DELETE FROM [User] WHERE (UsrId = @val3)"></asp:SqlDataSource>
            <br />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:Panel ID="VisitorPanel" runat="server">
                <asp:Button ID="SigninBut" runat="server" Text="Sign in" OnClick="SigninBut_Click" />
                <asp:Button ID="CrtAccBut" runat="server" Text="Create Account" OnClick="CrtAccBut_Click" />
            </asp:Panel>
            <asp:Panel ID="UserPanel" runat="server">
                <asp:Button ID="EditAccountBut" runat="server" Text="Edit Account" OnClick="EditAccountBut_Click" />
                <asp:Button ID="CrtProBut" runat="server" Visible="false" Text="Create Profile" OnClick="CrtProBut_Click" />
                <asp:Button ID="EditProfilBut" runat="server" Visible="false" Text="Edit Profile" OnClick="EditProfileBut_Click" />
                <asp:Button ID="SignoutBut" runat="server" Text="Sign out" OnClick="SignoutBut_Click" /><asp:Button ID="DltAccBut" runat="server" Text="Delete Account" Visible="false" OnClick="DltAccBut_Click" />
            </asp:Panel>

            <br />
            <asp:DropDownList ID="UsrNameList" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="UsrName" DataValueField="UsrId" Visible="false">
            </asp:DropDownList>
            <asp:Label ID="Comment" runat="server" class="warning" Text="Hi" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
