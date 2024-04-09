namespace _2024_InstitutoEducativo.Models
{
    public class Alumno : Persona
    {

        public int FechaAlta { get; set; }

        public bool Activo { get; set; }

        public int NumeroMatricula { get; set; }

        public Materia MateriasCursadas { get; set; }

        public Carrera Carrera { get; set; }

        public Calificacion Calificaciones { get; set; }



    }
}
