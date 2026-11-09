using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lant_Hotelier.Models
{
    public class Contact
    {
        [Key] 
        public int ID_Contact { get; set; }

        public string Telefon { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

       
        public int ID_Hotel { get; set; }

        [ForeignKey("ID_Hotel")]
        public Hotel Hotel { get; set; }
    }
}