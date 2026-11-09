using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lant_Hotelier.Models
{
    public class HotelServiciu
    {
        [Display(Name = "Hotel")]
        public int ID_Hotel { get; set; }

        [ForeignKey("ID_Hotel")]
        public virtual Hotel? Hotel { get; set; }

        [Display(Name = "Serviciu")]
        public int ID_Servicii { get; set; }

        [ForeignKey("ID_Servicii")]
        public virtual Servicii? Servicii { get; set; }
    }
}