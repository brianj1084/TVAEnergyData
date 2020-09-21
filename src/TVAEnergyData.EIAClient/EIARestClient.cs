using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TVAEnergyData.EIAClient.Models;

namespace TVAEnergyData.EIAClient
{
    public class EIARestClient : IEIARestClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public EIARestClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(_configuration["EIASettings:BaseUrl"]);
        }
        public async Task<EIAResponse> GetSeries(string seriesId, string start = null, string end = null)
        {
            var requestUrl = BuildRequestUrl($"/series/?series_id={seriesId}");

            if (start != null) requestUrl += $"&start={start}";
            if (end != null) requestUrl += $"&end={end}";

            var jsonString = await _httpClient.GetStringAsync(requestUrl);

            return EIAResponse.FromJson(jsonString);
        }

        private string BuildRequestUrl(string url)
        {
            var apiKey = _configuration["EIASettings:APIKey"];
            return url.Contains("?") ? $"{url}&api_key={apiKey}" : $"{url}?api_key={apiKey}";
        }
    }
}
