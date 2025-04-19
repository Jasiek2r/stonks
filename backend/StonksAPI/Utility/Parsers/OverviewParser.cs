using AutoMapper;
using System.Globalization;

namespace StonksAPI.Utility.Parsers
{
    public class OverviewParser : IOverviewParser
    {
        private readonly IMapper _mapper;
        public OverviewParser(IMapper mapper)
        {
            _mapper = mapper;
        }
        public CompanyOverview ParseJsonResponse(string jsonString)
        {
            // Deserialize the JSON string into a CompanyOverview object
            var companyOverviewResponse = System.Text.Json.JsonSerializer.Deserialize<CompanyOverviewResponse>(jsonString);
            // Check if the deserialization was successful
            if (companyOverviewResponse == null)
            {
                throw new InvalidOperationException("Failed to parse JSON response.");
            }

            CompanyOverview companyOverview = ConvertProperties(companyOverviewResponse);

            // Return the parsed CompanyOverview object
            return companyOverview;
        }

        private CompanyOverview ConvertProperties(CompanyOverviewResponse response)
        {
            // Helper method for mapping string properties to numeric ones

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            CompanyOverview companyOverview = _mapper.Map<CompanyOverview>(response);

            return companyOverview;
        }
    }
}
