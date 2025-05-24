using StonksAPI.DTO.Dividend;
using StonksAPI.DTO.GeneralAssetInformation;
using StonksAPI.DTO.Quotation;
using StonksAPI.DTO.News;

namespace StonksAPI.Utility.Parsers
{
    // Implements a Facade for all Parsers
    public class ParserFacade : IParserFacade
    {

        // List of all JSON Parsers
        // If you want to extend it, make sure they are used under ParseJsonResponse<T>
        // and that every return class implements IDeserializable
        private readonly IQuotationParser _quotationParser;
        private readonly IGeneralAssetInformationParser _generalAssetInformationParser;
        private readonly IDividendParser _dividendParser;
        private readonly INewsParser _newsParser;
        public ParserFacade(
            IQuotationParser quotationParser,
            IGeneralAssetInformationParser generalAssetInformationParser,
            IDividendParser dividendParser,
            INewsParser newsParser)
        {
            _quotationParser = quotationParser;
            _generalAssetInformationParser = generalAssetInformationParser;
            _dividendParser = dividendParser;
            _newsParser = newsParser;
        }
        public IDeserializable ParseJsonResponse<T>(string jsonResponse) where T : IDeserializable {

            return typeof(T).Name switch
            {
                nameof(Quotations) => _quotationParser.Parse(jsonResponse),
                nameof(GeneralAssetInformation) => _generalAssetInformationParser.Parse(jsonResponse),
                nameof(Dividends) => _dividendParser.Parse(jsonResponse),
                nameof(NewsResponse) => _newsParser.Parse(jsonResponse),
                _ => throw new ArgumentException($"Nieznany typ {typeof(T).Name}")
            };
        } 
    }
}
