using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBet.API.Models;
using FakeBet.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FakeBet.API.Repository.Implementations
{
    public class BetRepository : IBetRepository
    {
        private AppDbContext context;

        public BetRepository(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<Bet> GetBetByIdAsync(ulong id)
        {
            return await this.context.Bets.SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task AddBetAsync(Bet bet)
        {
            await context.Bets.AddAsync(bet);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Bet>> GetBetsByMatchIdAsync(string matchId)
        {
            return this.context.Bets.Select(x => x).Where(x => x.MatchId == matchId).AsEnumerable();
        }

        public async Task<IEnumerable<Bet>> GetWinnersByMatchIdAsync(string matchId, Team winner) 
        {
            return winner == Team.A
                ? this.context.Bets.Select(bet => bet)
                    .Where(bet => bet.MatchId == matchId && bet.BetOnTeamA > 0)
                : this.context.Bets.Select(bet => bet)
                    .Where(bet => bet.MatchId == matchId && bet.BetOnTeamB > 0);
        }
    }
}