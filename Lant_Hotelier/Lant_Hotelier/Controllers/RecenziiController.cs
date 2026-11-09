using Microsoft.AspNetCore.Authorization;
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lant_Hotelier.Models;
using Lant_Hotelier.Services;

namespace Lant_Hotelier.Controllers
{

    [Authorize]
        public class RecenziiController : Controller
    {
        private readonly RecenziiService _recenziiService;
        private readonly HoteluriService _hoteluriService; // Avem nevoie de el pentru dropdown-ul de hoteluri

        public RecenziiController(RecenziiService recenziiService, HoteluriService hoteluriService)
        {
            _recenziiService = recenziiService;
            _hoteluriService = hoteluriService;
        }

        // INDEX: Afișează recenziile filtrate pe Lanț folosind serviciul
        public async Task<IActionResult> Index(int? lantId)
        {
            if (lantId == null) return RedirectToAction("Index", "Lanturi");

            var recenzii = await _recenziiService.GetRecenziiByLantIdAsync(lantId.Value);

            ViewBag.LantId = lantId;
            return View(recenzii);
        }

        // CREATE: GET - Pregătim dropdown-ul folosind HoteluriService
        public async Task<IActionResult> Create(int lantId)
        {
            ViewBag.LantId = lantId;

            // Folosim HoteluriService pentru a lua hotelurile din lanț
            var hoteluriLant = await _hoteluriService.GetHoteluriByLantIdAsync(lantId);
            ViewBag.ID_Hotel = new SelectList(hoteluriLant, "ID_Hotel", "Nume");

            return View();
        }

        // CREATE: POST - Delegăm logica de salvare către serviciu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Recenzie recenzie, int lantId)
        {
            ModelState.Remove("Hotel");
            ModelState.Remove("Utilizator");

            if (ModelState.IsValid)
            {
                // Setăm ID-ul clientului implicit (sau preluat din logica de business)
                recenzie.ID_Client = 1;

                await _recenziiService.CreateRecenzieAsync(recenzie);
                return RedirectToAction(nameof(Index), new { lantId = lantId });
            }

            // În caz de eroare, reîncărcăm dropdown-ul
            ViewBag.LantId = lantId;
            var hoteluriLant = await _hoteluriService.GetHoteluriByLantIdAsync(lantId);
            ViewBag.ID_Hotel = new SelectList(hoteluriLant, "ID_Hotel", "Nume", recenzie.ID_Hotel);
            return View(recenzie);
        }

        // DELETE: POST - Delegăm ștergerea către serviciu
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int lantId)
        {
            await _recenziiService.DeleteRecenzieAsync(id);
            return RedirectToAction(nameof(Index), new { lantId = lantId });
        }
    }
}