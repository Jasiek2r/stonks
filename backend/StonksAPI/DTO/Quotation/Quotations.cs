using StonksAPI.Utility;

namespace StonksAPI.DTO.Quotation
{
    public class Quotations : IDeserializable
    {
        public required List<Quotation> QuotationsList { get; set; }
    }
}
