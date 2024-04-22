using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2024_InstitutoEducativo.Models
{
    public class Materia
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = ErrorMsge.StringMaxMin)]
        public string MateriaNombre { get; set; }

        public int CodMateria { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [StringLength(80, MinimumLength = 2, ErrorMessage = ErrorMsge.StringMaxMin)]
        public string Descripcion { get; set; }


        public int CupoMaximo { get; set; }

        public int MateriaCursadaId { get; set; }

        public MateriaCursada MateriaCursada { get; set; }

        public Calificacion Calificacion { get; set; }

        public List<Calificacion> Calificaciones { get; set; }

        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }

    }
}
