using LocalHiringPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models.AI
{
    public class AIIntentHandlerResponse
    {
        public string Intent { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Tag { get; set; }= string.Empty;
        public object? Data { get; set; } = null;
    }
}
