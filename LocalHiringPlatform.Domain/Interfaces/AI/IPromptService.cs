using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Interfaces.AI
{
    public interface IPromptService
    {
        Task<string> GetPromptAsync(
            string promptFileName);
    }
}
