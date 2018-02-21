namespace FakeBet.Repository.Implementations
{
    using System.Threading.Tasks;

    using FakeBet.Models;
    using FakeBet.Repository.Interfaces;
    public class VoteRepository : IVoteRepository
    {
        private AppDbContext context;

        public VoteRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddVoteAsync(Vote vote)
        {
            await this.context.Votes.AddAsync(vote);
        }
    }
}
