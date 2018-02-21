namespace FakeBet.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FakeBet.Models;

    public interface IUserRepository
    {
        Task<User> GetUserAsync(string nickName);

        Task RegisterUserAsync(User user);

        Task DeactivateUserAsync(string nickName);

        Task ActivateUserAsync(string nickName);

        Task BanUserAsync(string nickName);

        Task<List<User>> Get20BestUsersAsync();
    }
}