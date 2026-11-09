using System.ComponentModel.DataAnnotations;

namespace Lant_Hotelier.ViewModels
{
    public class ProfilViewModel
    {
        public string Email { get; set; }
        public string? NumeComplet { get; set; }
        public string? PozaProfil { get; set; }
        public string? Rol { get; set; }

        [Display(Name = "Poză Profil Nouă")]
        public IFormFile? PozaFisier { get; set; }
    }
}
