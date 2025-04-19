using Newtonsoft.Json;
using StonksAPI.DTO.Dividend;

namespace StonksAPI.Utility.Parsers
{
    public class DividendParser : IDividendParser
    {
        public Dividends ParseJsonResponse(string jsonString)
        {
            Dividends? dividends = JsonConvert.DeserializeObject<Dividends>(jsonString);
            if(dividends is null)
            {
                throw new InvalidOperationException("Failed to parse JSON response.");
            }
            return dividends;
        }
    }
}
