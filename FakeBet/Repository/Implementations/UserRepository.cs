namespace FakeBet.Repository.Implementations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FakeBet.Models;
    using FakeBet.Repository.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class UserRepository : IUserRepository
    {
        // EF 
        private AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<User> Users => this.context.Users;

        public async Task<User> GetUserAsync(Guid userId)
        {
            return await this.Users.SingleOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task RegisterUserAsync(User user)
        {
            await this.context.AddAsync(user);
            await this.context.SaveChangesAsync();
        }

        public async Task DeactivateUserAsync(Guid userId)
        {
            var user = await this.GetUserAsync(userId);

            if (user != null)
            {
                this.context.Remove(user);

                user.Status = UserStatus.Deactivated;

                await this.context.AddAsync(user);

                await this.context.SaveChangesAsync();
            }
        }

        public async Task ActivateUserAsync(Guid userId)
        {
            var user = await this.GetUserAsync(userId);

            this.context.Remove(user);

            user.Status = UserStatus.Active;

            await this.context.AddAsync(user);

            await this.context.SaveChangesAsync();
        }

        public async Task BanUserAsync(Guid userId)
        {
            var user = await this.GetUserAsync(userId);

            this.context.Remove(user);

            user.Status = UserStatus.Banned;

            await this.context.AddAsync(user);

            await this.context.SaveChangesAsync();
        }
    }
}