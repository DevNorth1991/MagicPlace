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


        public Task<T> GetAll<T>(string token)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.GET,
                ApiUrl = _url + "api/v1/Occupant/",
                Token = token

            });

        }

        public Task<T> GetById<T>(int id,string token)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.GET,
                ApiUrl = _url + "api/v1/Occupant/" + id,
                Token = token

            });


        }

        public Task<T> Create<T>(OccupantCreateDto dto , string token)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.POST,
                Data = dto,
                ApiUrl = _url + "api/v1/Occupant/",
                Token = token

            });

        }

        public Task<T> Update<T>(OccupantUpdateDto dto , string token)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.PUT,
                Data = dto,
                ApiUrl = _url + "api/v1/Occupant/" + dto.IdCard,
                Token = token

            });

        }

        public Task<T> DeleteById<T>(int idcard ,string token)
        {

            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.DELETE,
                ApiUrl = _url + "api/v1/Occupant/" + idcard,
                Token = token

            });

        }


    }
}
