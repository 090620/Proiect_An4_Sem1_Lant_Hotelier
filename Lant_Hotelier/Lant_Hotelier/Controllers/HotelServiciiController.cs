using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lant_Hotelier.Models;
using Lant_Hotelier.Services;

namespace Lant_Hotelier.Controllers
{

    [Authorize]
        public class HotelServiciiController : Controller
    {
        private readonly ServiciiService _serviciuService;

        public HotelServiciiController(ServiciiService serviciuService) => _serviciuService = serviciuService;

        public async Task<IActionResult> Index(int? lantId)
        {
            if (lantId == null) return RedirectToAction("Index", "Lanturi");
            ViewBag.LantId = lantId;
            return View(await _serviciuService.GetServiciiByLantIdAsync(lantId.Value));
        }

        public async Task<IActionResult> Create(int lantId)
        {
            ViewBag.LantId = lantId;
            var lanturi = await _serviciuService.GetToateLanturileAsync();
            ViewBag.ID_Lant = new SelectList(lanturi.Where(l => l.ID_Lant == lantId), "ID_Lant", "Nume_Lant", lantId);
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
                [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Servicii serviciu, int lantId)
        {
            serviciu.ID_Lant = lantId;
            ModelState.Remove("LantHotelier");
            ModelState.Remove("HotelServicii");

            if (ModelState.IsValid)
            {
                await _serviciuService.CreateServiciuAsync(serviciu);
                return RedirectToAction(nameof(Index), new { lantId });
            }
            ViewBag.LantId = lantId;
            var lanturi = await _serviciuService.GetToateLanturileAsync();
            ViewBag.ID_Lant = new SelectList(lanturi, "ID_Lant", "Nume_Lant", lantId);
            return View(serviciu);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, int lantId)
        {
            if (id == null) return NotFound();
            var serviciu = await _serviciuService.GetServiciuByIdAsync(id.Value);
            if (serviciu == null) return NotFound();
            ViewBag.LantId = lantId;
            var lanturi = await _serviciuService.GetToateLanturileAsync();
            ViewBag.ID_Lant = new SelectList(lanturi, "ID_Lant", "Nume_Lant", serviciu.ID_Lant);
            return View(serviciu);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
                [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Servicii serviciu, int lantId)
        {
            if (id != serviciu.ID_Servicii) return NotFound();
            ModelState.Remove("LantHotelier");
            ModelState.Remove("HotelServicii");

            if (ModelState.IsValid)
            {
                await _serviciuService.UpdateServiciuAsync(serviciu);
                return RedirectToAction(nameof(Index), new { lantId });
            }
            ViewBag.LantId = lantId;
            var lanturi = await _serviciuService.GetToateLanturileAsync();
            ViewBag.ID_Lant = new SelectList(lanturi, "ID_Lant", "Nume_Lant", serviciu.ID_Lant);
            return View(serviciu);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
                [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int lantId)
        {
            await _serviciuService.DeleteServiciuAsync(id);
            return RedirectToAction(nameof(Index), new { lantId });
        }
    }
}
