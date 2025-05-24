using StonksAPI.DTO.Search;
using System.Text.Json;

namespace StonksAPI.Services
{
    public class SearchService : ISearchService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private static List<SearchTickerResponse>? _cachedListings;
        private static DateTime _lastCacheUpdate = DateTime.MinValue;
        private const int CACHE_DURATION_HOURS = 24;
        private readonly IStonksApiService _apiService;

        public SearchService(HttpClient httpClient, IConfiguration configuration, IStonksApiService apiService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        }

        public async Task<IEnumerable<SearchTickerResponse>> SearchTickers(string query)
        {
            try
            {
                // Pobierz lub odśwież cache
                if (_cachedListings == null || DateTime.Now - _lastCacheUpdate > TimeSpan.FromHours(CACHE_DURATION_HOURS))
                {
                    await RefreshListingsCache();
                }

                // Jeśli cache jest pusty, zwróć pustą listę
                if (_cachedListings == null)
                {
                    return Enumerable.Empty<SearchTickerResponse>();
                }

                // Wyszukaj w cache'u
                return _cachedListings
                    .Where(listing =>
                        listing.Symbol.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        listing.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                    .Take(20) // Limit wyników do 20 najlepszych dopasowań
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Błąd podczas wyszukiwania: {ex.Message}");
            }
        }

        private async Task RefreshListingsCache()
        {
            var apiKey = _configuration.GetSection("ApiSettings:ApiKey").Value;
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("Brak skonfigurowanego klucza API");
            }

            try
            {
                Console.WriteLine("Rozpoczęcie odświeżania cache'u listingu...");
                var url = $"https://www.alphavantage.co/query?function=LISTING_STATUS&apikey={apiKey}";
                var response = await _httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                if (content.Contains("Thank you for using Alpha Vantage!") || 
                    content.Contains("Our standard API call frequency is"))
                {
                    throw new InvalidOperationException("Przekroczono limit API");
                }

                // Przetwarzanie CSV
                var listings = new List<SearchTickerResponse>();
                using (var reader = new StringReader(content))
                {
                    // Pomiń nagłówek
                    reader.ReadLine();

                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var values = line.Split(',');
                        if (values.Length >= 3)
                        {
                            listings.Add(new SearchTickerResponse
                            {
                                Symbol = values[0],
                                Name = values[1],
                                Type = "Equity",
                                Region = "United States",
                                Currency = "USD",
                                MarketOpen = "09:30",
                                MarketClose = "16:00",
                                Timezone = "UTC-04"
                            });
                        }
                    }
                }

                _cachedListings = listings;
                _lastCacheUpdate = DateTime.Now;
                Console.WriteLine($"Cache zaktualizowany. Pobrano {listings.Count} spółek.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas odświeżania cache'u: {ex.Message}");
                throw;
            }
        }

        private string GetPropertyValue(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out var property))
            {
                return property.GetString() ?? string.Empty;
            }
            return string.Empty;
        }

        public async Task<List<SearchTickerResponse>> SearchTicker(string query)
        {
            try
            {
                var result = await _apiService.GetGeneralInformation(query);
                if (result?.BestMatches == null)
                {
                    return new List<SearchTickerResponse>();
                }

                return result.BestMatches.Select(match => new SearchTickerResponse
                {
                    Symbol = match.Symbol,
                    Name = match.Name,
                    Type = match.Type,
                    Region = match.Region,
                    Currency = match.Currency,
                    MarketOpen = match.MarketOpen,
                    MarketClose = match.MarketClose,
                    Timezone = match.Timezone
                }).ToList();
            }
            catch
            {
                return new List<SearchTickerResponse>();
            }
        }
    }
} 