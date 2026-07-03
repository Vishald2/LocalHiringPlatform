using LocalHiringPlatform.Domain.Configuration;
using LocalHiringPlatform.Domain.Interfaces.AI;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Services.AI
{
    public class PromptService : IPromptService
    {
        private readonly IOptions<ApplicationSettings> _applicationSettings;

        public PromptService(
            IOptions<ApplicationSettings> applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        public async Task<string> GetPromptAsync(
            string promptFileName)
        {
            var filePath =
                Path.Combine(
                    _applicationSettings.Value.PromptFolder,
                    promptFileName);

            return await File.ReadAllTextAsync(filePath);
        }
    }
}
