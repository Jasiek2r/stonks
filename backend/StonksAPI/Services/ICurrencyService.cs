using StonksAPI.DTO.Currency;

namespace StonksAPI.Services
{
    public interface ICurrencyService
    {
        Task<CurrencyCalculationResponse> CalculateCurrencyConversion(CurrencyCalculationRequest request);
    }
} 