using MagicPlace_Utilities;
using MagicPlaceFront.Models;
using MagicPlaceFront.Models.Dto;
using MagicPlaceFront.Services.IServices;

namespace MagicPlaceFront.Services
{
    public class RoomServices : BaseServices, IRoomServices
    {

        public readonly IHttpClientFactory _httpClient;
        private readonly string _roomUrl;
        public RoomServices(IHttpClientFactory httpClient, IConfiguration config) : base(httpClient)
        {
            _httpClient = httpClient;
            _roomUrl = config.GetValue<string>("ServicesUrl:API_URL");
        }


        public Task<T> GetAll<T>()
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.GET,
                ApiUrl = _roomUrl + "api/Place/"

            });

        }

        public Task<T> GetById<T>(int id)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.GET,
                ApiUrl = _roomUrl + "api/Place/" + id

            });


        }

        public Task<T> Create<T>(RoomCreateDto dto)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.POST,
                Data = dto,
                ApiUrl = _roomUrl + "api/Place/"

            });

        }

        public Task<T> Update<T>(RoomUpdateDto dto)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.PUT,
                Data = dto,
                ApiUrl = _roomUrl + "api/Place/" + dto.Id

            });

        }

        public Task<T> DeleteById<T>(int id)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.DELETE,
                ApiUrl = _roomUrl + "api/Place/" + id

            });

        }

      
    }
}
