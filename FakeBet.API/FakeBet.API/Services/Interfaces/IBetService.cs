
using System.Collections.Generic;
using System.Threading.Tasks;
using FakeBet.API.DTO;
using FakeBet.API.Models;

namespace FakeBet.API.Services.Interfaces
{
    public interface IBetService
    {
        Task<BetDTO> GetBetByIdAsync(ulong id);
        Task AddBetAsync(BetDTO betDTO);
        Task<IEnumerable<Bet>> GetWinnersBetsByMatchIdAsync(string matchId, Team winner);
    }
}