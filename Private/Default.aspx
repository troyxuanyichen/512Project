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

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Profile]" DeleteCommand="DELETE FROM Profile WHERE (UsrId = @val1)">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [User]" DeleteCommand="DELETE FROM [User] WHERE (UsrId = @val1)" >
            </asp:SqlDataSource>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <br />
            <br />
            <asp:Button ID="SignoutBut" runat="server" Text="Sign out" Visible="false" OnClick="SignoutBut_Click" />
            <asp:Button ID="EditAccountBut" runat="server" Text="Edit Account" OnClick="EditAccountBut_Click" />
            <asp:Button ID="DltAccBut" runat="server" Text="Delete Account" Visible="false" OnClick="DltAccBut_Click" />
            <asp:Button ID="EditProfilBut" runat="server" Text="Edit Profile" OnClick="EditProfileBut_Click" />
            <br />
            <br />
            <asp:DropDownList ID="UsrNameList" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="UsrName" DataValueField="UsrId" Visible="false">
            </asp:DropDownList>

            <br />
            <br />

            <asp:Button ID="CrtProBut" CssClass="button" runat="server" Text="Create Profile" OnClick="CrtProBut_Click" Visible="false" />

            <br />
            <br />
            <asp:Label ID="Comment" runat="server" class="warning" Text="Hi" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
