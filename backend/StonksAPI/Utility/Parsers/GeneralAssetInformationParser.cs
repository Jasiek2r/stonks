using Newtonsoft.Json.Linq;
using StonksAPI.DTO.GeneralAssetInformation;

namespace StonksAPI.Utility.Parsers
{
    public class GeneralAssetInformationParser : IGeneralAssetInformationParser
    {
        public IDeserializable Parse(string jsonResponse)
        {
            var jsonObject = JObject.Parse(jsonResponse);
            var bestMatches = jsonObject["bestMatches"]?.ToObject<List<AssetInformation>>() ?? new List<AssetInformation>();
            
            return new GeneralAssetInformation
            {
                BestMatches = bestMatches
            };
        }
    }
} 