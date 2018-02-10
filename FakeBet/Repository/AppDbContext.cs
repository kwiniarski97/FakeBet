using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public DbSet<HighScore> HighScore { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<Match> Matches { get; set; }
    }
}