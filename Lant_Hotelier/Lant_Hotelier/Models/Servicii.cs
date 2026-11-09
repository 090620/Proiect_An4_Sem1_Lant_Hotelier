using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lant_Hotelier.Models
{
    public class Servicii
    {
        [Key]
        public int ID_Servicii { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string Nume { get; set; }

        public string? Descriere { get; set; }

        public string? Iconita { get; set; }

        public decimal Pret { get; set; } = 0;

        [Display(Name = "Lanț Hotelier")]
        public int? ID_Lant { get; set; }

        [ForeignKey("ID_Lant")]
        public virtual LantHotelier? LantHotelier { get; set; }

        public virtual ICollection<HotelServiciu>? HotelServicii { get; set; }
    }
}