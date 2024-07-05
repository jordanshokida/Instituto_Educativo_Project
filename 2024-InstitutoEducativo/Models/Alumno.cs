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

        public Alumno(string nombre, string apellido, string email,string dni, int numeroMatricula) : base(nombre,apellido,email,dni)
        {
            
            
            NumeroMatricula = numeroMatricula;
            MateriasCursadas = new List<MateriaCursada>();
            Calificaciones = new List<Calificacion>();
        }


        [Range(Restrictions.RANGE_MIN, Restrictions.RANGE_MAX,ErrorMessage = ErrorMsge.IntMaxMin)]
        [Display(Name = Alias.Matricula)]
        public int? NumeroMatricula { get; set; }

        
        public List<MateriaCursada> MateriasCursadas { get; set; }

        public int CarreraId { get; set; }

        public Carrera Carrera { get; set; }

        public List<Calificacion> Calificaciones { get; set; }



    }
}
