using FootballLeague.Application.IRepositories;
using FootballLeague.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FootballLeague.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IFootballLeagueRepository<Player> _footballLeagueRepository;
        private readonly IPlayersRepository _playersRepository;

        public PlayersController(IFootballLeagueRepository<Player> footballLeagueRepository, IPlayersRepository playersRepository)
        {
            _footballLeagueRepository = footballLeagueRepository;
            _playersRepository = playersRepository;
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

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayersByTeamId(int teamId)
        {
            var players = await _playersRepository.GetPlayersByTeamIdAsync(teamId);
            if (players == null || !players.Any())
            {
                return NotFound($"No players found for team ID {teamId}.");
            }
            return Ok(players);
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

        [HttpPost("players/batch")]
        public async Task<ActionResult> CreatePlayers([FromBody] List<Player> players)
        {
            if ((players == null) || !players.Any())
            {
                return BadRequest("No players provided.");
            }

            var playersToAdd = new List<Player>();
            var existingPlayers = new List<string>();

            foreach (var item in players)
            {
                if (await _playersRepository.PlayerExistsAsync(item.Name))
                {
                    existingPlayers.Add(item.Name);
                }
                else
                {
                    playersToAdd.Add(new Player
                    {
                        Name = item.Name,
                        Position = item.Position,
                        Nationality = item.Nationality,
                        Age = item.Age,
                        TeamId = item.TeamId
                    });
                }
            }

            if (!playersToAdd.Any())
                return Conflict("All provided players already exist.");

            await _playersRepository.AddPlayers(playersToAdd);

            if (existingPlayers.Any())
            {
                return Ok($"{playersToAdd.Count} players added successfully. The following players were skipped as they already exist: {string.Join(", ", existingPlayers)}");
            }
            return Ok($"{players.Count} players added successfully.");
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