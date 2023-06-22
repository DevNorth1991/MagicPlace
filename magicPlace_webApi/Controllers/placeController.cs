using magicPlace_webApi.DataStore;
using magicPlace_webApi.Models.Dto;
using magicPlace_webApi.DataStore;
using magicPlace_webApi.Models;
using magicPlace_webApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using magicPlace_webApi.Repository.IRepository;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace MagicPlace_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {


        //activamos el servicio de logger 
        private readonly ILogger<PlaceController> _logger;
        //activamos el servicio DBcontext
        //  private readonly ApplicationDbContext _context;
        private readonly IRoomRepository _roomRepository;
        //automapper injection 

        private readonly IMapper _mapper;

        //inicalizamos nuestra clase respuesta 

        protected ApiResponse _response;

        public PlaceController(ILogger<PlaceController> logger, IRoomRepository roomRepository, IMapper mapper)
        {

            _logger = logger;
            _roomRepository = roomRepository;
            _mapper = mapper;
            _response = new();

        }



        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetRooms()
        {

            try
            {

                _logger.LogInformation("Trayendo lista de Habitaciones");


                IEnumerable<Room> roomList = await _roomRepository.GetAll();

                //guardamnos el resultado en el objeto Results da la clase ApiResponses

                _response.Results = _mapper.Map<IEnumerable<RoomDto>>(roomList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.isSucces = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return (_response);

        }



        [HttpGet("{id:int}", Name = "GetRoom")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> getRoom(int id)
        {


            try
            {


                if (id == 0)
                {
                    _logger.LogError("error al traer habitacion por id");
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    return BadRequest(_response);
                }

                var room = await _roomRepository.getById(r => r.Id == id);

                if (room == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.isSucces = false;
                    return NotFound(_response);

                }


                _response.Results = _mapper.Map<RoomDto>(room);
                _response.statusCode = HttpStatusCode.OK;
                _response.isSucces = true;
                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.isSucces = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

        }






        [HttpPost]
        [Authorize(Roles ="admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateRoom([FromBody] RoomCreateDto roomCreateDto)
        {


            try
            {


                if (roomCreateDto == null)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    _response.Results = roomCreateDto;
                    return BadRequest(_response);
                }

                if (!ModelState.IsValid)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    _response.Results = roomCreateDto;
                    return BadRequest(_response);

                }

                //name validation

                if (await _roomRepository.getById(r => r.Name.ToLower() == roomCreateDto.Name.ToLower()) != null)
                {


                    ModelState.AddModelError("ErrorMessages", "La habitacion con ese Nombre ya existe!!!");
                    return BadRequest(ModelState);

                }

                Room modelo = _mapper.Map<Room>(roomCreateDto);
                modelo.CreationDate = DateTime.Now;
                modelo.UpdateTime = DateTime.Now;
                await _roomRepository.Create(modelo);
                _response.Results = modelo;
                _response.isSucces = true;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetRoom", new { id = modelo.Id }, _response);


            }
            catch (Exception ex)
            {
                _response.isSucces = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string> { ex.ToString() };

            }

            return _response;

        }




        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> DeleteRoom(int id)//la interfaz IAction results No puede devolver un tipo 
        {

            try
            {

                if (id == 0)
                {

                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    return BadRequest(_response);
                }

                // var room = RoomStore.RoomList.FirstOrDefault(r => r.Id == id);
                var room = await _roomRepository.getById(r => r.Id == id);

                if (room == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.isSucces = false;
                    return NotFound(_response);
                }

                await _roomRepository.Delete(room);
                _response.isSucces = true;
                _response.statusCode = HttpStatusCode.NoContent;

                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.isSucces = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return BadRequest(_response); // por eso se lo enviamos envuelto de un badRequest

        }


        //AHORA VAMOS A ACTUALIZAR MEDIANTE HTTPUT 
        //volvemos a utilizar Un IActionResult ya que retornaremos un no content y no pprecisaremos el modelo 

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomUpdateDto roomUpdateDto)
        {

            try
            {


                if (roomUpdateDto == null || id != roomUpdateDto.Id)
                {

                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    return BadRequest(_response);


                }

                //obtenemos el registro a editar 
                var roomTemp = await _roomRepository.getById(r => r.Id == id, tracked: false);

                if (roomTemp == null)
                {

                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.isSucces = false;
                    return NotFound(_response);

                }



                Room modelo = _mapper.Map<Room>(roomUpdateDto);
                modelo.CreationDate = roomTemp.CreationDate;
                await _roomRepository.Update(modelo);
                _response.Results = modelo;
                _response.statusCode = HttpStatusCode.NoContent;
                _response.isSucces = true;

                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.isSucces = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_response);

        }


        [HttpPatch("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> PatchRoom(int id, JsonPatchDocument<RoomUpdateDto> patchDto)
        {

            try
            {

                if (patchDto == null || id == 0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    return BadRequest(_response);

                }

                //obtenemos el registro a editar 
                var room = await _roomRepository.getById(r => r.Id == id, tracked: false);

                RoomUpdateDto roomUpdateDto = _mapper.Map<RoomUpdateDto>(room);

                if (room == null)
                {

                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    return BadRequest(_response);

                }

                patchDto.ApplyTo(roomUpdateDto, ModelState);

                if (!ModelState.IsValid)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    _response.Results = ModelState;
                    return BadRequest(_response);

                }

                Room roomModified = _mapper.Map<Room>(roomUpdateDto);
                await _roomRepository.Update(roomModified);
                _response.statusCode = HttpStatusCode.NoContent;
                _response.isSucces = true;
                _response.Results = roomModified;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.isSucces = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };

            }
            return Ok(_response);

        }


    }//end class PlaceController




}
