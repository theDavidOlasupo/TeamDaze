<%@ Page Async="true" Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransactionsHistory.aspx.cs" Inherits="TeamDaze.Web.Pages.TransactionsHistory" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl" TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- page title -->
    <header id="page-header">
        <h1>Transaction History</h1>
    </header>
    <!-- /page title -->


    <div id="content" class="padding-20">

        <!-- 
						PANEL CLASSES:
							panel-default
							panel-danger
							panel-warning
							panel-info
							panel-success

						INFO: 	panel collapse - stored on user localStorage (handled by app.js _panels() function).
								All pannels should have an unique ID or the panel collapse status will not be stored!
					-->
        <div id="panel-1" class="panel panel-default">
            <div class="panel-heading">
                <span class="title elipsis">
                    <strong>Transaction History</strong>
                    <!-- panel title -->
                </span>

                <!-- right options -->
                <ul class="options pull-right list-inline">
                    <li><a href="#" class="opt panel_colapse" data-toggle="tooltip" title="Colapse" data-placement="bottom"></a></li>
                    <li><a href="#" class="opt panel_fullscreen hidden-xs" data-toggle="tooltip" title="Fullscreen" data-placement="bottom"><i class="fa fa-expand"></i></a></li>
                    <li><a href="#" class="opt panel_close" data-confirm-title="Confirm" data-confirm-message="Are you sure you want to remove this panel?" data-toggle="tooltip" title="Close" data-placement="bottom"><i class="fa fa-times"></i></a></li>
                </ul>
                <!-- /right options -->

            </div>

            <!-- panel content -->
            <div class="panel-body">
                <fieldset>

                    <div class="row">
                        <div class="form-group">
                            <div class="col-md-12 col-sm-12">
                                <label>Start Date *</label>

                                <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                <rjs:popcalendar id="StartDate" runat="server" control="txtStartDate"
                                    format="yyyy mm dd" readonly="True" invaliddatemessage="The date value entered is invalid." />

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStartDate" ErrorMessage="*" InitialValue="" ForeColor="Red"></asp:RequiredFieldValidator>

                            </div>
                        </div>


                        <div class="form-group">
                            <div class="col-md-12 col-sm-12">
                                <label>End Date *</label>

                                <asp:TextBox ID="txtEndDate" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
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


                </fieldset>
                <div class="table-responsive">
                    <table class="table table-bordered table-vertical-middle nomargin">
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>Amount</th>
                            </tr>
                        </thead>
                        <asp:ListView ID="lstTransactionHistory" runat="server" ClientIDMode="Static">
                            <ItemTemplate>

                                <tbody>
                                    <tr>
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
                </div>
            </div>
            <!-- /panel content -->

        </div>
    </div>
</asp:Content>
