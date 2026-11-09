using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lant_Hotelier.Models;
using Lant_Hotelier.Services;

namespace Lant_Hotelier.Controllers
{
    [Authorize]
    public class RezervariController : Controller
    {
        private readonly RezervariService _rezervariService;
        private readonly LanturiService _lanturiService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RezervariController(RezervariService rezervariService,
            LanturiService lanturiService,
            UserManager<ApplicationUser> userManager)
        {
            _rezervariService = rezervariService;
            _lanturiService = lanturiService;
            _userManager = userManager;
        }

        // Admin: vede TOATE rezervarile
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var toate = await _rezervariService.GetAllAsync();
            return View(toate);
        }

        // User: vede DOAR rezervarile lui
        public async Task<IActionResult> RezervariMele()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            var rezervari = await _rezervariService.GetByUserIdAsync(user.Id);
            return View(rezervari);
        }

        // STEP 1: Alege lantul hotelier
        public async Task<IActionResult> Create()
        {
            var lanturi = await _lanturiService.GetAllLanturiAsync();
            ViewBag.Lanturi = new SelectList(lanturi, "ID_Lant", "Nume_Lant");
            return View();
        }

        // STEP 2: Dupa selectia lantului, alege hotelul (AJAX)
        [HttpGet]
        public async Task<IActionResult> GetHoteluri(int lantId)
        {
            var hoteluri = await _rezervariService.GetHoteluriByLantIdAsync(lantId);
            return Json(hoteluri.Select(h => new { id = h.ID_Hotel, nume = h.Nume, oras = h.Oras }));
        }

        // STEP 3: Dupa selectia hotelului, alege camera (AJAX)
        [HttpGet]
        public async Task<IActionResult> GetCamere(int hotelId)
        {
            var camere = await _rezervariService.GetCamereByHotelIdAsync(hotelId);
            return Json(camere.Select(c => new { id = c.ID_Camera, tip = c.Tip_Camera, pret = c.Pret, descriere = c.Descriere }));
        }

        // STEP 4: Ofertele disponibile pt lantul selectat (AJAX)
        [HttpGet]
        public async Task<IActionResult> GetOferte(int lantId)
        {
            var oferte = await _rezervariService.GetOferteByLantIdAsync(lantId);
            return Json(oferte.Select(o => new {
                id = o.ID_Oferta, titlu = o.Titlu, pretNou = o.PretNou,
                pretVechi = o.PretVechi, procent = o.ProcentReducere, cod = o.CodPromotional
            }));
        }

        // POST: Creeaza rezervarea
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rezervare rezervare)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            ModelState.Remove("Camera");
            ModelState.Remove("Utilizator");
            ModelState.Remove("Oferta");
            ModelState.Remove("Hotel");
            ModelState.Remove("AspNetUserId");

            if (ModelState.IsValid)
            {
                rezervare.AspNetUserId = user.Id;
                rezervare.ID_Utilizator = 1;
                rezervare.Status = StatusRezervare.Pending;
                rezervare.DataCreare = DateTime.Now;

                // Calculeaza pretul total
                var camera = await _rezervariService.GetCameraByIdAsync(rezervare.ID_Camera);
                if (camera != null)
                {
                    var nrNopti = (rezervare.Data_Sfarsit - rezervare.Data_Inceput).Days;
                    rezervare.PretTotal = camera.Pret * nrNopti;

                    // Aplica reducerea ofertei daca exista
                    if (rezervare.ID_Oferta.HasValue)
                    {
                        var oferte = await _rezervariService.GetOferteByLantIdAsync(0);
                        // recalculeaza cu reducere - pretul camerei * nrNopti * (1 - reducere/100)
                        rezervare.PretTotal = rezervare.PretTotal * (1 - camera.Pret / 100);
                    }
                }

                await _rezervariService.CreateRezervareAsync(rezervare);
                return RedirectToAction(nameof(Confirmare), new { id = rezervare.ID_Rezervare });
            }

            var lanturi = await _lanturiService.GetAllLanturiAsync();
            ViewBag.Lanturi = new SelectList(lanturi, "ID_Lant", "Nume_Lant");
            return View(rezervare);
        }

        // Pagina de confirmare / sumar
        public async Task<IActionResult> Confirmare(int id)
        {
            var rezervare = await _rezervariService.GetRezervareByIdAsync(id);
            if (rezervare == null) return NotFound();

            // Verifica ca userul curent e proprietarul
            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (rezervare.AspNetUserId != user?.Id) return Forbid();
            }

            return View(rezervare);
        }

        // Admin: Accepta rezervarea
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accepta(int id)
        {
            await _rezervariService.SchimbaStatusAsync(id, StatusRezervare.Confirmata);
            TempData["Success"] = "Rezervarea a fost confirmată!";
            return RedirectToAction(nameof(Index));
        }

        // Admin: Respinge rezervarea
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Respinge(int id)
        {
            await _rezervariService.SchimbaStatusAsync(id, StatusRezervare.Respinsa);
            TempData["Success"] = "Rezervarea a fost respinsă.";
            return RedirectToAction(nameof(Index));
        }

        // Admin: Sterge rezervarea
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _rezervariService.DeleteRezervareAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
