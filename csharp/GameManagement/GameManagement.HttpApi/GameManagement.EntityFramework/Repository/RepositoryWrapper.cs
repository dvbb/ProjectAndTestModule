using GameManagement.Contract.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.EntityFramework.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly GameManagementDbContext _context;
        private IPlayerRepository _player;
        private ICharacterRepository _character;

        public IPlayerRepository Player
        {
            get { return _player ??= new PlayerRepository(_context); }
        }

        public ICharacterRepository Character
        {
            get { return _character ??= new CharacterRepository(_context); }
        }

        public Task<int> Save()
        {
            return _context.SaveChangesAsync();
        }
    }
}
