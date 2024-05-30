using AutoMapper;
using GameManagement.Entities.Dtos;
using GameMenagement.Entities;

namespace GameManagement.HttpApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // entity => dto
            CreateMap<Player, PlayerDto>();
            CreateMap<Character, CharacterDto>();
            CreateMap<Player, PlayerWithCharactersDto>();

            // dto => entity
            CreateMap<PlayerForCreationDto, Player>();
            CreateMap<PlayerForUpdateDto, Player>();
        }
    }
}
