using Azure.Messaging.ServiceBus;
using global::LocalHiringPlatform.ServiceBus.Configuration;
using global::LocalHiringPlatform.ServiceBus.Interfaces;
using LocalHiringPlatform.ServiceBus.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace LocalHiringPlatform.ServiceBus.Services
{
    // The class must declare the generic parameter T

    public class ServiceBusConsumer : IServiceBusConsumer,
                                        IAsyncDisposable
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusProcessor _processor;
        private readonly ILogger<ServiceBusConsumer> _logger;

        private readonly Dictionary<string, Func<string, CancellationToken, Task>>
                _handlers = new();

        private readonly Dictionary<string, Func<string, CancellationToken, Task>> _handlers2 =
         new Dictionary<string, Func<string, CancellationToken, Task>>();


        private readonly IServiceScopeFactory _scopeFactory;

        public ServiceBusConsumer(
            IOptions<ServiceBusOptions> options,
            ILogger<ServiceBusConsumer> logger,
            IServiceScopeFactory serviceScopeFactory
            )
        {
            _scopeFactory = serviceScopeFactory;
            _logger = logger;

            _client = new ServiceBusClient(options.Value.ConnectionString);

            _processor = _client.CreateProcessor(
                options.Value.QueueName);
        }

        public void RegisterHandler2(string messageType,
    Func<string, CancellationToken, Task> handler)
        {

            _handlers2[messageType]=handler;

            //_handlers[typeof(T).Name] = async (json, cancellationToken) =>
            //{
            //    var message = JsonSerializer.Deserialize<T>(json);

            //    if (message == null)
            //    {
            //        throw new InvalidOperationException(
            //            $"Unable to deserialize message to {typeof(T).Name}.");
            //    }

            //    await handler(message, cancellationToken);
            //};
        }

        public void RegisterHandler<T>(
            Func<T, CancellationToken, Task> handler)
        {

            _handlers[typeof(T).Name] = async (json, cancellationToken) =>
            {
                var message = JsonSerializer.Deserialize<T>(json);

                if (message == null)
                {
                    throw new InvalidOperationException(
                        $"Unable to deserialize message to {typeof(T).Name}.");
                }

                await handler(message, cancellationToken);
            };
        }

        public async Task StartAsync(
            CancellationToken cancellationToken = default)
        {
            _processor.ProcessMessageAsync += ProcessMessageAsync;
            _processor.ProcessErrorAsync += ProcessErrorAsync;

            await _processor.StartProcessingAsync(cancellationToken);

            _logger.LogInformation("Service Bus consumer started.");
        }

        public async Task StopAsync(
            CancellationToken cancellationToken = default)
        {
            await _processor.StopProcessingAsync(cancellationToken);

            _logger.LogInformation("Service Bus consumer stopped.");
        }

        public async ValueTask DisposeAsync()
        {
            await _processor.DisposeAsync();
            await _client.DisposeAsync();
        }

        private async Task ProcessMessageAsync(
            ProcessMessageEventArgs args)
        {
            var json = args.Message.Body.ToString();

            if (!args.Message.ApplicationProperties.TryGetValue(
                ServiceBusConstants.MessageType,
                out var messageTypeValue))
            {
                throw new InvalidOperationException(
                    "MessageType property is missing.");
            }

            var messageType = messageTypeValue?.ToString();

            if (string.IsNullOrWhiteSpace(messageType))
            {
                await args.CompleteMessageAsync(args.Message);
                throw new InvalidOperationException(
                    "MessageType property is empty.");
            }

            if (!_handlers.TryGetValue(messageType, out var handlerWrapper))
            {
                throw new InvalidOperationException(
                    $"No handler registered for '{messageType}'.");
            }

            if (!_handlers2.TryGetValue(messageType, out var handlerWrapper2))
            {
                throw new InvalidOperationException(
                    $"No handler registered for '{messageType}'.");
            }

            _logger.LogInformation(
                "Processing message of type {MessageType}",
                messageType);

            try
            {
                await handlerWrapper2(json, args.CancellationToken);

               // await handlerWrapper(json, args.CancellationToken);

                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error processing {MessageType}",
                    messageType);

                throw;
            }
        }

        private Task ProcessErrorAsync(
            ProcessErrorEventArgs args)
        {
            _logger.LogError(
                args.Exception,
                "Azure Service Bus error.");

            return Task.CompletedTask;
        }
    }
}
