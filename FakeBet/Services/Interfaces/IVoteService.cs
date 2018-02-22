using System.Threading.Tasks;
using FakeBet.DTO;

namespace FakeBet.Services.Interfaces
{
    public interface IVoteService
    {
        Task AddVoteAsync(VoteAddDTO voteDto);
    }
}
