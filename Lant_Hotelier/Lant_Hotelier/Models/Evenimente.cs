using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lant_Hotelier.Models
{
    public class Evenimente
    {
        [Key]
        public int ID_Eveniment { get; set; }

        [Required(ErrorMessage = "Titlul este obligatoriu")]
        public string Titlu { get; set; }

        public string? Descriere { get; set; }

        [Required(ErrorMessage = "Imaginea este obligatorie")]
        public string ImagineUrl { get; set; }

        [Required(ErrorMessage = "Data este obligatorie")]
        public string DataSiOra { get; set; }

        [Required(ErrorMessage = "Locația este obligatorie")]
        public string Locatie { get; set; }

        [Display(Name = "Lanț Hotelier")]
        public int ID_Lant { get; set; }

        [ForeignKey("ID_Lant")]
        public virtual LantHotelier? LantHotelier { get; set; }
    }
}