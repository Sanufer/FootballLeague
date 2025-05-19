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
        private readonly ITeamsRepository _teamsRepository;
        public TeamsController(IFootballLeagueRepository<Team> footballLeagueRepository, ITeamsRepository teamsRepository)
        {
            _footballLeagueRepository = footballLeagueRepository;
            _teamsRepository = teamsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var teams = await _footballLeagueRepository.GetAllRecords();
            if (teams == null || !teams.Any())
            {
                return NotFound("No teams found.");
            }
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid team ID.");
            }

            var team = await _footballLeagueRepository.GetRecord(id);
            if (team == null)
            {
                return NotFound($"Team with ID {id} not found.");
            }
            return Ok(team);
        }

        [HttpGet("league/{leagueId}")]
        public async Task<ActionResult<List<Team>>> GetTeamsByLeagueId(int leagueId)
        {
           if(leagueId <= 0)
           {
               return BadRequest("Invalid league ID.");
           }

           var teams = await _teamsRepository.GetTeamsByLeagueIdAsync(leagueId);
           if(teams == null || teams.Any())
           {
               return NotFound($"No teams found for league ID {leagueId}.");
           }
           return Ok(teams);
        }        

        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam([FromBody] Team team)
        {
            if (team == null)
            {
                return BadRequest("Team cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(team.Name))
            {
                return BadRequest("Team name is required.");
            }

            await _footballLeagueRepository.AddRecord(team);
            return CreatedAtAction(nameof(GetTeam), new { id = team.TeamId }, team);
        }

        [HttpPost("teams/batch")]
        public async Task<ActionResult> CreateTeams([FromBody] List<Team> teams)
        {
            if (teams == null || !teams.Any())
            {
                return BadRequest("No teams provided.");
            }

            var teamsToAdd = new List<Team>();
            var existingTeams = new List<string>();

            foreach (var item in teams)
            {
                if (await _teamsRepository.TeamExistsAsync(item.Name))
                {
                    existingTeams.Add(item.Name);
                }
                else
                {
                    teamsToAdd.Add(item);
                }
            }

            if (existingTeams.Any())
            {
                return Conflict($"The following teams already exist: {string.Join(", ", existingTeams)}");
            }

            await _teamsRepository.AddTeams(teamsToAdd);
            return CreatedAtAction(nameof(GetTeams), teamsToAdd);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] Team team)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid team ID.");
            }

            if (team == null)
            {
                return BadRequest("Team cannot be null.");
            }

            var existingTeam = await _footballLeagueRepository.GetRecord(id);
            if (existingTeam == null)
            {
                return NotFound($"Team with ID {id} not found.");
            }

            existingTeam.Name = team.Name ?? existingTeam.Name;
            existingTeam.Coach = team.Coach ?? existingTeam.Coach;
            existingTeam.Stadium = team.Stadium ?? existingTeam.Stadium;
            existingTeam.FoundedYear = team.FoundedYear;
            existingTeam.City = team.City ?? existingTeam.City;
            existingTeam.LeagueId = team.LeagueId;

            await _footballLeagueRepository.UpdateRecord(team);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid team ID.");
            }

            var team = await _footballLeagueRepository.GetRecord(id);
            if (team == null)
            {
                return NotFound($"Team with ID {id} not found.");
            }

            await _footballLeagueRepository.DeleteRecord(team);
            return NoContent();
        }
    }
}
