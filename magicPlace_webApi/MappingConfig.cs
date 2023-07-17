using AutoMapper;
using magicPlace_webApi.Models;
using magicPlace_webApi.Models.Dto;

namespace magicPlace_webApi
{
    public class MappingConfig: Profile
    {


        //creamos el constructor para poder hacer las relaciones de mapeo 

        public MappingConfig()
        {
            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();

            //veremos lo mismo pero en una sola linea mediante el metodo ReverseMap();

            CreateMap<Room, RoomCreateDto>().ReverseMap();


            CreateMap<Room, RoomUpdateDto>().ReverseMap();

            //repetiremos el proceso de mapeo con la clase Occupant

            CreateMap<Occupant, OccupantDto>();
            CreateMap<OccupantDto, Occupant>();

            //veremos lo mismo pero en una sola linea mediante el metodo ReverseMap();

            CreateMap<Occupant, OccupantCreateDto>().ReverseMap();


            CreateMap<Occupant, OccupantUpdateDto>().ReverseMap();

            //el siguiente Map se hace con fines de resguardar datos de usuario al trabajar con Identity

            CreateMap<ApplicationUser, UserDto>().ReverseMap();

        }

    }
}
