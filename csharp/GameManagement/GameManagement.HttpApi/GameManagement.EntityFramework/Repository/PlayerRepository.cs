using GameManagement.Contract.IRepository;
using GameManagement.Entities.ReponseType;
using GameManagement.Entities.RequstFeatures;
using GameMenagement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.EntityFramework.Repository
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(GameManagementDbContext context) : base(context)
        {
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            return await FindALL().OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Player?> GetPlayerById(Guid playerId)
        {
            return await FindByCondition(p => p.Id == playerId).FirstOrDefaultAsync();
        }

        public async Task<PagedList<Player>> GetPlayers(PlayerParameter parameter)
        {
            return await FindALL().
                OrderBy(p => p.Id)
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .ToPagedList(parameter.PageNumber,parameter.PageSize);
        }

        public async Task<Player?> GetPlayerWithCharacters(Guid playerId)
        {
            return await FindByCondition(p => p.Id == playerId)
                .Include(p => p.Characters)
                .FirstOrDefaultAsync();
        }
    }
}
