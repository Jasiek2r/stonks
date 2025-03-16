namespace StonksAPI.DTO.Holding
{
    public class CreateHoldingDto
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? PurchaseAmount { get; set; }
    }
}
