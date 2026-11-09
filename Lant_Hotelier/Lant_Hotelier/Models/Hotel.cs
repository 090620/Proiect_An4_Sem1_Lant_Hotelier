using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lant_Hotelier.Models
{
    public class Hotel
    {
        [Key]
        public int ID_Hotel { get; set; }

        [Required(ErrorMessage = "Numele hotelului este obligatoriu")]
        [StringLength(150, ErrorMessage = "Numele nu poate depăși 150 caractere")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Orașul este obligatoriu")]
        [StringLength(100, ErrorMessage = "Orașul nu poate depăși 100 caractere")]
        public string Oras { get; set; }

        [Required(ErrorMessage = "Țara este obligatorie")]
        [StringLength(100, ErrorMessage = "Țara nu poate depăși 100 caractere")]
        public string Tara { get; set; }

        [Required(ErrorMessage = "Adresa este obligatorie")]
        [StringLength(200, ErrorMessage = "Adresa nu poate depăși 200 caractere")]
        public string Adresa { get; set; }

        [StringLength(1000, ErrorMessage = "Descrierea nu poate depăși 1000 caractere")]
        public string Descriere { get; set; }

        [Display(Name = "URL Imagine")]
        public string Imagine { get; set; }

        public int ID_Lant { get; set; }

        [ForeignKey("ID_Lant")]
        public LantHotelier LantHotelier { get; set; }
    }
}
