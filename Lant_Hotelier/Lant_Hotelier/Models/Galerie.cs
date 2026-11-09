using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lant_Hotelier.Models
{
    [Table("Galerii")] 
    public class Galerie
    {
        [Key]
        public int ID_Galerie { get; set; }

        [Required(ErrorMessage = "Numele imaginii este obligatoriu.")]
        public string NumeImagine { get; set; }

        [Required(ErrorMessage = "Calea imaginii este obligatorie.")]
        public string CaleImagine { get; set; }

        [Display(Name = "Hotel")]
        public int ID_Hotel { get; set; }

        [ForeignKey("ID_Hotel")]
        public virtual Hotel? Hotel { get; set; }
    }
}