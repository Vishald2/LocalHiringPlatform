using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.AI
{
    public interface ILLMService
    {
        Task<string> GenerateAsync(
            string prompt);
    }
}
