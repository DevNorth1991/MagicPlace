using magicPlace_webApi.Models.Dto;

namespace magicPlace_webApi.DataStore
{
    public class RoomStore
    {
        public static List<RoomUpdateDto> RoomList = new List<RoomUpdateDto>
        {

                 new RoomUpdateDto {Id=1,Name="Vista a la playa",Occupants=4,SquareMeters=16},
                 new RoomUpdateDto {Id=2,Name="Vista interior",Occupants=4,SquareMeters=16},
                 new RoomUpdateDto {Id=3,Name="Vista piscina",Occupants=4,SquareMeters=16},
                 new RoomUpdateDto {Id=4,Name="matrimonial",Occupants=4,SquareMeters=20},
                 new RoomUpdateDto {Id=5,Name="switt",Occupants=4,SquareMeters=16},
                 new RoomUpdateDto {Id=6,Name="simple",Occupants=4,SquareMeters=25},

        };
    }
}
