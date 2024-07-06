using _2024_InstitutoEducativo.Helpers;
using _2024_InstitutoEducativo.Models;
using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.ViewModels
{
    public class CalificacionViewModel
    {

        
        public int AlumnoId { get; set; }

        public Alumno Alumno { get; set; }
        public string NombreCompleto { get; set; }
     
        public int MateriaCursadaId { get; set; }

        [Display(Name = Alias.MateriaCursada)]
        public string Nombre { get; set; }

        [Range(1, 10, ErrorMessage = ErrorMsge.NotaFinalRango)]
        [Display(Name = Alias.NotaFinal)]
        public int? NotaFinal { get; set; }

          
    }
}
