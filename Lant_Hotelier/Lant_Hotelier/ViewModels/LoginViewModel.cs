using System.ComponentModel.DataAnnotations;

namespace Lant_Hotelier.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email-ul este obligatoriu")]
        [EmailAddress(ErrorMessage = "Introdu un email valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Ține-mă minte")]
        public bool RememberMe { get; set; }
    }
}
