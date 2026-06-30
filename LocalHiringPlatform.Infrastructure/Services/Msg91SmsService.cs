using LocalHiringPlatform.Domain.Configuration;
using LocalHiringPlatform.Domain.Interfaces;
using LocalHiringPlatform.Infrastructure.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LocalHiringPlatform.Infrastructure.Services
{
    public class Msg91SmsService : ISmsService
    {
        private readonly HttpClient _httpClient;
        private readonly Msg91Settings _settings;

        public Msg91SmsService(
            HttpClient httpClient,
            IOptions<Msg91Settings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task SendOtpAsync(string mobileNumber)
        {
            var request = new SendOtpRequest
            {
                Mobile = mobileNumber,
                AuthKey = _settings.AuthKey,
                TemplateId = _settings.TemplateId
            };

            // var response = await _httpClient.PostAsJsonAsync("otp", request);

            var response = await _httpClient.PostAsJsonAsync("otp", request);
            //, new JsonSerializerOptions);
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //});

            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine("====================================");
            Console.WriteLine(responseBody);
            Console.WriteLine("====================================");
        }

        public async Task<OtpVerificationResult> VerifyOtpAsync(string mobileNumber, string otp)
        {
            throw new NotImplementedException();
        }
    }

    class SendOtpRequest
    {
        [JsonPropertyName("template_id")]
        public string TemplateId { get; set; } = string.Empty;

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; } = string.Empty;

        [JsonPropertyName("authkey")]
        public string AuthKey { get; set; } = string.Empty;
    }

    class SendOtpResponse
    {
    }

    class VerifyOtpRequest
    {
    }

    class VerifyOtpResponse
    {
    }
}
