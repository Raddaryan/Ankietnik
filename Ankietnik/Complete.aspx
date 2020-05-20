<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Complete.aspx.cs" Inherits="Ankietnik.Complete" %>
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
            <h4>Wypełnij ankietę:</h4>
                <asp:Repeater ID="rpt" runat="server">
                     <ItemTemplate>
                         <p>
                             <asp:HiddenField ID="hiddenId" runat="server" Value='<%# Eval("Id") %>' />
                             <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Content") %>' />
                                <asp:RadioButtonList ID="YesNo" runat="server" AutoPostBack="true">
                                    <asp:ListItem Text="Tak" Value="1" />
                                    <asp:ListItem Text="Nie" Value="0" />
                                </asp:RadioButtonList>
                             <asp:RequiredFieldValidator ID="reqAnswer" runat="server" ControlToValidate="YesNo" errormessage="Wybierz odpowiedź." forecolor="Red" Font-Size="10"></asp:RequiredFieldValidator>
                         </p>
                     </ItemTemplate>
                 </asp:Repeater>
                <div class="form-inline">
                    <div class="form-group mr-2">
                        <asp:Label ID="Label2" runat="server" Text="Hasło: "></asp:Label>
                    </div>
                    <div class="form-group mr-2">
                        <asp:TextBox ID="PasswordTextBox" CssClass="form-control" runat="server" TextMode="Password" ValidateRequestMode="Enabled"></asp:TextBox>
                    </div>
                    <div class="form-group mr-2">
                        <asp:Button ID="ButtonWyslijOdp" CssClass="btn btn-primary" Text="Wyślij" runat="server" OnClick="ButtonWyslijOdp_Clik" />
                    </div>
                </div>
                <asp:RequiredFieldValidator runat="server" id="reqPassword" controltovalidate="PasswordTextBox" errormessage="Wpisz swoje hasło" forecolor="Red" Font-Size="10" />
            </div>
        </div>
        <div class="col-md-2"></div>
    </div>
</asp:Content>
