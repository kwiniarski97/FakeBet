namespace FakeBet.Repository.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FakeBet.Models;
    using FakeBet.Repository.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class MatchRepository : IMatchRepository
    {
        private AppDbContext context;

        public MatchRepository(AppDbContext context)
        {
            this.context = context;
        }

        private IQueryable<Match> Matches => this.context.Matches;

        public async Task AddNewMatchAsync(Match match)
        {
            await this.context.AddAsync(match);
            await this.context.SaveChangesAsync();
        }

        public async Task<Match> GetMatchAsync(string matchId)
        {
            return await this.Matches.SingleOrDefaultAsync(m => m.MatchId == matchId);
        }

        public async Task RemoveMatchAsync(string matchId)
        {
            var match = await this.GetMatchAsync(matchId);
            this.context.Remove(match);
            await this.context.SaveChangesAsync();
        }

        // todo test
        public async Task<IEnumerable<Match>> GetNotStartedMatchesAsync()
        {
            var matches = await (from match in this.Matches where match.Status == MatchStatus.NonStarted select match)
                              .ToListAsync();

            return matches;
        }

        public async Task ChangeMatchStatusAsync(string matchId, MatchStatus status)
        {
            if (await this.GetMatchAsync(matchId) == null)
            {
                throw new Exception("Match doesn't exist");
            }

            this.context.Matches.Single(m => m.MatchId == matchId).Status = status;
            await this.context.SaveChangesAsync();
        }
    }
}