using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lant_Hotelier.Models
{
    public class Utilizator
    {
        [Key] 
        public int ID_Utilizator { get; set; }

        [Required]
        public string Nume { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Parola { get; set; }

        public string Rol_Utilizator { get; set; } 

       
        public ICollection<Rezervare> Rezervari { get; set; }
        public ICollection<Recenzie> Recenzii { get; set; }
    }
}