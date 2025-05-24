namespace StonksAPI.DTO.Search
{
    public class SearchTickerResponse
    {
        public required string Symbol { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required string Region { get; set; }
        public required string Currency { get; set; }
        public required string MarketOpen { get; set; }
        public required string MarketClose { get; set; }
        public required string Timezone { get; set; }
    }
} 