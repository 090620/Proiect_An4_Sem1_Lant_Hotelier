using Microsoft.AspNetCore.Authorization;
using Lant_Hotelier.Models;
using Lant_Hotelier.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize]
public class GalerieController : Controller
{
    private readonly GalerieService _galerieService;
    public GalerieController(GalerieService galerieService) => _galerieService = galerieService;

    // Toti utilizatorii autentificati pot vedea galeria
    public async Task<IActionResult> Index() =>
        View(await _galerieService.GetToatePozeleAsync());

    // Doar Admin poate adauga/edita/sterge
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(int? lantId)
    {
        var lanturi = await _galerieService.GetToateLanturileAsync();
        ViewBag.ID_Lant = new SelectList(lanturi, "ID_Lant", "Nume_Lant", lantId);
        ViewBag.ID_Hotel = lantId.HasValue
            ? new SelectList(await _galerieService.GetHoteluriByLantAsync(lantId.Value), "ID_Hotel", "Nume")
            : new SelectList(new List<Hotel>(), "ID_Hotel", "Nume");
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Galerie galerie)
    {
        ModelState.Remove("Hotel");
        if (ModelState.IsValid)
        {
            await _galerieService.SalveazaPozaAsync(galerie);
            return RedirectToAction(nameof(Index));
        }
        var lanturi = await _galerieService.GetToateLanturileAsync();
        ViewBag.ID_Lant = new SelectList(lanturi, "ID_Lant", "Nume_Lant");
        ViewBag.ID_Hotel = new SelectList(new List<Hotel>(), "ID_Hotel", "Nume");
        return View(galerie);
    }

    [HttpGet]
    public async Task<IActionResult> GetHoteluriByLant(int lantId)
    {
        var hoteluri = await _galerieService.GetHoteluriByLantAsync(lantId);
        var result = hoteluri.Select(h => new { id = h.ID_Hotel, nume = h.Nume });
        return Json(result);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var poza = await _galerieService.GetPozaByIdAsync(id);
        if (poza == null) return NotFound();
        var lanturi = await _galerieService.GetToateLanturileAsync();
        ViewBag.ID_Lant = new SelectList(lanturi, "ID_Lant", "Nume_Lant", poza.Hotel?.ID_Lant);
        ViewBag.ID_Hotel = new SelectList(
            poza.Hotel != null
                ? await _galerieService.GetHoteluriByLantAsync(poza.Hotel.ID_Lant)
                : new List<Hotel>(),
            "ID_Hotel", "Nume", poza.ID_Hotel);
        return View(poza);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Galerie galerie)
    {
        if (id != galerie.ID_Galerie) return NotFound();
        ModelState.Remove("Hotel");
        if (ModelState.IsValid)
        {
            await _galerieService.UpdatePozaAsync(galerie);
            return RedirectToAction(nameof(Index));
        }
        return View(galerie);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _galerieService.DeletePozaAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
