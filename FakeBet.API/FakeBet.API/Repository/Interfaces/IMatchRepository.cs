using System.Collections.Generic;
using System.Threading.Tasks;
using FakeBet.API.Models;

namespace FakeBet.API.Repository.Interfaces
{
    public interface IMatchRepository
    {
        Task AddNewMatchAsync(Match match);

        Task<Match> GetMatchAsync(string matchId);

        Task RemoveMatchAsync(string matchId);

        Task<IEnumerable<Match>> GetNotStartedMatchesAsync();

        Task ChangeMatchStatusAsync(string matchId, MatchStatus status);
}
}