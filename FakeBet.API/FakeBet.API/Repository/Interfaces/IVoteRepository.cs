using System;
using System.Threading.Tasks;
using FakeBet.API.Models;

namespace FakeBet.API.Repository.Interfaces
{
    public interface IVoteRepository
    {
        Task<Vote> GetVoteByIdAsync(Guid id);
        Task AddVoteAsync(Vote vote);
    }
}