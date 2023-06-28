namespace MagicPlaceFront.Models.Dto
{
    public class RegisterRequestDto
    {

        public string UserName { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; }
        public string UserRol { get; set; }
    }
}
