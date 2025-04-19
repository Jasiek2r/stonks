using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;

namespace StonksAPI.DTO.Dividend
{
    public class Dividend
    {
        private decimal _amount;

        [JsonProperty("ex_dividend_date")]
        public string? ExDividendDate { get; set; }
        [JsonProperty("declaration_date")]
        public string? DeclarationDate { get; set; }
        [JsonProperty("record_date")]
        public string? RecordDate { get; set; }
        [JsonProperty("payment_date")]
        public string? PaymentDate { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get => _amount; set
            {
                Decimal.TryParse(value.ToString(), out _amount);
            }
        }
    }
}
