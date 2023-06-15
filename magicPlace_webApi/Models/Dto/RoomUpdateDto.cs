using System.ComponentModel.DataAnnotations;

namespace magicPlace_webApi.Models.Dto
{
    public class RoomUpdateDto
    {
        [Required]
        public int Id { get; set; }

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
