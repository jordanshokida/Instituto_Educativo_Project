﻿using _2024_InstitutoEducativo.Helpers;
using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.Models
{
    public class Calificacion
    {
        public Calificacion()
        {
            
        }

        public Calificacion(int? notaFinal, int materiaCursadaId, int profesorId, int alumnoId)
        {
           
            NotaFinal = notaFinal;
            MateriaCursadaId = materiaCursadaId;
            ProfesorId = profesorId;          
            AlumnoId = alumnoId;            
        }

        public int Id { get; set; }
        
        
        [Range (1,10, ErrorMessage = ErrorMsge.NotaFinalRango)]
        [Display(Name = Alias.NotaFinal)]
        public int? NotaFinal { get; set; }

        public int MateriaCursadaId { get; set; }

        public MateriaCursada MateriaCursada { get; set; }

        public int ProfesorId { get; set; }

        public Profesor Profesor {  get; set; }

        public int AlumnoId { get; set; }

        public Alumno Alumno {  get; set; }

         
    }
}
