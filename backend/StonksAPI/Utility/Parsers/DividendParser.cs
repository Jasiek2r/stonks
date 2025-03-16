using Newtonsoft.Json;
using StonksAPI.DTO.Dividend;

namespace StonksAPI.Utility.Parsers
{
    public class DividendParser : IDividendParser
    {
        public Dividends ParseJsonResponse(string jsonString)
        {
            Dividends dividends = JsonConvert.DeserializeObject<Dividends>(jsonString);
            return dividends;
        }
    }
}
