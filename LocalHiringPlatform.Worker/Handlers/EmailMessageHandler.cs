using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.ServiceBus.Interfaces;
using LocalHiringPlatform.ServiceBus.Messages;
using LocalHiringPlatform.ServiceBus.Services;

namespace LocalHiringPlatform.Worker.Handlers;

public class EmailMessageHandler : IMessageHandler<OutboundEmailMessage>
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
        OutboundEmailMessage message,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Sending email to {Email}",
            message.To);


        var message2 = new EmailRequestModel
        {
            To = "vishald3511@gmail.com",
            Subject = "Azure Service Bus Test",
            HtmlBody = "<h2>Hello from LocalHire</h2>"
        };


        await _emailService.SendEmailAsync(message2);

        _logger.LogInformation(
            "Email sent successfully to {Email}",
            message.To);
    }
}