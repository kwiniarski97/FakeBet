using System;
using System.Threading.Tasks;
using FakeBet.API.DTO;

namespace FakeBet.API.Services.Interfaces
{
    public interface IBetService
    {
        Task<BetDTO> GetBetByIdAsync(ulong id);
        Task AddBetAsync(BetDTO betDTO);
    }
}
