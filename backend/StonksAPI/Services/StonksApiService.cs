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
using StonksAPI.DTO.News;
using System.Collections.Concurrent;

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
        private readonly string[] _alternateKeys;
        private static readonly SemaphoreSlim _apiSemaphore = new SemaphoreSlim(1, 1);
        private static DateTime _lastApiCall = DateTime.MinValue;
        private const int API_CALL_DELAY_MS = 15000; // Zwiększamy do 15 sekund
        
        // Cache dla danych giełdowych
        private static readonly ConcurrentDictionary<string, (DateTime Timestamp, Quotations Data)> _quotationsCache = new();
        private static readonly ConcurrentDictionary<string, (DateTime Timestamp, NewsResponse Data)> _newsCache = new();
        private const int CACHE_DURATION_MINUTES = 5;
        private const int NEWS_CACHE_DURATION_MINUTES = 15; // Newsy cache'ujemy dłużej

        public StonksApiService(IConfiguration configuration, HttpClient httpClient, IParserFacade parserFacade)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _parserFacade = parserFacade;
            _apiKey = _configuration["ApiSettings:ApiKey"]!;
            _alternateKeys = _configuration.GetSection("ApiSettings:AlternateKeys").Get<string[]>() ?? Array.Empty<string>();
        }

        private async Task WaitForApiLimit()
        {
            await _apiSemaphore.WaitAsync();
            try
            {
                var timeSinceLastCall = DateTime.Now - _lastApiCall;
                if (timeSinceLastCall.TotalMilliseconds < API_CALL_DELAY_MS)
                {
                    await Task.Delay(API_CALL_DELAY_MS - (int)timeSinceLastCall.TotalMilliseconds);
                }
                _lastApiCall = DateTime.Now;
            }
            finally
            {
                _apiSemaphore.Release();
            }
        }

        private async Task<string> FetchJson(string baseUrl)
        {
            var keys = new[] { _apiKey }.Concat(_alternateKeys).Where(k => !string.IsNullOrEmpty(k)).ToArray();
            Exception? lastException = null;

            foreach (var key in keys)
            {
                try
                {
                    await WaitForApiLimit();
                    
                    var url = baseUrl + key;
                    Console.WriteLine($"[DEBUG] Próba pobrania danych z URL: {url}");
                    
                    var response = await _httpClient.GetAsync(url);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    
                    Console.WriteLine($"[DEBUG] Status kod odpowiedzi: {response.StatusCode}");
                    Console.WriteLine($"[DEBUG] Content-Type: {response.Content.Headers.ContentType}");
                    Console.WriteLine($"[DEBUG] Długość odpowiedzi: {jsonString.Length} znaków");
                    Console.WriteLine($"[DEBUG] Pierwsze 200 znaków odpowiedzi: {jsonString.Substring(0, Math.Min(200, jsonString.Length))}");

                    if (jsonString.Contains("Thank you for using Alpha Vantage!") ||
                        jsonString.Contains("Our standard API call frequency is") ||
                        jsonString.Contains("API rate limit"))
                    {
                        Console.WriteLine("[DEBUG] Wykryto przekroczenie limitu API");
                        throw new Exception("Przekroczono limit API - proszę spróbować za chwilę");
                    }

                    if (jsonString.Contains("Error Message") || 
                        jsonString.Contains("Information") ||
                        string.IsNullOrWhiteSpace(jsonString) ||
                        jsonString == "{}")
                    {
                        Console.WriteLine("[DEBUG] Wykryto błąd w odpowiedzi API");
                        throw new Exception("Brak danych w odpowiedzi API");
                    }

                    Console.WriteLine("[DEBUG] Pomyślnie pobrano dane z API");
                    return jsonString;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DEBUG] Błąd dla klucza {key.Substring(0, 3)}***: {ex.Message}");
                    lastException = ex;
                    
                    if (ex.Message.Contains("Przekroczono limit API"))
                    {
                        Console.WriteLine($"[DEBUG] Czekam {API_CALL_DELAY_MS}ms przed kolejną próbą");
                        await Task.Delay(API_CALL_DELAY_MS);
                    }
                }
            }

            throw new Exception($"Nie udało się pobrać danych z żadnego klucza API. Ostatni błąd: {lastException?.Message}");
        }

        public async Task<Quotations> GetAssetData(string ticker, string interval)
        {
            try
            {
                // Sprawdź cache
                var cacheKey = $"{ticker}_{interval}";
                if (_quotationsCache.TryGetValue(cacheKey, out var cachedData))
                {
                    if (DateTime.Now - cachedData.Timestamp < TimeSpan.FromMinutes(CACHE_DURATION_MINUTES))
                    {
                        Console.WriteLine($"Zwracam dane z cache dla {ticker}");
                        return cachedData.Data;
                    }
                    else
                    {
                        // Usuń przedawnione dane z cache
                        _quotationsCache.TryRemove(cacheKey, out _);
                    }
                }

                MapIntervalRoute.IntervalRoute = interval;
                var intervalRoute = MapIntervalRoute.IntervalRoute;

                var baseUrl = $"https://www.alphavantage.co/query?function={intervalRoute}&symbol={ticker}&outputsize=full&apikey=";
                var jsonString = await FetchJson(baseUrl);

                var quotations = (Quotations)_parserFacade.ParseJsonResponse<Quotations>(jsonString);

                if (quotations?.QuotationsList == null || !quotations.QuotationsList.Any())
                {
                    throw new Exception("Brak danych historycznych dla tego symbolu");
                }

                // Dodaj dane do cache
                _quotationsCache.TryAdd(cacheKey, (DateTime.Now, quotations));

                return quotations;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania danych dla {ticker}: {ex.Message}");
                throw new Exception($"Nie udało się pobrać danych dla {ticker}: {ex.Message}");
            }
        }

        public async Task<Quotations> GetIntradayAssetData(string ticker, string interval)
        {
            var baseUrl = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={ticker}&interval={interval}&apikey=";
            var jsonString = await FetchJson(baseUrl);
            var quotations = (Quotations)_parserFacade.ParseJsonResponse<Quotations>(jsonString);

            if (quotations?.QuotationsList == null || !quotations.QuotationsList.Any())
            {
                throw new Exception("Brak danych intraday dla tego symbolu");
            }

            return quotations;
        }

        public async Task<GeneralAssetInformation> GetGeneralInformation(string ticker)
        {
            var baseUrl = $"https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords={ticker}&apikey=";
            var jsonString = await FetchJson(baseUrl);
            var assetInformation = (GeneralAssetInformation)_parserFacade.ParseJsonResponse<GeneralAssetInformation>(jsonString);
            return assetInformation;
        }

        public async Task<Dividends> GetDividends(string ticker)
        {
            var baseUrl = $"https://www.alphavantage.co/query?function=DIVIDENDS&symbol={ticker}&apikey=";
            var jsonString = await FetchJson(baseUrl);
            var dividends = (Dividends)_parserFacade.ParseJsonResponse<Dividends>(jsonString);
            return dividends;
        }

        public async Task<CompanyOverview> GetCompanyOverview(string ticker)
        {
            var baseUrl = $"https://www.alphavantage.co/query?function=OVERVIEW&symbol={ticker}&apikey=";
            var jsonString = await FetchJson(baseUrl);
            var companyOverview = (CompanyOverview)_parserFacade.ParseJsonResponse<CompanyOverviewResponse>(jsonString);
            return companyOverview;
        }

        public async Task<NewsResponse> GetMarketNews(string? tickers = null, string? topics = null)
        {
            try
            {
                // Sprawdź cache
                var cacheKey = $"news_{tickers ?? "all"}_{topics ?? "all"}";
                if (_newsCache.TryGetValue(cacheKey, out var cachedData))
                {
                    if (DateTime.Now - cachedData.Timestamp < TimeSpan.FromMinutes(NEWS_CACHE_DURATION_MINUTES))
                    {
                        Console.WriteLine($"Zwracam newsy z cache dla {tickers}");
                        return cachedData.Data;
                    }
                    else
                    {
                        // Usuń przedawnione dane z cache
                        _newsCache.TryRemove(cacheKey, out _);
                    }
                }

                var baseUrl = "https://www.alphavantage.co/query?function=NEWS_SENTIMENT";
                
                if (!string.IsNullOrEmpty(tickers))
                {
                    baseUrl += $"&tickers={tickers}";
                }
                
                if (!string.IsNullOrEmpty(topics))
                {
                    baseUrl += $"&topics={topics}";
                }
                
                baseUrl += "&apikey=";
                
                var jsonString = await FetchJson(baseUrl);
                var news = (NewsResponse)_parserFacade.ParseJsonResponse<NewsResponse>(jsonString);

                if (news?.Items == null || !news.Items.Any())
                {
                    throw new Exception("Brak dostępnych wiadomości");
                }

                // Dodaj dane do cache
                _newsCache.TryAdd(cacheKey, (DateTime.Now, news));

                return news;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania newsów: {ex.Message}");
                throw;
            }
        }
    }
}
