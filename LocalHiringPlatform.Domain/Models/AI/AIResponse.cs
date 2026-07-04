using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models.AI
{
    public class AIResponse
    {
        public string Intent { get; set; } = string.Empty;

        public string Response { get; set; } = string.Empty;
    }
}
