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
        public void SendMailAlerts(string Receipient, string mailbody, string subject)
        {
            string to = Receipient; //To address    
            string from = "teamdaze43@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            message.Subject = subject;
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
