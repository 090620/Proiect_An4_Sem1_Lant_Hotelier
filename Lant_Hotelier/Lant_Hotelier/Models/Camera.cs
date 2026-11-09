using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lant_Hotelier.Models
{
    public class Camera
    {
        [Key]
        public int ID_Camera { get; set; }

        [Required(ErrorMessage = "Tipul camerei este obligatoriu")]
        [StringLength(100, ErrorMessage = "Tipul nu poate depăși 100 caractere")]
        [Display(Name = "Tip Cameră")]
        public string Tip_Camera { get; set; }

        [Required(ErrorMessage = "Prețul este obligatoriu")]
        [Range(1, 99999, ErrorMessage = "Prețul trebuie să fie între 1 și 99999")]
        [Display(Name = "Preț (RON)")]
        public decimal Pret { get; set; }

        [StringLength(500, ErrorMessage = "Descrierea nu poate depăși 500 caractere")]
        public string Descriere { get; set; }

        public int ID_Lant { get; set; }

        [ForeignKey("ID_Lant")]
        public LantHotelier LantHotelier { get; set; }
    }
}
