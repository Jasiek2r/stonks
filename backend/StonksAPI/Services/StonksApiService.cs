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

        public StonksApiService(IConfiguration configuration, HttpClient httpClient, IMapper autoMapper) {
            _configuration = configuration;
            _httpClient = httpClient;
            _autoMapper = autoMapper;
        }


        public async Task<Quotations> GetAssetData(string ticker, string interval)
        {
            // map interval to path (i.e. Daily -> TIME_SERIES_DAILY and so on)
            MapIntervalRoute.IntervalRoute = interval;
            var intervalRoute = MapIntervalRoute.IntervalRoute;

            // fetch data from an external stock market API source
            string apiKey = _configuration["ApiSettings:ApiKey"]!;
            var url = $"https://www.alphavantage.co/query?function={intervalRoute}&symbol={ticker}&apikey={apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            //stringify and deserialize the data
            var jsonString = await response.Content.ReadAsStringAsync();

            //deserialize object
            ApiResponse jsonResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);
           
            //convert to uniform internal format
            ApiResponseAdapter apiResponseAdapter = new ApiResponseAdapter(jsonResponse);
            ApiSeries jsonSeries = apiResponseAdapter.extractSeries();
            
            //map to Quotations object            
            Quotations quotations = _autoMapper.Map<Quotations>(jsonSeries);

            //return Quotations to the controller
            return quotations;
        }
    }
}
