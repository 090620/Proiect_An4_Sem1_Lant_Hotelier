using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lant_Hotelier.Models;
using Lant_Hotelier.Services;

namespace Lant_Hotelier.Controllers
{

    [Authorize]
        public class CamereController : Controller
    {
        private readonly CamereService _camereService;

        public CamereController(CamereService camereService) => _camereService = camereService;

        public async Task<IActionResult> Index(int? lantId)
        {
            if (lantId == null) return RedirectToAction("Index", "Lanturi");
            ViewBag.LantId = lantId;
            return View(await _camereService.GetCamereByLantIdAsync(lantId.Value));
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
        public async Task<IActionResult> Create(Camera camera, int lantId)
        {
            camera.ID_Lant = lantId;
            ModelState.Remove("LantHotelier");

            if (ModelState.IsValid)
            {
                await _camereService.CreateCameraAsync(camera);
                return RedirectToAction(nameof(Index), new { lantId });
            }
            ViewBag.LantId = lantId;
            return View(camera);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, int lantId)
        {
            if (id == null) return NotFound();
            var camera = await _camereService.GetCameraByIdAsync(id.Value);
            if (camera == null) return NotFound();
            ViewBag.LantId = lantId;
            return View(camera);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
                [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Camera camera, int lantId)
        {
            if (id != camera.ID_Camera) return NotFound();
            camera.ID_Lant = lantId;
            ModelState.Remove("LantHotelier");

            if (ModelState.IsValid)
            {
                await _camereService.UpdateCameraAsync(camera);
                return RedirectToAction(nameof(Index), new { lantId });
            }
            ViewBag.LantId = lantId;
            return View(camera);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id, int lantId)
        {
            await _camereService.DeleteCameraAsync(id);
            return RedirectToAction(nameof(Index), new { lantId });
        }
    }
}
