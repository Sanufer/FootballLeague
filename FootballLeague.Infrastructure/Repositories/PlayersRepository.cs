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
    public class PlayersRepository : IPlayersRepository
    {
        private readonly FootballLeagueDbContext _footballLeagueDbContext;
        public PlayersRepository(FootballLeagueDbContext footballLeagueDbContext)
        {
            _footballLeagueDbContext = footballLeagueDbContext;
        }

        public async Task AddPlayers(List<Player> players)
        {
            await _footballLeagueDbContext.Players.AddRangeAsync(players);
            await _footballLeagueDbContext.SaveChangesAsync();
        }

        public Task<List<Player>> GetPlayersByTeamIdAsync(int teamId)
        {
            return _footballLeagueDbContext.Players
                .Where(p => p.TeamId == teamId)
                .ToListAsync();
        }

        public async Task<bool> PlayerExistsAsync(string name)
        {
           return await _footballLeagueDbContext.Players.AnyAsync(p=> p.Name == name);
        }
    }
}