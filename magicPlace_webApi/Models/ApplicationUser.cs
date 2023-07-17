using Microsoft.AspNetCore.Identity;

namespace magicPlace_webApi.Models
{
    public class ApplicationUser: IdentityUser
    {
        //ejemplo vamos a gregarle esta Propiedad para verla reflejada en la tabla cunado la vallamos a crear 
        public string  NameUserIdentity { get; set; }

    }
}
