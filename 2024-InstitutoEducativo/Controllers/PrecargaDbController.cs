using _2024_InstitutoEducativo.Data;
using _2024_InstitutoEducativo.Helpers;
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

        private List<string> roles = new List<string>() { Configs.AlumnoRolName,Configs.ProfesorRolName, Configs.EmpleadoRolName,"Usuario" };
        private readonly List<Direccion> _direcciones = new List<Direccion>();
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
            AgregarDirecciones();
            AgregarTelefonos();
            Agregar
            CrearProfesor().Wait();
            CrearEmpleado().Wait();



            //Agregas todo lo demas en orden de dependica. 

            AddDirecciones();
            return RedirectToAction("Index","Home", new {mensaje="Se ejecuto la precarga"});
        }



        private Carrera? CrearCarrera(String nombre)
        {
            Carrera carrera = new Carrera()
            {
                Nombre = nombre
            };
            _context.Carreras.Add(carrera);
            _context.SaveChanges();
            if(carrera.Id != 0)
            {
                return carrera;
            }

            return null;
        }

        private Materia? CrearMateria(String nombre, int codMateria, String descripcion, int cupoMax, int carreraId) {
            Materia materia = new Materia()
            {
                MateriaNombre = nombre,
                CodMateria = codMateria,
                Descripcion = descripcion,
                CupoMaximo = cupoMax,
                CarreraId = carreraId
            };
            _context.Materias.Add(materia);
            _context.SaveChanges();
            if (materia.Id != 0)
            {
                return materia;
            }

            return null;
        }


        private async Task AgregarAlumnos()
        {
            if (_context.Alumnos.Any())
            {
                int? dirId = GetNextDireccionId();

                if (dirId.HasValue)
                {
                    var alumno1 = await CrearAlumno("Luis Alberto", "Spinetta", "luisalbertospinetta@edu.ort.ar", "21956986", true, 00001, 0);
                    if (alumno1 != null)
                    {
                        var 
                    }
                }

            }
        } 
        private async Task<Alumno?> CrearAlumno(String nombre, String Apellido, String mail, String dni,bool activo, int nroMatricula, int carreraId)
        {

            Carrera carrera = _context.Carreras.FirstOrDefault(c => c.Id == carreraId);

            if (carrera != null) { 
            Alumno alumno = new Alumno()
            {
                Nombre = nombre,
                Apellido = Apellido,
                Email = mail,
                Dni = dni,
                Activo = true,
                NumeroMatricula = nroMatricula,
                Carrera = carrera,
                MateriasCursadas = new List<MateriaCursada>(),
                Calificaciones = new List<Calificacion>()
            };
                var resultado = await _userManager.CreateAsync(alumno , Configs.PasswordGenerica);
                if(resultado.Succeeded)
                {
                    await _userManager.AddToRoleAsync(alumno, Configs.AlumnoRolName);
                    return alumno;
                }
         }
            return null;
        }




        private void CrearEmpleados()
        {
            
        }

        private void CrearProfesores()
        {
            
        }

        private void CrearAlumnos()
        {
            
        }

        private void CrearUsuario()
        {
            
        }

        #region Roles
        private async Task CrearRoles()
        {
            foreach(var rolName in roles)
            {
                if(!await _rolemanager.RoleExistsAsync(rolName)) {
                   await _rolemanager.CreateAsync(new Rol(rolName));
                }
            }
        }
        #endregion

        #region Direcciones

        private void AgregarDirecciones()
        {
            if (!_context.Direcciones.Any())
            {
                _direcciones.Add(CrearDireccion("San Pedro", 920, "Moron", "Buenos Aires", "Argentina"));
                _direcciones.Add(CrearDireccion("Italia", 1050, "Moron", "Buenos Aires", "Argentina"));
                _direcciones.Add(CrearDireccion("Suipacha", 359, "CABA", "Buenos Aires", "Argentina"));
                _direcciones.Add(CrearDireccion("9 de Julio", 311, "CABA", "Buenos Aires", "Argentina"));
                _direcciones.Add(CrearDireccion("Belgrano", 250, "CABA", "Buenos Aires", "Argentina"));
                _direcciones.Add(CrearDireccion("San Martin", 329, "CABA", "Buenos Aires", "Argentina"));
                _direcciones.Add(CrearDireccion("Marcelo T de Alvear", 359, "CABA", "Buenos Aires", "Argentina"));
                _direcciones.Add(CrearDireccion("Moreno", 185, "CABA", "Buenos Aires", "Argentina"));
            }

            _context.Direcciones.AddRange(_direcciones);
            _context.SaveChanges();
        }


        private void AgregarTelefonos()
        {
            if (!_context.Telefonos.Any())
            {
                _context.Telefonos.Add(CrearTelefono("011", "1532653562"));
                _context.Telefonos.Add(CrearTelefono("011", "1589562345"));
                _context.Telefonos.Add(CrearTelefono("011", "1578566351"));
                _context.Telefonos.Add(CrearTelefono("011", "1571825362"));
            }

        }
        private Direccion? CrearDireccion(String calle, int numero, String localidad, String provincia, String pais)
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
        private Telefono? CrearTelefono(String codArea, String numero)
        {
            Telefono telefono = new Telefono()
            {
                CodArea = codArea,
                Numero = numero
            };
            return telefono;
        }

        private int? GetNextDireccionId()
        {
            if (_direcciones.Any())
            {
                var random = new Random();
                int i = random.Next(_direcciones.Count()); //Me da un numero entre 0 y el anterior al count.
                int id = _direcciones[i].Id;
                _direcciones.RemoveAt(i);
                return id;
            }
            return null;
        }

        #endregion
    }
}

