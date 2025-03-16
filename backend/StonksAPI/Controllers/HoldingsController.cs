using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StonksAPI.DTO.Holding;
using StonksAPI.Entities;
using StonksAPI.Migrations;
using StonksAPI.Services;
using System.Security.Claims;

namespace StonksAPI.Controllers
{

    /*
     * REST controller for managing user's virtual portfolio and tracking their investments
     */
    [Route("holdings")]
    [Authorize]
    public class HoldingsController : Controller
    {
        private readonly IHoldingsService _holdingsService;
        private readonly UserDbContext _userDbContext;
        public HoldingsController(IHoldingsService holdingsService, UserDbContext userDbContext)
        {
            _holdingsService = holdingsService;
            _userDbContext = userDbContext;
        }

        [HttpGet("{id}")]
        public ActionResult<Holding> GetHolding([FromRoute] int id)
        {
            var holding = _userDbContext.Holdings.FirstOrDefault(x => x.Id == id);
            return Ok(holding);
        }


        [HttpGet]
        public ActionResult<IEnumerable<Holding>> GetAllHoldingsForUser()
        {
            //Fetches all financial assets held by the currently logged in user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var holdings = _holdingsService.GetAllHoldingsForUser(userId);
            return Ok(holdings);

        }

        [HttpPost]
        public ActionResult CreateHolding([FromBody] CreateHoldingDto dto)
        {
            //Creates a new asset owned by the currently logged in user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int createdHoldingId = _holdingsService.CreateHolding(dto, userId);

            return Created($"/holdings/{createdHoldingId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteHolding([FromRoute] int id) {
            var result = _holdingsService.DeleteHolding(id);
            if (!result) return NotFound(id);
            return Ok();
        }
    }
}
