﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Ankietnik.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login</title>
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
            <div class="MessagePanelDiv">
                <asp:Panel ID="Message" runat="server" Visible="False">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <asp:Label ID="labelMessage" runat="server" />
                </asp:Panel>
            </div>
            <div class="container px-3 py-3 border" style="background-color:whitesmoke;">
                <form id="form1" runat="server">
                    <div>
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Email: "></asp:Label>
                            <asp:TextBox ID="UsernameTextBox" CssClass="form-control" runat="server" TextMode="Email" ValidateRequestMode="Enabled"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" id="reqUsername" controltovalidate="UsernameTextBox" errormessage="Wpisz swój adres email" forecolor="Red" Font-Size="10" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="Hasło: "></asp:Label>
                            <asp:TextBox ID="PasswordTextBox" CssClass="form-control" runat="server" TextMode="Password" ValidateRequestMode="Enabled"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" id="reqPassword" controltovalidate="PasswordTextBox" errormessage="Wpisz swoje hasło" forecolor="Red" Font-Size="10" />
                        </div>
                        <asp:Button ID="LogInButton" CssClass="btn btn-primary btn-block" runat="server" Text="Zaloguj" OnClick="LogInButton_Click" ValidateRequestMode="Enabled" />
                        <br />
                        <p class="text-center">
                            <asp:Label ID="Label3" runat="server" Text="Nie masz konta? "></asp:Label>
                            <asp:HyperLink ID="RegisterLink" NavigateUrl="Register.aspx" runat="server">Zarejestruj się!</asp:HyperLink>
                        </p>
                    </div>
                </form>
            </div>
        </div>
        <div class="col-md-4"></div>
    </div>
</body>
</html>
