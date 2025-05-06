using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeague.Application.IRepositories
{
    public interface IFootballLeagueRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllRecords();

        Task<T> GetRecord(int id);

        Task AddRecord(T dbRecord);

        Task UpdateRecord(T dbRecord);

        Task DeleteRecord(T dbRecord);
    }
}