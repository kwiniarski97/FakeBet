using System.Collections.Generic;
using System.Threading.Tasks;
using FakeBet.Models;

namespace FakeBet.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string nickName);

        Task RegisterUserAsync(User user);

        Task ChangeUserStatusAsync(string nickName, UserStatus status);

        Task<List<User>> Get20BestUsersAsync();
    }
}