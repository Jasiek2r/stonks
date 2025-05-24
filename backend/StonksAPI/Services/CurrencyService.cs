using StonksAPI.DTO.Currency;

namespace StonksAPI.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly Dictionary<string, decimal> _exchangeRates = new()
        {
            { "USD", 1.0m },
            { "EUR", 0.91m },
            { "GBP", 0.79m },
            { "JPY", 142.36m },
            { "CHF", 0.86m },
            { "AUD", 1.47m },
            { "CAD", 1.34m },
            { "PLN", 3.97m }
        };

        public Task<CurrencyCalculationResponse> CalculateCurrencyConversion(CurrencyCalculationRequest request)
        {
            if (!_exchangeRates.ContainsKey(request.FromCurrency))
                throw new ArgumentException($"Unsupported currency: {request.FromCurrency}");

            if (!_exchangeRates.ContainsKey(request.ToCurrency))
                throw new ArgumentException($"Unsupported currency: {request.ToCurrency}");

            var fromRate = _exchangeRates[request.FromCurrency];
            var toRate = _exchangeRates[request.ToCurrency];
            var exchangeRate = toRate / fromRate;
            var convertedAmount = request.Amount * exchangeRate;

            return Task.FromResult(new CurrencyCalculationResponse
            {
                FromCurrency = request.FromCurrency,
                ToCurrency = request.ToCurrency,
                Amount = request.Amount,
                ConvertedAmount = convertedAmount
            });
        }
    }
} 