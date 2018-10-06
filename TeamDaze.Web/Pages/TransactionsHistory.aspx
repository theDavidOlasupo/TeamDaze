<%@ Page Async="true" Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="TransactionsHistory.aspx.cs" Inherits="TeamDaze.Web.Pages.TransactionsHistory" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl" TagPrefix="rjs" %>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title></title>
    <link rel="stylesheet" href="../main.css">
    <link href="https://fonts.googleapis.com/css?family=K2D:200,300,400,500,600,700,800" rel="stylesheet">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link href="../../bootstrap-pincode-input.css" rel="stylesheet" />

</head>

<body>
    <form id="main" runat="server">
    <main>
        <div class="container">
            <div class="header">
                <h1 class="title">Transaction History</h1>
            </div>


            <div>
                <div class="table card-table table-vcenter text-nowrap">
                    <div class="row">
                        <div class="form-group">
                            <div class="col-md-12 col-sm-12">
                                <label>Start Date *</label>

                                <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true" CssClass="form-control" style="width:200px"></asp:TextBox>
                                <rjs:popcalendar id="StartDate" runat="server" control="txtStartDate"
                                    format="yyyy mm dd" readonly="True" invaliddatemessage="The date value entered is invalid." />

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStartDate" ErrorMessage="*" InitialValue="" ForeColor="Red"></asp:RequiredFieldValidator>

                            </div>
                        </div>


                        <div class="form-group">
                            <div class="col-md-12 col-sm-12">
                                <label>End Date *</label>

                                <asp:TextBox ID="txtEndDate" runat="server" ReadOnly="true" CssClass="form-control" style="width:200px"></asp:TextBox>
                                <rjs:popcalendar id="PopCalendar1" runat="server" control="txtEndDate"
                                    format="yyyy mm dd" readonly="True" invaliddatemessage="The date value entered is invalid." />

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEndDate" ErrorMessage="*" InitialValue="" ForeColor="Red"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button ID="btnView" Text="View" runat="server" OnClick="btnView_Click" CssClass="btn btn-primary"></asp:Button>
                        </div>
                    </div>
                </div>
            <section class="stats">

                <table class="table card-table table-vcenter text-nowrap">
                    <thead>
                        <tr>
                            <th>Transaction ID</th>
                            <th>Transaction Date</th>
                            <th>Total Amount</th>
                        </tr>
                    </thead>
                     <asp:ListView ID="lstTransactionHistory" runat="server" ClientIDMode="Static">
                            <ItemTemplate>

                    <tbody>
                       <tr>
                            <td><%# Eval("CreatedOn").ToString() %></td>
                                        <td><%# Eval("CreatedOn").ToString() %></td>
                                        <td><%# Eval("Amount").ToString() %></td>
                                    </tr>
                    </tbody>
                     </ItemTemplate>
                        </asp:ListView>
                        <tfoot>
                            <tr>
                                <td>Sum</td>
                                <td>
                                    <asp:Label ID="lblTotalSum" runat="server"></asp:Label></td>
                            </tr>
                        </tfoot>
                </table>


            </section>
        </div>

            </div>
    </main>
        </form>
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
    </script>

    <script>
        $('.form-two').hide()

        function nextPage(e) {
            e.preventDefault()
            $('.form-two').show()
            $('.form-one').hide()
        }

        function showStaffForm() {
            $('.add-new-staff').show()
            $('.staff-account-list').hide()
        }
    </script>
</body>

</html