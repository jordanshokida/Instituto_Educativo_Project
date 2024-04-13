namespace _2024_InstitutoEducativo.Models
{
    public class Persona

    {  
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set;}

        public string Email { get; set;}

        public Telefono Telefono { get; set;}

        public List<Telefono> Telefonos { get; set;}

        public Direccion Direccion { get; set; }

        public string Dni { get; set; }

        public string NombreCompleto
        { 
            get {
                return $"{Nombre}, {Apellido}";
                }
        }

    }
}
