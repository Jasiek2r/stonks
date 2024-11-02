using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace StonksAPI.Controllers
{

    /*
     * Main API controller for StonksPG Web API,
     * which we can connect to through a root route https://127.0.0.1/api/ .
     * Each method defined here serves as an endpoint which uses HTTP protocol.
     * When the request is sent to a specific endpoint's route (for example https://127.0.0.1/api/get-asset/TSLA),
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
        [HttpGet("/get-asset/{ticker}")]
        public IActionResult GetStockData([FromRoute] string ticker)
        {
            // TODO: Fetch a request from external API
            return Ok();
        }
    }
}
