using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace magicPlace_webApi.Models
{
    public class Room
    {

        [Key]//si dejaramos solo esta prpiedad tambien funcionaria 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//modo de controlar el auto incremento del id
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string  Detail { get; set; }

        [Required]
        public double Fee { get; set; }

        public int Occupants { get; set; }

        [Required]
        public int SquareMeters { get; set; }

        public string  ImageUrl { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
