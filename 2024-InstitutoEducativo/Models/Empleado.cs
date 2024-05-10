using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.Models
{
    public class Empleado: Persona
    {
        [Required(ErrorMessage = ErrorMsge.Required)]
        [StringLength(Restrictions.STRINGLENGTH_MAX, MinimumLength = Restrictions.STRINGLENGTH_MIN1, ErrorMessage = ErrorMsge.StringMaxMin)]
        private String _legajo;
        public String Legajo
        {
            get { return _legajo.ToUpper(); }
            set { _legajo = value.ToUpper(); }
        }
    }
}
