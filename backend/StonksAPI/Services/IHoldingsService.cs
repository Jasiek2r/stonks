using Microsoft.Identity.Client;
using StonksAPI.DTO.Holding;
using StonksAPI.Entities;

namespace StonksAPI.Services
{
    public interface IHoldingsService
    {
        public IEnumerable<Holding> GetAllHoldingsForUser(string userId);
        public int CreateHolding(CreateHoldingDto dto, string userId);
        public bool DeleteHolding(int holdingId);
    }
}
