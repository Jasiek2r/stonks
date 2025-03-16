using StonksAPI.Utility;

namespace StonksAPI.DTO.Quotation
{
    public class Quotations : IDeserializable
    {
        public List<Quotation> QuotationsList { get; set; }
    }
}
