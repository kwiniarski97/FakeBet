using System;
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
    }
}