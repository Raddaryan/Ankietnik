﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Ankietnik.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContntentPlaceHolder1" runat="server">
        <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 py-3">
            <div class="MessagePanelDiv">
                <asp:Panel ID="Message" runat="server" Visible="False">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <asp:Label ID="labelMessage" runat="server" />
                </asp:Panel>
            </div>
            <div class="container px-3 py-3 border" style="background-color:whitesmoke;">
            <h4>Wypełnij ankiety:</h4>
                <asp:ListBox ID="ListBoxWypelnij" runat="server" OnSelectedIndexChanged="ListBoxWypelnij_SelectedIndexChanged"></asp:ListBox>
                <asp:Button ID="ButtonWypelnij" Text="Wypełnij" runat="server" OnClick="ButtonWypelnij_Click" />
            <h4>Sprawdź wypełnione:</h4>
                <asp:ListBox ID="ListBoxSprawdz" runat="server"></asp:ListBox><a href="Register.aspx.cs">Register.aspx.cs</a>
            </div>
        </div>
        <div class="col-md-2"></div>
    </div>
</asp:Content>
