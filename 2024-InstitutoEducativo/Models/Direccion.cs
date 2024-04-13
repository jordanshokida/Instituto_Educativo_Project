using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2024_InstitutoEducativo.Models
{
    public class Direccion
    {
        [Key,ForeignKey("Persona")]
        public int Id { get; set; }

        public int PersonaId { get; set; }

        public Persona Persona { get; set; }

        public string Calle { get; set; }

        public int Numero { get; set; }

        public string Localidad { get; set; }

        public string Provincia { get; set; }

        public string Pais { get; set; }
    }
}
