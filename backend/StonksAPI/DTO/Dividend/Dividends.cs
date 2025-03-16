using Newtonsoft.Json;

namespace StonksAPI.DTO.Dividend
{
    public class Dividends
    {
        [JsonProperty("data")]
        public List<Dividend> DividendList { get; set; }
    }
}
