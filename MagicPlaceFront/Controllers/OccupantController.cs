using AutoMapper;
using MagicPlaceFront.Models;
using MagicPlaceFront.Models.Dto;
using MagicPlaceFront.Models.ViewModel;
using MagicPlaceFront.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Reflection;

namespace MagicPlaceFront.Controllers
{
    public class OccupantController : Controller
    {

        private readonly IOccupantServices _services;
        private readonly IRoomServices _roomServices;
        private readonly IMapper _mapper;

        public OccupantController(IOccupantServices services, IRoomServices roomServices, IMapper mapper)
        {
            _services = services;
            _roomServices = roomServices;
            _mapper = mapper;
        }






        public async Task<IActionResult> IndexOccupants()
        {

            List<OccupantDto> listOccupants = new();

            var response = await _services.GetAll<ApiResponse>();

            if (response != null && response.isSucces)
            {

                listOccupants = JsonConvert.DeserializeObject<List<OccupantDto>>(Convert.ToString(response.Results));

            }

            return View(listOccupants);
        }



        //Creacion de nuevo ocupante 

        public async Task<IActionResult> AddOccupant()
        {

            OcuppantViewModel vmOccupant = new();

            //obtenemos las habitaciones 

            var response = await _roomServices.GetAll<ApiResponse>();

            if (response != null && response.isSucces)
            {

                vmOccupant.RoomList = JsonConvert.DeserializeObject<List<RoomDto>>(Convert.ToString(response.Results)).
                                      Select(r => new SelectListItem
                                      {

                                          Text = r.Name,
                                          Value = r.Id.ToString()

                                      });

            }
            return View(vmOccupant);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOccupant(OcuppantViewModel modelo)
        {

            if (ModelState.IsValid)
            {

                var response = await _services.Create<ApiResponse>(modelo.ObjOccupant);

                if (response != null && response.isSucces)
                {
                    TempData["isSucces"] = "Inquilino Agregado Exitosamente";
                    return RedirectToAction(nameof(IndexOccupants));

                }
                else
                {
                    TempData["error"] = "Error al agregar inquilino";
                    if (response.ErrorMessages.Count > 0)
                    {

                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());

                    }

                }

            }

            
            //TODO : enviar la lista de sleccion de Habitacion nuevamente

            var res = await _roomServices.GetAll<ApiResponse>();

            if (res != null && res.isSucces)
            {

                modelo.RoomList = JsonConvert.DeserializeObject<List<RoomDto>>(Convert.ToString(res.Results)).
                                      Select(r => new SelectListItem
                                      {

                                          Text = r.Name,
                                          Value = r.Id.ToString()

                                      });

            }

            return View(modelo);

        }



        //update Occupant 


        public async Task<IActionResult> UpdateOccupant(int IdCard)
        {

            OcuppantUpdateViewModel vmUpdateOccupant = new();

            var response = await _services.GetById<ApiResponse>(IdCard);

            if (response != null && response.isSucces)
            {

                OccupantDto modelo = JsonConvert.DeserializeObject<OccupantDto>(Convert.ToString(response.Results));
                vmUpdateOccupant.ObjOccupant = _mapper.Map<OccupantUpdateDto>(modelo);
            }

            //obtenemos las habitaciones 

            response = await _roomServices.GetAll<ApiResponse>();

            if (response != null && response.isSucces)
            {

                vmUpdateOccupant.RoomList = JsonConvert.DeserializeObject<List<RoomDto>>(Convert.ToString(response.Results)).
                                      Select(r => new SelectListItem
                                      {

                                          Text = r.Name,
                                          Value = r.Id.ToString()

                                      });

            }
            return View(vmUpdateOccupant);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOccupant(OcuppantUpdateViewModel modelo)
        {


            if (ModelState.IsValid)
            {

                var response = await _services.Update<ApiResponse>(modelo.ObjOccupant);

                if (response != null && response.isSucces)
                {
                    TempData["isSucces"] = "Inquilino actualizado";

                    return RedirectToAction(nameof(IndexOccupants));

                }
                else
                {

                    if (response.ErrorMessages.Count > 0)
                    {

                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());

                    }

                }


            }


            //TODO : enviar la lista de sleccion de Habitacion nuevamente

            var res = await _roomServices.GetAll<ApiResponse>();

            if (res != null && res.isSucces)
            {

                modelo.RoomList = JsonConvert.DeserializeObject<List<RoomDto>>(Convert.ToString(res.Results)).
                                      Select(r => new SelectListItem
                                      {

                                          Text = r.Name,
                                          Value = r.Id.ToString()

                                      });

            }

            TempData["error"] = "Algo salio mal";


            return View(modelo);
        }


        //delete Occupant 

        public async Task<IActionResult> DeleteOccupant(int IdCard)
        {

            OcuppanDeleteViewModel vmDeleteOccupant = new();

            var response = await _services.GetById<ApiResponse>(IdCard);

            if (response != null && response.isSucces)
            {

                OccupantDto modelo = JsonConvert.DeserializeObject<OccupantDto>(Convert.ToString(response.Results));
                vmDeleteOccupant.ObjOccupant = modelo;
            }

            //obtenemos las habitaciones 

            response = await _roomServices.GetAll<ApiResponse>();

            if (response != null && response.isSucces)
            {

                vmDeleteOccupant.RoomList = JsonConvert.DeserializeObject<List<RoomDto>>(Convert.ToString(response.Results)).
                                      Select(r => new SelectListItem
                                      {

                                          Text = r.Name,
                                          Value = r.Id.ToString()

                                      });

            }
            return View(vmDeleteOccupant);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOccupant(OcuppanDeleteViewModel modelo)
        {


            if (modelo.ObjOccupant.IdCard > 0)
            {

                var response = await _services.DeleteById<ApiResponse>(modelo.ObjOccupant.IdCard);
                if (response != null && response.isSucces)
                {
                    TempData["isSucces"] = "Inquilino Eliminado Exitosamente";
                    return RedirectToAction(nameof(IndexOccupants));

                }
                else
                {

                    if (response.ErrorMessages.Count > 0)
                    {

                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());

                    }

                }

            }







            //TODO : enviar la lista de sleccion de Habitacion nuevamente

            var res = await _roomServices.GetAll<ApiResponse>();

            if (res != null && res.isSucces)
            {

                modelo.RoomList = JsonConvert.DeserializeObject<List<RoomDto>>(Convert.ToString(res.Results)).
                                      Select(r => new SelectListItem
                                      {

                                          Text = r.Name,
                                          Value = r.Id.ToString()

                                      });

            }

            return View(modelo);
        }
    }
}
