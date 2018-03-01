using System;
using System.Threading.Tasks;
using FakeBet.DTO;

namespace FakeBet.Services.Interfaces
{
    public interface IVoteService
    {
        Task<VoteDTO> GetVoteByIdAsync(Guid id);
        Task AddVoteAsync(VoteDTO voteDto);
    }
}
