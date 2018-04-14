using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBet.API.Models;
using FakeBet.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FakeBet.API.Repository.Implementations
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

        public async Task<IEnumerable<Match>> GetNotStartedMatchesAsync()
        {
            var matches = await (from match in Matches where match.Status == MatchStatus.NonStarted select match)
                .ToListAsync();

            return matches;
        }

        public async Task UpdateMatchAsync(Match match)
        {
            var original = await GetMatchAsync(match.MatchId);
            original.MapValuesWhenNotNullOrAreDifferent(match);
            context.Matches.Update(original);
            await this.context.SaveChangesAsync();
        }
        
        public async Task EndMatchAsync(Match match)
        {
            var original = await GetMatchAsync(match.MatchId);
            original.Winner = match.Winner;
            context.Matches.Update(original);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            var matches = this.context.Matches.Select(x => x).Where(match => match.MatchTime.AddDays(7) > DateTime.UtcNow);
            return matches;
        }
    }
}