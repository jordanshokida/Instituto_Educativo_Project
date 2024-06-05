using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.ViewModels
{
    public class RegistroUsuario
    {

        [Required(ErrorMessage = ErrorMsge.Required)]
        [Display(Name = Alias.CorreoElectronico)]
        [EmailAddress(ErrorMessage = ErrorMsge.NotValid)]
        public string Email{ get; set;}

        [Required(ErrorMessage = ErrorMsge.Required)]
        [DataType(DataType.Password)]
        [Display(Name =Alias.Contrasenia)]
        public string Password {  get; set;}

        [Required(ErrorMessage =ErrorMsge.Required)]
        [DataType(DataType.Password)]
        [Display(Name =Alias.ConfirmContrasenia)]
        [Compare("Password",ErrorMessage = ErrorMsge.PassMissmatch)]
        public string ConfirmacionPassword { get; set; }  

    }
}
