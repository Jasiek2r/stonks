using StonksAPI.DTO;

namespace StonksAPI.Utility.Parsers
{
    public interface IQuotationParser
    {
        public Quotations ParseJsonResponse(string jsonString);
    }
}
