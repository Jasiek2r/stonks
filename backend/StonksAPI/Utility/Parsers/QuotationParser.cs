using AutoMapper;
using Newtonsoft.Json;
using StonksAPI.DTO.Quotation;
using StonksAPI.Utility;

namespace StonksAPI.Utility.Parsers
{
    // Helper class for 
    public class QuotationParser : IQuotationParser
    {
        private readonly IMapper _autoMapper;
        public QuotationParser(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public IDeserializable Parse(string jsonString)
        {
            //deserialize object
            ApiResponse? apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);

            if (apiResponse == null)
            {
                throw new InvalidOperationException("Failed to parse JSON response.");
            }

            //convert to uniform internal format
            ApiResponseAdapter apiResponseAdapter = new ApiResponseAdapter(apiResponse);
            ApiSeries jsonSeries = apiResponseAdapter.ExtractSeries();

            //map to Quotations object            
            Quotations quotations = _autoMapper.Map<Quotations>(jsonSeries);

            return quotations;
        }
    }
}
