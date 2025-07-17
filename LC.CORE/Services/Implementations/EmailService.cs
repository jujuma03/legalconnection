using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.CORE.Services.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.CORE.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;
        private readonly IViewRenderService _viewRenderService;

        public EmailService(
            IOptions<EmailConfig> emailConfig,
            IViewRenderService viewRenderService
            )
        {
            _emailConfig = emailConfig.Value;
            _viewRenderService = viewRenderService;
        }
        public Task SendEmail(string subject, string message, params string[] emails)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(ConstantHelpers.PROJECT, _emailConfig.UserEmailAddress));

                foreach (var email in emails)
                    emailMessage.To.Add(new MailboxAddress("", email));

                emailMessage.Cc.Add(new MailboxAddress(ConstantHelpers.PROJECT, _emailConfig.UserEmailAddress));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(TextFormat.Html) { Text = message };

                var client = new SmtpClient();
                var action = Task.Run(async () =>
                {
                    await client.ConnectAsync(_emailConfig.MailServerAddress, Convert.ToInt32(_emailConfig.MailServerPort));
                    await client.AuthenticateAsync(_emailConfig.UserEmailAddress, _emailConfig.UserPassword);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                });

                return action;

            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }
    }
}
