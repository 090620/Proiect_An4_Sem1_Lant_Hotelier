using System.ComponentModel.DataAnnotations;

namespace Lant_Hotelier.Models
{
    public class Oferte
    {
        [Key]
        public int ID_Oferta { get; set; }

        [Required(ErrorMessage = "Titlul este obligatoriu")]
        [StringLength(150, ErrorMessage = "Titlul nu poate depăși 150 caractere")]
        public string Titlu { get; set; }

        [StringLength(500, ErrorMessage = "Descrierea nu poate depăși 500 caractere")]
        public string? Descriere { get; set; }

        [Required(ErrorMessage = "Prețul vechi este obligatoriu")]
        [Range(0.01, 99999, ErrorMessage = "Prețul trebuie să fie pozitiv")]
        [Display(Name = "Preț Vechi (€)")]
        public decimal PretVechi { get; set; }

        [Required(ErrorMessage = "Prețul nou este obligatoriu")]
        [Range(0.01, 99999, ErrorMessage = "Prețul trebuie să fie pozitiv")]
        [Display(Name = "Preț Nou (€)")]
        public decimal PretNou { get; set; }

        [Range(1, 100, ErrorMessage = "Procentul trebuie să fie între 1 și 100")]
        [Display(Name = "Reducere (%)")]
        public int ProcentReducere { get; set; }

        [StringLength(50, ErrorMessage = "Codul nu poate depăși 50 caractere")]
        [Display(Name = "Cod Promoțional")]
        public string? CodPromotional { get; set; }

        public int? ID_Lant { get; set; }
        public virtual LantHotelier? LantHotelier { get; set; }
    }
}
