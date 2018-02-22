using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBet.Models;
using FakeBet.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FakeBet.Repository.Implementations
{
    public class MatchRepository : IMatchRepository
    {
        private AppDbContext context;

        public MatchRepository(AppDbContext context)
        {
            this.context = context;
        }

        private IQueryable<Match> Matches => context.Matches;

        public async Task AddNewMatchAsync(Match match)
        {
            await context.AddAsync(match);
            await context.SaveChangesAsync();
        }

        public async Task<Match> GetMatchAsync(string matchId)
        {
            return await Matches.SingleOrDefaultAsync(m => m.MatchId == matchId);
        }

        public async Task RemoveMatchAsync(string matchId)
        {
            var match = await GetMatchAsync(matchId);
            context.Remove(match);
            await context.SaveChangesAsync();
        }

        // todo test
        public async Task<IEnumerable<Match>> GetNotStartedMatchesAsync()
        {
            var matches = await (from match in Matches where match.Status == MatchStatus.NonStarted select match)
                              .ToListAsync();

            return matches;
        }

        public async Task ChangeMatchStatusAsync(string matchId, MatchStatus status)
        {
            if (await GetMatchAsync(matchId) == null)
            {
                throw new Exception("Match doesn't exist");
            }

            context.Matches.Single(m => m.MatchId == matchId).Status = status;
            await context.SaveChangesAsync();
        }
    }
}