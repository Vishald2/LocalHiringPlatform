using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Helpers
{
    public class SendOtpRequest
    {
            [JsonPropertyName("template_id")]
            public string TemplateId { get; set; } = string.Empty;

            [JsonPropertyName("mobile")]
            public string Mobile { get; set; } = string.Empty;

            [JsonPropertyName("authkey")]
            public string AuthKey { get; set; } = string.Empty;
    }
}
