using StonksAPI.DTO.Search;

namespace StonksAPI.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<SearchTickerResponse>> SearchTickers(string query);
    }
} 