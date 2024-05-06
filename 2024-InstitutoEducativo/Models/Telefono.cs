using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.Models
{
    public class Telefono
    {
        [Key, ForeignKey("Telefono")]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[0-9]*", ErrorMessage = ErrorMsge.OnlyNumbers)]
        [StringLength(5, MinimumLength = 2, ErrorMessage = ErrorMsge.StringMaxMin)]
        public String CodArea { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[0-9]*", ErrorMessage = ErrorMsge.OnlyNumbers)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = ErrorMsge.StringMaxMin)]
        public string Numero { get; set;}

        public int PersonaId { get; set;}

        public Persona Persona { get; set;}

    }
}
