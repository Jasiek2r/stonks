using Newtonsoft.Json;
using System.Numerics;

namespace StonksAPI.Utility
{
    public class CompanyOverviewResponse : IDeserializable
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Exchange")]
        public string Exchange { get; set; }
        [JsonProperty("Currency")]
        public string Currency { get; set; }
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("Sector")]
        public string Sector { get; set; }
        [JsonProperty("Industry")]
        public string Industry { get; set; }
        [JsonProperty("Address")]
        public string Address { get; set; }
        [JsonProperty("MarketCapitalization")]
        public string MarketCapitalization { get; set; }
        [JsonProperty("EBITDA")]
        public string EBITDA { get; set; }
        [JsonProperty("PERatio")]
        public string PERatio { get; set; }
        [JsonProperty("PEGRatio")]
        public string PEGRatio { get; set; }
        [JsonProperty("BookValue")]
        public string BookValue { get; set; }
        [JsonProperty("DividendPerShare")]
        public string DividendPerShare { get; set; }
        [JsonProperty("DividendYield")]
        public string DividendYield { get; set; }
        [JsonProperty("EPS")]
        public string EPS { get; set; }
        [JsonProperty("RevenuePerShareTTM")]
        public string RevenuePerShareTTM { get; set; }
        [JsonProperty("ProfitMargin")]
        public string ProfitMargin { get; set; }
        [JsonProperty("OperatingMarginTTM")]
        public string OperatingMarginTTM { get; set; }
        [JsonProperty("ReturnOnAssetsTTM")]
        public string ReturnOnAssetsTTM { get; set; }
        [JsonProperty("ReturnOnEquityTTM")]
        public string ReturnOnEquityTTM { get; set; }
        [JsonProperty("RevenueTTM")]
        public string RevenueTTM { get; set; }
        [JsonProperty("GrossProfitTTM")]
        public string GrossProfitTTM { get; set; }
        [JsonProperty("DilutedEPSTTM")]
        public string DilutedEPSTTM { get; set; }
        [JsonProperty("QuarterlyEarningsGrowthYOY")]
        public string QuarterlyEarningsGrowthYOY { get; set; }
        [JsonProperty("QuarterlyRevenueGrowthYOY")]
        public string QuarterlyRevenueGrowthYOY { get; set; }
        [JsonProperty("AnalystTargetPrice")]
        public string AnalystTargetPrice { get; set; }
        [JsonProperty("TrailingPE")]
        public string TrailingPE { get; set; }
        [JsonProperty("ForwardPE")]
        public string ForwardPE { get; set; }
        [JsonProperty("PriceToSalesRatioTTM")]
        public string PriceToSalesRatioTTM { get; set; }
        [JsonProperty("PriceToBookRatio")]
        public string PriceToBookRatio { get; set; }
        [JsonProperty("EVToRevenue")]
        public string EVToRevenue { get; set; }
        [JsonProperty("EVToEBITDA")]
        public string EVToEBITDA { get; set; }
        [JsonProperty("Beta")]
        public string Beta { get; set; }
        [JsonProperty("SharesOutstanding")]
        public string SharesOutstanding { get; set; }
        [JsonProperty("DividendDate")]
        public string DividendDate { get; set; }
        [JsonProperty("ExDividendDate")]
        public string ExDividendDate { get; set; }
    }
}
