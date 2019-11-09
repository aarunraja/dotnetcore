using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MLC.Core
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<SMTPSettings> _settings;
        public EmailService(IOptions<SMTPSettings> settings)
        {
            _settings = settings;
        }
        public async Task SendEmail(string email, string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _settings.Value.Username,
                    Password = _settings.Value.Password
                };

                client.Credentials = credential;
                client.Host = _settings.Value.Host;
                client.Port = int.Parse(_settings.Value.Port);
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress("admin@mlc.com");
                    emailMessage.Subject = subject;
                    emailMessage.IsBodyHtml = true;
                    emailMessage.Body = message;
                    client.Send(emailMessage);
                }
            }
            await Task.CompletedTask;
        }
    }
}
