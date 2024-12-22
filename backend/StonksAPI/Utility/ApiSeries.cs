using Newtonsoft.Json;

namespace StonksAPI.Utility
{
    public class ApiSeries
    {
        [JsonProperty("Time Series")]
        public Dictionary<string, Quote> TimeSeries { get; set; }
    }
}
