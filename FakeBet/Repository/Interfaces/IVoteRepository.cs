using System;
using System.Threading.Tasks;
using FakeBet.Models;

namespace FakeBet.Repository.Interfaces
{
    public interface IVoteRepository
    {
        Task<Vote> GetVoteByIdAsync(Guid id);
        Task AddVoteAsync(Vote vote);
    }
}