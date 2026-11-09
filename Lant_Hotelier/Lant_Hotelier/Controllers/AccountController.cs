using Lant_Hotelier.Services;
using Lant_Hotelier.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lant_Hotelier.Controllers
{
    public class AccountController : Controller
    {
        private readonly IdentityService _identityService;

        public AccountController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        // GET: /Account/Register
        public IActionResult Register() => View();

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _identityService.RegisterAsync(model);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        // GET: /Account/Login
        public IActionResult Login() => View();

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _identityService.LoginAsync(model);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError(string.Empty, "Email sau parolă incorectă.");
            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _identityService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Profil
        [Authorize]
        public async Task<IActionResult> Profil()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _identityService.GetUserByEmailAsync(email);
            if (user == null) return NotFound();

            var roles = await _identityService.GetRolesAsync(user);
            var model = new ProfilViewModel
            {
                Email = user.Email!,
                NumeComplet = user.NumeComplet,
                PozaProfil = user.PozaProfil,
                Rol = roles.FirstOrDefault() ?? "User"
            };
            return View(model);
        }

        // POST: /Account/UpdatePoza - upload fisier real
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePoza(IFormFile? PozaFisier)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (PozaFisier == null || PozaFisier.Length == 0)
            {
                TempData["Error"] = "Te rugăm să selectezi un fișier de imagine.";
                return RedirectToAction(nameof(Profil));
            }

            // Validare tip fisier
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
            if (!allowedTypes.Contains(PozaFisier.ContentType.ToLower()))
            {
                TempData["Error"] = "Sunt acceptate doar fișierele JPG, PNG, GIF sau WEBP.";
                return RedirectToAction(nameof(Profil));
            }

            var result = await _identityService.UpdatePozaProfilAsync(email, PozaFisier);
            if (result.Succeeded)
                TempData["Success"] = "Poza de profil a fost actualizată cu succes!";
            else
                TempData["Error"] = "Eroare la salvarea pozei.";

            return RedirectToAction(nameof(Profil));
        }

        // GET: /Account/AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
