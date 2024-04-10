namespace _2024_InstitutoEducativo.Models
{
    public class Profesor: Persona
    {
        public int Legajo { get; set; }

        public Calificacion CalificacionesRealizadas { get; set; }

        public MateriaCursada MateriasCursadaActiva { get; set; }
    }
}
