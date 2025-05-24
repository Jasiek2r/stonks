using System.Text.Json.Serialization;
using StonksAPI.Utility;

namespace StonksAPI.DTO.News
{
    public class NewsResponse : IDeserializable
    {
        [JsonPropertyName("items")]
        public required List<NewsItem> Items { get; set; }
    }

    public class NewsItem
    {
        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [JsonPropertyName("url")]
        public required string Url { get; set; }

        [JsonPropertyName("time_published")]
        public required string TimePublished { get; set; }

        [JsonPropertyName("summary")]
        public required string Summary { get; set; }

        [JsonPropertyName("banner_image")]
        public string? BannerImage { get; set; }

        [JsonPropertyName("source")]
        public required string Source { get; set; }

        [JsonPropertyName("overall_sentiment_score")]
        public float SentimentScore { get; set; }

        [JsonPropertyName("overall_sentiment_label")]
        public string? SentimentLabel { get; set; }
    }
} 