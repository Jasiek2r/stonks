using Newtonsoft.Json;
using StonksAPI.Utility;

namespace StonksAPI.DTO.Dividend
{
    public class Dividends : IParsingResult
    {
        [JsonProperty("data")]
        public List<Dividend> DividendList { get; set; }
    }
}
