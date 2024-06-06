using _2024_InstitutoEducativo.Data;
using _2024_InstitutoEducativo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _2024_InstitutoEducativo.Controllers
{
    public class PrecargaDbController : Controller
    {
        private readonly InstitutoContext _context;
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _rolemanager;

        private List<string> roles = new List<string>() { "Alumno", "Profesor", "Empleado","Usuario" };

        public PrecargaDbController(InstitutoContext context, UserManager<Persona> userManager, RoleManager<Rol> rolemanager)
        {

            this._context = context;
            this._userManager = userManager;
            this._rolemanager = rolemanager;
        }

       // public PrecargaDbController(InstitutoContext context)
       // {

       //     this._context = context;
        //}

        public IActionResult Seed()
        {
            //Delete Database 
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();
            //Re-createDatabase
            //CrearRoles
            CrearRoles().Wait();
            CrearUsuario().Wait();
            CrearAlumno().Wait();
            CrearProfesor().Wait();
            CrearEmpleado().Wait();



            //Agregas todo lo demas en orden de dependica. 

            AddDirecciones();
            return RedirectToAction("Index","Home", new {mensaje="Proceso Seed Finalizado"});
        }

        private async Task CrearEmpleado()
        {
            
        }

        private async Task CrearProfesor()
        {
            
        }

        private async Task CrearAlumno()
        {
            
        }

        private async Task CrearUsuario()
        {
            
        }

        private async Task CrearRoles()
        {
            foreach(var rolName in roles)
            {
                if(! await _rolemanager.RoleExistsAsync(rolName)) {
                   await _rolemanager.CreateAsync(new Rol(rolName));
                }
            }
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

