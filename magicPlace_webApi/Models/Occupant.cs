using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace magicPlace_webApi.Models
{
    public class Occupant
    {

        //lo que vamos a hacer es que nuestra ORM no le asigne una id por defecto 
        //sino que vamos a tomar el dni para esto y vamos a hacer uso de un dataNotation para que esto suceda


        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCard { get; set; }


        [Required]
        public int RoomId { get; set; }


        //mediante foreignKey anotacion de data notation 
        // vamos aindicarle cual propiedad es la key que vinulara las tablas 

        [ForeignKey("RoomId")]
        public Room ObjRoom { get; set; }

        [Required]
        public string NameOccupant { get; set; }

        public DateTime DateCreationOcccupant { get; set; }

        public DateTime DateUpdateOcccupant { get; set; }


    }
}
