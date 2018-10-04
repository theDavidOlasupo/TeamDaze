<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"  CodeBehind="InecReg.aspx.cs" Inherits="TeamDaze.Web.Pages.InecReg" %>

 
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <h2 style="color: white">Touch 'N' Pay</h2>

 <div class="form-group">
        <asp:Label runat="server" Text="Enter Your BVN:" ForeColor="White" Font-Bold="true" ID="Label4"> </asp:Label>
        <asp:TextBox runat="server" autocomplete="off" CssClass="dateT form-control " placeholder="Enter BVN/PhoneNUmber Here" ID="txtBvn" MaxLength="12"></asp:TextBox>
        <em style="color: white">**Dial *565*0# to get your BVN number </em>
       
     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required Field*" ControlToValidate="txtBvn" BackColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="^\d+$" ControlToValidate="txtBvn" BackColor="Red" ErrorMessage="Enter a valid number sequence"></asp:RegularExpressionValidator>
        <em style="color: white">**Charges apply</em>
     <%--<button runat="server" id="btnSubmit" class="btn btn-warning" onclick="btn_Click" title="submit"> Submt form</button>--%>
        <%--<asp:Button ID="btnCapture" runat="server" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" CssClass="btn btn-warning" Text="Capture FingerPrint & Pay" Visible="true" />--%>

    </div>

   <asp:radioButtonList ID="radio1" runat="server" TextAlign="Right"  RepeatLayout="Flow" RepeatDirection="Horizontal">
</asp:radioButtonList>
  
     <asp:Button ID="Button1"  class="btn btn-warning" runat="server"  Height="45px" OnClick="Button1_Click" Text="Button" Width="157px" />
  
     <asp:Label ID="lblGotBVn" runat="server" Text="Details gotten" BackColor="Red" Visible="false"></asp:Label>
    <hr />
    <asp:Image ID="bvnImage" Height="180px" Width="180px" runat="server" />


  <div id="Alertdiv" runat="server" visible="false" class="alert alert-success alert-dismissable">
        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
        <strong>Message:</strong> Read the response below!
    </div>

</asp:content>