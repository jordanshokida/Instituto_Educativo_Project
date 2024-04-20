using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.Models
{
    public class Alumno : Persona
    {
        [Required(ErrorMessage = ErrorMsge.Required)]
        public bool Activo { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [Range(1,1000000,ErrorMessage = ErrorMsge.IntMaxMin)]
        [Display(Name = Alias.Matricula)]
        public int NumeroMatricula { get; set; }

        public MateriaCursada MateriaCursada { get; set; }

        public List<Materia> MateriasCursadas { get; set; }

        public int CarreraId { get; set; }

        public Carrera Carrera { get; set; }

        public Calificacion Calificacion { get; set; }

        public List<Calificacion> Calificaciones { get; set; }



    }
}
