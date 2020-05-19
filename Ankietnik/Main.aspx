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
                <div>
                    <asp:Label ID="HeaderWypelnijBrak" runat="server" Text="<h4>Nie masz ankiet do wypełnienia.</h4>"></asp:Label>
                    <asp:Label ID="HeaderWypelnij" runat="server" Text="<h4>Wypełnij ankiety:</h4>"></asp:Label>
                    <asp:DropDownList 
                        ID="ListWypelnij" 
                        CssClass="btn btn-secondary"
                        runat="server" 
                        OnSelectedIndexChanged="ListWypelnij_SelectedIndexChanged" 
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Button ID="ButtonWypelnij" Text="Wypełnij" runat="server" OnClick="ButtonWypelnij_Click" CssClass="btn btn-primary"/>  
                </div>
                <br />
                <div>
                    <asp:Label ID="HeaderSprawdźBrak" runat="server" Text="Po wypełnieniu ankiet tutaj sprawdzisz czy otrzymaliśmy Twoje odpowiedzi."></asp:Label>    
                    <asp:Label ID="HeaderSprawdź" runat="server" Text="<h4>Sprawdź wypełnione:</h4>"></asp:Label>            
                    <div class="form-group">
                        <asp:DropDownList 
                            ID="ListSprawdz" 
                            CssClass="btn btn-secondary"
                            runat="server"
                            OnSelectedIndexChanged="ListSprawdz_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>       
                        <asp:Label ID="Label2" runat="server" Text="Hasło: "></asp:Label>
                        <asp:TextBox ID="PasswordTextBox" CssClass="form-control" runat="server" TextMode="Password" ValidateRequestMode="Enabled"></asp:TextBox>
                        <asp:Button ID="ButtonSprawdz" Text="Sprawdź" runat="server" OnClick="ButtonSprawdz_Click" CssClass="btn btn-primary"/>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-2"></div>
    </div>
</asp:Content>
