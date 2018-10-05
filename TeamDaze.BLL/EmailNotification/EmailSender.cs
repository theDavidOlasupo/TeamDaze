using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Data;
using System.Configuration;

namespace TeamDaze.BLL.EmailNotification
{
    public class EmailSender
    {
        public static void SendMail(string recipient, string body)
        {
            var EmailSettingsDs = new DataSet();
            string FromEmail;
            string Password;
            int PortNo;
            string Host;
            MailMessage mail = new MailMessage();


            FromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
            Password = ConfigurationManager.AppSettings["Password"].ToString();
            PortNo = Convert.ToInt32(ConfigurationManager.AppSettings["PortNo"]);
            Host = ConfigurationManager.AppSettings["SmtpServer"].ToString();

            mail = new MailMessage();
            mail.From = new MailAddress(FromEmail);
            mail.To.Add(recipient);

            mail.Subject = "Team Daze : OTP";
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Port = PortNo;
            SmtpServer.Host = Host;
            SmtpServer.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential(FromEmail, Password);
            SmtpServer.UseDefaultCredentials = true;
            SmtpServer.Credentials = NetworkCred;


            if (IsConnectionAvailable() == true)
            {
                try
                {
                    SmtpServer.Send(mail);
                    //JobEmailTransactionDA.UpdateFailedEmailStatus(EmailTransactionObj.MessageID);
                    //JobSuccessLogging.SendSuccessStatusToText();
                }

                catch (Exception ex)
                {
                    // ExceptionLogging.SendErrorToText(ex);
                }
            }
        }

        public static bool IsConnectionAvailable()
        {

            var objUrl = new System.Uri("http://www.gmail.com/");
            WebRequest objWebReq;
            objWebReq = WebRequest.Create(objUrl);
            WebResponse objResp;
            try
            {
                //Attempt to get response and return True
                objResp = objWebReq.GetResponse();
                objResp.Close();
                objWebReq = null;
                return true;
            }
            catch (Exception ex)
            {

                objWebReq = null;
                return false;
            }
        }
    }
}
