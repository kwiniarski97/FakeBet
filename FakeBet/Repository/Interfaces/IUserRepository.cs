using FakeBet.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FakeBet.Repository.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }

        Task<User> GetUserAsync(Guid userId);

        Task RegisterUserAsync(User user);

        Task DeactivateUserAsync(Guid userId);

        Task ActivateUserAsync(Guid userId);

        Task BanUserAsync(Guid userId);
    }
}