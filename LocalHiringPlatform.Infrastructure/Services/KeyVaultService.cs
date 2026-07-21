using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using LocalHiringPlatform.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly SecretClient _secretClient;

        public KeyVaultService(IConfiguration configuration)
        {
            var keyVaultUrl = configuration["KeyVault:Url"]!;

            _secretClient = new SecretClient(
                new Uri(keyVaultUrl),
                new DefaultAzureCredential());
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);

            return secret.Value;
        }
    }
}
