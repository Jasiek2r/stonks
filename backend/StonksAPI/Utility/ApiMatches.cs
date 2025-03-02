using Newtonsoft.Json;

namespace StonksAPI.Utility
{
    public class ApiMatches
    {
        [JsonProperty("bestMatches")]
        public List<GeneralAssetInformation> Matches { get; set; }
    }
}
