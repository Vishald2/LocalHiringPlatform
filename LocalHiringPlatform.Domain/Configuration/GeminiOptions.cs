using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Configuration
{
    public class GeminiOptions
    {
        public string ApiKey { get; set; } = string.Empty;

        public string GeminiEndpoint { get; set; } = string.Empty;
    }
}
