using AutoMapper;
using CSRedis;
using GameManagement.Contract.IRepository;
using GameManagement.Entities.Dtos;
using GameManagement.Entities.RequstFeatures;
using GameManagement.HttpApi.Redis;
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

        [HttpGet("redis/test/{datas}")]
        public async Task<IActionResult> RedisTest(string datas)
        {
            RedisClient client = new RedisClient("127.0.0.1", 6379);
            Random random = new Random();
            client.Set(random.Next(9999).ToString(), datas);

          
            //读取
            string name = client.Get("name");
            string pwd = client.Get("password");

            //存储
            client.Set("name", "11");
            client.Set("password", "22");

            //存list集合
            List<int> ls = new List<int>();
            ls.Add(131);
            ls.Add(2564);
            ls.Add(5465);
            client.Set("1", ls);


            return Ok("");
        }
    }
}
