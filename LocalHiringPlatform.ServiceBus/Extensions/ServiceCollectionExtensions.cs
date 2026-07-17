namespace LocalHiringPlatform.ServiceBus.Extensions
{
    using global::LocalHiringPlatform.ServiceBus.Configuration;
    using global::LocalHiringPlatform.ServiceBus.Interfaces;
    using global::LocalHiringPlatform.ServiceBus.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBus(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<ServiceBusOptions>(
                configuration.GetSection(ServiceBusOptions.SectionName));

            services.AddSingleton<IServiceBusPublisher, ServiceBusPublisher>();

            return services;
        }
    }
}
