<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Ankietnik.Main" %>

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
                <asp:DropDownList 
                    ID="ListWypelnij" 
                    CssClass="btn btn-default btn-sm"
                    runat="server" 
                    OnSelectedIndexChanged="ListWypelnij_SelectedIndexChanged" 
                    AutoPostBack="true">
                </asp:DropDownList>
                <asp:Button ID="ButtonWypelnij" Text="Wypełnij" runat="server" OnClick="ButtonWypelnij_Click" CssClass="btn btn-primary"/>
            <h4>Sprawdź wypełnione:</h4>
                <asp:DropDownList 
                    ID="ListSprawdz" 
                    CssClass="btn btn-default btn-sm"
                    runat="server"
                    OnSelectedIndexChanged="ListSprawdz_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
                 <asp:Button ID="ButtonSprawdz" Text="Sprawdź" runat="server" OnClick="ButtonSprawdz_Click" CssClass="btn btn-primary"/>
            </div>
        </div>
        <div class="col-md-2"></div>
    </div>
</asp:Content>
