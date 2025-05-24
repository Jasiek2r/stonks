using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StonksAPI.DTO.Currency;
using StonksAPI.DTO.Dividend;
using StonksAPI.DTO.GeneralAssetInformation;
using StonksAPI.DTO.Quotation;
using StonksAPI.DTO.Search;
using StonksAPI.Services;
using StonksAPI.Utility;
using System.Net;

namespace StonksAPI.Controllers
{

    /*
     * Main API controller for StonksPG Web API,
     * which we can connect to through a root route https://127.0.0.1/api/ .
     * Each method defined here serves as an endpoint which uses HTTP protocol.
     * When the request is sent to a specific endpoint's route (for example https://127.0.0.1/api/asset/TSLA?interval=Daily),
     * with a specific access method (i.e. HTTP's GET, POST, PUT, PATCH, DELETE or any other)
     * then the backend server triggers execution of an endpoint's function, passing in any HTTP payload.
     * After executing the function, we usually return the HTTP Status Code (200 for OK, 404 for NotFound etc.)
     * This HTTP status returning can be done by calling IActionResult StatusCode(int statusCode) or by simply 
     * using more user-friendly names like Ok() or NotFound().
     * For testing endpoints manually you can use Postman, StonksAPI.http file or web browser's developer tools.
     * Of course both REST API controllers and HTTP protocol have alot more to offer, but it's enough for this
     * already quite long comment.
     */
    [ApiController]
    [Route("api")]
    public class StonksApiController : Controller
    {
        private readonly IStonksApiService _stonksApiService;
        private readonly ICurrencyService _currencyService;
        private readonly ISearchService _searchService;

        public StonksApiController(
            IStonksApiService stonksApiService, 
            ICurrencyService currencyService,
            ISearchService searchService)
        {
            _stonksApiService = stonksApiService;
            _currencyService = currencyService;
            _searchService = searchService;
        }

        [HttpGet]
        public ActionResult WelcomeToApi()
        {
            // Return a greeting message on the main page
            return Ok("Welcome to Stock Market API 2024/2025");
        }
        
        [HttpGet("{ticker}")] // asset/{asset_ticker_name}?interval={interval}
        public async Task<IActionResult> GetAssetData([FromRoute] string ticker, [FromQuery] string interval)
        {
            // If interval is not present in a request, return a 400 BadRequest status code
            if (string.IsNullOrEmpty(interval))
            {
                return BadRequest("Specifying an interval is obligatory!");
            }

            // Otherwise obtain asset data from third-party API and return it to the user
            Quotations quotations = await _stonksApiService.GetAssetData(ticker, interval);

            // Return a 200 OK Status Code to the user along with quotations in JSON format
            return Ok(quotations);
        }
        [HttpGet("{ticker}/intraday")]
        public async Task<IActionResult> GetIntradayAssetData([FromRoute] string ticker, [FromQuery] string interval)
        {
            // If interval is not present in a request, return a 400 BadRequest status code
            if (string.IsNullOrEmpty(interval))
            {
                return BadRequest("Specifying an interval is obligatory!");
            }

            // Otherwise obtain asset data from third-party API and return it to the user
            Quotations quotations = await _stonksApiService.GetIntradayAssetData(ticker, interval);

            // Return a 200 OK Status Code to the user along with quotations in JSON format
            return Ok(quotations);
        }

        [HttpGet("{ticker}/info")]        
        public async Task<IActionResult> GetGeneralInformation([FromRoute] string ticker)
        {
            // Fetch general asset information
            GeneralAssetInformation assetInformation = await _stonksApiService.GetGeneralInformation(ticker);
            return Ok(assetInformation);
            
        }

        [HttpGet("{ticker}/dividends")]
        public async Task<IActionResult> GetDividends([FromRoute] string ticker)
        {
            Dividends dividends = await _stonksApiService.GetDividends(ticker);
            return Ok(dividends);
        }
        [HttpGet("{ticker}/overview")]
        public async Task<IActionResult> GetOverview([FromRoute] string ticker)
        {
            // Fetch company's overview
            CompanyOverview overview = await _stonksApiService.GetCompanyOverview(ticker);
            return Ok(overview);
        }
        // WBBBBB

        [HttpGet("tickers")]
        public async Task<IActionResult> GetAllTickers()
        {
            try
            {
                // Pobierz wszystkie dostępne spółki
                var tickers = await _searchService.SearchTickers("");
                return Ok(tickers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTickers([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Wprowadź tekst do wyszukiwania");
            }

            try
            {
                var results = await _searchService.SearchTickers(query);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("currency/calculate")]
        public async Task<IActionResult> CalculateCurrency([FromBody] CurrencyCalculationRequest request)
        {
            try
            {
                var result = await _currencyService.CalculateCurrencyConversion(request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
