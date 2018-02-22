using System.Threading.Tasks;
using FakeBet.Models;
using FakeBet.Repository.Interfaces;

namespace FakeBet.Repository.Implementations
{
    public class VoteRepository : IVoteRepository
    {
        private AppDbContext context;

        public VoteRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddVoteAsync(Vote vote)
        {
            await context.Votes.AddAsync(vote);
        }
    }
}
