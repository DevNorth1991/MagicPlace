using System.ComponentModel.DataAnnotations;

namespace MagicPlaceFront.Models.Dto
{
    public class RoomCreateDto
    {

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Detail { get; set; }

        [Required]
        public double Fee { get; set; }

        public int Occupants { get; set; }

        public int SquareMeters { get; set; }

        public string ImageUrl { get; set; }
    }
}
