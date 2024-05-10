using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2024_InstitutoEducativo.Models
{
    public class Materia
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú]*", ErrorMessage = ErrorMsge.OnlyAlphabet)]
        [StringLength(Restrictions.STRINGLENGTH_MAX, MinimumLength = Restrictions.STRINGLENGTH_MIN1, ErrorMessage = ErrorMsge.StringMaxMin)]

        public string MateriaNombre { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [Range(Restrictions.RANGE_MIN, Restrictions.RANGE_MAX, ErrorMessage = ErrorMsge.IntMaxMin)]

        public int CodMateria { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [StringLength(Restrictions.STRINGLENGTH_MAX3, MinimumLength = Restrictions.STRINGLENGTH_MIN1, ErrorMessage = ErrorMsge.StringMaxMin)]


        public string Descripcion { get; set; }

        [Display(Name = Alias.CupoMaximo)]
        public int CupoMaximo { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [Range(Restrictions.RANGE_MIN, Restrictions.RANGE_MAX, ErrorMessage = ErrorMsge.IntMaxMin)]
        [Display(Name = Alias.MateriaId)]

        public int MateriaCursadaId { get; set; }

        public MateriaCursada MateriaCursada { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [Range(Restrictions.RANGE_MIN, Restrictions.RANGE_MAX1, ErrorMessage = ErrorMsge.IntMaxMin)]

        public Calificacion Calificacion { get; set; }

        public List<Calificacion> Calificaciones { get; set; }

        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }

    }
}
