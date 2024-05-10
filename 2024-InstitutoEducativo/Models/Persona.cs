using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.Models
{
    public class Persona

    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú]*",ErrorMessage = ErrorMsge.OnlyAlphabet)]
        [StringLength(Restrictions.STRINGLENGTH_MAX, MinimumLength = Restrictions.STRINGLENGTH_MIN1, ErrorMessage = ErrorMsge.StringMaxMin)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [RegularExpression("@[a-zA-Z áéíóú]*", ErrorMessage = ErrorMsge.OnlyAlphabet)]
        [StringLength(Restrictions.STRINGLENGTH_MAX, MinimumLength = Restrictions.STRINGLENGTH_MIN1, ErrorMessage = ErrorMsge.StringMaxMin)]
        public string Apellido { get; set;}

        [Required(ErrorMessage = ErrorMsge.Required)]
        [Display(Name = Alias.CorreoElectronico)]
        [EmailAddress(ErrorMessage = ErrorMsge.NotValid)]
        public string Email { get; set;}
        
        public Telefono Telefono { get; set;}

        public List<Telefono> Telefonos { get; set;}

        public Direccion Direccion { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [Display(Name = Alias.FechaAlta)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime FechaAlta { get; set; } = DateTime.Now.Date;

        [Required(ErrorMessage = ErrorMsge.Required)]
        [Display(Name = Alias.Contrasenia)]
        [DataType(DataType.Password)]
        public String Password { get; set;}
        [Required(ErrorMessage = ErrorMsge.Required)]

        [RegularExpression(@"^[0-9]{8}$", ErrorMessage= ErrorMsge.DniFormat)]
        [Display(Name = Alias.PersonaDocumento)]
        public string Dni { get; set; }

        public string NombreCompleto
        { 
            get {
                return $"{Nombre}, {Apellido}";
                }
        }

    }
}
