<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TeamDaze.Web.Index" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/loginstyle.css" rel="stylesheet" />
   <div class="form-horizontal">
       
           
                <div class="alert alert-danger alert-dismissible" id="ErrorDiv" role="alert" runat="server" visible="false">
                   
                </div>
           
        <div class="container" style="margin-top:50px; margin-bottom:50px;">
            <div style="width:50px; margin:0 auto;">
                <p> <img src="assets/images/300sterling logo flat bnr.fw.png" alt="logo" /></p>
            </div>
            <div class="login">
                <h1>Login to Team Daze - Finger Print Payment System</h1>
                <div class="form-group">
                  <label class = "control-label col-md-2">Username</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required" InitialValue="" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                  <label class = "control-label col-md-2">Password</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" InitialValue="" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group" style="margin-top:10px;">
                    <div class="col-md-offset-2 col-md-10">
                       <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-default" OnClick="btnLogin_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
