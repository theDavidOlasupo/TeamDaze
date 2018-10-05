using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamDaze.Api.Utilities;
using TeamDaze.BLL.DAL;
using TeamDaze.BLL.DTO;
using TeamDaze.BLL.EmailNotification;

namespace TeamDaze.Web.Pages
{
    public partial class Enrollment : System.Web.UI.Page
    {
        string BaseUrl = ConfigurationManager.AppSettings["ApiUrl"].ToString();
        EmailSender sender = new EmailSender();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnVerifyBVN_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            string EndpointUrl = $"{BaseUrl}/api/nibss/searchbvn?bvn={txtBVN.Text}";
            string responseString = "";
           // NibssRepository nibssRepository = new NibssRepository();
           //var resp= nibssRepository.BvnSearch(txtBVN.Text);

            var bvnSearchResponse = await new ApiRequest(EndpointUrl).MakeHttpClientRequest(null, ApiRequest.Verbs.GET, null);
            if (bvnSearchResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                responseString = await bvnSearchResponse.Content.ReadAsStringAsync();
                DefaultApiReponse<BvnSearchResp> response = Newtonsoft.Json.JsonConvert.DeserializeObject<DefaultApiReponse<BvnSearchResp>>(responseString);

                string Otp = rnd.Next(0, 9999).ToString("D4");
                string phoneNo = response.Object.PhoneNumber1;
                // creates a number between 1 and 12;

                //Save Otp
                EndpointUrl = $"{BaseUrl}/api/otp";

                OtpRequest otpRequest = new OtpRequest
                {
                    Bvn = txtBVN.Text,
                    Otp = Otp
                };

                var headers = new Dictionary<string, string>();
                var r = await new ApiRequest(EndpointUrl).MakeHttpClientRequest(otpRequest, ApiRequest.Verbs.POST, headers);

                if (r.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    responseString = await r.Content.ReadAsStringAsync();

                    EmailSender.SendMail(response.Object.Email, Otp);
                }
            }

            //Rand 


            //DefaultApiReponse<List<TransactionDto>> response = Newtonsoft.Json.JsonConvert.DeserializeObject<DefaultApiReponse<List<TransactionDto>>>(responseString);
            //return response.Object;
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
                        }
                    }
                }
            }
        }
    }
}