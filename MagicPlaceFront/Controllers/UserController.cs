using MagicPlace_Utilities;
using MagicPlaceFront.Models;
using MagicPlaceFront.Models.Dto;
using MagicPlaceFront.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MagicPlaceFront.Controllers
{
    public class UserController : Controller
    {

        public readonly IUserService _userService;

        public UserController(IUserService userService)
        {

            _userService = userService;
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var response = await _userService.Login<ApiResponse>(dto);

            if (response != null && response.isSucces == true)
            {

                LoginResponseDto responseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Results));

                //configuracion de los Claims esto es para mantener el user name y el role en todo Momento 

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, responseDto.User.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, responseDto.User.UserRol));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                //configuracion de la sesion 
                HttpContext.Session.SetString(SD.SessionToken, responseDto.Token);

                //vamosa retoenar a una vista de index pero como en este  caso la vista no se encuentra en el mismo 
                //constrolador lo que haremos sera indicarle como segundo parametro dl metodo el nombre del controlador 

                return RedirectToAction("Index", "Home");
            }
            else
            {

                ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                return View(dto);

            }

         
        }


        //register

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {

            var response = await _userService.Register<ApiResponse>(dto);
            if (response != null && response.isSucces)
            {

                return RedirectToAction("login");

            }
            return View();
        }



        //metodo cierre de sesion 


        public async Task<IActionResult>Logout()
        {

            //clean Session Variable 
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.SessionToken,"");
            return RedirectToAction("Index","Home");

        }

        //acces denied

        public IActionResult AccessDenied()
        {
            return View();

        }





    }
}
