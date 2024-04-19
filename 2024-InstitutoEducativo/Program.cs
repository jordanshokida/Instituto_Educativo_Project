namespace _2024_InstitutoEducativo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

           

            var app = Startup.InicializarApp(args);//Pasamos los argumentos que son recibidos en la ejecución

           

            app.Run();
        }
    }
}
