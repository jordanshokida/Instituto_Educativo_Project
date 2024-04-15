using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.Models
{
    public class Persona

    {  
        public int Id { get; set; }
        [Required]  
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set;}
        [Required]
        public string Email { get; set;}
        
        public Telefono Telefono { get; set;}

        public List<Telefono> Telefonos { get; set;}

        public Direccion Direccion { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; } = DateTime.Now.Date;
        [Required]
        public String Password { get; set;}
        [Required]
        public string Dni { get; set; }

        public string NombreCompleto
        { 
            get {
                return $"{Nombre}, {Apellido}";
                }
        }

    }
}
