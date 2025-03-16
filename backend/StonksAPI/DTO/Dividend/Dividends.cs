using Newtonsoft.Json;
using StonksAPI.Utility;

namespace StonksAPI.DTO.Dividend
{
    public class Dividends : IDeserializable
    {
        [JsonProperty("data")]
        public List<Dividend> DividendList { get; set; }
    }
}
