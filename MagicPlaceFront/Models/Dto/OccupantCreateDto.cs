using System.ComponentModel.DataAnnotations;

namespace MagicPlaceFront.Models.Dto
{
    public class OccupantCreateDto
    {
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "El número debe ser mayor a cero.")]
        public int IdCard { get; set; }


        [Required]
        public int RoomId { get; set; }

        [Required]
        public string NameOccupant { get; set; }


    }
}
