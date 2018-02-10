namespace FakeBet.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using FakeBet.Models;

    public interface IUserService
    {
        Task RegisterUserAsync(User user);

        Task<User> GetUserAsync(Guid userId);

        Task ActivateUserAsync(Guid userId);

        Task DeactivateUserAsync(Guid userId);

        Task BanUserAsync(Guid userId);
    }
}