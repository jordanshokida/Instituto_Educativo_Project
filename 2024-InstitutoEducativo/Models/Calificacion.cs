using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.Models
{
    public class Calificacion
    {
        public Calificacion()
        {
            
        }

        
        public int Id { get; set; }
        
        [Required(ErrorMessage = ErrorMsge.NotaFinalRequerida)]
        [Range (1,10, ErrorMessage = ErrorMsge.NotaFinalRango)]
        public int NotaFinal { get; set; }

       //public int MateriaId { get; set; }

        //public Materia Materia { get; set; }

        public int MateriaCursadaId { get; set; }

        public MateriaCursada MateriaCursada { get; set; }

        public int ProfesorId { get; set; }

        public Profesor Profesor {  get; set; }

        public int AlumnoId { get; set; }

        public Alumno Alumno {  get; set; }

         
    }
}
