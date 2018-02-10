namespace FakeBet.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using FakeBet.Models;

    public interface IUserService
    {
        Task RegisterUserAsync(User user);

        Task<User> GetUserAsync(string nickName);

        Task ActivateUserAsync(string nickName);

        Task DeactivateUserAsync(string nickName);

        Task BanUserAsync(string nickName);
    }
}