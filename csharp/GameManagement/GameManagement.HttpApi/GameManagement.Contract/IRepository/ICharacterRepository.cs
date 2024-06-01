using GameManagement.Entities.ReponseType;
using GameManagement.Entities.RequstFeatures;
using GameMenagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.Contract.IRepository
{
    public interface ICharacterRepository : IBaseRepository<Character>
    {
        public Task<Character?> GetCharacterById(Guid id);
        public Task<PagedList<Character>> GetCharacters(CharacterParameter parameter);
    }
}
