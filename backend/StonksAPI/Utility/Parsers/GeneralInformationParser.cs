using AutoMapper;
using Newtonsoft.Json;
using StonksAPI.DTO;
using StonksAPI.DTO.GeneralAssetInformation;

namespace StonksAPI.Utility.Parsers
{
    public class GeneralInformationParser : IGeneralInfoParser
    {
        public GeneralAssetInformation ParseJsonResponse(string jsonString)
        {
            //deserialize object
            ApiMatches matches = JsonConvert.DeserializeObject<ApiMatches>(jsonString)!;
            GeneralAssetInformation assetInformation = matches.Matches.First();
            
            return assetInformation;
        }
    }
}
