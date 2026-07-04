using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Models.AI
{
    public class ChatMessageModel
    {
        public string Role { get; set; } = "";

        public string Message { get; set; } = "";
    }
}
