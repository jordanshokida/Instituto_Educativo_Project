using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = ErrorMsge.Required)]
        [Display(Name = Alias.CorreoElectronico)]
        [EmailAddress(ErrorMessage = ErrorMsge.NotValid)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.Contrasenia)]
        public string Password { get; set; }

        public bool Recordarme { get; set; }

    }
}
