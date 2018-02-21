namespace FakeBet.Repository.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FakeBet.Models;
    using FakeBet.Repository.Interfaces;

    using Microsoft.EntityFrameworkCore;

    using Remotion.Linq.Clauses;

    public class UserRepository : IUserRepository
    {
        // EF 
        private AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        private IQueryable<User> Users => this.context.Users;

        public async Task<User> GetUserAsync(string nickName)
        {
            return await this.Users.SingleOrDefaultAsync(u => u.NickName == nickName);
        }

        public async Task RegisterUserAsync(User user)
        {
            if (await this.GetUserAsync(user.NickName) != null)
            {
                throw new Exception("User with this nickname already exist");
            }

            await this.context.AddAsync(user);
            await this.context.SaveChangesAsync();
        }

        public async Task DeactivateUserAsync(string nickName)
        {
            await this.ChangeUserStatus(nickName, UserStatus.Deactivated);
        }

        public async Task ActivateUserAsync(string nickName)
        {
            await this.ChangeUserStatus(nickName, UserStatus.Active);
        }

        public async Task BanUserAsync(string nickName)
        {
            await this.ChangeUserStatus(nickName, UserStatus.Banned);
        }

        public async Task<List<User>> Get20BestUsersAsync()
        {
            var topusers = this.Users.OrderBy(x => x.Points).Take(20);
            return await topusers.ToListAsync();

        }

        private async Task ChangeUserStatus(string nickName, UserStatus status)
        {
            if (await this.GetUserAsync(nickName) == null)
            {
                throw new Exception("User not found");
            }

            this.context.Users.Single(u => u.NickName == nickName).Status = status;

            await this.context.SaveChangesAsync();
        }
    }
}