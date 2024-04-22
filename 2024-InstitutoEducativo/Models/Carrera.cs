using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2024_InstitutoEducativo.Models
{
    public class Carrera
    {
        [Key,ForeignKey("Alumno")]
        public int Id { get; set; }

        [Required (ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú]*", ErrorMessage = ErrorMsge.OnlyAlphabet)]
        [StringLength(24, MinimumLength = 1, ErrorMessage = ErrorMsge.StringMaxMin)]
        public string Nombre { get; set; }

        public Materia Materia { get; set; }

        public List<Materia> Materias { get; set; }

        public Alumno Alumno { get; set; } 
        
        public List<Alumno> Alumnos { get; set; }
    }
}
