using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2024_InstitutoEducativo.Models
{
    public class Materia
    {
        
        public int Id { get; set; }

        public string MateriaNombre { get; set; }

        public int CodMateria { get; set; }

        public string Descripcion { get; set; }

        public int CupoMaximo { get; set; }

        public MateriaCursada MateriasCursadas { get; set; }

        public List<Calificacion> Calificaciones { get; set; }

        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }

    }
}
