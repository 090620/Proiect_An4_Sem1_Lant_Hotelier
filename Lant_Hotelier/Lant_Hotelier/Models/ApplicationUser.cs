using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lant_Hotelier.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string? NumeComplet { get; set; }

        public string? PozaProfil { get; set; } // URL sau cale relativă
    }
}
