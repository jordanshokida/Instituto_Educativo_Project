namespace _2024_InstitutoEducativo.Models
{
    public class Telefono
    {
        public int Id { get; set; }

        public int CodArea { get; set; }

        public string Numero { get; set;}

        public int PersonaId { get; set;}

        public Persona Persona { get; set;}

    }
}
