using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.ServiceBus.Interfaces
{
    public interface IServiceBusConsumer
    {
        void RegisterHandler<T>(
            Func<T, CancellationToken, Task> handler);

        void RegisterHandler2(string messageType, 
            Func<string, CancellationToken, Task> handler);

        Task StartAsync(
            CancellationToken cancellationToken = default);

        Task StopAsync(
            CancellationToken cancellationToken = default);
    }
}
