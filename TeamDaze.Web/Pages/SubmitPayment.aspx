﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubmitPayment.aspx.cs" Inherits="TeamDaze.Web.Pages.SubmitPayment" %>
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Make Payment</title>
    <script src="../Scripts/jquery-3.3.1.js"></script>
<%--    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>
    <link href="../bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../main.css">
    <link href="https://fonts.googleapis.com/css?family=K2D:200,300,400,500,600,700,800" rel="stylesheet">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link href="../bootstrap-pincode-input.css" rel="stylesheet">
</head>

<body>
    <form id="main" runat="server">
        <main>
            <div class="container">
                <div class="header">
                    <h1 class="title">Make Payment</h1>
                </div>
            </div>
                <div id="Alertdiv" runat="server" visible="false" class="alert alert-success alert-dismissable">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong>Message:</strong> Read the response below!
            </div>


            <div class="modal text-center">
                <div id="accountenrollment">
                    <div class="m-auto">
                        <div class="text-center">
                            <div class="form-one-account" id="formoneaccount" runat="server">
                                <div class="row">
                                    
                                     <div class="col-md-12">
                                            <div class="form-group">
                                              <h1><b>Steps To Carry Out Transactions</b></h1> 
                                               
                                                    <h2 > <label class="form-label">Enter Amount to be paid</label></h2> 
                                                    <h2> <label class="form-label">Input Finger Print & BVN to Pay</label></h2> 
                                                    <h2> <label class="form-label">Get E-Receipt</label></h2> 
                                            </div>
                                        </div>

                                     <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="form-label">Amount</label>
                                                <asp:TextBox ID="txtAmount" runat="server" class="form-control">
                                                </asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter an amount to pay" ControlToValidate="txtAmount" BackColor="Red"></asp:RequiredFieldValidator>
   
                                            </div>
                                        </div>

                                    <div class="col-md-12" style="margin: auto;">
                                        <div class="form-group text-center">
                                            <asp:Button ID="Button1" class="btn btn-white btn--round" Text="Click To Make Payments" runat="server"
                                                OnClick="btnMakePayment_Click" CssClass="btn btn-primary"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                             <%--<div class="form-two-account" id="formtwoaccount" runat="server">
                                <div class="row">
                                    
                                     <div class="col-md-12">
                                            <div class="form-group">
                                             <img src="~/fingerprint.png" height="200" width="200" />
                                            </div>
                                        </div>

                                   

                                    <div class="col-md-12" style="margin: auto;">
                                        <div class="form-group text-center">
                                               <asp:Button ID="Button1" class="btn btn-white btn--round" Text="Click To Make Payments" runat="server"
                                                OnClick="btnMakePayment_Click" CssClass="btn btn-primary"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>


                        </div>
                    </div>
                </div>
            </div>


        </main>
    </form>
    <script type="text/javascript" src="../bootstrap-pincode-input.js"></script>
    <script>
        $('.otp').pincodeInput({
            // 6 input boxes = code of 6 digits long
            inputs: 6,

            // hide digits like password input             
            hidedigits: false,

            // keyDown callback             
            keydown: function (e) { },

            // callback on every input on change (keyup event)
            change: function (input, value, inputnumber) {
                // input = the input textbox DOM element
                // value = the value entered by user (or removed)
                // inputnumber = the position of the input box (in touch mode we only have 1 big input box, so always 1 is returned here)
            },

            // callback when all inputs are filled in (keyup event)
            complete: function (value, e, errorElement) {
                $('.otp-action').prop('disabled', false);
                // value = the entered code
                // e = last keyup event
                // errorElement = error span next to to this, fill with html 
                // e.g. : $(errorElement).html("Code not correct")
            }
        })
        $('.otp').pincodeInput();
    </script>
    <script>
        let $body = $(document.body)
        $('.jsModalOpen').click(function (e) {
            e.preventDefault()
            $body.addClass('modal-is-open')
        })

        $('.jsModalClose').click(function (e) {
            e.preventDefault()
            $body.removeClass('modal-is-open')
        })

        $('.form-two-account').hide()
        $('#next').click(function (e) {
            e.preventDefault()
            $('.form-two-account').show()
            $('.form-one-account').hide()
        })

        $('#accountNext').click(function (e) {
            e.preventDefault()
            $('.form-two-account').show()
            $('.form-one-account').hide()
        })
    </script>


</body>

</html>

