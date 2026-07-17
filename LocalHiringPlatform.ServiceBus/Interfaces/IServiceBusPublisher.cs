using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.ServiceBus.Interfaces
{
    public interface IServiceBusPublisher
    {
        Task PublishAsync<T>(
            T message,
            CancellationToken cancellationToken = default);
    }
}
