using System;
using System.Net;
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
        private readonly IConfigurationRoot _config;
        private readonly IResourceProvider _resourceProvider;
        private readonly MailAddress From;

        private const string ResetPasswordSubjectKey = "ResetPasswordEmailMessageSubject";
        private const string ResetPasswordBodyKey = "ResetPasswordEmailMessageBody";
        private const string ConfirmSubjectKey = "ConfirmEmailMessageSubject";
        private const string ConfirmBodyKey = "ConfirmEmailMessageBody";
        private const string ResettedPaswordKey = nameof(ResettedPaswordKey);

        private string _clientAppHost
        {
            get => _config[Configuration.ClientHost];
        }

        public EmailSender(SmtpClient client, IConfigurationRoot configuration, IResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            ConfigureClient();

            From = new MailAddress(_config[$"{EmailConfig.Section}:{EmailConfig.Mail}"], "Crowdfinding team");
        }

        private void ConfigureClient()
        {
            _client.Host = _config[$"{EmailConfig.Section}:{EmailConfig.Host}"];
            _client.Port = int.Parse(_config[$"{EmailConfig.Section}:{EmailConfig.Port}"]);
            _client.Credentials = new NetworkCredential(_config[$"{EmailConfig.Section}:{EmailConfig.Mail}"], _config[$"{EmailConfig.Section}:{EmailConfig.Password}"]);
            _client.EnableSsl = true;
        }

        public async Task SendEmailConfirmationAsync(string email, string token)
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

        public async Task SendResetPasswordUrlAsync(string email, string token)
        {
            var to = new MailAddress(email);
            var url = $"{_clientAppHost}/{Endpoints.User.ResetPassword}?token={token}";
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
                Subject = _resourceProvider.GetString(ResetPasswordSubjectKey),
                Body = _resourceProvider.GetString(ResetPasswordBodyKey, password),
                IsBodyHtml = false,
            };

            await _client.SendMailAsync(message);
        }
    }
}
