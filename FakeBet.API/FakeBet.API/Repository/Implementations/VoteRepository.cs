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
