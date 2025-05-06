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
    public class LeaguesRepository : ILeaguesRepository
    {
        private readonly FootballLeagueDbContext _footballLeagueDbContext;
        public LeaguesRepository(FootballLeagueDbContext footballLeagueDbContext)
        {
            _footballLeagueDbContext = footballLeagueDbContext;
        }
    }
}