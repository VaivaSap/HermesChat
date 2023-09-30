using Humanizer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace HermesChat_TeamA
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {

            var mail = "testastestauskas43@gmail.com";
            var password = "vkdpimzmrkqphlaq";
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail,
                    to: email,
                    subject,
                    message
                    ));
        }
    }
}
