using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballLeague.Domain.Entities;

namespace FootballLeague.Application.IRepositories
{
    public interface ITeamsRepository
    {    
        Task AddTeams(List<Team> teams);

        Task<bool> TeamExistsAsync(string name);
    }
}