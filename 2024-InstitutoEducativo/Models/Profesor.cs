using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.Models
{
    public class Profesor: Persona
    {
        public Profesor()
        {
            
        }

        public Profesor(string nombre, string apellido, string email, string dni, string legajo) : base(nombre, apellido, email, dni)
        {

            Legajo = legajo;
            MateriasCursadaActiva = new List<MateriaCursada>();
            CalificacionesRealizadas = new List<Calificacion>();
        }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [StringLength(Restrictions.STRINGLENGTH_MAX, MinimumLength = Restrictions.STRINGLENGTH_MIN1, ErrorMessage = ErrorMsge.StringMaxMin)]
        private String _legajo;
        public String Legajo {
            get { return _legajo.ToUpper();}
            set {_legajo = value.ToUpper();}
        }

        public List<Calificacion> CalificacionesRealizadas { get; set; }

        public List<MateriaCursada> MateriasCursadaActiva { get; set; }
    }
}
