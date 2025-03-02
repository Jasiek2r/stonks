﻿using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace StonksAPI.Utility
{
    public class GeneralAssetInformation
    {
        [JsonProperty("1. symbol")]
        public string Ticker {  get; set; }

        [JsonProperty("2. name")]
        public string Name { get; set; }
        
        [JsonProperty("3. type")]
        public string Type { get; set; }
        
        [JsonProperty("4. region")]
        public string Region { get; set; }
        
        [JsonProperty("5. marketOpen")]
        public string MarketOpen { get; set; }
        
        [JsonProperty("6. marketClose")]
        public string MarketClose { get; set; }
        
        [JsonProperty("7. timezone")]
        public string Timezone { get; set; }

        [JsonProperty("8. currency")]
        public string Currency { get; set; }

    }
}
