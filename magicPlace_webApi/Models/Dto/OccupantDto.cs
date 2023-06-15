using System.ComponentModel.DataAnnotations;

namespace magicPlace_webApi.Models.Dto
{
    public class OccupantDto
    {

        [Required]
        public int IdCard { get; set; }


        [Required]
        public int RoomId { get; set; }

        [Required]
        public string NameOccupant { get; set; }

        public RoomDto ObjRoom { get; set; }
    }
}
