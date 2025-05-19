using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballLeague.Domain.Entities;

namespace FootballLeague.Application.IRepositories
{
    public interface IPlayersRepository
    {
        Task AddPlayers(List<Player> players);

        Task<bool> PlayerExistsAsync(string name);

        Task<List<Player>> GetPlayersByTeamIdAsync(int teamId);
    }
}