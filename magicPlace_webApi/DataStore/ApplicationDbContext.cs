using magicPlace_webApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace magicPlace_webApi.DataStore
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        //creamos un nuevo dbSet para crear la tabla UsersIdentity 

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Occupant> Occupants { get; set; }
        
        public DbSet<User> Users { get; set; }


        //aqui vamos a sobrescribir Un metodo OnModel Creating para crear nuestros registros p[or defecto 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //vamos a insertar por ultimo esta linea de codigo para que 
            base.OnModelCreating(modelBuilder);

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

/*
 La línea de código que mencionas, base.OnModelCreating(modelBuilder);
, llama al método OnModelCreating de la clase base IdentityDbContext<ApplicationUser>.

Cuando se hereda de una clase base en C#, es común que se llame al método de la clase base correspondiente en el método de la clase derivada.
Esto permite ejecutar las configuraciones y lógica definidas en la implementación de la clase base antes de agregar o modificar cualquier comportamiento adicional en la clase derivada.

En el caso de la línea base.OnModelCreating(modelBuilder);, se está llamando al método OnModelCreating de la clase base IdentityDbContext<ApplicationUser>. Esta clase base contiene configuraciones específicas de ASP.NET Identity relacionadas con la autenticación y autorización de usuarios. Llamar a base.OnModelCreating(modelBuilder) asegura que se ejecuten estas configuraciones antes de agregar cualquier configuración adicional definida en el método OnModelCreating de la clase ApplicationDbContext.

En resumen, la línea base.OnModelCreating(modelBuilder); permite ejecutar las configuraciones de modelo de la clase base IdentityDbContext<ApplicationUser> antes de aplicar cualquier configuración adicional en el método OnModelCreating de la clase ApplicationDbContext. Esto es importante para mantener la funcionalidad proporcionada por ASP.NET Identity y garantizar que todas las configuraciones necesarias se apliquen correctamente en el modelo de la base de datos.
 
 */