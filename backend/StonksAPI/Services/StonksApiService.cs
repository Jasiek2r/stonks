using AutoMapper;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using StonksAPI.Utility;
using StonksAPI.Utility.Parsers;
using StonksAPI.DTO.Quotation;
using StonksAPI.DTO.GeneralAssetInformation;
using StonksAPI.DTO.Dividend;
using System.Reflection.Metadata.Ecma335;

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
        private readonly string _apiKey;
        private readonly IParserFacade _parserFacade;
        public StonksApiService(IConfiguration configuration, HttpClient httpClient, IParserFacade parserFacade)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _parserFacade = parserFacade;
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


        public async Task<Quotations> GetAssetData(string ticker, string interval)
        {
            // map interval to path (i.e. Daily -> TIME_SERIES_DAILY and so on)
            MapIntervalRoute.IntervalRoute = interval;
            var intervalRoute = MapIntervalRoute.IntervalRoute;

            var url = $"https://www.alphavantage.co/query?function={intervalRoute}&symbol={ticker}&apikey={_apiKey}";
            var jsonString = await FetchJson(url);
            Quotations quotations = (Quotations)_parserFacade.ParseJsonResponse<Quotations>(jsonString);

            //return Quotations to the controller
            return quotations;
        }
        public async Task<Quotations> GetIntradayAssetData(string ticker, string interval)
        {
            var url = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={ticker}&interval={interval}&apikey={_apiKey}";
            var jsonString = await FetchJson(url);
            Quotations quotations = (Quotations)_parserFacade.ParseJsonResponse<Quotations>(jsonString);

            //return Quotations to the controller
            return quotations;
        }

        public async Task<GeneralAssetInformation> GetGeneralInformation(string ticker)
        {
            var url = $"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={ticker}&apikey={_apiKey}";
            var jsonString = await FetchJson(url);
            GeneralAssetInformation assetInformation =
                (GeneralAssetInformation)_parserFacade.ParseJsonResponse<GeneralAssetInformation>(jsonString);

            //return General Asset Information to the controller
            return assetInformation;
        }
        public async Task<Dividends> GetDividends(string ticker)
        {
            var url = $"https://www.alphavantage.co/query?function=DIVIDENDS&symbol={ticker}&apikey={_apiKey}";
            var jsonString = await FetchJson(url);
            Dividends dividends =
                (Dividends)_parserFacade.ParseJsonResponse<Dividends>(jsonString);
            //return a list of dividends to the controller
            return dividends;
        }
        public async Task<CompanyOverview> GetCompanyOverview(string ticker)
        {
            var url = $"https://www.alphavantage.co/query?function=OVERVIEW&symbol={ticker}&apikey=demo";
            var jsonString = await FetchJson(url);
            CompanyOverview companyOverview =
                (CompanyOverview)_parserFacade.ParseJsonResponse<CompanyOverviewResponse>(jsonString);
            //return a list of dividends to the controller
            return companyOverview;
        }
    }
}
