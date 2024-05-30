using GameMenagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.Contract.IRepository
{
    public interface IPlayerRepository : IBaseRepository<Player>
    {
        public Task<List<Player>> GetAllPlayers();
        public Task<Player?> GetPlayerById(Guid playerId);
        public Task<Player?> GetPlayerWithCharacters(Guid playerId);
    }
}
