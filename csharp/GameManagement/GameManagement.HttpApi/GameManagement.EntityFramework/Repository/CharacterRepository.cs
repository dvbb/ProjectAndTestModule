using GameManagement.Contract.IRepository;
using GameManagement.Entities.ReponseType;
using GameManagement.Entities.RequstFeatures;
using GameManagement.EntityFramework.Repository.Extension;
using GameMenagement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.EntityFramework.Repository
{
    public class CharacterRepository : BaseRepository<Character>, ICharacterRepository
    {
        public CharacterRepository(GameManagementDbContext context) : base(context)
        {
        }

        public async Task<Character?> GetCharacterById(Guid id)
        {
            return await FindByCondition(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<PagedList<Character>> GetCharacters(CharacterParameter parameter)
        {
            return await FindALL()
                .OrderByQuery(parameter.OrderBy)
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .ToPagedList(parameter.PageNumber, parameter.PageSize);
        }
    }
}
