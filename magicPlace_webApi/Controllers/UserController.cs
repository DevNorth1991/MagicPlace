using magicPlace_webApi.Models;
using magicPlace_webApi.Models.Dto;
using magicPlace_webApi.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace magicPlace_webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly IUserRepository _userRepo;
        private ApiResponse _response;

        public UserController(IUserRepository userRepo)
        {

            _userRepo = userRepo;
            _response = new();

        }



        //endpoint login lo vamos a modificar para  que la ruta nos quede  /api/User/login

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {


            var loginResponse = await _userRepo.Login(loginRequestDto);
           
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token)) { 
            
            
                _response.statusCode= HttpStatusCode.BadRequest;
                _response.isSucces = false;
                _response.ErrorMessages.Add("User name Or Password ainvalidos");
                return BadRequest(_response);
            
            }
            _response.isSucces = true;
            _response.statusCode = HttpStatusCode.OK;
            _response.Results = loginResponse;
            return Ok(loginResponse);


        }




        //endpoint registrar 

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {

            bool isUserUnique = await _userRepo.IsUserUnique(registerRequestDto.UserName);
            if (!isUserUnique)
            {

                _response.statusCode = HttpStatusCode.BadRequest;
                _response.isSucces = false;
                _response.ErrorMessages.Add("El usuario ya existe!!");
                return BadRequest(_response);

            }

            var user = await _userRepo.Register(registerRequestDto);

            if (user == null)
            {

                _response.statusCode = HttpStatusCode.BadRequest;
                _response.isSucces = false;
                _response.ErrorMessages.Add("Error al registrar usuario!!");
                return BadRequest(_response);

            }

            _response.statusCode = HttpStatusCode.OK;
            _response.isSucces = true;
            return Ok(_response);


        }


    }
}
