using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lant_Hotelier.Models;
using Lant_Hotelier.Services;

namespace Lant_Hotelier.Controllers
{
    [Authorize]
    public class LanturiController : Controller
    {
        // Injectăm Serviciul, nu Contextul!
        private readonly LanturiService _lanturiService;

        public LanturiController(LanturiService lanturiService)
        {
            _lanturiService = lanturiService;
        }

        // 1. LISTA - Read
        public async Task<IActionResult> Index()
        {
            var lanturi = await _lanturiService.GetAllLanturiAsync();
            return View(lanturi);
        }

        // 2. CREATE (GET)
        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        // 3. CREATE (POST) - Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LantHotelier lant)
        {
            // Relația de navigare nu vine din formular
            ModelState.Remove("Hoteluri");

            if (ModelState.IsValid)
            {
                await _lanturiService.CreateLantAsync(lant);
                return RedirectToAction(nameof(Index));
            }
            return View(lant);
        }

        // 4. EDIT (GET)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var lant = await _lanturiService.GetLantByIdAsync(id.Value);

            if (lant == null) return NotFound();
            return View(lant);
        }

        // 5. EDIT (POST) - Update
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LantHotelier lant)
        {
            if (id != lant.ID_Lant) return NotFound();

            // Curățăm validarea pentru relațiile navigaționale
            ModelState.Remove("Hoteluri");

            if (ModelState.IsValid)
            {
                await _lanturiService.UpdateLantAsync(lant);
                return RedirectToAction(nameof(Index));
            }
            return View(lant);
        }

        // 6. DELETE (POST) - Delete
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _lanturiService.DeleteLantAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // 7. DETAILS (Redirecționare către Hoteluri)
        public IActionResult Details(int id)
        {
            return RedirectToAction("Index", "Hoteluri", new { lantId = id });
        }
    }
}
git status