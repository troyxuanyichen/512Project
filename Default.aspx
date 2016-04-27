<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main Page</title>
    <link href="/css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="avatar.jpg" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-panel">
            <div class="header">
                <h3>Welcome to Your Account Manager by Troy
                </h3>
            </div>
            <div class="control-panel">
                <asp:Panel ID="VisitorPanel" runat="server">
                    <asp:Button ID="SigninBut" class="button" runat="server" Text="Sign in" OnClick="SigninBut_Click" />
                    <asp:Button ID="CrtAccBut" class="button" runat="server" Text="Create Account" OnClick="CrtAccBut_Click" />
                </asp:Panel>
                <asp:Panel ID="UserPanel" runat="server">
                    <asp:Button ID="EditAccountBut" class="button" runat="server" Text="Edit Account" OnClick="EditAccountBut_Click" />
                    <asp:Button ID="CrtProBut" class="button" runat="server" Visible="false" Text="Create Profile" OnClick="CrtProBut_Click" />
                    <asp:Button ID="ViewProfileBut" class="button" runat="server" Text="View My Profile" OnClick="ViewProfileBut_Click" />
                    <asp:Button ID="EditProfileBut" class="button" runat="server" Visible="false" Text="Edit Profile" OnClick="EditProfileBut_Click" /><br />
                    <asp:Button ID="DltProfileBut" class="button" runat="server" Text="Delete Profile" OnClick="DltProfileBut_Click" AutoPostBack="true" />
                    <asp:Button ID="DltAccBut" class="button" runat="server" Text="Delete Account" OnClick="DltAccBut_Click" AutoPostBack="true" />
                    <br />
                    <br />
                    <asp:Button ID="SignoutBut" class="button" runat="server" Text="Sign out" OnClick="SignoutBut_Click" />
                </asp:Panel>
            </div>

            <div id="nameList" class="small-panel" runat="server">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Comment" class="warning" runat="server" Text="" Visible="false"></asp:Label><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="UsrNameList" CssClass="dropdownlist" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="Email">
                </asp:DropDownList>            
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                <br />
                <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="ViewOtherProfileBut" class="button" runat="server" Text="View other Profile" OnClick="ViewOtherProfileBut_Click" />
            </div>

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
            <asp:HiddenField ID="HiddenField1" runat="server" />
        </div>
    </form>
</body>
</html>
