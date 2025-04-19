using Newtonsoft.Json;

namespace StonksAPI.Utility
{
    public class ApiResponse
    {
        [JsonProperty("Time Series (Daily)")]
        public Dictionary<string, Quote>? TimeSeriesDaily { get; set; }
        [JsonProperty("Weekly Time Series")]
        public Dictionary<string, Quote>? TimeSeriesWeekly { get; set; }
        [JsonProperty("Monthly Time Series")]
        public Dictionary<string, Quote>? TimeSeriesMonthly { get; set; }
        [JsonProperty("Time Series (60min)")]
        public Dictionary<string, Quote>? TimeSeriesHourly { get; set; }
        [JsonProperty("Time Series (30min)")]
        public Dictionary<string, Quote>? TimeSeries30min { get; set; }
        [JsonProperty("Time Series (15min)")]
        public Dictionary<string, Quote>? TimeSeries15min { get; set; }
        [JsonProperty("Time Series (5min)")]
        public Dictionary<string, Quote>? TimeSeries5min { get; set; }

    }


}
