using System;
using System.Threading.Tasks;
using FakeBet.Models;
using FakeBet.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FakeBet.Repository.Implementations
{
    public class VoteRepository : IVoteRepository
    {
        private AppDbContext context;

        public VoteRepository(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<Vote> GetVoteByIdAsync(Guid id)
        {
            return await this.context.Votes.SingleOrDefaultAsync(v => v.VoteId == id);
        }

        public async Task AddVoteAsync(Vote vote)
        {
            await context.Votes.AddAsync(vote);
        }
    }
}
