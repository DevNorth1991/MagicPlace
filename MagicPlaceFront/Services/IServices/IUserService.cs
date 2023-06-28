using MagicPlaceFront.Models.Dto;

namespace MagicPlaceFront.Services.IServices
{
    public interface IUserService
    {
        Task<T> Login<T>(LoginRequestDto loginRequestDto);
        Task<T> Register<T>(RegisterRequestDto registerRequestDto);

    }
}
