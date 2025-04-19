using AutoMapper;
using System.Globalization;

namespace StonksAPI.Utility.Parsers
{
    public class OverviewParser : IOverviewParser
    {
        private readonly IMapper _mapper;
        public OverviewParser(IMapper mapper)
        {
            _mapper = mapper;
        }
        public CompanyOverview ParseJsonResponse(string jsonString)
        {
            // Deserialize the JSON string into a CompanyOverview object
            var companyOverviewResponse = System.Text.Json.JsonSerializer.Deserialize<CompanyOverviewResponse>(jsonString);
            // Check if the deserialization was successful
            if (companyOverviewResponse == null)
            {
                throw new InvalidOperationException("Failed to parse JSON response.");
            }

            CompanyOverview companyOverview = ConvertProperties(companyOverviewResponse);

            // Return the parsed CompanyOverview object
            return companyOverview;
        }

        private CompanyOverview ConvertProperties(CompanyOverviewResponse response)
        {
            // Helper method for mapping string properties to numeric ones

            // For some reason the automapper doesn't work
            // so we have to do it manually
            
            CompanyOverview companyOverview = new CompanyOverview(response);
            
            companyOverview.EBITDA = Decimal.Parse(response.EBITDA, CultureInfo.InvariantCulture);
            companyOverview.PERatio = Decimal.Parse(response.PERatio, CultureInfo.InvariantCulture);
            companyOverview.PEGRatio = Decimal.Parse(response.PEGRatio, CultureInfo.InvariantCulture);
            companyOverview.BookValue = Decimal.Parse(response.BookValue, CultureInfo.InvariantCulture);
            companyOverview.DividendPerShare = Decimal.Parse(response.DividendPerShare, CultureInfo.InvariantCulture);
            companyOverview.DividendYield = Decimal.Parse(response.DividendYield, CultureInfo.InvariantCulture);
            companyOverview.EPS = Decimal.Parse(response.EPS, CultureInfo.InvariantCulture);
            companyOverview.RevenuePerShareTTM = Decimal.Parse(response.RevenuePerShareTTM, CultureInfo.InvariantCulture);
            companyOverview.ProfitMargin = Decimal.Parse(response.ProfitMargin, CultureInfo.InvariantCulture);
            companyOverview.OperatingMarginTTM = Decimal.Parse(response.OperatingMarginTTM, CultureInfo.InvariantCulture);
            companyOverview.ReturnOnAssetsTTM = Decimal.Parse(response.ReturnOnAssetsTTM, CultureInfo.InvariantCulture);
            companyOverview.ReturnOnEquityTTM = Decimal.Parse(response.ReturnOnEquityTTM, CultureInfo.InvariantCulture);
            companyOverview.RevenueTTM = long.Parse(response.RevenueTTM, CultureInfo.InvariantCulture);
            companyOverview.GrossProfitTTM = long.Parse(response.GrossProfitTTM, CultureInfo.InvariantCulture);
            companyOverview.DilutedEPSTTM = Decimal.Parse(response.DilutedEPSTTM, CultureInfo.InvariantCulture);
            companyOverview.QuarterlyEarningsGrowthYOY = Decimal.Parse(response.QuarterlyEarningsGrowthYOY, CultureInfo.InvariantCulture);
            companyOverview.QuarterlyRevenueGrowthYOY = Decimal.Parse(response.QuarterlyRevenueGrowthYOY, CultureInfo.InvariantCulture);
            companyOverview.AnalystTargetPrice = Decimal.Parse(response.AnalystTargetPrice, CultureInfo.InvariantCulture);
            companyOverview.MarketCap = long.Parse(response.MarketCapitalization, CultureInfo.InvariantCulture);
            companyOverview.TrailingPE = Decimal.Parse(response.TrailingPE, CultureInfo.InvariantCulture);
            companyOverview.ForwardPE = Decimal.Parse(response.ForwardPE, CultureInfo.InvariantCulture);
            companyOverview.PriceToSalesRatioTTM = Decimal.Parse(response.PriceToSalesRatioTTM, CultureInfo.InvariantCulture);
            companyOverview.PriceToBookRatio = Decimal.Parse(response.PriceToBookRatio, CultureInfo.InvariantCulture);
            companyOverview.EVToRevenue = Decimal.Parse(response.EVToRevenue, CultureInfo.InvariantCulture);
            companyOverview.EVToEBITDA = Decimal.Parse(response.EVToEBITDA, CultureInfo.InvariantCulture);
            companyOverview.Beta = Decimal.Parse(response.Beta, CultureInfo.InvariantCulture);
            companyOverview.SharesOutstanding = long.Parse(response.SharesOutstanding, CultureInfo.InvariantCulture);

            return companyOverview;
        }
    }
}
