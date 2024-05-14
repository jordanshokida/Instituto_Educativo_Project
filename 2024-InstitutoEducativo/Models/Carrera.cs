using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2024_InstitutoEducativo.Models
{
    public class Carrera
    {
        public Carrera()
        {
            
        }

 
        public int Id { get; set; }

        [Required (ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú 1-10]*", ErrorMessage = ErrorMsge.NotValid)]
        [StringLength(Restrictions.STRINGLENGTH_MAX5, MinimumLength = Restrictions.STRINGLENGTH_MIN, ErrorMessage = ErrorMsge.StringMaxMin)]
        public string Nombre { get; set; }

        public Materia Materia { get; set; }

        public List<Materia> Materias { get; set; }

        public Alumno Alumno { get; set; } 
        
        public List<Alumno> Alumnos { get; set; }
    }
}
