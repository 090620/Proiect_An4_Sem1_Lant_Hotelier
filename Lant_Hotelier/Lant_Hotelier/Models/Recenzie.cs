using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lant_Hotelier.Models
{
    public class Recenzie
    {
        [Key]
        public int ID_Recenzie { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu")]
        [StringLength(100, ErrorMessage = "Numele nu poate depăși 100 caractere")]
        [Display(Name = "Numele tău")]
        public string Nume_Client { get; set; }

        [Required(ErrorMessage = "Comentariul este obligatoriu")]
        [StringLength(1000, ErrorMessage = "Comentariul nu poate depăși 1000 caractere")]
        public string Comentariu { get; set; }

        [Required(ErrorMessage = "Rating-ul este obligatoriu")]
        [Range(1, 5, ErrorMessage = "Rating-ul trebuie să fie între 1 și 5")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Selectează un hotel")]
        public int ID_Hotel { get; set; }

        [ForeignKey("ID_Hotel")]
        public Hotel Hotel { get; set; }

        public int ID_Client { get; set; }

        [ForeignKey("ID_Client")]
        public Utilizator Utilizator { get; set; }
    }
}
