using AutoMapper;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using StonksAPI.Utility;
using StonksAPI.DTO;

namespace StonksAPI.Services
{
    public class StonksApiService : IStonksApiService
    {
        /*
         * This class is going to outsource API logic as to not make controllers too bloated
         * and make the codebase more maintainable.
         */

        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IMapper _autoMapper;
        private readonly string _apiKey;

        public StonksApiService(IConfiguration configuration, HttpClient httpClient, IMapper autoMapper) {
            _configuration = configuration;
            _httpClient = httpClient;
            _autoMapper = autoMapper;
            _apiKey = _configuration["ApiSettings:ApiKey"]!;
        }


        // Helper method for fetching data from alphavantage API
        private async Task<string> FetchJson(string url)
        {
            // fetch data from an external stock market API source
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            //stringify and deserialize the data
            var jsonString = await response.Content.ReadAsStringAsync();

            return jsonString;
        }


        //Helper method for converting raw json string to our Quotations object
        private Quotations ParseJsonResponse(string jsonString)
        {
            //deserialize object
            ApiResponse jsonResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);

            //convert to uniform internal format
            ApiResponseAdapter apiResponseAdapter = new ApiResponseAdapter(jsonResponse);
            ApiSeries jsonSeries = apiResponseAdapter.extractSeries();

            //map to Quotations object            
            Quotations quotations = _autoMapper.Map<Quotations>(jsonSeries);

            return quotations;
        }


        public async Task<Quotations> GetAssetData(string ticker, string interval)
        {
            // map interval to path (i.e. Daily -> TIME_SERIES_DAILY and so on)
            MapIntervalRoute.IntervalRoute = interval;
            var intervalRoute = MapIntervalRoute.IntervalRoute;

            var url = $"https://www.alphavantage.co/query?function={intervalRoute}&symbol={ticker}&apikey={_apiKey}";
            var jsonString = await FetchJson(url);
            Quotations quotations = ParseJsonResponse(jsonString);

            //return Quotations to the controller
            return quotations;
        }
        public async Task<Quotations> GetIntradayAssetData(string ticker, string interval)
        {
            var url = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={ticker}&interval={interval}&apikey={_apiKey}";
            var jsonString = await FetchJson(url);
            Quotations quotations = ParseJsonResponse(jsonString);

            //return Quotations to the controller
            return quotations;
        }
    }
}
