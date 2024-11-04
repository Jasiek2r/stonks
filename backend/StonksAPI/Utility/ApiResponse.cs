using Newtonsoft.Json;

namespace StonksAPI.Utility
{
    public class ApiResponse
    {
        [JsonProperty("Time Series (Daily)")]
        public Dictionary<string, Quote> TimeSeries { get; set; }
    }

    
}
