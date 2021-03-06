﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckOwner.aspx.cs" Inherits="Ankietnik.CheckOwner" %>
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
            <asp:Label ID="HeaderNieMaOdp" runat="server" Text="Niestety nie otrzymalismy jeszcze żadnych odpowiedzi na tę ankietę.<br />"></asp:Label>
            <asp:Label ID="HeaderLiczbaOdpOwner" runat="server" Text="<h4>Liczba wypełnionych ankiet:</h4>"></asp:Label>
                <asp:Label ID="lblLiczbaWypelnionych" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="HeaderWynikiOwner" runat="server" Text="<br /><h4>Wyniki Ankiet:</h4>"></asp:Label>  
                <asp:Repeater ID="rpt" runat="server">
                     <ItemTemplate>
                         <p>
                             <asp:HiddenField ID="hiddenId" runat="server" Value='<%# Eval("QuestionId") %>' />
                             <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Content") %>' />
                             <br />
                             <asp:Label ID="lblYes" CssClass="ml-5 font-italic" runat="server" Text="Liczba głosów: " />
                             <asp:Label ID="Label1" CssClass="ml-5 font-italic" runat="server" Text='<%# Eval("Result") %>' />
                         </p>
                     </ItemTemplate>
                 </asp:Repeater>
            <br />
                <asp:Label ID="HeaderKtoNieWypelnil" runat="server" Text="<h4>Lista oczekujących na wypełnienie:</h4>"></asp:Label> 
                <asp:Label ID="KtoNieWypelnil" runat="server" Text=""></asp:Label> 
                <asp:Label ID="HeaderWszyscyWypelnili" runat="server" Text="Wszystkie osoby wypełniły swoje ankiety."></asp:Label> 
            </div>
        </div>
        <div class="col-md-2"></div>
    </div>
</asp:Content>
