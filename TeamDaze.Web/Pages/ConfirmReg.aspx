<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="ConfirmReg.aspx.cs" Inherits="TeamDaze.Web.Pages.ConfirmReg" %>


 
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <h2 style="color: white">INEC Demo Registraton</h2>
      <div id="Alertdiv" runat="server" visible="false" class="alert alert-success alert-dismissable">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Message:</strong> Read the response below!
    </div>
 <div class="form-group" >
     <div class="col-md-4">
        <asp:Label runat="server" Text="BVN:" ForeColor="White" Font-Bold="true" ID="Label4"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " Enabled="false" ID="txtBvn" MaxLength="12"></asp:TextBox>
         
  
      <asp:Label runat="server" Text="Registration Date:" ForeColor="White" Font-Bold="true" ID="Label1"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " Enabled="false" ID="TextRegistrationDate" MaxLength="12"></asp:TextBox>

     <asp:Label runat="server" Text="First Name:" ForeColor="White" Font-Bold="true" ID="Label2"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " Enabled="false" ID="TextFirstName" MaxLength="12"></asp:TextBox>

     <asp:Label runat="server" Text="Last Name:" ForeColor="White" Font-Bold="true" ID="Label3"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " Enabled="false" ID="TextLastName" MaxLength="12"></asp:TextBox>


     
     <asp:Label runat="server" Text="Date Of Birth:" ForeColor="White" Font-Bold="true" ID="Label5"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " Enabled="false" ID="TextDateOfBirth" MaxLength="12"></asp:TextBox>

      
     <asp:Label runat="server" Text="Lga Of Origin:" ForeColor="White" Font-Bold="true" ID="Label12"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " Enabled="false" ID="TextLgaOfOrigin" MaxLength="12"></asp:TextBox>

      <asp:Label runat="server" Text="Lga Of Residence:" ForeColor="White" Font-Bold="true" ID="Label13"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " Enabled="false" ID="TextLgaOfResidence" MaxLength="12"></asp:TextBox>

           </div>
     <div class="col-md-4">

     <asp:Label runat="server" Text="Gender:" ForeColor="White" Font-Bold="true" ID="Label6"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " Enabled="false" ID="TextGender" MaxLength="12"></asp:TextBox>

     
     <asp:Label runat="server" Text="Email:" ForeColor="White" Font-Bold="true" ID="Label7"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " Enabled="false" ID="TextEmail" MaxLength="12"></asp:TextBox>

       
     <asp:Label runat="server" Text="Residential Address:" ForeColor="White" Font-Bold="true" ID="Label8"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " Enabled="false" ID="TextResidentialAddress" MaxLength="12"></asp:TextBox>

  
     <asp:Label runat="server" Text="State Of Origin:" ForeColor="White" Font-Bold="true" ID="Label9"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " Enabled="false" ID="TextStateOfOrigin" MaxLength="12"></asp:TextBox>

  
     <asp:Label runat="server" Text="State Of Residence:" ForeColor="White" Font-Bold="true" ID="Label10"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " Enabled="false" ID="TextStateOfResidence" MaxLength="12"></asp:TextBox>

     
     <asp:Label runat="server" Text="Base64Image:" ForeColor="White" Font-Bold="true" ID="Label11"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " Enabled="false" ID="TextBase64Image" MaxLength="12"></asp:TextBox>

     <asp:Label runat="server" Text="OTP:" ForeColor="White" Font-Bold="true" ID="lblOTP"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control" placeholder="Enter The OTP Sent To your Mail" Enabled="true" ID="TextOTP" MaxLength="12"></asp:TextBox>
           </div>

      <div class="col-md-4">

    <img id="imagebtn" runat="server" style="border-radius:15px;" />
           </div>

     <asp:Button ID="btnConfirmReg" OnClick="btnConfirmReg_Click" CssClass="btn btn-danger col-lg-push-8" BorderStyle="Outset" runat="server" Text="Button" />
    </div>
<%--    <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>--%>

  <%-- <asp:radioButtonList ID="radio1" runat="server" TextAlign="Right"  RepeatLayout="Flow" RepeatDirection="Horizontal">
</asp:radioButtonList>
  --%>

  
     
    <hr />
    <asp:Image ID="bvnImage" Height="180px" Width="180px" runat="server" />




</asp:content>