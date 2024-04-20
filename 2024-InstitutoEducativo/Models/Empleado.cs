namespace _2024_InstitutoEducativo.Models
{
    public class Empleado: Persona
    {

        private String _legajo;
        public String Legajo
        {
            get { return _legajo.ToUpper(); }
            set { _legajo = value.ToUpper(); }
        }
    }
}
