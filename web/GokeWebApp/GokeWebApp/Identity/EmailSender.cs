using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Goke.AspNetCore.Identity.Models;
using GokeWebApp.Data;

namespace Goke.AspNetCore.Identity
{
    public class EmailSender : IEmailSender<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public EmailSender(
                           //IOptions<AuthMessageSenderOptions> optionsAccessor,
                           IConfiguration configuration,
                           ILogger<EmailSender> logger)
        {
            _configuration = configuration;
            //Options = optionsAccessor.Value;
            _logger = logger;
        }

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
            SendEmailAsync(email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>. <br> Copy into browser: <p>{confirmationLink}</p>");

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
            SendEmailAsync(email, "Reset your password", $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
            SendEmailAsync(email, "Reset your password", $"Please reset your password using the following code: {resetCode}");


        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            var MAIL_SERVER = _configuration["MAIL_SERVER"];
            var MAIL_PORT = int.Parse(_configuration["MAIL_PORT"] ?? "0");
            var MAIL_USERNAME = _configuration["MAIL_USERNAME"];
            var MAIL_PASSWORD = _configuration["MAIL_PASSWORD"];
            var ADMIN_EMAIL = _configuration["ADMIN_EMAIL"] ?? "admin@goke.me";

            using (var message = new MailMessage())
            {
                message.From = new MailAddress(ADMIN_EMAIL);
                message.To.Add(new MailAddress(email));
                message.Subject = subject;
                message.Body = htmlMessage;
                message.IsBodyHtml = true;

                using var client = new SmtpClient
                {
                    Port = MAIL_PORT,
                    Host = MAIL_SERVER!, // Replace with your SMTP host
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(MAIL_USERNAME, MAIL_PASSWORD),
                };
                await client.SendMailAsync(message);
            }
        }


    }
}
