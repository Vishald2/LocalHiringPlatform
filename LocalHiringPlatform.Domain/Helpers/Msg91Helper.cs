using LocalHiringPlatform.Domain.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Domain.Helpers
{
    public class Msg91Helper
    {
        private readonly HttpClient _httpClient;
        private readonly Msg91Settings _msg91Settings;

        public Msg91Helper(
    HttpClient httpClient,
    IOptions<Msg91Settings> options)
        {
            _httpClient = httpClient;
            _msg91Settings = options.Value;
        }

        public async Task<string> VerifyAccessTokenAsync(
            string accessToken)
        {
            var authToken =
                _msg91Settings.WidgetAuthToken;

            var request = new HttpRequestMessage(
    HttpMethod.Post,
    _msg91Settings.BaseUrl +
    _msg91Settings.VerifyAccessTokenEndpoint);

            //request.Headers.Authorization =
            //    new AuthenticationHeaderValue(
            //        "Bearer",
            //        _msg91Settings.AuthKey);


            request.Headers.Add(
                            "authkey",
                            _msg91Settings.AuthKey);

            request.Headers.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            var requestBody =
                new Dictionary<string, string>
                {
                    { "access-token", accessToken }
                };

            request.Content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
