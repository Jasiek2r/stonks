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
        public async Task<Quotations> GetAssetData(string ticker, string interval) { return null; }
        public async Task<GeneralAssetInformation> GetGeneralInformation(string ticker) { return null; }
        public async Task<Quotations> GetIntradayAssetData(string ticker, string interval) { return null; }
        public async Task<Dividends> GetDividends(string ticker) { return null; }
        public async Task<CompanyOverview> GetCompanyOverview(string ticker) { return null; }

    }
}
