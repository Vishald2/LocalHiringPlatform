using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models
{
    public class EmailRequestModel
    {
        public string To { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string HtmlBody { get; set; } = string.Empty;

        public string? PlainTextBody { get; set; } = string.Empty;

        public List<string> Cc { get; set; } = [];

        public List<string> Bcc { get; set; } = [];
    }
}
