using Newtonsoft.Json;
using StonksAPI.DTO.GeneralAssetInformation;

namespace StonksAPI.Utility
{
    public class ApiMatches
    {
        [JsonProperty("bestMatches")]
        public List<GeneralAssetInformation> Matches { get; set; }
    }
}
