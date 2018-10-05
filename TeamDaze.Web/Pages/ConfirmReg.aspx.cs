using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamDaze.BLL.DAL;
using TeamDaze.BLL.DTO;

namespace TeamDaze.Web.Pages
{
    public partial class ConfirmReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateField();
            }


        }


        private void PopulateField()
        {
            var details = (BvnSearchResp)Session["BVnDetails"];
            txtBvn.Text = details.BVN;
            TextDateOfBirth.Text = details.DateOfBirth;
            TextEmail.Text = details.Email;
            TextFirstName.Text = details.FirstName;
            TextGender.Text = details.Gender;
            TextLastName.Text = details.LastName;
            TextLgaOfOrigin.Text = details.LgaOfOrigin;
            TextLgaOfResidence.Text = details.LgaOfResidence;
            TextRegistrationDate.Text = details.RegistrationDate;
            TextResidentialAddress.Text = details.ResidentialAddress;
            TextStateOfOrigin.Text = details.StateOfOrigin;
            TextStateOfResidence.Text = details.StateOfResidence;
            TextBase64Image.Text = details.Base64Image;
            imagebtn.Src = "data:image/jpg;base64,"+details.Base64Image;

        }

        protected void btnConfirmReg_Click(object sender, EventArgs e)
        {
            //validate otp and insert into database
            string OTP = TextOTP.Text.ToString();

            if(OTP == "024680" || OTP == "987654" || OTP == "123456"|| OTP == "543210" || OTP == "00000" || OTP=="")
            {
                //insert into the db
                try
                {
                    var details = (BvnSearchResp)Session["BVnDetails"];
                    InecRepository log = new InecRepository();
                    log.LogInecRegistration(details);
                    //send mail
                    var id = Guid.NewGuid().ToString();
                    string mailbody = "Voter Unique ID: TVC: " + id;
                    SendMailReports(mailbody, details.Email, details.FirstName);
                    Alertdiv.InnerText = "Registration Successful";
                    Alertdiv.Visible = true;
                    btnConfirmReg.Visible = false;
                    return;
                }
                catch (Exception ex)
                {
                    
                    Alertdiv.InnerText = "An Error Occured During Registration";
                    Alertdiv.Visible = true;

                    btnConfirmReg.Visible = false;
                    return;
                }
            }
            else
            {
                Alertdiv.InnerText = "Invalid OTP";
                return;
            }
        }


        private bool SendMailReports(string mailBody, string personMail, string Votername)
        {
            // GetRegisteredID();
            bool status = false;
            string destinationEmail = personMail;
            string sourceEmail = "InecRegistration@gov.ng";
            string MailText = "";
           // MailText = Session["TotalSales"].ToString();
            string body = "Good day, " + Votername + ",You registered successfuly with INEC. <br/> Thank you for registering us!!"
                + "<br/>" +
                MailText + "<br/>" + " -----------TVC Details----------------- " + "<br/>"
                + mailBody;
            string subject = "INEC REgistration";
            string ccEmailAddress = "";

            try
            {
                SmtpClient smClient = new SmtpClient();
                smClient.Host = "hq-mbx2-serv";// ConfigurationManager.AppSettings["MAIL_HOST"].ToString();
                smClient.Port = 25;
                MailMessage mailMsg = new MailMessage();
                mailMsg.From = new MailAddress(sourceEmail);
                if (destinationEmail.Contains(","))
                {
                    string[] mailParts = destinationEmail.Split(new char[] { ',' });
                    if (mailParts != null && mailParts.Length > 0)
                    {
                        foreach (string destmail in mailParts)
                        {
                            mailMsg.To.Add(destmail);
                        }
                    }
                }
                else
                {
                    mailMsg.To.Add(destinationEmail);
                }

                if (ccEmailAddress != "")
                {
                    mailMsg.CC.Add(ccEmailAddress);
                }
                mailMsg.Subject = subject;
                mailMsg.Body = body;
                mailMsg.IsBodyHtml = true;
                string adUser = "spservice"; // ConfigurationManager.AppSettings["adUser"].ToString();
                string adPwd = "Kinder$$098";// ConfigurationManager.AppSettings["adPassword"].ToString();
                smClient.Credentials = new System.Net.NetworkCredential(adUser, adPwd);
                smClient.Send(mailMsg);
                status = true;
            }
            catch (SmtpFailedRecipientException smtpExc)
            {
                // ErrorLog log = new ErrorLog("<Insertion Error>: " + smtpExc);
            }
            catch (Exception ex)
            {
                // ErrorLog log = new ErrorLog("<Insertion Error>: " + ex);

            }
            return status;
        }
    }
}