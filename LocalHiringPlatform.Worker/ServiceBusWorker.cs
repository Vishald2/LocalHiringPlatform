using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.ServiceBus.Interfaces;
using LocalHiringPlatform.ServiceBus.Messages;
using System.Text.Json;

namespace LocalHiringPlatform.Worker
{
    public class ServiceBusWorker : BackgroundService
    {
        private readonly IServiceBusConsumer _consumer;
        private readonly IServiceScopeFactory _scopeFactory;

        public ServiceBusWorker(
            IServiceBusConsumer consumer,
            IServiceScopeFactory scopeFactory)
        {
            _consumer = consumer;
            _scopeFactory = scopeFactory;
        }
            
        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            //_consumer.RegisterHandler<EmailRequestModel>(
            //    HandleOutboundEmailAsync);

            _consumer.RegisterHandler(typeof(EmailRequestModel).Name,
                HandleOutboundEmailAsync);

            await _consumer.StartAsync(stoppingToken);
        }

        private async Task HandleOutboundEmailAsync(
          string json,
          CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var handler =
                scope.ServiceProvider.GetRequiredService<
                    IMessageHandler<EmailRequestModel>>();

            var emailRequestModel = JsonSerializer.Deserialize<EmailRequestModel>(json);

            if (emailRequestModel == null)
            {
                throw new InvalidOperationException(
                    $"Unable to deserialize message to {typeof(EmailRequestModel).Name}.");
            }

            await handler.HandleAsync(
                emailRequestModel,
                cancellationToken);
        }

        private async Task HandleOutboundEmailAsyncOLD(
            EmailRequestModel message,
            CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();

            var handler =
                scope.ServiceProvider.GetRequiredService<
                    IMessageHandler<EmailRequestModel>>();

            await handler.HandleAsync(
                message,
                cancellationToken);
        }

        public override async Task StopAsync(
            CancellationToken cancellationToken)
        {
            await _consumer.StopAsync(cancellationToken);

            await base.StopAsync(cancellationToken);
        }
    }
}
