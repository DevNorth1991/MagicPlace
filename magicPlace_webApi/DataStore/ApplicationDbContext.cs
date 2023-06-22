using magicPlace_webApi.Models;
using Microsoft.EntityFrameworkCore;

namespace magicPlace_webApi.DataStore
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<Room> Rooms { get; set; }

        public DbSet<Occupant> Occupants { get; set; }
        
        public DbSet<User> Users { get; set; }


        //aqui vamos a sobrescribir Un metodo OnModel Creating para crear nuestros registros p[or defecto 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Room>().HasData(

                new Room
                {

                    Id = 1,
                    Name = "habitacion magica",
                    Detail = "",
                    Fee = 125.0,
                    Occupants = 4,
                    SquareMeters = 16,
                    CreationDate = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    ImageUrl = ""

                },
                 new Room
                 {

                     Id = 2,
                     Name = "habitacion excellent",
                     Detail = "",
                     Fee = 1109.0,
                     Occupants = 4,
                     SquareMeters = 16,
                     CreationDate = DateTime.Now,
                     UpdateTime = DateTime.Now,
                     ImageUrl = ""

                 },
                  new Room
                  {

                      Id = 3,
                      Name = "habitacion premium ",
                      Detail = "",
                      Fee = 100.0,
                      Occupants = 4,
                      SquareMeters = 16,
                      CreationDate = DateTime.Now,
                      UpdateTime = DateTime.Now,
                      ImageUrl = ""

                  }


                );


        }



    }
}
