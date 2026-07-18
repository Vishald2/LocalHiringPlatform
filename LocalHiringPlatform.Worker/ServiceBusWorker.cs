using LocalHiringPlatform.Domain.Models;
using LocalHiringPlatform.ServiceBus.Interfaces;
using LocalHiringPlatform.ServiceBus.Messages;

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
            _consumer.RegisterHandler<EmailRequestModel>(
                HandleOutboundEmailAsync);

            await _consumer.StartAsync(stoppingToken);
        }

        private async Task HandleOutboundEmailAsync(
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
