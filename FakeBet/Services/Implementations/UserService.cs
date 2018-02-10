namespace FakeBet.Services.Implementations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FakeBet.Models;
    using FakeBet.Repository.Interfaces;
    using FakeBet.Services.Interfaces;

    public class UserService : IUserService
    {
        private IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task RegisterUserAsync(User user)
        {
           await this.repository.RegisterUserAsync(user);
        }

        public async Task<User> GetUserAsync(string nickName)
        {
            return await this.repository.GetUserAsync(nickName);
        }

        public async Task ActivateUserAsync(string nickName)
        {
            await this.repository.ActivateUserAsync(nickName);
        }

        public async Task DeactivateUserAsync(string nickName)
        {
            await this.repository.DeactivateUserAsync(nickName);
        }

        public async Task BanUserAsync(string nickName)
        {
            await this.repository.BanUserAsync(nickName);
        }
    }
}