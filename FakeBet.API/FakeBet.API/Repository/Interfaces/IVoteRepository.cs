using System;
using System.Threading.Tasks;
using FakeBet.API.Models;

namespace FakeBet.API.Repository.Interfaces
{
    public interface IVoteRepository
    {
        Task<Bet> GetVoteByIdAsync(Guid id);
        Task AddVoteAsync(Bet bet);
    }
}