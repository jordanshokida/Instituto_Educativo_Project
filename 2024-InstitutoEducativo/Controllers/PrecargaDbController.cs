using _2024_InstitutoEducativo.Data;
using _2024_InstitutoEducativo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _2024_InstitutoEducativo.Controllers
{
    public class PrecargaDbController : Controller
    {
        private readonly InstitutoContext _context;


        public PrecargaDbController(InstitutoContext context)
        {

            this._context = context;
        }
        public IActionResult Index()
        {
            AddDirecciones();
            return View();
        }


        #region Direcciones

        private void AddDirecciones()
        {
            if (!_context.Direcciones.Any())
            {
                _context.Direcciones.Add(CreateDireccion("San Pedro", 920, "Moron", "Buenos Aires", "Argentina"));
                _context.Direcciones.Add(CreateDireccion("Italia", 1050, "Moron", "Buenos Aires", "Argentina"));
                _context.Direcciones.Add(CreateDireccion("Suipacha", 359, "CABA", "Buenos Aires", "Argentina"));
            }

            _context.Direcciones.AddRange(_context.Direcciones);
            _context.SaveChanges();
        }

        private Direccion? CreateDireccion(String calle, int numero, String localidad, String provincia, String pais)
        {
            Direccion direccion = new Direccion()
            {
                Calle = calle,
                Numero = numero,
                Localidad = localidad,
                Provincia = provincia,
                Pais = pais
            };
            return direccion;
        }

        //private int? GetNextDireccionId()
        //{
        //    if (_context.Direcciones.Any())
        //    {
        //       var random = new Random();
        //       int i = random.Next(_context.Direcciones.Count()); //Me da un numero entre 0 y el anterior al count.
        //       int id = _context.Direcciones[i].Id;
        //       _context.Direcciones.Remove(i);
        //       return id;
        //    }
        //return null;
        //}

        #endregion
    }
}

