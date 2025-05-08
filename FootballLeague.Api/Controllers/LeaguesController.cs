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
        private readonly ILeaguesRepository _leaguesRepository;
        private readonly IFootballLeagueRepository<League> _footballLeagueRepository;
        public LeaguesController(ILeaguesRepository leaguesRepository , IFootballLeagueRepository<League> footballLeagueRepository)
        {
           _leaguesRepository = leaguesRepository;
           _footballLeagueRepository = footballLeagueRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeagues()
        {
          var league  = await _footballLeagueRepository.GetAllRecords();
          return Ok(league);
        }
        
        [HttpGet("{leagueId}")]
        public async Task<IActionResult> GetLeagues(int leagueId)
        {
            var league = await _footballLeagueRepository.GetRecord(leagueId);
            if (league == null)
            {
                return NotFound($"League with ID {leagueId} not found.");
            }
            return Ok(league);
        }

        [HttpPost]
        public async Task<IActionResult> AddLeagues([FromBody] League league)
        {
            if (league == null)
            {
                return BadRequest("League cannot be null");
            }

            await _footballLeagueRepository.AddRecord(league);
            return CreatedAtAction(nameof(GetAllLeagues), new { id = league.LeagueId }, league);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLeagues(int leagueId, [FromBody] League league)
        {
            var existingLeague = await _footballLeagueRepository.GetRecord(leagueId);
            if (existingLeague == null)
            {
                return NotFound($"League with ID {leagueId} not found.");
            }

            if (league == null)
            {
                return BadRequest("League cannot be null");
            }

            existingLeague.Name = league.Name;
            existingLeague.Country = league.Country;
            existingLeague.FoundedYear = league.FoundedYear;

            await _footballLeagueRepository.UpdateRecord(existingLeague);
            return NoContent();
        }   

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeagues(int id)
        {
            var league = await _footballLeagueRepository.GetRecord(id);
            if (league == null)
            {
                return NotFound($"League with ID {id} not found.");
            }

            await _footballLeagueRepository.DeleteRecord(league);
            return NoContent();
        }
    }
}