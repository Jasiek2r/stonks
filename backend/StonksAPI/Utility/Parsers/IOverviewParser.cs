using StonksAPI.DTO.Dividend;

namespace StonksAPI.Utility.Parsers
{
    public interface IOverviewParser
    {
        public CompanyOverview ParseJsonResponse(string jsonString);
    }
}
