using System;
using System.Threading.Tasks;
using FakeBet.API.Models;
using FakeBet.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FakeBet.API.Repository.Implementations
{
    public class VoteRepository : IVoteRepository
    {
        private AppDbContext context;

        public VoteRepository(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<Bet> GetVoteByIdAsync(Guid id)
        {
            return await this.context.Bets.SingleOrDefaultAsync(v => v.BetId == id);
        }

        public async Task AddVoteAsync(Bet bet)
        {
            await context.Bets.AddAsync(bet);
        }
    }
}
