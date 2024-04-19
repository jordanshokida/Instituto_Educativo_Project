﻿namespace _2024_InstitutoEducativo.Models
{
    public class Alumno : Persona
    {

        public bool Activo { get; set; }

        public int NumeroMatricula { get; set; }

        public MateriaCursada MateriaCursada { get; set; }

        public List<Materia> MateriasCursadas { get; set; }

        public int CarreraId { get; set; }

        public Carrera Carrera { get; set; }

        public Calificacion Calificacion { get; set; }

        public List<Calificacion> Calificaciones { get; set; }



    }
}
