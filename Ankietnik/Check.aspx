<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Check.aspx.cs" Inherits="Ankietnik.Check" %>
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
                    <div class="form-inline">
                        <div class="form-group mr-2">
                            <asp:Label ID="Label2" runat="server" Text="Hasło: "></asp:Label>
                        </div>
                        <div class="form-group mr-2">
                            <asp:TextBox ID="PasswordTextBox" CssClass="form-control" runat="server" TextMode="Password" ValidateRequestMode="Enabled"></asp:TextBox>
                        </div>
                        <div class="form-group mr-2">
                            <asp:Button ID="ButtonSprawdz" Text="Sprawdź" runat="server" OnClick="ButtonSprawdz_Click" CssClass="btn btn-primary"/>
                        </div>
                    </div>
                    <asp:RequiredFieldValidator runat="server" id="reqPassword" controltovalidate="PasswordTextBox" errormessage="Wpisz swoje hasło" forecolor="Red" Font-Size="10" />
                    <br />
            <asp:Label ID="HeaderTuajOdpowiedzi" runat="server" Text="Po wpisaniu hasła tutaj sprawdzisz czy otrzymaliśmy Twoje odpowiedzi."></asp:Label>
            <asp:Label ID="HeaderOdpowiedzi" runat="server" Text="<h4>Twoje odpowiedzi:</h4>"></asp:Label>   
                <asp:Repeater ID="rpt" runat="server">
                     <ItemTemplate>
                         <p>
                             <asp:HiddenField ID="hiddenId" runat="server" Value='<%# Eval("QuestionId") %>' />
                             <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Content") %>' />
                             <asp:Label ID="lblAnswer" CssClass="ml-5" runat="server" Text='<%# Eval("Response") %>' />
                         </p>
                     </ItemTemplate>
                 </asp:Repeater>
            </div>
        </div>
        <div class="col-md-2"></div>
    </div>
</asp:Content>
