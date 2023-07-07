using AutoMapper;
using MagicPlace_Utilities;
using MagicPlaceFront.Models;
using MagicPlaceFront.Models.Dto;
using MagicPlaceFront.Models.ViewModel;
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

        public async Task<IActionResult> Index(int pageNumber = 1)
        {



            List<RoomDto> roomList = new();
            RoomPaginatedViewModel roomVM = new RoomPaginatedViewModel();

            //validamos el page number 

            if (pageNumber < 1) pageNumber = 1;



            var response = await _roomServices.GetAllPaginated<ApiResponse>(HttpContext.Session.GetString(SD.SessionToken), pageNumber, 4);

            if (response != null && response.isSucces)
            {

                roomList = JsonConvert.DeserializeObject<List<RoomDto>>(Convert.ToString(response.Results));

                roomVM = new RoomPaginatedViewModel()
                {

                    RoomList = roomList,
                    PageNumber = pageNumber,
                    TotalPages = JsonConvert.DeserializeObject<int>(Convert.ToString(response.TotalPages))
                };

                //logica d e las clases de Bootstrap
                if (pageNumber > 1) roomVM.Previous = "";//sacamos la clase 
                if (roomVM.TotalPages <= pageNumber) roomVM.Next = "disabled";//agregamos la clase disabled e bootstrap

                //le pasamos la lista a la vista 
                return View(roomVM);
            }
            return NotFound("No se encontarron registros en la query");

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