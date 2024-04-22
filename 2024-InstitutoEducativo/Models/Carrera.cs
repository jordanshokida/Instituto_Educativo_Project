using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2024_InstitutoEducativo.Models
{
    public class Carrera
    {
        [Key,ForeignKey("Alumno")]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public Materia Materia { get; set; }

        public List<Materia> Materias { get; set; }

        public Alumno Alumno { get; set; } 
        
        public List<Alumno> Alumnos { get; set; }
    }
}
