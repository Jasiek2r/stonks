using System.ComponentModel.DataAnnotations;

namespace StonksAPI.DTO.Currency
{
    public class CurrencyCalculationRequest
    {
        public required string FromCurrency { get; set; }

        public required string ToCurrency { get; set; }

        [Required]
        [Range(0.0001, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public required decimal Amount { get; set; }
    }
} 