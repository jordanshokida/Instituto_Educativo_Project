using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2024_InstitutoEducativo.Models
{
    public class Telefono
    {
        public Telefono()
        {
            
        }

       
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[0-9]*", ErrorMessage = ErrorMsge.OnlyNumbers)]
        [StringLength(Restrictions.STRINGLENGTH_MAX4, MinimumLength = Restrictions.STRINGLENGTH_MIN1, ErrorMessage = ErrorMsge.StringMaxMin)]
        public String CodArea { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[0-9]*", ErrorMessage = ErrorMsge.OnlyNumbers)]
        [StringLength(Restrictions.STRINGLENGTH_MAX2, MinimumLength = Restrictions.STRINGLENGTH_MIN3, ErrorMessage = ErrorMsge.StringMaxMin)]
        public string Numero { get; set;}

        public int PersonaId { get; set;}

        public Persona Persona { get; set;}

    }
}
