namespace _2024_InstitutoEducativo.Models
{
    public class Profesor: Persona
    {
        private String _legajo;
        public String Legajo {
            get { return _legajo.ToUpper();}
            set {_legajo = value.ToUpper();}
        }

        public int CalificacionId { get; set; }
        public List<Calificacion> CalificacionesRealizadas { get; set; }
        public int MateriaCursadaId { get; set; }
        public List<MateriaCursada> MateriasCursadaActiva { get; set; }
    }
}
