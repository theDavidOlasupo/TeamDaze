<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="Enrollment.aspx.cs" Inherits="TeamDaze.Web.Pages.Enrollment" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Account Enrollment</title>
    <script src="http://localhost:59703/Scripts/jquery-3.3.1.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
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
                    <h1 class="title">Account Enrollment</h1>
                </div>
            </div>


            <div class="modal text-center">
                <div id="accountenrollment">
                    <div class="m-auto">
                        <div class="text-center">
                            <div class="form-one-account" id="formoneaccount" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="form-label">BVN</label>
                                            <asp:TextBox ID="txtBVN" runat="server" class="form-control custom-input" placeholder="" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBVN" ErrorMessage="*"
                                                InitialValue="" ForeColor="Red" ValidationGroup="BVNVerification"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>

                                    <div class="col-md-12" style="margin: auto;">
                                        <div class="form-group">
                                            <asp:Button ID="btnVerifyBVN" class="btn btn-white btn--round" Text="Verify BVN" runat="server"
                                                OnClick="btnVerifyBVN_Click" CssClass="btn btn-primary" ValidationGroup="BVNVerification"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-two-account" runat="server" id="formtwoaccount" visible="false">
                                <label class="form-label single-text">Enter the OTP number you just received.</label>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtOtp" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOtp" ErrorMessage="*" InitialValue="" ForeColor="Red"
                                            ValidationGroup="OTPValidation"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="col-md-12 text-center">
                                    <div class="form-group">
                                        <asp:Button ID="btnValidateOtp" Text="Validate Otp" runat="server"
                                            OnClick="btnValidateOtp_Click" CssClass="btn btn-primary" ValidationGroup="OTPValidation"></asp:Button>
                                    </div>
                                </div>
                            </div>
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
            $('.form-two').show()
            $('.form-one').hide()
        })

        $('#accountNext').click(function (e) {
            e.preventDefault()
            $('.form-two-account').show()
            $('.form-one-account').hide()
        })



        //$('#btnEnrollment').click(function (e) {
        //    if ($('#drpEnrollmentType').val() == "Card") {
        //        $('#cardenrollment').show()
        //        $('#enrollmentType').hide()
        //        $('#accountenrollment').hide()
        //    }
        //    else {
        //        $('#accountenrollment').show()
        //        $('#cardenrollment').hide()
        //        $('#enrollmentType').hide()
        //    }
        });
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
