using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lant_Hotelier.Models
{
    public enum StatusRezervare
    {
        Pending,
        Confirmata,
        Respinsa
    }

    public class Rezervare
    {
        [Key]
        public int ID_Rezervare { get; set; }

        [Required(ErrorMessage = "Data de check-in este obligatorie")]
        [Display(Name = "Data Check-in")]
        public DateTime Data_Inceput { get; set; }

        [Required(ErrorMessage = "Data de check-out este obligatorie")]
        [Display(Name = "Data Check-out")]
        public DateTime Data_Sfarsit { get; set; }

        [StringLength(500)]
        [Display(Name = "Observații")]
        public string? Observatii { get; set; }

        [Required]
        public int ID_Camera { get; set; }

        [ForeignKey("ID_Camera")]
        public Camera? Camera { get; set; }

        // Hotel selectat explicit de user
        public int? ID_Hotel { get; set; }
        [ForeignKey("ID_Hotel")]
        public Hotel? Hotel { get; set; }

        // Legatura cu Identity user
        public string? AspNetUserId { get; set; }

        // Pastreaza si ID_Utilizator vechi pt compatibilitate
        public int ID_Utilizator { get; set; }

        [ForeignKey("ID_Utilizator")]
        public Utilizator? Utilizator { get; set; }

        public int? ID_Oferta { get; set; }
        [ForeignKey("ID_Oferta")]
        public Oferte? Oferta { get; set; }

        public StatusRezervare Status { get; set; } = StatusRezervare.Pending;

        public DateTime DataCreare { get; set; } = DateTime.Now;

        // Pret total calculat
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PretTotal { get; set; }

        // Tip camera selectat
        [StringLength(200)]
        public string? Tip_Camera { get; set; }
    }
}
