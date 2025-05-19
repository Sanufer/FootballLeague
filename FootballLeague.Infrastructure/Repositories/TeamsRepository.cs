using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballLeague.Application.IRepositories;
using FootballLeague.Domain.Entities;
using FootballLeague.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Infrastructure.Repositories
{
    public class TeamsRepository : ITeamsRepository
    {
        private readonly FootballLeagueDbContext _footballLeagueDbContext;

        public TeamsRepository(FootballLeagueDbContext footballLeagueDbContext)
        {
            _footballLeagueDbContext = footballLeagueDbContext;
        }

        public async Task AddTeams(List<Team> teams)
        {
          await _footballLeagueDbContext.Teams.AddRangeAsync(teams);
          await _footballLeagueDbContext.SaveChangesAsync();
        }

        public async Task<bool> TeamExistsAsync(string name)
        {
            return  await _footballLeagueDbContext.Teams.AnyAsync(t => t.Name == name);
        }

        public async Task<List<Team>> GetTeamsByLeagueIdAsync(int leagueId)
        {
            return await _footballLeagueDbContext.Teams
            .Where(t => t.LeagueId == leagueId)
            .ToListAsync();
        }
    }
}