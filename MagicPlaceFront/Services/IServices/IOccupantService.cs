using MagicPlaceFront.Models.Dto;

namespace MagicPlaceFront.Services.IServices
{
    public interface IOccupantServices
    {

        Task<T> GetAll<T>(string token);
        Task<T> GetById<T>(int id, string token);
        Task<T> Create<T>(OccupantCreateDto dto, string token);
        Task<T> Update<T>(OccupantUpdateDto dto, string token);
        Task<T> DeleteById<T>(int id, string token);
    }
}
