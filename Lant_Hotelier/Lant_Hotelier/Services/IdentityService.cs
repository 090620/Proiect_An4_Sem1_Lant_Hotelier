using Lant_Hotelier.Models;
using Lant_Hotelier.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Lant_Hotelier.Services
{
    public class IdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                NumeComplet = model.NumeComplet
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Daca nu exista NICIUN utilizator cu rolul Admin,
                // primul cont creat primeste Admin.
                // Astfel, daca stergi si recreezi adminul, logica ramane corecta.
                var adminExists = (await _userManager.GetUsersInRoleAsync("Admin")).Any();
                var rol = adminExists ? "User" : "Admin";

                await EnsureRoleExistsAsync(rol);
                await _userManager.AddToRoleAsync(user, rol);
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string? email)
        {
            if (string.IsNullOrEmpty(email)) return null;
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> UpdatePozaProfilAsync(string? email, IFormFile? fisier)
        {
            if (string.IsNullOrEmpty(email) || fisier == null || fisier.Length == 0)
                return IdentityResult.Failed(new IdentityError { Description = "Fisier invalid." });

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Utilizatorul nu a fost gasit." });

            var uploadFolder = Path.Combine(_env.WebRootPath, "uploads", "profile-pictures");
            Directory.CreateDirectory(uploadFolder);

            if (!string.IsNullOrEmpty(user.PozaProfil))
            {
                var oldPath = Path.Combine(_env.WebRootPath, user.PozaProfil.TrimStart('/'));
                if (File.Exists(oldPath)) File.Delete(oldPath);
            }

            var extension = Path.GetExtension(fisier.FileName).ToLowerInvariant();
            var fileName = $"{user.Id}_{DateTime.UtcNow.Ticks}{extension}";
            var filePath = Path.Combine(uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
                await fisier.CopyToAsync(stream);

            user.PozaProfil = $"/uploads/profile-pictures/{fileName}";
            return await _userManager.UpdateAsync(user);
        }

        private async Task EnsureRoleExistsAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public async Task EnsureRolesCreatedAsync()
        {
            foreach (var role in new[] { "Admin", "User" })
                await EnsureRoleExistsAsync(role);
        }
    }
}
