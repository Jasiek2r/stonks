using StonksAPI.DTO;
using StonksAPI.Entities;
using System.Security.Claims;
using StonksAPI.Exceptions;
using AutoMapper;
using StonksAPI.Utility;


namespace StonksAPI.Services
{
    public class HoldingsService : IHoldingsService
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMapper _mapper;
        public HoldingsService(UserDbContext userDbContext, IMapper mapper) {
            _userDbContext = userDbContext;
            _mapper = mapper;
        }
        public int CreateHolding(CreateHoldingDto dto, string userId)
        {
            var holding = _mapper.Map<Holding>(dto);

            //update user id from NameIdentifier Claim
            if (!int.TryParse(userId, out int holdingUserId))
            {
                throw new BadRequestException("Failed to create a new holding, user is not logged in");
            }
            holding.UserId = holdingUserId;

            //add holding to database
            _userDbContext.Holdings.Add(holding);
            _userDbContext.SaveChanges();

            return holding.Id;
        }

        public IEnumerable<Holding> GetAllHoldingsForUser(string userId)
        {
            if (!int.TryParse(userId, out int holdingUserId))
            {
                throw new BadRequestException("Holdings fetch failed, user is not logged in");
            }
            //Fetch holdings from the database and filter them to only those owned by current user
            var holdings = _userDbContext.Holdings.Where(r => r.UserId == holdingUserId).ToList();
            return holdings;
        }

        public bool DeleteHolding(int holdingId)
        {
            var holding = _userDbContext.Holdings.FirstOrDefault(x => x.Id == holdingId);
            if (holding == null)
            {
                // If the holding does not appear in our database, there is nothing to delete
                return false;
            }
            // Otherwise remove the holding
            _userDbContext.Holdings.Remove(holding);
            _userDbContext.SaveChanges();
            return true;
        }
    }
}
