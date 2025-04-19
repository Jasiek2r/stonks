namespace StonksAPI.Utility
{
    public class CompanyOverview : IDeserializable
    {
        public CompanyOverview(CompanyOverviewResponse response)
        {
            Name = response.Name;
            Description = response.Description;
            Exchange = response.Exchange;
            Currency = response.Currency;
            Country = response.Country;
            Sector = response.Sector;
            Industry = response.Industry;
            Address = response.Address;
            DividendDate = response.DividendDate;
            ExDividendDate = response.ExDividendDate;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Exchange { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public string Address { get; set; }
        public long? MarketCap { get; set; }
        public decimal? EBITDA { get; set; }
        public decimal? PERatio { get; set; }
        public decimal? PEGRatio { get; set; }
        public decimal? BookValue { get; set; }
        public decimal? DividendPerShare { get; set; }
        public decimal? DividendYield { get; set; }
        public decimal? EPS { get; set; }
        public decimal? RevenuePerShareTTM { get; set; }
        public decimal? ProfitMargin { get; set; }
        public decimal? OperatingMarginTTM { get; set; }
        public decimal? ReturnOnAssetsTTM { get; set; }
        public decimal? ReturnOnEquityTTM { get; set; }
        public long? RevenueTTM { get; set; }
        public long? GrossProfitTTM { get; set; }
        public decimal? DilutedEPSTTM { get; set; }
        public decimal? QuarterlyEarningsGrowthYOY { get; set; }
        public decimal? QuarterlyRevenueGrowthYOY { get; set; }
        public decimal? AnalystTargetPrice { get; set; }
        public decimal? TrailingPE { get; set; }
        public decimal? ForwardPE { get; set; }
        public decimal? PriceToSalesRatioTTM { get; set; }
        public decimal? PriceToBookRatio { get; set; }
        public decimal? EVToRevenue { get; set; }
        public decimal? EVToEBITDA { get; set; }
        public decimal? Beta { get; set; }
        public long? SharesOutstanding { get; set; }
        public string DividendDate { get; set; }
        public string ExDividendDate { get; set; }
    }
}
