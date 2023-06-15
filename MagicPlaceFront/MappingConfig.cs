using AutoMapper;
using MagicPlaceFront.Models.Dto;

namespace MagicPlaceFront
{
    public class MappingConfig : Profile
    {

        public MappingConfig()
        {

            CreateMap<RoomDto, RoomCreateDto>().ReverseMap();
            CreateMap<RoomDto, RoomUpdateDto>().ReverseMap();

            CreateMap<OccupantDto, OccupantCreateDto>().ReverseMap();
            CreateMap<OccupantDto, OccupantUpdateDto>().ReverseMap();


        }

    }
}
