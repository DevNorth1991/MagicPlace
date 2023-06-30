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

namespace magicPlace_webApi.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class OccupantController : ControllerBase
    {


        //activamos el servicio de logger 
        private readonly ILogger<OccupantController> _logger;
        //activamos el servicio DBcontext
        //  private readonly ApplicationDbContext _context;
        private readonly IRoomRepository _roomRepository;
        private readonly IOccupantRepository _occupaRepository;
        //automapper injection 

        private readonly IMapper _mapper;

        //inicalizamos nuestra clase respuesta 

        protected ApiResponse _response;

        public OccupantController(ILogger<OccupantController> logger, IRoomRepository roomRepository, IOccupantRepository occupaRepository, IMapper mapper)
        {

            _logger = logger;
            _roomRepository = roomRepository;
            _occupaRepository = occupaRepository;
            _mapper = mapper;
            _response = new();


        }



        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetOccupants()
        {

            try
            {

                _logger.LogInformation("Trayendo lista de inquilinos");


                IEnumerable<Occupant> occupaList = await _occupaRepository.GetAll(incluir: "ObjRoom");

                //guardamnos el resultado en el objeto Results da la clase ApiResponses

                _response.Results = _mapper.Map<IEnumerable<OccupantDto>>(occupaList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.isSucces = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }



        [HttpGet("{id:int}", Name = "GetOccupant")]
        [Authorize]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> getOccupant(int id)
        {


            try
            {


                if (id == 0)
                {
                    _logger.LogError("error al traer el inquilino con el DNI = " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    return BadRequest(_response);
                }

                //buscamos por dni

                var occupa = await _occupaRepository.getById(r => r.IdCard == id, incluir: "ObjRoom");

                if (occupa == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.isSucces = false;
                    return NotFound(_response);

                }


                _response.Results = _mapper.Map<OccupantDto>(occupa);
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
        [Authorize(Roles = "admin")]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateOccupant(OccupantCreateDto occupaCreateDto)
        {


            try
            {


                if (occupaCreateDto == null)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    _response.Results = occupaCreateDto;
                    return BadRequest(_response);
                }

                if (!ModelState.IsValid)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    _response.Results = occupaCreateDto;
                    return BadRequest(_response);

                }

                //dni validation

                if (await _occupaRepository.getById(r => r.IdCard == occupaCreateDto.IdCard) != null)
                {


                    ModelState.AddModelError("ErrorMessages", "Ya existe un inquilino con ese DNI= " + occupaCreateDto.IdCard);


                    return BadRequest(ModelState); ;

                }

                ////name validation

                if (await _occupaRepository.getById(r => r.NameOccupant.ToLower() == occupaCreateDto.NameOccupant.ToLower()) != null)
                {


                    ModelState.AddModelError("ErrorMessages", "Ya existe un inquilino con ese nombre");

                    return BadRequest(ModelState);

                }

                //validamos que exista un Numero de habitacion para tal inquilino

                if (await _roomRepository.getById(r => r.Id == occupaCreateDto.RoomId) == null)
                {


                    ModelState.AddModelError("ErrorMessages", "No existe Habitacion con el ID Numero = " + occupaCreateDto.RoomId);
                    return BadRequest(ModelState);

                }

                Occupant modelo = _mapper.Map<Occupant>(occupaCreateDto);
                modelo.DateCreationOcccupant = DateTime.Now;
                modelo.DateUpdateOcccupant = DateTime.Now;
                await _occupaRepository.Create(modelo);
                _response.Results = modelo;
                _response.isSucces = true;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetOccupant", new { id = modelo.IdCard }, _response);


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

        public async Task<IActionResult> DeleteOccupant(int id)//la interfaz IAction results No puede devolver un tipo 
        {

            try
            {

                if (id == 0)
                {

                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    return BadRequest(_response);
                }


                var occupa = await _occupaRepository.getById(r => r.IdCard == id);

                if (occupa == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.isSucces = false;
                    return NotFound(_response);
                }

                await _occupaRepository.Delete(occupa);
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
        [Authorize(Roles = "admin")]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> UpdateOccupant(int id, [FromBody] OccupantUpdateDto occupaUpdateDto)
        {

            try
            {


                if (occupaUpdateDto == null || id != occupaUpdateDto.IdCard)
                {

                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    return BadRequest(_response);


                }


                //name validation

                if (await _occupaRepository.getById(r => r.NameOccupant.ToLower() == occupaUpdateDto.NameOccupant.ToLower()) != null)
                {


                    ModelState.AddModelError("ErrorMessages", "Ya existe un inquilino con ese nombre");


                    return BadRequest(ModelState);

                }

                //validamos que exista un Numero de habitacion para tal inquilino

                if (await _roomRepository.getById(r => r.Id == occupaUpdateDto.RoomId) == null)
                {


                    ModelState.AddModelError("ErrorMessages", "No existe Habitacion con el ID = " + occupaUpdateDto.RoomId);

                    return BadRequest(ModelState);

                }


                //obtenemos el registro a editar 
                var occupaTemp = await _occupaRepository.getById(r => r.IdCard == id, tracked: false);

                if (occupaTemp == null)
                {

                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.isSucces = false;
                    return NotFound(_response);

                }




                Occupant modelo = _mapper.Map<Occupant>(occupaUpdateDto);
                modelo.DateCreationOcccupant = occupaTemp.DateCreationOcccupant;





                await _occupaRepository.Update(modelo);
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

        public async Task<IActionResult> PatchRoom(int id, JsonPatchDocument<OccupantUpdateDto> patchDto)
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
                var occupa = await _occupaRepository.getById(r => r.IdCard == id, tracked: false);

                OccupantUpdateDto occupaUpdateDto = _mapper.Map<OccupantUpdateDto>(occupa);

                if (occupa == null)
                {

                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    return BadRequest(_response);

                }

                patchDto.ApplyTo(occupaUpdateDto, ModelState);

                if (!ModelState.IsValid)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSucces = false;
                    _response.Results = ModelState;
                    return BadRequest(_response);

                }

                Occupant occupaModified = _mapper.Map<Occupant>(occupaUpdateDto);
                await _occupaRepository.Update(occupaModified);
                _response.statusCode = HttpStatusCode.NoContent;
                _response.isSucces = true;
                _response.Results = occupaModified;

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
