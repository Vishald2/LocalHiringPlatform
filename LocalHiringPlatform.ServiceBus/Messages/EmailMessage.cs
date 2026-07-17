using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.ServiceBus.Messages
{
    public class OutboundEmailMessage
    {
        public string To { get; set; } = string.Empty;

        public string From { get; set; } = "no-reply@getlocalhire.com";

        public string Subject { get; set; } = string.Empty;

        public string HtmlBody { get; set; } = string.Empty;
    }
}
