using _2024_InstitutoEducativo.Helpers;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.Net;
namespace _2024_InstitutoEducativo.Models
{
    public class Empleado: Persona
    {
        public Empleado()
        {
        }

        public Empleado(string nombre, string apellido, string email, string dni, string legajo) : base(nombre, apellido, email, dni)
        {
            Legajo = legajo;
            
        }

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
