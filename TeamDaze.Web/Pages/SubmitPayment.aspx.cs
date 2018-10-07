using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing.Imaging;
using Neurotec.Biometrics;
using TeamDaze.BLL.DTO;
using TeamDaze.BLL.DAL;
using TeamDaze.Web.UranusCore;
using System.Configuration;

namespace TeamDaze.Web.Pages
{
    internal class EnrollmentResult
    {
        public NffvStatus engineStatus;
        public NffvUser engineUser;
    };


    public partial class SubmitPayment : System.Web.UI.Page
    {
        Nffv _engine;
        UranusCore.UranusCoreClient client = new UranusCore.UranusCoreClient();

        string password = "";
        string scanner = "Nitgen";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //radio1.Items.Add(new ListItem("RT", "1"));
                //radio1.Items.Add(new ListItem("RI", "2"));
                //clearField();
                //_engine = new Nffv(dbName, password);


            }
            else
            {

            }


        }

        public void btn_Click(object sender, EventArgs a)
        {
            //try
            //{
            //    //get the thumb print
            //    //chose the finger type
            //    //get the bvn and sedn to nibs
            //    //if it's a "00"
            //    //get the bvn details ...Enroll of it does not exist
            //    //ask for anxious finger
            //    var fingerChosen = radio1.SelectedValue.ToString();
            //    string bvn = txtBvn.ToString();
            //    string Amount = txtAmount.ToString();

            //    EnrollmentResult enrollmentResults = new EnrollmentResult();
            //    enrollmentResults.engineUser = _engine.Enroll(20000, out enrollmentResults.engineStatus);
            //   // args.Result = enrollmentResults;
            //    if (enrollmentResults.engineStatus == NffvStatus.TemplateCreated)
            //    {
            //        //it took a snap shot

            //        var img = enrollmentResults.engineUser.GetBitmap();
            //        var base64 = ConvertImageToBAse64(img);

            //        //call bvn service with temeplate.
            //        //if 00 ..check that bvn doesn't exist on db as user or panic figer
            //        //if panic finger..disable user
            //        //if does not exist..ask for panic finger and enroll

            //        //tokenize card on registration
            //        //pay by calling cards api and then ..pass the token
            //        return;  
            //        //convert the image to base 64 
            //    }
            //    else
            //    {
            //        return ;
            //    }

            //}
            //catch(Exception ex)
            //{

            //}
        }
        public void clearField()
        {
            txtAmount.Text = "";
            //txtBvn.Text = "";
            //txtBvn.Text = "";
            // txtEmail.Text = "";
            //txtName.Text = "";
            // txtPhone.Text = "";

        }
        //public string doEnroll(object sender, DoWorkEventArgs args)
        //{
        //    EnrollmentResult enrollmentResults = new EnrollmentResult();
        //    enrollmentResults.engineUser = _engine.Enroll(20000, out enrollmentResults.engineStatus);
        //    args.Result = enrollmentResults;
        //    if (enrollmentResults.engineStatus == NffvStatus.TemplateCreated)
        //    {
        //        //it took a snap shot

        //        var img = enrollmentResults.engineUser.GetBitmap();
        //        var base64 = ConvertImageToBAse64(img);
        //        return base64;
        //        //convert the image to base 64 
        //    }
        //    else
        //    {
        //        return "99";
        //    }
        //}
        //public string ConvertImageToBAse64(Bitmap bitmapImage)
        //{
        //    MemoryStream ms = new MemoryStream();

        //    bitmapImage.Save(ms, ImageFormat.Png);
        //    string Base64Image = Convert.ToBase64String(ms.ToArray());
        //    Fingerprint fp2 = new Fingerprint();
        //    //
        //    fp2.AsBitmap = new Bitmap(bitmapImage);

        //    Person person = new Person();
        //    person.Fingerprints.Add(fp2);

        //    //  fp2.AsIsoTemplate 
        //    Afis.Extract(person);
        //    var imgArray = fp2.Template;
        //    var isoTemplate = fp2.AsIsoTemplate;
        //    var isoImage = Convert.ToBase64String(isoTemplate);
        //    var ImagByteArray = Convert.ToBase64String(imgArray);
        //    ms.Position = 0;

        //    //BinaryWriter a = new BinaryWriter(File.Open("iso19794-2 template from fp_image1.dat", FileMode.Create));
        //    //foreach (byte element in isoTemplate)
        //    //{
        //    //    a.Write(element);
        //    //}


        //    return ImagByteArray;
        //    //return Base64Image;
        //}
        //process payment
        protected void btnMakePayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAmount.Text == "")
                {
                    //Alertdiv.InnerText = "ENTER AN AMOUNT !!!";
                    //Alertdiv.Visible = true;
                    return;
                }

              TeamDaze.BLL.DAL.Sendmail mailTrxn = new Sendmail();
                decimal amount = Convert.ToDecimal(txtAmount.Text.ToString());
                string mailbody = "";
                string subject = "";
                 //var response = client.Search();
                //if (response.ResponseCode == BFSClientReturnErrorCode.HIT_CONFIRMED)
                    if (true)
                    {
                     mailbody = "Dear Customer, This is an alert for the payment you just made on Touch 'N' Pay platform Amount ";
                    mailbody += "Amount:" + amount.ToString() + "Naira <br> If you did not carry out this transaction kindly contact us at teamdaze43@gmail.com or 08105931866";
                    subject = "Touch 'N' Pay Transaction Alert";
                    mailTrxn.SendMailAlerts("azeez.adedayo@yahoo.com", mailbody, subject);
                    string UserBVN = "";
                    TransactionRepository LogTrxn = new TransactionRepository();
                    UserBVN = ConfigurationManager.AppSettings["TestBVN"].ToString();
                    string merchantAPI = "";
                    int UserType = 0;
                    ////get the user's details with the userID
                    //CustomerCreation customerDetails = new CustomerCreation();
                    //UserBVN = customerDetails.BVN;

                    CustomerRepository customer = new CustomerRepository();
                    var userDetail = customer.GetCustomer(UserBVN);
                    var det = userDetail.Item2;
                    //check if this is a card or bank user
                    if (ConfigurationManager.AppSettings["TestBVN"].ToString() == "2269644939")
                        UserType = 2;
                    else
                        UserType = 3;

                    if (UserType == 2) //2 -card user
                    {
                        //call FlutterWave API & Log transactions
                        //mail user on alert
                        FlutterChargeCard chargeCard = new FlutterChargeCard
                        {
                            amount = amount.ToString(),
                            cardtype = det[0].Status.ToString(),
                            chargetoken = det[0].BVN,
                            country = "NG",
                            currency = "NGN",
                            custid= det[0].BVN,
                            merchantid = "testMerchant",
                            narration="Test trxn"
                        };
                        //Mock the FlutterWave API call and response
                       //response mock
                        FlutterCardResponse flutterCard = new FlutterCardResponse
                        {
                          status = "success",
                          data = null
                        };
                            //send sucess mail alert & log Trxn
                        if(flutterCard.status == "success")
                        {
                            //success
                            //100 is the test merchant ID
                            //1 is the status for a successful trxn
                            LogTrxn.LogTrxn(det[0].BVN, det[0].BVN, det[0].PhoneNumber, 100, amount.ToString(), "1");
                            Alertdiv.InnerText = "Payment Succesful";
                            Alertdiv.Visible = true;
                            mailbody = "Hi there, This is an alert for the payment you just made on Touch 'N' Pay platform Amount ";
                            mailbody += "Amount:" + amount.ToString() + "Naira <br> If you did not carry out this transaction kindly contact us at teamdaze43@gmail.com or 08105931866";
                            subject = "Touch 'N' Pay Transaction Alert";
                            mailTrxn.SendMailAlerts(det[0].EmailAddress, mailbody, subject);

                            Response.Redirect("TransactionConfirmation.aspx");
                        }
                        else
                        {
                            //failed
                            //0 is the status for a successful trxn
                            LogTrxn.LogTrxn(det[0].BVN, det[0].BVN, det[0].PhoneNumber, 100, amount.ToString(), "0");
                            // SendMail()
                             mailbody = "Hi there, This is an alert for the payment you just made on Touch 'N' Pay platform Amount ";
                            mailbody += "Amount:" + amount.ToString() + "Naira <br> If you did not carry out this transaction kindly contact us at teamdaze43@gmail.com or 08105931866";
                             subject = "Touch 'N' Pay Transaction Alert";
                            mailTrxn.SendMailAlerts(det[0].EmailAddress, mailbody, subject);
                            Alertdiv.InnerText = "Payment Failed";
                            Alertdiv.Visible = true;
                            Response.Write("<script language='javascript'>alert('Payment Failed');</script>");
                            return;
                        }






                    }
                    else if(UserType ==3)
                    {
                        //bank user
                        //call the bank API &
                        //based on the customer's bank code...POST the trxn to their API
                        BankPayoutReq bankPayout = new BankPayoutReq
                        {
                            AcctType = "savings",
                            Amount = amount.ToString(),
                            BVN = det[0].BVN,
                            MerchantID = "",//would always be fixed for any ONE bank..e.g Sterling -101, Polaris- 098
                            TrxnRef = "Payment:" + UserBVN
                        };
                        bool CallHostbank = true; //simulation of calling the host bank
                        if (CallHostbank)
                        {
                            //log the trxn
                            LogTrxn.LogTrxn(det[0].BVN, det[0].BVN, det[0].PhoneNumber, 100, amount.ToString(), "1");
                            Response.Redirect("TransactionConfirmation.aspx");
                        }
                        else
                        {
                            //ignore this sha
                        }
                    }
                    else
                    {
                        //Alertdiv.InnerText = "I Could Not Get Your details currently, please try again.";
                        //Alertdiv.Visible = true;
                        return;
                    }
                }
                else
                {
                    //Alertdiv.InnerText = "User Does Not Exist On NIBSS Database";
                    //Alertdiv.Visible = true;
                    return;

                }
            }
            catch (Exception ex)
            {

            }
        }

         
    }




}