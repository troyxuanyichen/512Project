<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main Page</title>
    <link href="~/css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="avatar.jpg" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="ControlPanel">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT UsrId, UsrName, Password, Email FROM [User] WHERE (UsrId = @uid)" DeleteCommand="DELETE FROM [User] WHERE (UsrId = @val2)">
                <DeleteParameters>
                    <asp:ControlParameter ControlID="HiddenField1" Name="val2" PropertyName="Value" />
                </DeleteParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="HiddenField1" Name="uid" PropertyName="Value" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Profile]" DeleteCommand="DELETE FROM [Profile] WHERE (UsrId = @val1)">
                <DeleteParameters>
                    <asp:ControlParameter ControlID="HiddenField1" Name="val1" PropertyName="Value" />
                </DeleteParameters>
            </asp:SqlDataSource>
            <br />
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:Panel ID="VisitorPanel" runat="server">
                <asp:Button ID="SigninBut" runat="server" Text="Sign in" OnClick="SigninBut_Click" />
                <asp:Button ID="CrtAccBut" runat="server" Text="Create Account" OnClick="CrtAccBut_Click" />
            </asp:Panel>
            <asp:Panel ID="UserPanel" runat="server">
                <asp:Button ID="EditAccountBut" runat="server" Text="Edit Account" OnClick="EditAccountBut_Click" />
                <asp:Button ID="CrtProBut" runat="server" Visible="false" Text="Create Profile" OnClick="CrtProBut_Click" />
                <asp:Button ID="ViewProfileBut" runat="server" Text="View My Profile" OnClick="ViewProfileBut_Click" />
                <asp:Button ID="EditProfileBut" runat="server" Visible="false" Text="Edit Profile" OnClick="EditProfileBut_Click" />
                <asp:Button ID="DltProfileBut" runat="server" Text="Delete My Profile" OnClick="DltProfileBut_Click" AutoPostBack="true" />
                <asp:Button ID="SignoutBut" runat="server" Text="Sign out" OnClick="SignoutBut_Click" />
                <asp:Button ID="DltAccBut" runat="server" Text="Delete Account" OnClick="DltAccBut_Click" AutoPostBack="true" />
                <asp:Button ID="ViewOtherProfileBut" runat="server" Text="View other Profile" OnClick="ViewOtherProfileBut_Click" />
                <asp:DropDownList ID="UsrNameList" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="Email">
                </asp:DropDownList>
            </asp:Panel>
            <br />
            <asp:Label ID="Comment" runat="server" class="warning" Text="" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
