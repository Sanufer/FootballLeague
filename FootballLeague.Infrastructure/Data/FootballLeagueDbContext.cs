using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Infrastructure.Data
{
    public class FootballLeagueDbContext : DbContext
    {
        public FootballLeagueDbContext(DbContextOptions<FootballLeagueDbContext> options) : base(options)
        {

        }

        public DbSet<Leagues> Leagues { get; set; }

        public DbSet<Teams> Teams { get; set; }

        public DbSet<Player> Player { get; set; }

        public DbSet<PlayerStats> PlayerStats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}