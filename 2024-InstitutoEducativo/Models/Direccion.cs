using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2024_InstitutoEducativo.Models
{
    public class Direccion
    {
        public Direccion()
        {
            
        }

        [Key,ForeignKey("Persona")]
        public int Id { get; set; }

        public int PersonaId { get; set; }

        public Persona Persona { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú]*", ErrorMessage = ErrorMsge.OnlyAlphabet)]
        [StringLength(Restrictions.STRINGLENGTH_MAX, MinimumLength = Restrictions.STRINGLENGTH_MIN1, ErrorMessage = ErrorMsge.StringMaxMin)]

        public string Calle { get; set; }

        [Range(Restrictions.RANGE_MIN, Restrictions.RANGE_MAX2, ErrorMessage = ErrorMsge.Range)]     
        [Required(ErrorMessage = ErrorMsge.Required)]
        public int Numero { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú]*", ErrorMessage = ErrorMsge.OnlyAlphabet)]
        [StringLength(Restrictions.STRINGLENGTH_MAX, MinimumLength =  Restrictions.STRINGLENGTH_MIN1, ErrorMessage = ErrorMsge.StringMaxMin)]

        public string Localidad { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú]*", ErrorMessage = ErrorMsge.OnlyAlphabet)]
        [StringLength(Restrictions.STRINGLENGTH_MAX, MinimumLength = Restrictions.STRINGLENGTH_MIN1, ErrorMessage = ErrorMsge.StringMaxMin)]

        public string Provincia { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú]*", ErrorMessage = ErrorMsge.OnlyAlphabet)]
        [StringLength(Restrictions.STRINGLENGTH_MAX, MinimumLength = Restrictions.STRINGLENGTH_MIN1, ErrorMessage = ErrorMsge.StringMaxMin)]

        public string Pais { get; set; }
    }
}
