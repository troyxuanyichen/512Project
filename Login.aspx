<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="SmallPanel">

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT UsrName, Password, Email, UsrId FROM [User] WHERE (Email = @val1)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="EmailBox" Name="val1" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />

            <asp:Label ID="EmailLabel" runat="server" Text="E-mail"></asp:Label>&nbsp
            <asp:TextBox ID="EmailBox" runat="server"></asp:TextBox>

            <br />
            <br />
            <asp:Label ID="PswLabel" runat="server" Text="Pass Word"></asp:Label>&nbsp
            <asp:TextBox ID="PswBox" TextMode="Password" runat="server"></asp:TextBox>
            <asp:CheckBox ID="DisplayPsw" runat="server" OnCheckedChanged="DisplayPsw_CheckedChanged" Text="Display password" />
            <br />
            <br />
            <asp:Button ID="LoginBut" CssClass="button" runat="server" Text="Login" OnClick="LoginBut_Click" />
            <br />
            <br />
            <br />

            <asp:Button ID="RetBut" CssClass="button" runat="server" Text="Return" OnClick="RetBut_Click" />

            <br />
            <br />
            <asp:Label ID="Comment" runat="server" Text="" Visible="false"></asp:Label>

        </div>
    </form>
</body>
</html>
