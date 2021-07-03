using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CrowdfindingApp.Common.Configs;
using CrowdfindingApp.Common.Immutable;
using CrowdfindingApp.Common.Core.Localization;

namespace CrowdfindingApp.Common.Core.Maintainers.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _client;
        private readonly Configs.EmailConfig _config;
        private readonly IResourceProvider _resourceProvider;
        private readonly MailAddress From;

        private const string ResetPasswordSubjectKey = "ResetPasswordEmailMessageSubject";
        private const string ResetPasswordBodyKey = "ResetPasswordEmailMessageBody";
        private const string ConfirmSubjectKey = "ConfirmEmailMessageSubject";
        private const string ConfirmBodyKey = "ConfirmEmailMessageBody";
        private const string ResettedPaswordKey = nameof(ResettedPaswordKey);
        private const string ResettedPaswordBody = nameof(ResettedPaswordBody);

        public EmailSender(SmtpClient client, AppConfiguration configuration, IResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
            _config = configuration?.EmailConfig ?? throw new ArgumentNullException(nameof(configuration));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            ConfigureClient();

            From = new MailAddress(_config.Mail, "Crowdfinding team");
        }

        private void ConfigureClient()
        {
            _client.Host = _config.Host;
            _client.Port = _config.Port;
            _client.Credentials = new NetworkCredential(_config.Mail, _config.Password);
            _client.EnableSsl = true;
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            _client.UseDefaultCredentials = false;
        }

        public async Task SendEmailConfirmationAsync(string email, string token)
        {
            var to = new MailAddress(email);
            var url = $"{_config.Host}/{Endpoints.User.EmailConfirmation}?token={token}";
            var message = new MailMessage(From, to)
            {
                Subject = _resourceProvider.GetString(ConfirmSubjectKey),
                Body = _resourceProvider.GetString(ConfirmBodyKey, url),
                IsBodyHtml = true
            };

            await _client.SendMailAsync(message);
        }

        public async Task SendResetPasswordUrlAsync(string email, string token)
        {
            var to = new MailAddress(email);
            var url = $"{_config.Host}/{Endpoints.User.ResetPassword}?token={token}";
            var message = new MailMessage(From, to)
            {
                Subject = _resourceProvider.GetString(ResetPasswordSubjectKey),
                Body = _resourceProvider.GetString(ResetPasswordBodyKey, url),
                IsBodyHtml = true,
            };

            await _client.SendMailAsync(message);
        }

        public async Task SendResettedPasswordAsync(string email, string password)
        {
            var to = new MailAddress(email);
            var message = new MailMessage(From, to)
            {
                Subject = _resourceProvider.GetString(ResettedPaswordKey),
                Body = _resourceProvider.GetString(ResettedPaswordBody, password),
                IsBodyHtml = false,
            };

            await _client.SendMailAsync(message);
        }
    }
}
