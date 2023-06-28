using AutoMapper;
using MagicPlace_Utilities;
using MagicPlaceFront.Models;
using MagicPlaceFront.Models.Dto;
using MagicPlaceFront.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicPlaceFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IRoomServices _roomServices;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IRoomServices roomServices, IMapper mapper)
        {
            _logger = logger;
            _roomServices = roomServices;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {

            List<RoomDto> roomList = new();
            var response = await _roomServices.GetAll<ApiResponse>(HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.isSucces)
            {

                roomList = JsonConvert.DeserializeObject<List<RoomDto>>(Convert.ToString(response.Results));

                //le pasamos la lista a la vista 
                return View(roomList);
            }
            // return NotFound();
            return View(roomList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}