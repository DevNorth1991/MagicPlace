namespace magicPlace_webApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; }
        public string UserRol { get; set; }



    }
}
