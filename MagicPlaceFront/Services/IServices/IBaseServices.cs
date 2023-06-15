using MagicPlaceFront.Models;

namespace MagicPlaceFront.Services.IServices
{
    public interface IBaseServices
    {

        public ApiResponse responseModel { get; set; }

        //creamos el servicio mas importante o el servicio Gnererico 

        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
