<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Enrollment.aspx.cs" Inherits="TeamDaze.Web.Pages.Enrollment" %>

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
                                <label>Enter BVN</label>
                                <asp:TextBox ID="txtBVN" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBVN" ErrorMessage="*" InitialValue="" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                            <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button ID="btnVerifyBVN" Text="Verify BVN" runat="server" OnClick="btnVerifyBVN_Click" CssClass="btn btn-primary"></asp:Button>
                        </div>
                    </div>


                        
                    </div>

                    <div class="form-group">
                            <div class="col-md-12 col-sm-12">
                                <label>Enter BVN</label>
                                <asp:TextBox ID="txtOtp" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOtp" ErrorMessage="*" InitialValue="" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button ID="btnValidateOtp" Text="Validate Otp" runat="server" OnClick="btnValidateOtp_Click" CssClass="btn btn-primary"></asp:Button>
                        </div>
                    </div>


                </fieldset>
            </div>
            <!-- /panel content -->

        </div>
    </div>
</asp:Content>
