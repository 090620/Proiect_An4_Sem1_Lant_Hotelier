using System.ComponentModel.DataAnnotations;

namespace Lant_Hotelier.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Numele complet este obligatoriu")]
        [StringLength(100)]
        [Display(Name = "Nume Complet")]
        public string NumeComplet { get; set; }

        [Required(ErrorMessage = "Email-ul este obligatoriu")]
        [EmailAddress(ErrorMessage = "Introdu un email valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Parola trebuie sa aiba minim 6 caractere")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmarea parolei este obligatorie")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parolele nu coincid")]
        [Display(Name = "Confirma Parola")]
        public string ConfirmPassword { get; set; }
    }
}
