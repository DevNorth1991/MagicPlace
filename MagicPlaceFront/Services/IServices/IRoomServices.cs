using MagicPlaceFront.Models.Dto;
using NuGet.Common;

namespace MagicPlaceFront.Services.IServices
{
    public interface IRoomServices
    {

        Task<T> GetAll<T>(string token);
        Task<T> GetById<T>(int id, string token);
        Task<T> Create<T>(RoomCreateDto dto, string token);
        Task<T> Update<T>(RoomUpdateDto dto, string token);
        Task<T> DeleteById<T>(int id, string token);
    }
}
