<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="CreateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Account</title>
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="avatar.jpg" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-panel">
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [User].* FROM [User]" InsertCommand="INSERT INTO [User] (FirstName, MiddleName, LastName, Password, Email) VALUES (@val1, @val2, @val3, @val4, @val5)">
                <InsertParameters>
                    <asp:ControlParameter ControlID="FirstNameInput" Name="val1" PropertyName="Text" />
                    <asp:ControlParameter ControlID="MiddleNameInput" Name="val2" PropertyName="Text" />
                    <asp:ControlParameter ControlID="LastNameInput" Name="val3" PropertyName="Text" />
                    <asp:ControlParameter ControlID="HashPass" Name="val4" PropertyName="Value" />
                    <asp:ControlParameter ControlID="EmailInput" Name="val5" PropertyName="Text" />
                </InsertParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [User].* FROM [User] WHERE (Email = @val4)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="EmailInput" Name="val4" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:HiddenField ID="HashPass" runat="server" />
            <br />

            <br />
            <br />
            <asp:Label ID="FirstNameLabel" runat="server" Text="First Name"></asp:Label>
            <asp:TextBox ID="FirstNameInput" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="MiddleNameLabel" runat="server" Text="Middle Name"></asp:Label>
            &nbsp;<asp:TextBox ID="MiddleNameInput" runat="server"></asp:TextBox>
            (Can be empty)<br />
            <br />
            <asp:Label ID="LastNameLabel" runat="server" Text="Last Name"></asp:Label>
            &nbsp;<asp:TextBox ID="LastNameInput" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="PswLabel" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="PswInput" runat="server" TextMode="Password"></asp:TextBox>
            &nbsp;(At least 6 codewords)
            <br />
            <br />
            <asp:Label ID="EmailLabel" runat="server" Text="E-mail"></asp:Label>
            <asp:TextBox ID="EmailInput" runat="server" Width="128px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="SavBut" class="button" runat="server" Text="Save" OnClick="SavBut_Click" />

            &nbsp;<br />
            <br />
            <asp:Button ID="BackBut" class="button" runat="server" Text="Back" OnClick="BackBut_Click" />

            <br />
            <asp:Label ID="Comment" class="warning" runat="server" Text="" Visible="false"></asp:Label>

        </div>
    </form>
</body>
</html>
