﻿using AutoMapper;
using MagicPlace_Utilities;
using MagicPlaceFront.Models;
using MagicPlaceFront.Models.Dto;
using MagicPlaceFront.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;

namespace MagicPlaceFront.Controllers
{
    public class RoomController : Controller
    {

        private readonly IRoomServices _roomServices;
        private readonly IMapper _mapper;

        public RoomController(IRoomServices roomServices, IMapper mapper)
        {

            _roomServices = roomServices;
            _mapper = mapper;


        }

        [Authorize(Roles ="admin")]
        public async Task<IActionResult> IndexRoom()
        {

            List<RoomDto> roomList = new();
            var response = await _roomServices.GetAll<ApiResponse>(HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.isSucces)
            {

                roomList = JsonConvert.DeserializeObject<List<RoomDto>>(Convert.ToString(response.Results));

                //le pasamos la lista a la vista 
                return View(roomList);
            }
            return NotFound();
            
        }



        //Crear nueva Habitacion
        //Get------> ya que a los metodos cuando no se les define un tipo por defecto son GET

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateRoom()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRoom(RoomCreateDto dto)
        {

            if (ModelState.IsValid)
            {

                var response = await _roomServices.Create<ApiResponse>(dto, HttpContext.Session.GetString(SD.SessionToken));

                if (response != null && response.isSucces)
                {
                    TempData["isSucces"] = "Habitacion Creada Exitosamente";
                    return RedirectToAction(nameof(IndexRoom));
                }
            }
            TempData["error"] = "Falla en la carga de datos";


            return View(dto);

        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateRoom(int id)
        {
            var response = await _roomServices.GetById<ApiResponse>(id, HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.isSucces)
            {

                RoomDto model = JsonConvert.DeserializeObject<RoomDto>(Convert.ToString(response.Results));

                return View(_mapper.Map<RoomUpdateDto>(model));

            }

            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRoom(RoomUpdateDto roomToUpdate) {

            if (ModelState.IsValid) { 

                var response = await _roomServices.Update<ApiResponse>(roomToUpdate, HttpContext.Session.GetString(SD.SessionToken));

                if (response != null && response.isSucces) {

                    TempData["isSucces"] = "Habitacion Actualizada";
                    return RedirectToAction(nameof(IndexRoom));

                }
                TempData["error"] = "Algo fallo en la actualizacion";

            }

            return View(roomToUpdate);

        }


        //delete Room
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var response = await _roomServices.GetById<ApiResponse>(id, HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.isSucces)
            {

                RoomDto model = JsonConvert.DeserializeObject<RoomDto>(Convert.ToString(response.Results));

                return View(model);

            }

            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRoom(RoomDto roomDto)
        {

            if (ModelState.IsValid)
            {

                var response = await _roomServices.DeleteById<ApiResponse>(roomDto.Id , HttpContext.Session.GetString(SD.SessionToken));

                if (response != null && response.isSucces)
                {

                    return RedirectToAction(nameof(IndexRoom));

                }
            }

            return View(roomDto);

        }

    }
}
