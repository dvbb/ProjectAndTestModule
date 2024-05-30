﻿using GameManagement.Contract.IRepository;
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

        public Task<List<Player>> GetAllPlayers()
        {
            return FindALL().OrderBy(p => p.Id).ToListAsync();
        }
    }
}