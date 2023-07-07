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


        public Task<T> GetAll<T>(string token)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.GET,
                ApiUrl = _roomUrl + "api/v1/Place/",
                Token = token

            });

        }

        public Task<T> GetAllPaginated<T>(string token,int pageNumber = 1 , int pageSize = 4)
        {
            //recordmos que ahora la url es : https://localhost:7001/api/v1/Place/roomsPaginated?PageNumber=1&PageSize=4
            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.GET,
                ApiUrl = _roomUrl + "api/v1/Place/roomsPaginated",
                Token = token,
                parameters = new Parameters() { PageNumber = pageNumber , PageSize = pageSize}

            });

        }

        public Task<T> GetById<T>(int id, string token)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.GET,
                ApiUrl = _roomUrl + "api/v1/Place/" + id,
                Token = token

            });


        }

        public Task<T> Create<T>(RoomCreateDto dto, string token)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.POST,
                Data = dto,
                ApiUrl = _roomUrl + "api/v1/Place/",
                Token = token

            });

        }

        public Task<T> Update<T>(RoomUpdateDto dto,string token)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.PUT,
                Data = dto,
                ApiUrl = _roomUrl + "api/v1/Place/" + dto.Id,
                Token = token

            });

        }

        public Task<T> DeleteById<T>(int id,string token)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.DELETE,
                ApiUrl = _roomUrl + "api/v1/Place/" + id,
                Token = token

            });

        }

      
    }
}
