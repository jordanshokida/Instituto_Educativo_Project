using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2024_InstitutoEducativo.Models
{
    public class Materia
    {

        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú]*", ErrorMessage = ErrorMsge.OnlyAlphabet)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = ErrorMsge.StringMaxMin)]

        public string MateriaNombre { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [Range(1, 1000000, ErrorMessage = ErrorMsge.IntMaxMin)]

        public int CodMateria { get; set; }



        public string Descripcion { get; set; }

        public int CupoMaximo { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [Range(1, 1000000, ErrorMessage = ErrorMsge.IntMaxMin)]
        [Display(Name = Alias.MateriaId)]

        public int MateriaCursadaId { get; set; }

        public MateriaCursada MateriaCursada { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [Range(1, 10, ErrorMessage = ErrorMsge.IntMaxMin)]

        public Calificacion Calificacion { get; set; }

        public List<Calificacion> Calificaciones { get; set; }

        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }

    }
}
