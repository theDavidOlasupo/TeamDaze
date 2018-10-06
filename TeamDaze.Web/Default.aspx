﻿<%@ Page Title="Home Page" Language="C#"  AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TeamDaze.Web._Default" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title></title>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="./bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="main.css">
    <link href="https://fonts.googleapis.com/css?family=K2D:200,300,400,500,600,700,800" rel="stylesheet">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link href="./bootstrap-pincode-input.css" rel="stylesheet">

</head>

<body>
    <main>
        <div class="container">
            <div class="header">
                <h1 class="title">Payment Platform</h1>
            </div>

            <section class="stats">
                <section class="main-stat stat-bottom">
                    <a href="#">
                        <div class="main-circle">
                            <div class="info-lg">
                                <div class="stat-info">
                                    Accept
                                    <br>Payments
                                </div>
                            </div>
                        </div>
                    </a>
                </section>


                <section class="main-stat">
                    <a href="~/Pages/TransactionsHistory.aspx" runat="server">
                        <div class="main-circle">
                            <div class="info-lg">
                                <div class="stat-info">
                                    View
                                    <br>Transactions
                                </div>
                            </div>
                        </div>
                    </a>
                </section>


                <section class="main-stat stat-bottom">
                    <a href="#">
                        <div class="main-circle jsModalOpen">
                            <div class="info-lg">
                                <div class="stat-info">
                                    Enroll
                                    <br>Customer
                                </div>
                            </div>
                        </div>
                    </a>
                </section>
            </section>
        </div>


        <div class="modal-container">
            <div class="modal modal--sm">
                <div>
                    <button class="modal__close jsModalClose">
                        <img src="./close.svg">
                    </button>
                    <div class="m-auto">
                        <div class="text-center">
                            <form>
                                <div class="form-one">
                                    <div class="row">


                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="form-label">BVN</label>
                                                <input type="text" class="form-control custom-input" placeholder="">
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="form-label">Card type</label>
                                                <select class="form-control custom-select">
                                                    <option value="">Verve</option>
                                                    <option value="">Mastercard</option>
                                                    <option value="">Option 3</option>
                                                </select>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="form-label">Card Number</label>
                                                <input type="text" class="form-control custom-input" placeholder="">
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="form-label">Expiry Date</label>
                                                <input type="text" class="form-control custom-input" name="" placeholder="MM/YY">
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="form-label">CVV</label>
                                                <input type="text" class="form-control custom-input" name="" placeholder="">
                                            </div>
                                        </div>

                                        <div class="col-md-7" style="margin-bottom:10px;">
                                            <div class="form-group">
                                                <label class="form-label">Card PIN</label>
                                                <input type="text" class="form-control custom-input" placeholder="">
                                            </div>
                                        </div>
                                        <div class="col-md-12" style="margin: auto;">
                                            <div class="form-group">
                                                <button type="button" class="btn btn-white btn--round" id="next">Next</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-two">
                                    <label class="form-label single-text">Enter the OTP number you just received.</label>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <input type="text" class="otp" style="display: none;">
                                        </div>
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <div class="form-group">
                                            <button class="btn btn-white btn--round">Submit</button>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </main>
    <script type="text/javascript" src="./bootstrap-pincode-input.js"></script>
    <script>
        $('.otp').pincodeInput({
            // 6 input boxes = code of 6 digits long
            inputs: 6,

            // hide digits like password input             
            hidedigits: false,

            // keyDown callback             
            keydown: function (e) {},

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
        $('.form-two').hide()
        $('#next').click(function (e) {
            e.preventDefault()
            $('.form-two').show()
            $('.form-one').hide()
        })
    </script>

    <!-- <script>
        $(document).ready(function () {
            $('.form-two').hide()

            function nextPage() {
                console.log('click')
                $('.form-two').show()
                $('.form-one').hide()
            }
        })
    </script> -->
</body>

</html>