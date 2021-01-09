using System;
using System.Net.Mail;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Localization;
using Microsoft.Extensions.Configuration;

namespace CrowdfindingApp.Common.Maintainers.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _client;
        private readonly IConfiguration _config;
        private readonly IResourceProvider _resourceProvider;

        private const string ResetPasswordSubjectKey = "ResetPasswordEmailMessageSubject";
        private const string ResetPasswordBodyKey = "ResetPasswordEmailMessageBody";
        private const string ConfirmSubjectKey = "ConfirmEmailMessageSubject";
        private const string ConfirmBodyKey = "ConfirmEmailMessageBody";

        private MailAddress From
        {
            get
            {
                if(From == null) From = new MailAddress("internal.app.email@yandex.by");
                return From;
            }

            set => From = value;
        }

        private string _clientAppHost
        {
            get => _config["ClientAppHost"];
        }

        public EmailSender(SmtpClient client, IConfiguration configuration, IResourceProvider resourceProvider)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _resourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
        }

        public async Task SendEmailConfirmationAsync(string email, string token = "")
        {
            var to = new MailAddress(email);
            var url = $"{_clientAppHost}/{Endpoints.User.EmailConfirmation}?token={token}";
            var message = new MailMessage(From, to)
            {
                Subject = _resourceProvider.GetString(ConfirmSubjectKey),
                Body = _resourceProvider.GetString(ConfirmBodyKey, url),
                IsBodyHtml = true
            };

            await _client.SendMailAsync(message);
        }

        public async Task SendResetPasswordUrlAsync(string email, string token = "")
        {
            var to = new MailAddress(email);
            var url = $"{_clientAppHost}/{Endpoints.User.ResetPassword}?token={token}";
            var message = new MailMessage(From, to)
            {
                Subject = _resourceProvider.GetString(ResetPasswordSubjectKey),
                Body = _resourceProvider.GetString(ResetPasswordBodyKey, url),
                IsBodyHtml = true
            };

            await _client.SendMailAsync(message);
        }
    }
}
