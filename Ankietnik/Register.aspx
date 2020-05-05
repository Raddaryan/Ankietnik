<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Ankietnik.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Register</title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
</head>
<body>
    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-4 py-3">
            <h2>Ankietnik</h2>
                <br />
            <div class="container px-3 py-3 border" style="background-color:whitesmoke;">
                <form id="form1" runat="server">
                    <div>
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Email: "></asp:Label>
                            <asp:TextBox ID="NewUsernameTextBox" CssClass="form-control" runat="server" TextMode="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" id="reqNewUsername" controltovalidate="NewUsernameTextBox" errormessage="Wpisz swój adres email" forecolor="Red" Font-Size="10" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="Hasło: "></asp:Label>
                            <asp:TextBox ID="NewPasswordTextBox" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" id="reqNewPassword" controltovalidate="NewPasswordTextBox" errormessage="Wpisz hasło" forecolor="Red" Font-Size="10" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label4" runat="server" Text="Powtórz hasło: "></asp:Label>
                            <asp:TextBox ID="RetypedTextBox" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" id="reqRetyped" controltovalidate="RetypedTextBox" errormessage="Powtórz hasło" forecolor="Red" Font-Size="10" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label5" runat="server" Text="Grupa: "></asp:Label>
                            <asp:TextBox ID="GroupTextBox" CssClass="form-control" runat="server" TextMode="Search"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" id="reqGroup" controltovalidate="GroupTextBox" errormessage="Wybierz grupę" forecolor="Red" Font-Size="10" />
                        </div>
                        <asp:Button ID="RegisterButton" CssClass="btn btn-primary btn-block" runat="server" Text="Zarejestruj" OnClick="RegisterButton_Click" />
                        <br />
                        <p class="text-center">
                            <asp:Label ID="Label3" runat="server" Text="Masz już konto? "></asp:Label>
                            <asp:HyperLink ID="Login" NavigateUrl="Login.aspx" runat="server">Zaloguj się!</asp:HyperLink>
                        </p>
                    </div>
                </form>
            </div>
        </div>
        <div class="col-md-4"></div>
    </div>
</body>
</html>
