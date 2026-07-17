using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.ServiceBus.Interfaces;
using LocalHiringPlatform.ServiceBus.Messages;
using LocalHiringPlatform.ServiceBus.Services;

namespace LocalHiringPlatform.Worker.Handlers;

public class EmailMessageHandler : IMessageHandler<EmailRequestModel>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<EmailMessageHandler> _logger;

    public EmailMessageHandler(
        IEmailService emailService,
        ILogger<EmailMessageHandler> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task HandleAsync(
        EmailRequestModel message,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Sending email to {Email}",
            message.To);


        var emailRequestModel = new EmailRequestModel
        {
            To = message.To,
            Subject = message.Subject,
            HtmlBody = message.HtmlBody
        };


        await _emailService.SendEmailAsync(emailRequestModel);

        _logger.LogInformation(
            "Email sent successfully to {Email}",
            message.To);
    }
}