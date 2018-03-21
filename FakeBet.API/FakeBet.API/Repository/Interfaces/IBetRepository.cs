using System;
using System.Threading.Tasks;
using FakeBet.API.Models;

namespace FakeBet.API.Repository.Interfaces
{
    public interface IBetRepository
    {
        Task<Bet> GetBetByIdAsync(ulong id);
        Task AddBetAsync(Bet bet);
    }
}