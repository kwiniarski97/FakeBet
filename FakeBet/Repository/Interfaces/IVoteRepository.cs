using System.Threading.Tasks;
using FakeBet.Models;

namespace FakeBet.Repository.Interfaces
{
    public interface IVoteRepository
    {
        Task AddVoteAsync(Vote vote);
    }
}