using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Helpers
{
    public class Msg91Helper
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public Msg91Helper(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> VerifyAccessTokenAsync(
            string accessToken)
        {
            var authToken =
                _configuration["Msg91:WidgetAuthToken"];

            var request =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    "https://control.msg91.com/api/v5/widget/verifyAccessToken");

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    authToken);

            request.Content =
                new StringContent(
                    $"{{\"accessToken\":\"{accessToken}\"}}",
                    Encoding.UTF8,
                    "application/json");

            var response =
                await _httpClient.SendAsync(request);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
