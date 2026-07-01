using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Configuration
{
    public class Msg91Settings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string AuthKey { get; set; } = string.Empty;
        public string TemplateId { get; set; } = string.Empty;
        public string WidgetId { get; set; } = string.Empty;
        public string WidgetAuthToken { get; set; } = string.Empty;
        public string VerifyAccessTokenEndpoint { get; set; } = string.Empty;
    }
}
