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

        public Task<User> GetUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task ActivateUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task DeactivateUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task BanUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}