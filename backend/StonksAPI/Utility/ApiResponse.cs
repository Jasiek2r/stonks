using Newtonsoft.Json;

namespace StonksAPI.Utility
{
    public class ApiResponse
    {
        [JsonProperty("Time Series (Daily)")]
        public Dictionary<string, Quote> TimeSeriesDaily { get; set; }
        [JsonProperty("Weekly Time Series")]
        public Dictionary<string, Quote> TimeSeriesWeekly { get; set; }

    }

    
}
