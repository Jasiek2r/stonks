using StonksAPI.Utility;

namespace StonksAPI.DTO.Quotation
{
    public class Quotations : IParsingResult
    {
        public List<Quotation> QuotationsList { get; set; }
    }
}
