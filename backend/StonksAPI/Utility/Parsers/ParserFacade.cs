﻿using StonksAPI.DTO.Dividend;
using StonksAPI.DTO.GeneralAssetInformation;
using StonksAPI.DTO.Quotation;

namespace StonksAPI.Utility.Parsers
{
    // Implements a Facade for all Parsers
    public class ParserFacade : IParserFacade
    {

        // List of all JSON Parsers
        // If you want to extend it, make sure they are used under ParseJsonResponse<T>
        // and that every return class implements IDeserializable
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
        public IDeserializable ParseJsonResponse<T>(string data) where T : IDeserializable {

            /*
             * We go through each type one by one and call an appropriate parser
             */
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
