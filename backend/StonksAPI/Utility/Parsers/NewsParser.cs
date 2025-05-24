using Newtonsoft.Json.Linq;
using StonksAPI.DTO.News;

namespace StonksAPI.Utility.Parsers
{
    public class NewsParser : INewsParser
    {
        public IDeserializable Parse(string jsonResponse)
        {
            try
            {
                Console.WriteLine("[DEBUG] Rozpoczynam parsowanie odpowiedzi news API");
                Console.WriteLine($"[DEBUG] Pierwsze 200 znaków odpowiedzi: {jsonResponse.Substring(0, Math.Min(200, jsonResponse.Length))}");
                
                var jsonObject = JObject.Parse(jsonResponse);
                
                // Sprawdź czy nie ma błędu od Alpha Vantage
                var errorMessage = jsonObject["Error Message"]?.ToString();
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    Console.WriteLine($"[DEBUG] Błąd API: {errorMessage}");
                    throw new Exception($"Alpha Vantage API error: {errorMessage}");
                }

                var note = jsonObject["Note"]?.ToString();
                if (!string.IsNullOrEmpty(note))
                {
                    Console.WriteLine($"[DEBUG] Informacja o limicie API: {note}");
                    throw new Exception("Przekroczono limit API - proszę spróbować za chwilę");
                }
                
                // Alpha Vantage zwraca wiadomości w tablicy "feed"
                var feed = jsonObject["feed"]?.ToObject<List<JObject>>();
                
                if (feed == null)
                {
                    Console.WriteLine("[DEBUG] Brak elementu 'feed' w odpowiedzi");
                    Console.WriteLine($"[DEBUG] Dostępne pola w odpowiedzi: {string.Join(", ", jsonObject.Properties().Select(p => p.Name))}");
                    return new NewsResponse { Items = new List<NewsItem>() };
                }

                Console.WriteLine($"[DEBUG] Znaleziono {feed.Count} wiadomości w odpowiedzi");

                var items = feed.Select(item =>
                {
                    var newsItem = new NewsItem
                    {
                        Title = item["title"]?.ToString() ?? "",
                        Url = item["url"]?.ToString() ?? "",
                        TimePublished = item["time_published"]?.ToString() ?? DateTime.Now.ToString("yyyyMMddTHHmmss"),
                        Summary = item["summary"]?.ToString() ?? "",
                        BannerImage = item["banner_image"]?.ToString(),
                        Source = item["source"]?.ToString() ?? "",
                        SentimentScore = float.TryParse(item["overall_sentiment_score"]?.ToString(), out float score) ? score : 0,
                        SentimentLabel = item["overall_sentiment_label"]?.ToString()
                    };

                    // Loguj każdy sparsowany element
                    Console.WriteLine($"[DEBUG] Sparsowano wiadomość: {newsItem.Title} | {newsItem.Source} | {newsItem.TimePublished}");
                    
                    return newsItem;
                }).ToList();

                Console.WriteLine($"[DEBUG] Pomyślnie sparsowano {items.Count} wiadomości");
                
                var response = new NewsResponse { Items = items };
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Błąd podczas parsowania wiadomości: {ex.Message}");
                Console.WriteLine($"[DEBUG] Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }

    public interface INewsParser
    {
        IDeserializable Parse(string jsonResponse);
    }
} 