using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.Entities.Dtos
{

    public class PlayerWithCharactersDto : PlayerDto
    {
        public IEnumerable<CharacterDto> Characters { get; set; }
    }
}
