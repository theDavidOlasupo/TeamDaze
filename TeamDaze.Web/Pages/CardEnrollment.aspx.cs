using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
using SourceAFIS.Simple;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace TeamDaze.Web.Pages
{
    public partial class CardEnrollment : Page, DPFP.Capture.EventHandler
    {
        string BaseUrl = ConfigurationManager.AppSettings["ApiUrl"].ToString();
        UranusCore.UranusCoreClient client = new UranusCore.UranusCoreClient();
        private string AuthenticatedID = null;
        private string AppToken = "BayometricToken";
        private string sessionKey = null;
        static AfisEngine Afis;

        private byte[] fingerPrint;
        protected void Page_Load(object sender, EventArgs e)
        {
            Init();
            Start();
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
                        mailTrxn.SendMailAlerts(response.Email, "Your otp is" + Otp, "Team Daze OTP");
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

        protected  void btnValidateOtp_Click(object sender, EventArgs e)
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
                        //Commented for reader
                        /*var resp = client.CreateSession(new AuthRequestInfo());
                        if (resp.ResponseCode == BFSClientReturnErrorCode.SUCCESS)
                        {
                            Session["sessionKey"] = resp.SessionKey;
                        }*/
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
                           // string sessionID = Session["sessionKey"].ToString();
                            //if (Register.ResponseCode == BFSClientReturnErrorCode.SUCCESS)
                            Start();
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
                                    //var Register = client.RegisterPerson(sessionID, bvn);

                                    // client.EndSession(sessionID);
                                    Thread.Sleep(2000);
                                    Alertdiv.InnerText = bvnDetails.FirstName + " " + bvnDetails.LastName + " Was created succesfully on Touch 'N' Go Platform successfully";
                                    Alertdiv.Visible = true;
                                    // return;

                                }
                                else
                                {
                                    //failed creation
                                    //client.EndSession(sessionID);
                                    Alertdiv.InnerText = "Failed Creation For " + bvnDetails.FirstName + " " + bvnDetails.LastName;
                                    Alertdiv.Visible = true;
                                    return;
                                }
                            }
                            else
                            {
                               // client.EndSession(sessionID);
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
        private int Enroll(byte[] fingerPrint, string matricNo)
        {
            /*RollCallForm.HostelStudent hostelStudent = null;
            try
            {
                //RegistrationDAO.StudentTemplate studentTemplate = new RegistrationDAO.StudentTemplate();
                Bitmap imageBitmap = new Bitmap(new MemoryStream(fingerPrint));
                BitmapSource image = Imaging.CreateBitmapSourceFromHBitmap(imageBitmap.GetHbitmap(), IntPtr.Zero,
                    Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                studentTemplate.AsBitmapSource = image;
                hostelStudent = new RollCallForm.HostelStudent();
                hostelStudent.MatricNo = matricNo;
                // Add fingerprint to the person
                hostelStudent.Fingerprints.Add(studentTemplate);
                Afis.Extract(hostelStudent);
                imageBitmap.Dispose();

            }
            catch (Exception ex)
            {
                //this.BeginInvoke((Action)(() =>
                //{
                //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                //        MessageBoxIcon.Error);
                //}));
            }*/
            return 1;
        }

        protected void Start()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                    SetPrompt("Using the fingerprint reader, scan your fingerprint.");
                }
                catch(Exception ex)
                {
                    SetPrompt("Can't initiate capture!");
                }
            }
        }

        protected void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch
                {
                    SetPrompt("Can't terminate capture!");
                }
            }
        }

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();	// Create a sample convertor.
            Bitmap bitmap = null;												            // TODO: the size doesn't matter
            Convertor.ConvertToPicture(Sample, ref bitmap);									// TODO: return bitmap as a result
            return bitmap;
        }

        protected new virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();				// Create a capture operation.

                if (null != Capturer)
                    Capturer.EventHandler = this;					// Subscribe for capturing events.
                else
                    SetPrompt("Can't initiate capture operation!");
            }
            catch
            {
                Debug.WriteLine("Can't initiate capture operation!");
            }
        }

        public byte[] ToByteArray(Bitmap image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                return ms.ToArray();
            }
        }
        protected virtual Bitmap Process(DPFP.Sample Sample)
        {
            var bitmap = ConvertSampleToBitmap(Sample);
            fingerPrint = ToByteArray(bitmap);
            // Draw fingerprint sample image.
            DrawPicture(bitmap);
            return bitmap;
        }

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            MakeReport("The fingerprint sample was captured.");
            Process(Sample);
            //Afis.Threshold = 10;
            Random random = new Random();
          int result = Enroll(fingerPrint, "#" + random.Next(1000, 9999));
            /*RollCallForm.HostelStudent match = Afis.Identify(probe, database).FirstOrDefault() as RollCallForm.HostelStudent;
            // Null result means that there is no candidate with similarity score above threshold
            if (match != null)
            {
                this.BeginInvoke((Action)(() =>
                {
                    btnRegister.Enabled = false;
                    MessageBox.Show("Double enrollment of fingerprint is not allowed", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }));
            }
            else
            {
                this.BeginInvoke((Action)(() =>
                {
                    btnRegister.Enabled = true;
                }));
            }*/

        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The finger was removed from the fingerprint reader.");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was touched.");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was connected.");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was disconnected.");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                MakeReport("The quality of the fingerprint sample is good.");
            else
                MakeReport("The quality of the fingerprint sample is poor.");
        }

        protected void SetPrompt(string prompt)
        {
            //this.Invoke(new Function(delegate () {
            //    Prompt.Text = prompt;
            //}));
            Debug.WriteLine(prompt);
        }

        protected void MakeReport(string message)
        {
            //this..BeginInvoke((Action)(() =>
            //{
            //    StatusText.AppendText(message + "\r\n");
            //}));
            Debug.WriteLine(message);
        }

        private void DrawPicture(Bitmap bitmap)
        {
            //this.Invoke(new Function(delegate () {
            //   // Picture.Image = new Bitmap(bitmap, Picture.Size);   // fit the image into the picture box
            //}));
        }

        private DPFP.Capture.Capture Capturer;

    }
}