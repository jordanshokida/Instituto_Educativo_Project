using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2024_InstitutoEducativo.Models
{
    public class Direccion
    {
        [Key,ForeignKey("Persona")]
        public int Id { get; set; }

        public int PersonaId { get; set; }

        public Persona Persona { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [StringLength(35, MinimumLength = 2, ErrorMessage = ErrorMsge.StringMaxMin)]
        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú]*", ErrorMessage = ErrorMsge.OnlyAlphabet)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = ErrorMsge.StringMaxMin)]

        public string Calle { get; set; }

        [Range(1, 56100, ErrorMessage = ErrorMsge.Range)]
        public int? Numero { get; set; }
        [Required(ErrorMessage = ErrorMsge.Required)]
        [Range(1, 99999, ErrorMessage = ErrorMsge.IntMaxMin)]

        public int Numero { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [StringLength(40, MinimumLength = 2, ErrorMessage = ErrorMsge.StringMaxMin)]
        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú]*", ErrorMessage = ErrorMsge.OnlyAlphabet)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = ErrorMsge.StringMaxMin)]

        public string Localidad { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [StringLength(25, MinimumLength = 2, ErrorMessage = ErrorMsge.StringMaxMin)]
        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú]*", ErrorMessage = ErrorMsge.OnlyAlphabet)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = ErrorMsge.StringMaxMin)]

        public string Provincia { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [StringLength(25, MinimumLength = 2, ErrorMessage = ErrorMsge.StringMaxMin)]
        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú]*", ErrorMessage = ErrorMsge.OnlyAlphabet)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = ErrorMsge.StringMaxMin)]

        public string Pais { get; set; }
    }
}
