using FakeBet.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeBet.API.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Match> Matches { get; set; }
    }
}