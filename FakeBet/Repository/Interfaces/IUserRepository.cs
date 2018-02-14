using FakeBet.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FakeBet.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string nickName);

        Task RegisterUserAsync(User user);

        Task DeactivateUserAsync(string nickName);

        Task ActivateUserAsync(string nickName);

        Task BanUserAsync(string nickName);
    }
}