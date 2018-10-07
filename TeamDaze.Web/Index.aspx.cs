using Neurotec.Biometrics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamDaze.Api.Utilities;
using TeamDaze.BLL.DAL;
using TeamDaze.BLL.DTO;
using TeamDaze.BLL.EmailNotification;
//using SourceAFIS.Templates;
//using SourceAFIS.General;
//using SourceAFIS.Simple;
namespace TeamDaze.Web
{
    public partial class Index : System.Web.UI.Page
    {
        string BaseUrl = ConfigurationManager.AppSettings["ApiUrl"].ToString();
        Sendmail mailer = new Sendmail();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnEnrollment_Click(object sender, EventArgs e)
        {
            try
            {
                await Task.Delay(2000);
                if (drpEnrollmentType.SelectedItem.Value == "Card")
                    Response.Redirect("~/Pages/CardEnrollment");
                else
                    Response.Redirect("~/Pages/Enrollment.aspx");
            }
            catch (Exception ex)
            {

            }
        }

        protected async void btnVerifyBVN_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            string EndpointUrl = $"{BaseUrl}/api/nibss/searchbvn?bvn={txtaccountBVN.Text}";
            string responseString = "";
            // NibssRepository nibssRepository = new NibssRepository();
            //var resp= nibssRepository.BvnSearch(txtBVN.Text);

            var bvnSearchResponse = await new ApiRequest(EndpointUrl).MakeHttpClientRequest(null, ApiRequest.Verbs.GET, null);
            if (bvnSearchResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                responseString = await bvnSearchResponse.Content.ReadAsStringAsync();
                DefaultApiReponse<BvnSearchResp> response = Newtonsoft.Json.JsonConvert.DeserializeObject<DefaultApiReponse<BvnSearchResp>>(responseString);
                Session["BvnSearchResp"] = response.Object;
                string Otp = rnd.Next(0, 9999).ToString("D4");
                string phoneNo = response.Object.PhoneNumber1;
                // creates a number between 1 and 12;

                //Save Otp
                EndpointUrl = $"{BaseUrl}/api/otp";

                OtpRequest otpRequest = new OtpRequest
                {
                    Bvn = txtaccountBVN.Text,
                    Otp = Otp
                };

                var headers = new Dictionary<string, string>();
                var r = await new ApiRequest(EndpointUrl).MakeHttpClientRequest(otpRequest, ApiRequest.Verbs.POST, headers);

                if (r.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    responseString = await r.Content.ReadAsStringAsync();

                    //mailer.SendMailAlerts(response.Object.Email, mailbody, "Team Daze - OTP" )
                    //EmailSender.SendMail(response.Object.Email, Otp);
                }
                formtwoaccount.Visible = true;
            }
        }

        protected async void btnValidateOtp_Click(object sender, EventArgs e)
        {
            string EndpointUrl = $"{BaseUrl}/api/otp?otp={txtOtp.Text}";
            var GetOtpResponse = await new ApiRequest(EndpointUrl).MakeHttpClientRequest(null, ApiRequest.Verbs.GET, null);
            if (GetOtpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseString = await GetOtpResponse.Content.ReadAsStringAsync();
                DefaultApiReponse<OtpResponse> response = Newtonsoft.Json.JsonConvert.DeserializeObject<DefaultApiReponse<OtpResponse>>(responseString);

                if (response == null)
                {
                    Response.Write("<script language='javascript'>alert('Invalid Otp');</script>");
                    return;
                }

                else if (response.Object.IsUsed == 1)
                {
                    Response.Write("<script language='javascript'>alert('Otp has been used already');</script>");
                    return;
                }
                else
                {
                    EndpointUrl = $"{BaseUrl}/api/otp";
                    var ValidateOtpResponse = await new ApiRequest(EndpointUrl).MakeHttpClientRequest(txtOtp.Text, ApiRequest.Verbs.POST, null);

                    if (ValidateOtpResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string ValidateOTPResponseString = await ValidateOtpResponse.Content.ReadAsStringAsync();
                        DefaultApiReponse<int> ValidationResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<DefaultApiReponse<int>>(responseString);
                        if (ValidationResponse.Object == 1)
                        {
                            //trigger thumbprint reader
                            //Save to DB
                            CustomerRepository customerRepository = new CustomerRepository();
                            BvnSearchResp custObj = (BvnSearchResp)Session["BvnSearchResp"];
                            customerRepository.CreateCustomer(new CustomerCreation
                            {
                                BVN = custObj.BVN,
                                EmailAddress = custObj.Email,
                                FirstName = custObj.FirstName,
                                LastName = custObj.LastName,
                                PhoneNumber = custObj.PhoneNumber1,
                                CardToken = null,
                                CardType = null,
                                EnrollmentType = "Account",
                                PanicFinger = "",
                                MaxAmount = 100000000000
                            });
                        }
                    }
                }
            }
        }
    }
}