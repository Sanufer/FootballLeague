using System;
using FootballLeague.Application.IRepositories;
using FootballLeague.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaguesController : ControllerBase
    {
        private readonly IFootballLeagueRepository<League> _footballLeagueRepository;

        public LeaguesController(IFootballLeagueRepository<League> footballLeagueRepository)
        {
            _footballLeagueRepository = footballLeagueRepository ?? throw new ArgumentNullException(nameof(footballLeagueRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeagues()
        {
            var leagues = await _footballLeagueRepository.GetAllRecords();
            if (leagues == null || !leagues.Any())
            {
                return NoContent();
            }
            return Ok(leagues);
        }

        [HttpGet("{leagueId:int}")]
        public async Task<IActionResult> GetLeague(int leagueId)
        {
            if (leagueId <= 0)
            {
                return BadRequest("Invalid league ID.");
            }

            var league = await _footballLeagueRepository.GetRecord(leagueId);
            if (league == null)
            {
                return NotFound($"League with ID {leagueId} not found.");
            }
            return Ok(league);
        }

        [HttpPost]
        public async Task<IActionResult> AddLeague([FromBody] League league)
        {
            if (league == null)
            {
                return BadRequest("League cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(league.Name))
            {
                return BadRequest("League name is required.");
            }

            await _footballLeagueRepository.AddRecord(league);
            return CreatedAtAction(nameof(GetLeague), new { leagueId = league.LeagueId }, league);
        }

        [HttpPut("{leagueId:int}")]
        public async Task<IActionResult> UpdateLeague(int leagueId, [FromBody] League league)
        {
            if (leagueId <= 0)
            {
                return BadRequest("Invalid league ID.");
            }

            if (league == null)
            {
                return BadRequest("League cannot be null.");
            }

            var existingLeague = await _footballLeagueRepository.GetRecord(leagueId);
            if (existingLeague == null)
            {
                return NotFound($"League with ID {leagueId} not found.");
            }

            existingLeague.Name = league.Name ?? existingLeague.Name;
            existingLeague.Country = league.Country ?? existingLeague.Country;
            existingLeague.FoundedYear = league.FoundedYear != 0 ? league.FoundedYear : existingLeague.FoundedYear;

            await _footballLeagueRepository.UpdateRecord(existingLeague);
            return NoContent();
        }

        [HttpDelete("{leagueId:int}")]
        public async Task<IActionResult> DeleteLeague(int leagueId)
        {
            if (leagueId <= 0)
            {
                return BadRequest("Invalid league ID.");
            }

            var league = await _footballLeagueRepository.GetRecord(leagueId);
            if (league == null)
            {
                return NotFound($"League with ID {leagueId} not found.");
            }

            await _footballLeagueRepository.DeleteRecord(league);
            return NoContent();
        }
    }
}