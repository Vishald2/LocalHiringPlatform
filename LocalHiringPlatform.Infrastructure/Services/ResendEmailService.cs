using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Resend;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class ResendEmailService : IEmailService
    {
        private readonly IResend _resend;

        private readonly ResendSettings _settings;
        private readonly ILogger _logger;

        public ResendEmailService(
            IResend resend,
            IOptions<ResendSettings> settings,
            ILogger<ResendEmailService> logger)
        {
            _resend = resend;
            _settings = settings.Value;
            _logger = logger;
        }

       public async Task SendEmailAsync(EmailRequestModel request)
        {
            if (string.IsNullOrWhiteSpace(request.To))
                throw new ArgumentException("Recipient email is required.");

            if (string.IsNullOrWhiteSpace(request.Subject))
                throw new ArgumentException("Email subject is required.");

            var message = new EmailMessage();

            message.From = $"{_settings.FromName} <{_settings.FromEmail}>";

            message.To.Add(request.To);

            message.Subject = request.Subject;

            message.HtmlBody = request.HtmlBody;

            try
            {
                _logger.LogInformation("Sending email to {Email}", request.To);
                var response = await _resend.EmailSendAsync(message);

                _logger.LogInformation("Successfully sent email to {Email}", request.To);

                // validate response
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email.");

                throw;
            }

           
        }
    }
}
