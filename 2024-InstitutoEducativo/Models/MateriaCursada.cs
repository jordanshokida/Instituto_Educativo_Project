using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.Models
{
    public class MateriaCursada
    {
        [Key, ForeignKey("Materia")]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int AnioCursada { get; set; } = DateTime.Now.Year;

        public string Cuatrimestre { get; set; }

        public Materia Materia { get; set; }

        public int ProfesorId { get; set; }

        public Profesor Profesor { get; set; }

        public int AlumnoId { get; set; }

        public Alumno Alumno { get; set; }

        public Calificacion Calificacion { get; set; }

        public List<Calificacion> Calificaciones { get; set; }  
    }
}
