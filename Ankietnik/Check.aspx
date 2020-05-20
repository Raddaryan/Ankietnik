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
            <h4>Twoje odpowiedzi:</h4>
                <asp:Repeater ID="rpt" runat="server">
                     <ItemTemplate>
                         <p>
                             <asp:HiddenField ID="hiddenId" runat="server" Value='<%# Eval("Id") %>' />
                             <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Content") %>' />
                             <asp:Label ID="lblAnswer" CssClass="ml-5" runat="server" Text=<em>'<%# Eval("Content") %>'</em> />
                         </p>
                     </ItemTemplate>
                 </asp:Repeater>
                <asp:Button ID="GoBack" CssClass="btn btn-primary" Text="Wróć" runat="server" OnClick="GoBack_Clik" />
            </div>
        </div>
        <div class="col-md-2"></div>
    </div>
</asp:Content>
