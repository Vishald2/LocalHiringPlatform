using Azure.Messaging.ServiceBus;
using global::LocalHiringPlatform.ServiceBus.Configuration;
using global::LocalHiringPlatform.ServiceBus.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace LocalHiringPlatform.ServiceBus.Services
{
    public class ServiceBusPublisher : IServiceBusPublisher, IAsyncDisposable
    {
        private readonly ServiceBusSender _sender;

        public ServiceBusPublisher(
            IOptions<ServiceBusOptions> options)
        {
            var client = new ServiceBusClient(options.Value.ConnectionString);

            _sender = client.CreateSender(options.Value.QueueName);
        }

        public async Task PublishAsync<T>(
                    T message,
                    CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(message);

            var serviceBusMessage = new ServiceBusMessage(json)
            {
                ContentType = "application/json"
            };

            serviceBusMessage.ApplicationProperties.Add(
                    "MessageType",
                    typeof(T).Name);

            await _sender.SendMessageAsync(
                    serviceBusMessage,
                    cancellationToken);
        }

        public async ValueTask DisposeAsync()
        {
            await _sender.DisposeAsync();
        }
    }
}
