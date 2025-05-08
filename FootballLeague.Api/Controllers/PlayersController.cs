using FootballLeague.Application.IRepositories;
using FootballLeague.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IFootballLeagueRepository<Player>  _footballLeagueRepository;

        public PlayersController(IFootballLeagueRepository<Player> footballLeagueRepository)
        {
            _footballLeagueRepository = footballLeagueRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            var players = await _footballLeagueRepository.GetAllRecords();
            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _footballLeagueRepository.GetRecord(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpPost]
        public async Task<ActionResult<Player>> CreatePlayer(Player player)
        {
            if (player == null)
            {
                return BadRequest();
            }

            await _footballLeagueRepository.AddRecord(player);
            return CreatedAtAction(nameof(GetPlayer), new { id = player.PlayerId }, player);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, Player player)
        {
            if (id != player.PlayerId)
            {
                return BadRequest();
            }

            var existingPlayer = await _footballLeagueRepository.GetRecord(id);
            if (existingPlayer == null)
            {
                return NotFound();
            }

            await _footballLeagueRepository.UpdateRecord(player);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _footballLeagueRepository.GetRecord(id);
            if (player == null)
            {
                return NotFound();
            }

            await _footballLeagueRepository.DeleteRecord(player);
            return NoContent();
        }
    }
}