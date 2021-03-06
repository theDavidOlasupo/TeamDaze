﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Confirmation.aspx.cs" Inherits="TeamDaze.Web.Pages.Confirmation" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Profile Confirmation</title>
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
            <div class="container flex-direction:column">
                <div class="header">
                    <h1 class="title">Profile Confirmation</h1>
                </div>
            </div>


            <div class="modal text-center">
                <div id="accountenrollment">
                    <div class="m-auto">
                        <div class="text-center">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="enroll-success" style="display: flex; height: 300px; align-items: center; justify-content: space-between">
                                        <label class="form-label single-text" style="color: #000">You have successfully enrolled the customer!</label>
                                        <img src="../check.svg" alt="" width="25" height="25">
                                    </div>
                                    <div class="col-md-12 text-center">
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnHome" class="btn btn-white btn--round" Text="Home" OnClick="btnHome_Click" />

                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </main>
    </form>
</body>

</html>
