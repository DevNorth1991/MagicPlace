using MagicPlace_Utilities;
using MagicPlaceFront.Models;
using MagicPlaceFront.Models.Dto;
using MagicPlaceFront.Services.IServices;

namespace MagicPlaceFront.Services
{
    public class UserService : BaseServices , IUserService
    {

        public readonly IHttpClientFactory _httpClientFactory;
        private string _url;
        public UserService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;
            _url = configuration.GetValue<string>("ServicesUrl:API_URL");
        }




        public Task<T> Login<T>(LoginRequestDto loginRequestDto)
        {
            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.POST,
                Data = loginRequestDto,
                ApiUrl= _url + "api/user/login"


            }) ;
        }

        public Task<T> Register<T>(RegisterRequestDto registerRequestDto)
        {
            return SendAsync<T>(new ApiRequest()
            {

                ApiTypes = SD.ApiType.POST,
                Data = registerRequestDto,
                ApiUrl = _url + "/api/User/register"


            });
        }
    }
}
