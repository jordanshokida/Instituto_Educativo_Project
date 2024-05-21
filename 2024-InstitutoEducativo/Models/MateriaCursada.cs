using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using _2024_InstitutoEducativo.Helpers;

namespace _2024_InstitutoEducativo.Models
{
    public class MateriaCursada
    {
        public MateriaCursada()
        {
            
        }

      
        public int Id { get; set; }


        [Required(ErrorMessage = ErrorMsge.Required)]
        [StringLength(Restrictions.STRINGLENGTH_MAX, MinimumLength = Restrictions.STRINGLENGTH_MIN1, ErrorMessage = ErrorMsge.StringMaxMin)]
        public string Nombre { get; set; }

        [Display(Name = Alias.AnioCursada)]
        public int AnioCursada { get; set; } = DateTime.Now.Year;

        [Required(ErrorMessage = ErrorMsge.Required)]
        [StringLength(Restrictions.STRINGLENGTH_MIN2, MinimumLength = Restrictions.STRINGLENGTH_MIN, ErrorMessage = ErrorMsge.StringMaxMin)]
        public string Cuatrimestre { get; set; }

      
        public int MateriaId { get; set; }

        public Materia Materia { get; set; }
        
        public int ProfesorId { get; set; }

        public Profesor Profesor { get; set; }
       
        public int AlumnoId { get; set; }

        public Alumno Alumno { get; set; }

        public List<Calificacion> Calificaciones { get; set; }  
    }
}
