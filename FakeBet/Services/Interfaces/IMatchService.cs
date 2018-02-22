using System.Collections.Generic;
using System.Threading.Tasks;
using FakeBet.DTO;
using FakeBet.Models;

namespace FakeBet.Services.Interfaces
{
    public interface IMatchService
    {
        Task AddNewMatchAsync(MatchAddDTO matchAdd);

        Task<Match> GetMatchAsync(string matchId);

        Task<IEnumerable<Match>> GetNotStartedMatchesAsync();

        Task ChangeMatchStatusAsync(string matchId, MatchStatus status);

    }
}