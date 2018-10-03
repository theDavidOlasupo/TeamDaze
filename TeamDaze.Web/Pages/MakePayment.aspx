<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MakePayment.aspx.cs" Inherits="TeamDaze.Web.Views.MakePayment" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .col-md-6:hover {
            opacity: 1.0;
            filter: alpha(opacity=30);
        }
    </style>
    <h1 style="color: white">
        <asp:Literal runat="server" ID="WelcomeText" /></h1>
    <div>
        <div class="jumbotron" style="background-color:#3e152b; text-decoration-color: #ffffff">
            <h1 style="color: white">Bio Bank</h1>
<b><h4 class="well" style="text-align: center; padding: 10px;border-radius:15px;color:#3e152b" position: center">Make Transactions Using FingerPrint..No More Cards and Passwords...</h4></b>
            <%--<b><h4 class="well" style="text-align: center;  padding: 10px;border-radius:15px;color:#3e152b" position: center">No More Cards and Passwords...</h4></b>--%>
        </div>
    </div>
    <div class="container" style="height: inherit">
        <div class="col-md-6">
            <h2 class="hTag" style="background-color: #3e152b; text-decoration-color: #ffffff; text-align: center; padding: 10px; border-radius: 15px 15px 15px 15px; border-top: 2px; color: #FFFFFF;">Steps To Carry Out Transactions</h2>
            <h3 style="text-align: center; font-family: Arial; color: white">Steps</h3>
            <p style="text-align: center; color: white">Enter Amount to be paid</p>
            <p style="text-align: center; color: white">Input Finger Print & BVN to Pay</p>
            <p style="text-align: center; color: white">Get E-Receipt</p>
            <div style="position: center;">
                <%--<asp:button type="button" runat="server" Text="Click To Make Payments" BorderStyle="Outset" id="btnReg" class="btn btn-primary btn-block"></asp:button></div>--%>
                <asp:Button runat="server" Text="Click To Make Payments" BorderStyle="Outset" CssClass="col-md-6" OnClick="Unnamed_Click" />
            </div>
        </div>
    </div>
</asp:Content>
