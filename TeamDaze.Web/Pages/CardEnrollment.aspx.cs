using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamDaze.Api.Utilities;
using TeamDaze.BLL.DAL;
using TeamDaze.BLL.DTO;
using TeamDaze.BLL.EmailNotification;
using TeamDaze.Web.UranusCore;

namespace TeamDaze.Web.Pages
{
    public partial class CardEnrollment : System.Web.UI.Page
    {
        string BaseUrl = ConfigurationManager.AppSettings["ApiUrl"].ToString();
        UranusCore.UranusCoreClient client = new UranusCore.UranusCoreClient();
        private string AuthenticatedID = null;
        private string AppToken = "BayometricToken";
        private string sessionKey = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        TeamDaze.BLL.DAL.Sendmail mailTrxn = new Sendmail();


        protected void btnVerifyBVN_Click(object sender, EventArgs e)
        {
            formone.Visible = false;
            formtwo.Visible = true;
            if (cardBVN.Text == "" || txtCardNumber.Text == "" || txtCVV.Text == "" || txtPin.Text == "")
            {
                Response.Write("<script language='javascript'>alert('Please Fill All Fields');</script>");
                return;
            }

            try
            {
                NibssRepository nibss = new NibssRepository();
                string EndpointUrl = $"{BaseUrl}/api/nibss/searchbvn?bvn={cardBVN.Text}";
                string responseString = "";
                // var bvnSearchResponse = await new ApiRequest(EndpointUrl).MakeHttpClientRequest(null, ApiRequest.Verbs.GET, null);
                //if (bvnSearchResponse.StatusCode == System.Net.HttpStatusCode.OK)
                if (true)
                {

                    //  DefaultApiReponse<BvnSearchResp> response = Newtonsoft.Json.JsonConvert.DeserializeObject<DefaultApiReponse<BvnSearchResp>>(responseString);
                    var response = nibss.BvnSearch(cardBVN.Text);
                    //Session["BvnSearchResp"] = response.Object;
                    Session["BvnSearchResp"] = response;
                    Random rnd = new Random();
                    string Otp = rnd.Next(0, 9999).ToString("D4");
                    //string phoneNo = response.Object.PhoneNumber1;
                    string phoneNo = response.PhoneNumber2;
                    OtpRequest otpRequest = new OtpRequest
                    {
                        Bvn = cardBVN.Text,
                        Otp = Otp
                    };
                    EndpointUrl = $"{BaseUrl}/api/otp";

                    // var r = await new ApiRequest(EndpointUrl).MakeHttpClientRequest(otpRequest, ApiRequest.Verbs.POST, null);

                    //if (r.StatusCode == System.Net.HttpStatusCode.OK)
                    if (true)
                    {
                        //  responseString = await r.Content.ReadAsStringAsync();

                        //EmailSender.SendMail(response.Email, Otp);
                        mailTrxn.SendMailAlerts(response.Email,"Your otp is"+Otp,"Team Daze OTP");
                    }
                }
                else
                {
                    Alertdiv.InnerText = "Unable To Get BVN From NiBSS";
                    Alertdiv.Visible = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                //  new ErrorLog();
                TeamDaze.BLL.DAL.ErrorLog loggger = new ErrorLog(ex.ToString());
                Response.Write("<script language='javascript'>alert('an error occured, kindly try again');</script>");
                return;
            }
        }

        protected async void btnValidateOtp_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                string bvn = cardBVN.Text;
                string otp = txtOtp.Text;
                //mock otp validation
                try
                {
                    if (true)
                    {
                        //capture finger print
                        var bvnDetails = (BvnSearchResp)Session["BvnSearchResp"];
                        var resp = client.CreateSession(new AuthRequestInfo());
                        if (resp.ResponseCode == BFSClientReturnErrorCode.SUCCESS)
                        {
                            Session["sessionKey"] = resp.SessionKey;
                        }
                        //create the user in the DB
                        CustomerRepository customer = new CustomerRepository();
                        //var Duplicated = customer.ValidateDuplicateEnrollment(bvn);
                        bool Duplicated = false;
                        if (Duplicated)
                        {
                            //bounce user
                            Alertdiv.InnerText = " Duplicate Records For " + bvnDetails.FirstName + " " + bvnDetails.LastName + " Unable to register";
                            Alertdiv.Visible = true;
                            return;
                        }
                        else
                        {
                            string sessionID = Session["sessionKey"].ToString();
                            //if (Register.ResponseCode == BFSClientReturnErrorCode.SUCCESS)
                            if (true)
                            {

                                //save user
                                CustomerCreation createcustomer = new CustomerCreation
                                {
                                    BVN = cardBVN.Text,
                                    CardToken = Guid.NewGuid().ToString(),
                                    CardType = "2",
                                    CreatedBy = "Daze",
                                    EmailAddress = bvnDetails.Email,
                                    Status = 1,
                                    CreatedOn = DateTime.Now,
                                    EnrollmentType = "2",
                                    FirstName = bvnDetails.FirstName,
                                    LastName = bvnDetails.LastName,
                                    MaxAmount = Convert.ToDecimal(ConfigurationManager.AppSettings["MaxAmount"].ToString()),
                                    PanicFinger = "not set",
                                    PhoneNumber = bvnDetails.PhoneNumber1
                                };

                                var CreationResponse = customer.CreateCustomer(createcustomer);

                                if (CreationResponse.Item1)
                                {
                                    //created the user, rerturn success response
                                    var Register =  client.RegisterPerson(sessionID, bvn);

                                   // client.EndSession(sessionID);
                                    Thread.Sleep(2000);
                                    Alertdiv.InnerText = bvnDetails.FirstName + " " + bvnDetails.LastName + " Was created succesfully on Touch 'N' Go Platform successfully";
                                    Alertdiv.Visible = true;
                                   // return;

                                }
                                else
                                {
                                    //failed creation
                                    client.EndSession(sessionID);
                                    Alertdiv.InnerText = "Failed Creation For " + bvnDetails.FirstName + " " + bvnDetails.LastName;
                                    Alertdiv.Visible = true;
                                    return;
                                }
                            }
                            else
                            {
                                client.EndSession(sessionID);
                                Alertdiv.InnerText = "Could Not Capture thumbprint for: " + bvnDetails.FirstName + " " + bvnDetails.LastName + " Reason:";
                                Alertdiv.Visible = true;
                                return;
                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                    Alertdiv.Visible = true;
                    Alertdiv.InnerText = "An error Occured Failed Creation For";
                    return;

                    //throw;
                }

            }
        }
    }
}