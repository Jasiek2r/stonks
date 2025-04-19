using AutoMapper;
using Newtonsoft.Json;
using StonksAPI.DTO.Quotation;

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

        //Helper method for converting raw json string to our Quotations object
        public Quotations ParseJsonResponse(string jsonString)
        {
            //deserialize object
            ApiResponse? jsonResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);

            if (jsonResponse == null)
            {
                throw new InvalidOperationException("Failed to parse JSON response.");
            }

            //convert to uniform internal format
            ApiResponseAdapter apiResponseAdapter = new ApiResponseAdapter(jsonResponse);
            ApiSeries jsonSeries = apiResponseAdapter.ExtractSeries();

            //map to Quotations object            
            Quotations quotations = _autoMapper.Map<Quotations>(jsonSeries);

            return quotations;
        }
    }
}
