using GameMenagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.EntityFramework
{
    public static class DataSeed
    {
        private static readonly Guid[] _guids =
        {
            Guid.NewGuid(),
            Guid.NewGuid()
        };

        public static Player[] Players { get; } = {
            new Player
            {
                Id = _guids[0],
                Account ="mw2021",
                AccountType ="Free",
                DateCreated = DateTime.Now
            },
            new Player
            {
                Id = _guids[1],
                Account ="dc2021",
                AccountType ="Free",
                DateCreated = DateTime.Now
            }
        };

        public static Character[] Characters { get; } =
        {
            new Character
            {
                Id = Guid.NewGuid(),
                Nickname ="Code Man",
                classes ="Mage",
                Level = 99,
                PlayerId =_guids[0],
                DateCreated = DateTime.Now,
            },
            new Character
            {
                Id = Guid.NewGuid(),
                Nickname ="WZ",
                classes ="Warrior",
                Level = 99,
                PlayerId =_guids[0],
                DateCreated = DateTime.Now,
            },
            new Character
            {
                Id = Guid.NewGuid(),
                Nickname ="asaka",
                classes ="Druid",
                Level = 29,
                PlayerId =_guids[0],
                DateCreated = DateTime.Now,
            },
            new Character
            {
                Id = Guid.NewGuid(),
                Nickname ="MyWon",
                classes ="Mage",
                Level = 5,
                PlayerId =_guids[1],
                DateCreated = DateTime.Now,
            },
            new Character
            {
                Id = Guid.NewGuid(),
                Nickname ="TBD",
                classes ="Wizzard",
                Level = 95,
                PlayerId =_guids[1],
                DateCreated = DateTime.Now,
            }
        };
    }
}
