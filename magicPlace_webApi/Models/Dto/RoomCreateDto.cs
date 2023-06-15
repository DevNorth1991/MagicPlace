using System.ComponentModel.DataAnnotations;

namespace magicPlace_webApi.Models.Dto
{
    public class RoomCreateDto
    {

        [Required(ErrorMessage ="El nombre es requerido")]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Detail { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        public double Fee { get; set; }

        public int Occupants { get; set; }

        [Required(ErrorMessage = "Los metros cuadrados son requeridos")]
        public int SquareMeters { get; set; }

        public string ImageUrl { get; set; }
    }
}
