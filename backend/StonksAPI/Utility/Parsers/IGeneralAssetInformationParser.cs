using StonksAPI.Utility;

namespace StonksAPI.Utility.Parsers
{
    public interface IGeneralAssetInformationParser
    {
        IDeserializable Parse(string jsonResponse);
    }
} 