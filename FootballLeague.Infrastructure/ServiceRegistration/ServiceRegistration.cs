using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballLeague.Application.IRepositories;
using FootballLeague.Domain.Entities;
using FootballLeague.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FootballLeague.Infrastructure.ServiceRegistration
{
  public static class RegisterInfrastructureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ILeaguesRepository,LeaguesRepository>();
        services.AddScoped<IPlayersRepository,PlayersRepository>();
        services.AddScoped<ITeamsRepository, TeamsRepository>();
        services.AddScoped(typeof(IFootballLeagueRepository<>), typeof(FootballLeagueRepository<>));
        return services;
    }
}
}