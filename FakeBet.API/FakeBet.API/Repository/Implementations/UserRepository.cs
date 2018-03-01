using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBet.API.Models;
using FakeBet.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FakeBet.API.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        // EF 
        private AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        private IQueryable<User> Users => context.Users;

        public async Task<User> GetUserAsync(string nickName)
        {
            return await Users.SingleOrDefaultAsync(u => u.NickName == nickName);
        }

        public async Task RegisterUserAsync(User user)
        {
            await context.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task ChangeUserStatusAsync(string nickName, UserStatus status)
        {
            context.Users.Single(u => u.NickName == nickName).Status = status;

            await context.SaveChangesAsync();
        }

        public async Task<List<User>> Get20BestUsersAsync()
        {
            //todo moze byc ze trzeba bedzie zrobic async
            var usersSorted = await Users.OrderBy(x => x.Points).Take(20).ToListAsync();
            return usersSorted;
        }
    }
}