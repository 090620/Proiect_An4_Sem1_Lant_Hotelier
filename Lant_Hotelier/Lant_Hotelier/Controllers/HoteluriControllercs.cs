using Microsoft.AspNetCore.Authorization;
﻿using Microsoft.AspNetCore.Mvc;
using Lant_Hotelier.Models;
using Lant_Hotelier.Services; // Asigură-te că folderul Services este inclus corect

namespace Lant_Hotelier.Controllers
{

    [Authorize]
        public class HoteluriController : Controller
    {
        // Înlocuim ApplicationDbContext cu HoteluriService
        private readonly HoteluriService _hoteluriService;

        public HoteluriController(HoteluriService hoteluriService)
        {
            _hoteluriService = hoteluriService;
        }

        // INDEX - Afișează hotelurile filtrate pe Lanț folosind serviciul
        public async Task<IActionResult> Index(int? lantId)
        {
            if (lantId == null) return RedirectToAction("Index", "Lanturi");

            var hoteluri = await _hoteluriService.GetHoteluriByLantIdAsync(lantId.Value);

            ViewBag.LantId = lantId;
            ViewBag.NumeLant = await _hoteluriService.GetNumeLantAsync(lantId.Value);

            return View(hoteluri);
        }

        // CREATE: GET
        [Authorize(Roles = "Admin")]
        public IActionResult Create(int lantId)
        {
            ViewBag.LantId = lantId;
            return View();
        }

        // CREATE: POST - Delegăm salvarea către serviciu
        [Authorize(Roles = "Admin")]
        [HttpPost]
                [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hotel hotel, int lantId)
        {
            hotel.ID_Lant = lantId;
            ModelState.Remove("LantHotelier");

            if (ModelState.IsValid)
            {
                await _hoteluriService.CreateHotelAsync(hotel);
                return RedirectToAction(nameof(Index), new { lantId = lantId });
            }
            ViewBag.LantId = lantId;
            return View(hotel);
        }

        // EDIT: GET
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, int lantId)
        {
            if (id == null) return NotFound();

            var hotel = await _hoteluriService.GetHotelByIdAsync(id.Value);

            if (hotel == null) return NotFound();

            ViewBag.LantId = lantId;
            return View(hotel);
        }

        // EDIT: POST - Delegăm actualizarea către serviciu
        [Authorize(Roles = "Admin")]
        [HttpPost]
                [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Hotel hotel, int lantId)
        {
            if (id != hotel.ID_Hotel) return NotFound();

            ModelState.Remove("LantHotelier");
            hotel.ID_Lant = lantId;

            if (ModelState.IsValid)
            {
                await _hoteluriService.UpdateHotelAsync(hotel);
                return RedirectToAction(nameof(Index), new { lantId = lantId });
            }
            ViewBag.LantId = lantId;
            return View(hotel);
        }

        // DELETE: POST - Delegăm ștergerea către serviciu
        [Authorize(Roles = "Admin")]
        [HttpPost]
                [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int lantId)
        {
            await _hoteluriService.DeleteHotelAsync(id);
            return RedirectToAction(nameof(Index), new { lantId = lantId });
        }
    }
}