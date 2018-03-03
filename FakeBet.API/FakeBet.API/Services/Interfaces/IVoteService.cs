using System;
using System.Threading.Tasks;
using FakeBet.API.DTO;

namespace FakeBet.API.Services.Interfaces
{
    public interface IVoteService
    {
        Task<VoteDTO> GetVoteByIdAsync(Guid id);
        Task AddVoteAsync(VoteDTO voteDto);
    }
}
