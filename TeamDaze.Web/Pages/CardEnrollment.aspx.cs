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
    public partial class CardEnrollment : System.Web.UI.Page
    {
        string BaseUrl = ConfigurationManager.AppSettings["ApiUrl"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnVerifyBVN_Click(object sender, EventArgs e)
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
                string EndpointUrl = $"{BaseUrl}/api/nibss/searchbvn?bvn={cardBVN.Text}";
                string responseString = "";
                var bvnSearchResponse = await new ApiRequest(EndpointUrl).MakeHttpClientRequest(null, ApiRequest.Verbs.GET, null);
                if (bvnSearchResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    DefaultApiReponse<BvnSearchResp> response = Newtonsoft.Json.JsonConvert.DeserializeObject<DefaultApiReponse<BvnSearchResp>>(responseString);
                    Session["BvnSearchResp"] = response.Object;
                    Random rnd = new Random();
                    string Otp = rnd.Next(0, 9999).ToString("D4");
                    string phoneNo = response.Object.PhoneNumber1;
                    OtpRequest otpRequest = new OtpRequest
                    {
                        Bvn = cardBVN.Text,
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
                else
                {
                    Alertdiv.InnerText = "Unable To Get BVN From NiBSS";
                    Alertdiv.Visible = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                // new ErrorLog()
                Response.Write("<script language='javascript'>alert('an error occured, kindly try again');</script>");
                return;
            }
        }

        protected async void btnValidateOtp_Click(object sender, EventArgs e)
        {
            string bvn = cardBVN.Text;
            string otp = txtOtp.Text;
            //mock otp validation
            if (true)
            {
                //capture finger print
                var bvnDetails = (BvnSearchResp)Session["BvnSearchResp"];

                //create the user in the DB
                CustomerRepository customer = new CustomerRepository();
                var Duplicated = customer.ValidateDuplicateEnrollment(bvn);
                if (Duplicated)
                {
                    //bounce user
                    Alertdiv.InnerText = " Duplicate Records For " + bvnDetails.FirstName + " " + bvnDetails.LastName+ " Unable to register";
                    Alertdiv.Visible = true;
                    return;
                }
                else
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
                        Alertdiv.InnerText = bvnDetails.FirstName + " " + bvnDetails.LastName + " Was created succesfully on Touch 'N' Go Platform successfully";
                        Alertdiv.Visible = true;
                        return;

                    }
                    else
                    {
                        //failed creation
                        Alertdiv.InnerText = "Failed Creation For" + bvnDetails.FirstName + " " + bvnDetails.LastName;
                        Alertdiv.Visible = true;
                        return;
                    }
                }


            }

        }
    }
}