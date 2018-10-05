using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace TeamDaze.BLL.DAL
{
    public class Mailer
    {

        private readonly MailMessage _mail;
        private readonly SmtpClient _smtpServer;
        public Mailer()
        {
            

            MailMessage mail = new MailMessage();



            SmtpClient smtpServer = new SmtpClient
            {
                Credentials = new System.Net.NetworkCredential
                    ("davniyi3@gmail.com", ""),
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true
            };
            _smtpServer = smtpServer;
            mail.From = new MailAddress("davniyi3@gmail.com",
                "Hackathon Demo-Mailer", System.Text.Encoding.UTF8);
            _mail = mail;
        }
        public void SendMail(string mailTo, string subject, string body)
        {

            _mail.To.Add(mailTo);
            _mail.Subject = subject;
            _mail.Body = body;

            _smtpServer.Send(_mail);

        }
        public void SendMail(List<string> mailTo, string subject, string body)
        {
            if (mailTo != null)
            {
                foreach (var mail in mailTo)
                {
                    _mail.To.Add(mail);
                }

                _mail.Subject = subject;
                _mail.Body = body;

                _smtpServer.Send(_mail);
            }


        }

        public void SendHtmlMail(string mailTo, string subject, string body)
        {

            _mail.To.Add(mailTo);
            _mail.Subject = subject;
            _mail.Body = body;
            _mail.IsBodyHtml = true;

            _smtpServer.Send(_mail);

        }


        public void SendEmbededImageWithMail(string image, string mailTo, string subject)
        {
            //var imageseparated = image.Replace("data:image/png;base64,", "");
            var newName = $"{Guid.NewGuid().ToString("N")}.jpg";

            var directory = $"{HttpContext.Current.Server.MapPath("~/MediaItems/Invoice")}";

            if (!System.IO.Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var filePath = $"{directory}/{newName}";

            var bytes = Convert.FromBase64String(image);
            using (var imageFile = new FileStream(filePath, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }



            _mail.Subject = subject;
            _mail.IsBodyHtml = true;
            _mail.AlternateViews.Add(getEmbeddedImage(filePath));
            _mail.To.Add(mailTo);

            _smtpServer.Send(_mail);
        }
        private AlternateView getEmbeddedImage(String filePath)
        {
            LinkedResource res = new LinkedResource(filePath);
            res.ContentId = Guid.NewGuid().ToString();
            string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
            return alternateView;
        }



    }

}