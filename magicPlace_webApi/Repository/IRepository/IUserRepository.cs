using magicPlace_webApi.Models;
using magicPlace_webApi.Models.Dto;

namespace magicPlace_webApi.Repository.IRepository
{
    public interface IUserRepository
    {

        Task<bool> IsUserUnique(string username);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<User> Register(RegisterRequestDto registerRequestDto);



    }
}
