using Microsoft.AspNetCore.Mvc;
using StonksAPI.Services;
using StonksAPI.DTO.News;

namespace StonksAPI.Controllers
{
    [ApiController]
    [Route("api/news")]
    public class NewsController : ControllerBase
    {
        private readonly IStonksApiService _apiService;

        public NewsController(IStonksApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<ActionResult<NewsResponse>> GetNews(
            [FromQuery] string? tickers = null,
            [FromQuery] string? topics = null)
        {
            try
            {
                var news = await _apiService.GetMarketNews(tickers, topics);
                return Ok(news);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
} 