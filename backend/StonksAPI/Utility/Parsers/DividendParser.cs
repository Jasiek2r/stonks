using Newtonsoft.Json;
using StonksAPI.DTO.Dividend;
using StonksAPI.Utility;

namespace StonksAPI.Utility.Parsers
{
    public class DividendParser : IDividendParser
    {
        public IDeserializable Parse(string jsonResponse)
        {
            Dividends? dividends = JsonConvert.DeserializeObject<Dividends>(jsonResponse);
            if(dividends is null)
            {
                throw new InvalidOperationException("Failed to parse JSON response.");
            }
            return dividends;
        }
    }
}
