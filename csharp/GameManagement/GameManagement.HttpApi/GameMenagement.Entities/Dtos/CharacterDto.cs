using GameMenagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.Entities.Dtos
{
    public class CharacterDto
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string classes { get; set; }
        public int Level { get; set; }
        public DateTime DateCreated { get; set; }
    }

}
