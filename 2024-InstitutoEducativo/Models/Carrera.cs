namespace _2024_InstitutoEducativo.Models
{
    public class Carrera
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<Materia> Materia { get; set; }

        public string Alumno { get; set; }
    }
}
