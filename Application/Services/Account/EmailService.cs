using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Account
{
    public class EmailService
    {
        public async Task ExecuteAsync( EmailModel email)
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("Mntagroup@gmail.com", "NadinSoft"),
                To = { email.To },
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = false,
            };
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 587) 
            {
                Credentials = new System.Net.NetworkCredential("Mntagroup@gmail.com", "gtqf vsyi ojby aamj"), 
                EnableSsl = true
            };
            await smtpServer.SendMailAsync(mail);
            await Task.CompletedTask;
        }
    }

    public class EmailModel
    {
        public EmailModel(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
