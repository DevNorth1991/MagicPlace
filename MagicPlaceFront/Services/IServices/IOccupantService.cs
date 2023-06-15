using MagicPlaceFront.Models.Dto;

namespace MagicPlaceFront.Services.IServices
{
    public interface IOccupantServices
    {

        Task<T> GetAll<T>();
        Task<T> GetById<T>(int id);
        Task<T> Create<T>(OccupantCreateDto dto);
        Task<T> Update<T>(OccupantUpdateDto dto);
        Task<T> DeleteById<T>(int id);
    }
}
