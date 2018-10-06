using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace TeamDaze.BLL.DAL
{
   
    public class Sendmail
    {
        public void SendMailAlerts(string Amount, string Receipient)
        {
            string to = Receipient; //To address    
            string from = "teamdaze43@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Hi there, This is an alert for the payment you just made on Touch 'N' Pay platform Amount ";
            mailbody += "Amount:" + Amount + "Naira <br> If you did not carry out this transaction kindly contact us at teamdaze43@gmail.com or 08105931866";
            message.Subject = "Touch 'N' Pay Transaction Alert";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("teamdaze43@gmail.com", "@teamdaze123");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                new ErrorLog(ex);
                throw ex;
            }
        }
    }
       
        
}
