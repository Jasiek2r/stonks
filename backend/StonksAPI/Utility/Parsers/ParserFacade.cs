using StonksAPI.DTO.Dividend;
using StonksAPI.DTO.GeneralAssetInformation;
using StonksAPI.DTO.Quotation;

namespace StonksAPI.Utility.Parsers
{
    public class ParserFacade : IParserFacade
    {
        private readonly IQuotationParser _quotationParser;
        private readonly IGeneralInfoParser _generalInfoParser;
        private readonly IDividendParser _dividendParser;
        public ParserFacade(IDividendParser dividendParser,
            IGeneralInfoParser generalInfoParser,
            IQuotationParser quotationParser)
        {
            _quotationParser = quotationParser;
            _generalInfoParser = generalInfoParser;
            _dividendParser = dividendParser;
        }
        public IParsingResult ParseJsonResponse<T>(string data) where T : IParsingResult {

            
            if(typeof(T) == typeof(Quotations))
            {
                Quotations result = _quotationParser.ParseJsonResponse(data);
                return result;
            }
            else if(typeof(T) == typeof(GeneralAssetInformation))
            {
                GeneralAssetInformation result = _generalInfoParser.ParseJsonResponse(data);
                return result;
            }
            else if(typeof(T) == typeof(Dividends))
            {
                Dividends result = _dividendParser.ParseJsonResponse(data);
                return result;
            }

            return default(T);
        } 
    }
}
