<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="CreateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Account</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [User].* FROM [User]" InsertCommand="INSERT INTO [User] (UsrName, Password, Email) VALUES (@val1, @val2, @val3)">
                <InsertParameters>
                    <asp:ControlParameter ControlID="UsrNameInput" DefaultValue="" Name="val1" PropertyName="Text" />
                    <asp:ControlParameter ControlID="HashPass" Name="val2" PropertyName="Value" />
                    <asp:ControlParameter ControlID="EmailInput" Name="val3" PropertyName="Text" />
                </InsertParameters>
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [User] WHERE (Email = @val4)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="EmailInput" Name="val4" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>

            <asp:HiddenField ID="HashPass" runat="server" />
            <br />

            <br />
            <br />
            <asp:Label ID="UsrNameLabel" runat="server" Text="User Name"></asp:Label>
            <asp:TextBox ID="UsrNameInput" runat="server" Width="128px"></asp:TextBox>
            <br />
            <asp:Label ID="PswLabel" runat="server" Text="Pass word"></asp:Label>
            <asp:TextBox ID="PswInput" runat="server" TextMode="Password" Width="128px"></asp:TextBox>
            <br />
            <asp:Label ID="EmailLabel" runat="server" Text="E-mail"></asp:Label>
            <asp:TextBox ID="EmailInput" runat="server" Width="128px"></asp:TextBox>
            <br />
            <asp:Button ID="SavBut" CssClass="button" runat="server" Text="Save" OnClick="SavBut_Click" />

            <br />
            <asp:Label ID="Comment" runat="server" Text="" Visible="false"></asp:Label>

        </div>
    </form>
</body>
</html>
