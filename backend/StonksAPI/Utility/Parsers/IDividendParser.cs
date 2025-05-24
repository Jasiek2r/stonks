using StonksAPI.DTO.Dividend;
using StonksAPI.DTO.GeneralAssetInformation;
using StonksAPI.Utility;

namespace StonksAPI.Utility.Parsers
{
    public interface IDividendParser
    {
        IDeserializable Parse(string jsonResponse);
    }
}
