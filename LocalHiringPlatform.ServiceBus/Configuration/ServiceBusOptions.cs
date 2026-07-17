using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.ServiceBus.Configuration
{
    public class ServiceBusOptions
    {
        public const string SectionName = "AzureServiceBus";

        public string ConnectionString { get; set; } = string.Empty;

        public string QueueName { get; set; } = string.Empty;
    }
}
