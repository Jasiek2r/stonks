using StonksAPI.DTO.Dividend;
using StonksAPI.DTO.GeneralAssetInformation;

namespace StonksAPI.Utility.Parsers
{
    public interface IDividendParser
    {
        public Dividends ParseJsonResponse(string jsonString);
    }
}
