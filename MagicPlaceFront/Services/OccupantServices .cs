using MagicPlace_Utilities;
using MagicPlaceFront.Models;
using MagicPlaceFront.Models.Dto;
using MagicPlaceFront.Services.IServices;

namespace MagicPlaceFront.Services
{
    public class OccupantServices : BaseServices, IOccupantServices
    {

        public readonly IHttpClientFactory _httpClient;
        private readonly string _url;
        public OccupantServices(IHttpClientFactory httpClient, IConfiguration config) : base(httpClient)
        {
            _httpClient = httpClient;
            _url = config.GetValue<string>("ServicesUrl:API_URL");
        }


        public Task<T> GetAll<T>()
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.GET,
                ApiUrl = _url + "api/Occupant/"

            });

        }

        public Task<T> GetById<T>(int id)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.GET,
                ApiUrl = _url + "api/Occupant/" + id

            });


        }

        public Task<T> Create<T>(OccupantCreateDto dto)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.POST,
                Data = dto,
                ApiUrl = _url + "api/Occupant/"

            });

        }

        public Task<T> Update<T>(OccupantUpdateDto dto)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.PUT,
                Data = dto,
                ApiUrl = _url + "api/Occupant/" + dto.IdCard

            });

        }

        public Task<T> DeleteById<T>(int idcard)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.DELETE,
                ApiUrl = _url + "api/Occupant/" + idcard

            });

        }


    }
}
