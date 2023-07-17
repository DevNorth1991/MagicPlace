namespace magicPlace_webApi.Models.Dto
{
    public class UserDto
    {
        
        public string Id { get; set; }

        public string UserName { get; set; }

        //esta es la propiedad que creamos en la Tabla AplicattionUser ----revisar si hace falta O no 
        public string NameUserIdentity { get; set; }
    }
}
