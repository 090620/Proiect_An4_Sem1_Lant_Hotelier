using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lant_Hotelier.Models;
using Lant_Hotelier.Services;

namespace Lant_Hotelier.Controllers
{

    [Authorize]
        public class EvenimenteController : Controller
    {
        private readonly EvenimenteService _evenimenteService;

        public EvenimenteController(EvenimenteService evenimenteService) => _evenimenteService = evenimenteService;

        public async Task<IActionResult> Index(int? lantId)
        {
            if (lantId == null) return RedirectToAction("Index", "Lanturi");
            ViewBag.LantId = lantId;
            return View(await _evenimenteService.GetEvenimenteByLantIdAsync(lantId.Value));
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
        public async Task<IActionResult> Create(Evenimente ev, int lantId)
        {
            ev.ID_Lant = lantId;
            ModelState.Remove("LantHotelier");

            if (ModelState.IsValid)
            {
                await _evenimenteService.CreateEvenimentAsync(ev);
                return RedirectToAction(nameof(Index), new { lantId });
            }
            ViewBag.LantId = lantId;
            return View(ev);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, int lantId)
        {
            if (id == null) return NotFound();
            var ev = await _evenimenteService.GetEvenimentByIdAsync(id.Value);
            if (ev == null) return NotFound();
            ViewBag.LantId = lantId;
            return View(ev);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
                [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Evenimente ev, int lantId)
        {
            if (id != ev.ID_Eveniment) return NotFound();
            ev.ID_Lant = lantId;
            ModelState.Remove("LantHotelier");

            if (ModelState.IsValid)
            {
                await _evenimenteService.UpdateEvenimentAsync(ev);
                return RedirectToAction(nameof(Index), new { lantId });
            }
            ViewBag.LantId = lantId;
            return View(ev);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id, int lantId)
        {
            await _evenimenteService.DeleteEvenimentAsync(id);
            return RedirectToAction(nameof(Index), new { lantId });
        }
    }
}
