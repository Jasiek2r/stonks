namespace StonksAPI.DTO.Currency
{
    public class CurrencyCalculationResponse
    {
        public required string FromCurrency { get; set; }
        public required string ToCurrency { get; set; }
        public decimal Amount { get; set; }
        public decimal ConvertedAmount { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime Timestamp { get; set; }
    }
} 