using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StonksAPI.DTO;
using StonksAPI.Entities;
using StonksAPI.Migrations;
using StonksAPI.Services;
using System.Security.Claims;

namespace StonksAPI.Controllers
{
    [Route("holdings")]
    public class HoldingsController : Controller
    {
        private readonly IHoldingsService _holdingsService;
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;
        public HoldingsController(IHoldingsService holdingsService, UserDbContext userDbContext, IMapper mapper)
        {
            _holdingsService = holdingsService;
            _userDbContext = userDbContext;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Holding> GetHolding([FromRoute] int id)
        {
            var holding = _userDbContext.Holdings.FirstOrDefault(x => x.Id == id);
            return Ok(holding);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Holding>> GetAllHoldingsForUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userId, out int holdingUserId))
            {
                return BadRequest();
            }
            var holdings = _userDbContext.Holdings.Where(r => r.UserId == holdingUserId).ToList();
            return Ok(holdings);
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateHolding([FromBody] CreateHoldingDto dto)
        {
            var holding = _mapper.Map<Holding>(dto);

            //update user id from NameIdentifier Claim
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userId, out int holdingUserId))
            {
                return BadRequest();
            }
            holding.UserId = holdingUserId;

            //add holding to database
            _userDbContext.Holdings.Add(holding);
            _userDbContext.SaveChanges();

            return Created($"/holdings/{holding.Id}", null);
        }
    }
}
