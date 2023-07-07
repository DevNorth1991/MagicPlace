using MagicPlace_Utilities;
using MagicPlaceFront.Models;
using MagicPlaceFront.Services.IServices;
//using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Web;

namespace MagicPlaceFront.Services
{
    public class BaseServices : IBaseServices
    {
        public ApiResponse responseModel { get; set; }
        public IHttpClientFactory _httpClient { get; set; }
     

        public BaseServices(IHttpClientFactory httpClientFactory)
        {
            this.responseModel = new();
            _httpClient = httpClientFactory;  

        }


        //aqui vamos a implementar el httpClient Factory 
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {

                //lo primero que debemos hacer es crear el servicio y nombrarlo a discrecion 
                var client = _httpClient.CreateClient("MagicPlace");
                //creamos el Messages
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");


                if (apiRequest.parameters == null)
                {

                    message.RequestUri = new Uri(apiRequest.ApiUrl);

                }
                else { 
                
                    // TODO  aqui vamos a construir la Url de nuevo 
                var builder = new UriBuilder(apiRequest.ApiUrl);
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["PageNumber"] = apiRequest.parameters.PageNumber.ToString();
                    query["PageSize"] = apiRequest.parameters.PageSize.ToString();
                    builder.Query = query.ToString();
                    string url = builder.ToString();
                    message.RequestUri = new Uri(url);
                }
                


                //validamso si el mensaje lleva o no datos
                //
                if (apiRequest.Data != null)
                {

                    //se trata de un post o un put que son los que envian datos 
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");

                }

                switch (apiRequest.ApiTypes)
                {

                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;

                }


                HttpResponseMessage apiResponseMessage = null;
                //aqui enviaremos incrustaremos el token en nuestra api 

                if (!string.IsNullOrEmpty(apiRequest.Token)) {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                
                }


                apiResponseMessage = await client.SendAsync(message);
                var ApiContent = await apiResponseMessage.Content.ReadAsStringAsync();
                //var APIResponse = JsonConvert.DeserializeObject<T>(ApiContent);
                //return APIResponse;

                try
                {

                    ApiResponse response = JsonConvert.DeserializeObject<ApiResponse>(ApiContent);

                    if (response!= null && apiResponseMessage.StatusCode == HttpStatusCode.BadRequest || apiResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    {

                        response.statusCode = HttpStatusCode.BadRequest;
                        response.isSucces = false;

                        //volvemos a serializar para enviar com respuesta json 

                        var res = JsonConvert.SerializeObject(response);
                        var obj = JsonConvert.DeserializeObject<T>(res);

                        return obj;


                    }


                }
                catch (Exception ex)
                {


                    var errorResponse = JsonConvert.DeserializeObject<T>(ApiContent);
                    return errorResponse;

                }

                var APIResponse = JsonConvert.DeserializeObject<T>(ApiContent);
                return APIResponse;


            }
            catch (Exception ex)
            {

                var dto = new ApiResponse
                {
                    //aqui es donde vams a controlar los error Messages   

                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    isSucces = false

                };
                var res = JsonConvert.SerializeObject(dto);
                var responseEx = JsonConvert.DeserializeObject<T>(res);
                return responseEx;

            }
        }


    }
}