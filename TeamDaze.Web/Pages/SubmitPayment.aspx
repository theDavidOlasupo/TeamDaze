<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubmitPayment.aspx.cs" Inherits="TeamDaze.Web.Pages.SubmitPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
</asp:Content>

    <h2 style="color: white">Touch 'N' Pay</h2>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="form-group">
        <asp:Label runat="server" Text="Amount To Pay:" ForeColor="White" Font-Bold="true" ID="Label4"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " placeholder="Enter Amount Here" ID="txtAmount" MaxLength="6"></asp:TextBox>
        <asp:Label runat="server" Text="BVN/Phone-Number:" ForeColor="White" Font-Bold="true" ID="Label1"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " placeholder="Enter BVN/PhoneNUmber here" ID="txtBvn" MaxLength="12"></asp:TextBox>
        <em style="color: white">**Dial *565*0# to get your BVN number </em>
       
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field*" ControlToValidate="txtAmount" BackColor="Red"></asp:RequiredFieldValidator>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required Field*" ControlToValidate="txtBvn" BackColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="^\d+$" ControlToValidate="txtBvn" BackColor="Red" ErrorMessage="Enter a valid number sequence"></asp:RegularExpressionValidator>
        <em style="color: white">**Charges apply</em>

        <asp:Button ID="btnCapture" runat="server" OnClientClick="this.disabled=true;" Onclick="btn_Click" UseSubmitBehavior="false" CssClass="btn btn-warning" Text="Capture FingerPrint & Pay" Visible="true" />

    </div>

  
  <div id="Alertdiv" runat="server" visible="false" class="alert alert-success alert-dismissable">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Message:</strong> Read the response below!
    </div>

</asp:Content>
