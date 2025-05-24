using System.Text.Json.Serialization;
using StonksAPI.Utility;

namespace StonksAPI.DTO.GeneralAssetInformation
{
    public class GeneralAssetInformation : IDeserializable
    {
        [JsonPropertyName("bestMatches")]
        public required List<AssetInformation> BestMatches { get; set; }
    }

    public class AssetInformation
    {
        [JsonPropertyName("1. symbol")]
        public required string Symbol { get; set; }

        [JsonPropertyName("2. name")]
        public required string Name { get; set; }

        [JsonPropertyName("3. type")]
        public required string Type { get; set; }

        [JsonPropertyName("4. region")]
        public required string Region { get; set; }

        [JsonPropertyName("5. marketOpen")]
        public required string MarketOpen { get; set; }

        [JsonPropertyName("6. marketClose")]
        public required string MarketClose { get; set; }

        [JsonPropertyName("7. timezone")]
        public required string Timezone { get; set; }

        [JsonPropertyName("8. currency")]
        public required string Currency { get; set; }

        [JsonPropertyName("9. matchScore")]
        public required string MatchScore { get; set; }
    }
}
