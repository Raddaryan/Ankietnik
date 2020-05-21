<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MainOwner.aspx.cs" Inherits="Ankietnik.MainOwner" %>
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
                    <asp:Label ID="HeaderSprawdźBrak" runat="server" Text="Nie otrzymaliśmy odpowiedzi na żadną z Twoich ankiet."></asp:Label>    
                    <asp:Label ID="HeaderSprawdź" runat="server" Text="<h4>Sprawdź ile odpowiedzi otrzymaliśmy:</h4>"></asp:Label>            
                    <div class="form-inline">
                        <div class="form-group mr-2">
                            <asp:DropDownList 
                                ID="ListSprawdz" 
                                CssClass="btn btn-secondary"
                                runat="server"
                                AutoPostBack="true">
                            </asp:DropDownList>  
                        </div>
                        <asp:Button ID="ButtonSprawdz" Text="Sprawdź" runat="server" OnClick="ButtonSprawdz_Click" CssClass="btn btn-primary"/>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-2"></div>
    </div>
</asp:Content>
