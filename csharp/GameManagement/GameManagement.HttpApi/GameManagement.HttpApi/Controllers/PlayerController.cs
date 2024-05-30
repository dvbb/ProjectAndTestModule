using AutoMapper;
using GameManagement.Contract.IRepository;
using GameManagement.Entities.Dtos;
using GameManagement.Entities.RequstFeatures;
using GameMenagement.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameManagement.HttpApi.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<PlayerController> _logger;
        private readonly IMapper _mapper;

        public PlayerController(IRepositoryWrapper repositoryWrapper, ILogger<PlayerController> logger, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("deprecated/all")]
        public async Task<IActionResult> GetAllPlayers()
        {
            try
            {
                var players = await _repositoryWrapper.Player.GetAllPlayers();
                var resuilt = _mapper.Map<IEnumerable<PlayerDto>>(players);
                return Ok(resuilt);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayers([FromQuery] PlayerParameter parameter)
        {
            try
            {
                var players = await _repositoryWrapper.Player.GetPlayers(parameter);
                Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(players.MetaData));
                var resuilt = _mapper.Map<IEnumerable<PlayerDto>>(players);
                return Ok(resuilt);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}", Name = "PlayById")]
        public async Task<IActionResult> GetPlayerById(Guid id)
        {
            try
            {
                var player = await _repositoryWrapper.Player.GetPlayerById(id);
                if (player is null)
                {
                    return NotFound();
                }
                var resuilt = _mapper.Map<PlayerDto>(player);
                return Ok(resuilt);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}/characters")]
        public async Task<IActionResult> GetPlayerWithCharacters(Guid id)
        {
            try
            {
                var player = await _repositoryWrapper.Player.GetPlayerWithCharacters(id);
                if (player is null)
                {
                    return NotFound();
                }
                var resuilt = _mapper.Map<PlayerWithCharactersDto>(player);
                return Ok(resuilt);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] PlayerForCreationDto player)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid parameters");
                }

                var playerEntity = _mapper.Map<Player>(player);

                // Create player in sql
                _repositoryWrapper.Player.Create(playerEntity);
                int affectedRow = await _repositoryWrapper.Save();

                // Validate and return 201 if succeeded
                var createdPlayer = _mapper.Map<PlayerDto>(playerEntity);
                return CreatedAtRoute(
                    routeName: "PlayById", //之前定义的路由别名，实际上是GetPlayerById()方法
                    routeValues: new { id = createdPlayer.Id },
                    value: createdPlayer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(Guid id, [FromBody] PlayerForUpdateDto player)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid parameters");
                }

                // Get player
                var playerEntity = await _repositoryWrapper.Player.GetPlayerById(id);
                if (playerEntity is null)
                {
                    return NotFound($"Plaer {id} does not exist.");
                }

                // Update Plyaer
                _mapper.Map(player, playerEntity);
                _repositoryWrapper.Player.Update(playerEntity);
                int affectedRow = await _repositoryWrapper.Save();

                // Validate and return 201 if succeeded
                var createdPlayer = _mapper.Map<PlayerDto>(playerEntity);
                return CreatedAtRoute(
                    routeName: "PlayById", //之前定义的路由别名，实际上是GetPlayerById()方法
                    routeValues: new { id = createdPlayer.Id },
                    value: createdPlayer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid parameters");
                }

                // Get player
                var playerEntity = await _repositoryWrapper.Player.GetPlayerWithCharacters(id);
                if (playerEntity is null)
                {
                    return NotFound($"Player {id} does not exist.");
                }
                if (playerEntity.Characters.Any())
                {
                    return BadRequest($"Player {id} has related characters, cannot be delete");
                }

                _repositoryWrapper.Player.Delete(playerEntity);
                int affectedRow = await _repositoryWrapper.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
