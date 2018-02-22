using FakeBet.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeBet.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<Match> Matches { get; set; }
    }
}