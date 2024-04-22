using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.Models
{
    public class Empleado: Persona
    {
        [Required(ErrorMessage = ErrorMsge.Required)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = ErrorMsge.StringMaxMin)]
        private String _legajo;
        public String Legajo
        {
            get { return _legajo.ToUpper(); }
            set { _legajo = value.ToUpper(); }
        }
    }
}
