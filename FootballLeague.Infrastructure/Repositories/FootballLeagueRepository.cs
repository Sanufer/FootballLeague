using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballLeague.Application.IRepositories;
using FootballLeague.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.Infrastructure.Repositories
{
    public class FootballLeagueRepository<T> : IFootballLeagueRepository<T> where T : class
    {
        private readonly FootballLeagueDbContext _footballLeagueDbContext;
        public FootballLeagueRepository(FootballLeagueDbContext footballLeagueDbContext)
        {
            _footballLeagueDbContext = footballLeagueDbContext;
        }

        public async Task<IEnumerable<T>> GetAllRecords()
        {
            return await _footballLeagueDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetRecord(int id)
        {
            return await _footballLeagueDbContext.Set<T>().FindAsync(id);
        }

        public async Task AddRecord(T dbRecord)
        {
            _footballLeagueDbContext.Set<T>().Add(dbRecord);
            await _footballLeagueDbContext.SaveChangesAsync();
        }

        public async Task UpdateRecord(T dbRecord)
        {
            _footballLeagueDbContext.Set<T>().Update(dbRecord);
            await _footballLeagueDbContext.SaveChangesAsync();
        }

        public async Task DeleteRecord(T dbRecord)
        {
            _footballLeagueDbContext.Set<T>().Remove(dbRecord);
            await _footballLeagueDbContext.SaveChangesAsync();
        }
    }
}