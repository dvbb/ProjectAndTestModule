using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameMenagement.Entities.Player;

namespace GameMenagement.Entities
{
    public class Character
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string classes { get; set; }
        public int Level { get; set; }
        public DateTime DateCreated { get; set; }

        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
