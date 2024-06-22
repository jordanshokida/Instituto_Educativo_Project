using _2024_InstitutoEducativo.Helpers;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace _2024_InstitutoEducativo.Models
{
    public class Alumno : Persona
    {
        public Alumno()
        {
        }

        public Alumno(string nombre, string apellido, string email,string dni,bool activo, int numeroMatricula) : base(nombre,apellido,email,dni)
        {
            
            Activo = activo;
            NumeroMatricula = numeroMatricula;
            
        }


        [Required(ErrorMessage = ErrorMsge.Required)]
        public bool Activo { get; set; }

        [Required(ErrorMessage = ErrorMsge.Required)]
        [Range(Restrictions.RANGE_MIN, Restrictions.RANGE_MAX,ErrorMessage = ErrorMsge.IntMaxMin)]
        [Display(Name = Alias.Matricula)]
        public int NumeroMatricula { get; set; }

        
        public List<MateriaCursada> MateriasCursadas { get; set; }

        public int CarreraId { get; set; }

        public Carrera Carrera { get; set; }

        public List<Calificacion> Calificaciones { get; set; }



    }
}
