namespace MagicPlaceFront.Models.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; }
      
    }
}
