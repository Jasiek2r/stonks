using StonksAPI.DTO.Dividend;
using StonksAPI.DTO.GeneralAssetInformation;
using StonksAPI.DTO.Quotation;
using StonksAPI.Utility;

namespace StonksAPI.Services
{
    /*
     * An interface which declares what kind of methods must be present inside StonksApiService class
     * We use this interface for registering a Scoped dependency of a service in DependencyInjection container
     */
    public interface IStonksApiService
    {
        public Task<Quotations> GetAssetData(string ticker, string interval);
        public Task<GeneralAssetInformation> GetGeneralInformation(string ticker);
        public Task<Quotations> GetIntradayAssetData(string ticker, string interval);
        public Task<Dividends> GetDividends(string ticker);
        public Task<CompanyOverview> GetCompanyOverview(string ticker);

    }
}
