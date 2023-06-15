using MagicPlaceFront.Models.Dto;

namespace MagicPlaceFront.Services.IServices
{
    public interface IRoomServices
    {

        Task<T> GetAll<T>();
        Task<T> GetById<T>(int id);
        Task<T> Create<T>(RoomCreateDto dto);
        Task<T> Update<T>(RoomUpdateDto dto);
        Task<T> DeleteById<T>(int id);
    }
}
