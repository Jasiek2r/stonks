using Newtonsoft.Json;
using StonksAPI.DTO.GeneralAssetInformation;

namespace StonksAPI.Utility
{
    public class ApiMatches
    {
        [JsonProperty("bestMatches")]
        public required List<GeneralAssetInformation> Matches { get; set; }
    }
}
