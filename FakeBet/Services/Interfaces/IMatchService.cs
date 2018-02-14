namespace FakeBet.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FakeBet.DTO;
    using FakeBet.Models;

    public interface IMatchService
    {
        Task AddNewMatchAsync(MatchAddDTO matchAdd);

        Task<Match> GetMatchAsync(string matchId);

        Task<IEnumerable<Match>> GetNotStartedMatchesAsync();

        Task ChangeMatchStatusAsync(string matchId, MatchStatus status);

    }
}