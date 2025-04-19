namespace StonksAPI.Entities
{
    /*
     * Entity Framework class which represents User stock market holdings
     */
    public class Holding
    {
        public int Id { get; set; }
        public string? Ticker { get; set; }
        public string? Name { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal PurchaseAmount { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }

    }
}
