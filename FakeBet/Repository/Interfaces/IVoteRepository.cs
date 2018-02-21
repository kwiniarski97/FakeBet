namespace FakeBet.Repository.Interfaces
{
    using System.Threading.Tasks;

    using FakeBet.Models;

    public interface IVoteRepository
    {
        Task AddVoteAsync(Vote vote);
    }
}