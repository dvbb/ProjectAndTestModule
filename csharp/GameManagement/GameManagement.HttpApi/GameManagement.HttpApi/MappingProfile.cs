using AutoMapper;
using GameManagement.Entities.Dtos;
using GameMenagement.Entities;

namespace GameManagement.HttpApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Player, PlayerDto>();
        }
    }
}
