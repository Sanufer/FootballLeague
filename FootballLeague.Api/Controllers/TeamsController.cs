using System;
using FootballLeague.Application.IRepositories;
using FootballLeague.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly IFootballLeagueRepository<Team> _footballLeagueRepository;
        public TeamsController(IFootballLeagueRepository<Team> footballLeagueRepository)
        {
            _footballLeagueRepository = footballLeagueRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var teams = await _footballLeagueRepository.GetAllRecords();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _footballLeagueRepository.GetRecord(id);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam([FromBody] Team team)
        {
            if (team == null)
            {
                return BadRequest();
            }

            await _footballLeagueRepository.AddRecord(team);
            return CreatedAtAction(nameof(GetTeam), new { id = team.TeamId }, team);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] Team team)
        {
            if (id != team.TeamId)
            {
                return BadRequest();
            }

            var existingTeam = await _footballLeagueRepository.GetRecord(id);
            if (existingTeam == null)
            {
                return NotFound();
            }
             
            existingTeam.Name = team.Name;
            existingTeam.Coach = team.Coach;
            existingTeam.Stadium = team.Stadium;
            existingTeam.FoundedYear = team.FoundedYear;
            existingTeam.City = team.City;
            existingTeam.LeagueId = team.LeagueId;

            await _footballLeagueRepository.UpdateRecord(team);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _footballLeagueRepository.GetRecord(id);
            if (team == null)
            {
                return NotFound();
            }

            await _footballLeagueRepository.DeleteRecord(team);
            return NoContent();
        }
    }
}
