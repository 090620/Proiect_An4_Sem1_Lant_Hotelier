using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lant_Hotelier.Models;
using Lant_Hotelier.Services;

namespace Lant_Hotelier.Controllers
{

    [Authorize]
        public class OferteController : Controller
    {
        private readonly OferteService _oferteService;

        public OferteController(OferteService oferteService) => _oferteService = oferteService;

        public async Task<IActionResult> Index(int? lantId)
        {
            if (lantId == null) return RedirectToAction("Index", "Lanturi");
            ViewBag.LantId = lantId;
            return View(await _oferteService.GetOferteByLantIdAsync(lantId.Value));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create(int lantId)
        {
            ViewBag.LantId = lantId;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
                [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Oferte oferta, int lantId)
        {
            oferta.ID_Lant = lantId;
            ModelState.Remove("LantHotelier");

            if (ModelState.IsValid)
            {
                await _oferteService.CreateOfertaAsync(oferta);
                return RedirectToAction(nameof(Index), new { lantId });
            }
            ViewBag.LantId = lantId;
            return View(oferta);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, int lantId)
        {
            if (id == null) return NotFound();
            var oferta = await _oferteService.GetOfertaByIdAsync(id.Value);
            if (oferta == null) return NotFound();
            ViewBag.LantId = lantId;
            return View(oferta);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
                [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Oferte oferta, int lantId)
        {
            if (id != oferta.ID_Oferta) return NotFound();
            oferta.ID_Lant = lantId;
            ModelState.Remove("LantHotelier");

            if (ModelState.IsValid)
            {
                await _oferteService.UpdateOfertaAsync(oferta);
                return RedirectToAction(nameof(Index), new { lantId });
            }
            ViewBag.LantId = lantId;
            return View(oferta);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
                [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int lantId)
        {
            await _oferteService.DeleteOfertaAsync(id);
            return RedirectToAction(nameof(Index), new { lantId });
        }
    }
}
