using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lant_Hotelier.Models
{
    public class LantHotelier
    {
        [Key]
        public int ID_Lant { get; set; }

        [Required(ErrorMessage = "Numele lanțului este obligatoriu")]
        [StringLength(150, ErrorMessage = "Numele nu poate depăși 150 caractere")]
        [Display(Name = "Nume Lanț")]
        public string Nume_Lant { get; set; }

        [StringLength(1000, ErrorMessage = "Descrierea nu poate depăși 1000 caractere")]
        public string Descriere { get; set; }

        public ICollection<Hotel> Hoteluri { get; set; }
    }
}
