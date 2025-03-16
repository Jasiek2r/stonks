namespace StonksAPI.DTO.Quotation
{
    public class Quotation
    {
        public string TimeInterval { get; set; }
        public decimal Open {  get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }

    }
}
