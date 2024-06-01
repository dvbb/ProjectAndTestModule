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
    [Route("api/character")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<CharacterController> _logger;
        private readonly IMapper _mapper;

        public CharacterController(IRepositoryWrapper repositoryWrapper, ILogger<CharacterController> logger, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacter(Guid id)
        {
            try
            {
                var characterEntity = await _repositoryWrapper.Character.GetCharacterById(id);
                if (characterEntity is null)
                {
                    return NotFound($"Character {id} was not found.");
                }
                _mapper.Map<CharacterDto>(characterEntity);
                return Ok(characterEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCharacters([FromQuery] CharacterParameter parameters)
        {
            try
            {
                var pagedList = await _repositoryWrapper.Character.GetCharacters(parameters);
                Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(pagedList.MetaData));
                var result = _mapper.Map<IEnumerable<CharacterDto>>(pagedList);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
