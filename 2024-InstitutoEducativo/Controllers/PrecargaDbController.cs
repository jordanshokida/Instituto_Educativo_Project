using _2024_InstitutoEducativo.Data;
using _2024_InstitutoEducativo.Helpers;
using _2024_InstitutoEducativo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2024_InstitutoEducativo.Controllers
{
    public class PrecargaDbController : Controller
    {
        private readonly InstitutoContext _context;
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private List<string> roles = new List<string>() { Configs.AlumnoRolName,Configs.ProfesorRolName, Configs.EmpleadoRolName, Configs.AdminRolName };
        
       

        public PrecargaDbController(UserManager<Persona> userManager, RoleManager<Rol> roleManager , InstitutoContext context)
        {         
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
        }

       // public PrecargaDbController(InstitutoContext context)
       // {

       //     this._context = context;
        //}

        public IActionResult Seed()
        {
            //Delete Database 
            /*_context.Database.EnsureDeleted();
            _context.Database.Migrate();*/
            //Re-createDatabase
            //CrearRoles
            CrearRoles().Wait();
            /*CrearUsuario().Wait();*/
            /* AgregarDirecciones();
             AgregarTelefonos(); */
            CrearAlumnos().Wait();
            CrearCarreras().Wait();
            CrearProfesores().Wait();
            CrearEmpleados().Wait();
            CrearPersonas().Wait();
            CrearMaterias().Wait();
            CrearMateriasCursadas().Wait();
            CrearCalificaciones().Wait();
            CrearDirecciones().Wait();
            CrearTelefonos().Wait();
            /*
            
            ;
            
            
            ;*/
            
            //Agregas todo lo demas en orden de dependica. 

            /* AddDirecciones();*/
            return RedirectToAction("Index","Home", new {mensaje="Se ejecuto la precarga"});
        }

        private async Task CrearRoles()
        {
            foreach (var rolName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(rolName))
                {
                    await _roleManager.CreateAsync(new Rol(rolName));
                }
            }
        }

        private List<Empleado> empleados = new List<Empleado>()
        {
            new Empleado("Jordán", "Shokida", "jordii.s@hotmail.com", "37273736","0234"),
            new Empleado("Jordán1", "Shokida1", "jordii.s1@hotmail.com", "37273737","1235"),
            new Empleado("Jordán2", "Shokida2", "jordii.s2@hotmail.com", "37273738","1236"),
            new Empleado("Jordán3", "Shokida3", "jordii.s3@hotmail.com", "37273739","1237"),
            new Empleado("Jordán4", "Shokida4", "jordii.s4@hotmail.com", "37273730","1238"),
            new Empleado("Jordán5", "Shokida5", "jordii.s5@hotmail.com", "37273731","1239"),
        };

        private async Task CrearEmpleados()
        {
            foreach (var empleado in empleados)
            {
                if (!_context.Empleados.Any(e => e.Legajo == empleado.Legajo))
                {
                    empleado.UserName = empleado.Email;
                    IdentityResult result = await _userManager.CreateAsync(empleado, Configs.PasswordGenerica);

                    if (result.Succeeded)
                    {
                        if (empleado.Apellido.Equals("Shokida"))
                        {
                            await _userManager.AddToRoleAsync(empleado, Configs.AdminRolName);
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(empleado, Configs.EmpleadoRolName);
                        }
                    }
                    else
                    {
                        // Manejar el error de creación del usuario
                        // Puedes usar result.Errors para obtener detalles del error
                    }
                }
            }
        }



        private List<Alumno> alumnos = new List<Alumno>()
        {
            new Alumno("Jordán16", "Shokida36", "jordii.s22@hotmail.com", "57273736", false, 0234),
            new Alumno("Jordán17", "Shokida31", "jordii.s21@hotmail.com", "67273737", false,1135),
            new Alumno("Jordán27", "Shokida32", "jordii.s32@hotmail.com", "77273738", false,1336),
            new Alumno("Jordán37", "Shokida33", "jordii.s33@hotmail.com", "87273739", false,1537),
            new Alumno("Jordán47", "Shokida34", "jordii.s34@hotmail.com", "97273730", false,1738),
            new Alumno("Jordán57", "Shokida35", "jordii.s35@hotmail.com", "07273731", false,1939),
        };

        private async Task CrearAlumnos()
        {
            var carreraPorDefecto = await _context.Carreras.FirstOrDefaultAsync(c => c.Nombre == "Ingeniero de IA");

            foreach (var alumno in alumnos)
            {
                if (!_context.Alumnos.Any(e => e.NumeroMatricula == alumno.NumeroMatricula))
                {
                    alumno.UserName = alumno.Email;

                    // Asignar la carrera por defecto al alumno
                    if (carreraPorDefecto != null)
                    {
                        alumno.CarreraId = carreraPorDefecto.Id;
                        alumno.Carrera = carreraPorDefecto;
                    }

                    IdentityResult result = await _userManager.CreateAsync(alumno, Configs.PasswordGenerica);

                    if (result.Succeeded)
                    {
                        if (alumno.Apellido.Equals("Shokida36"))
                        {
                            await _userManager.AddToRoleAsync(alumno, Configs.AdminRolName);
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(alumno, Configs.AlumnoRolName);
                        }
                    }
                    else
                    {
                        // Manejar el error de creación del usuario
                        // Puedes usar result.Errors para obtener detalles del error
                    }
                }
            }
        }



        private List<Carrera> carreras = new List<Carrera>() {

            new Carrera("Ingeniería Mecánica"),
            new Carrera("Ingeniero de IA"),
            new Carrera("Ingeniero de BigData")

        };

        
        private async Task CrearCarreras() {
            foreach (var carrera in carreras)
            {
                if (!_context.Carreras.Any(c => c.Nombre == carrera.Nombre))
                {
                    _context.Carreras.Add(carrera);
                    await _context.SaveChangesAsync();
                }
            }
        }

        private List<Profesor> profesores = new List<Profesor>()
        {
            new Profesor("Jordán8", "Shokida8", "jordii.s8@hotmail.com", "36273736","1334"),
            new Profesor("Jordán9", "Shokida9", "jordii.s10@hotmail.com", "35273737","1435"),
            new Profesor("Jordán10", "Shokida10", "jordii.s12@hotmail.com", "34273738","1536"),
            new Profesor("Jordán11", "Shokida11", "jordii.s13@hotmail.com", "33273739","1637"),
            new Profesor("Jordán12", "Shokida12", "jordii.s14@hotmail.com", "32273730","1738"),
            new Profesor("Jordán13", "Shokida13", "jordii.s15@hotmail.com", "31273731","1839"),
        };

        private async Task CrearProfesores()
        {
            foreach (var profesor in profesores)
            {
                if (!_context.Profesores.Any(e => e.Legajo == profesor.Legajo))
                {
                    profesor.UserName = profesor.Email;
                    IdentityResult result = await _userManager.CreateAsync(profesor, Configs.PasswordGenerica);

                    if (result.Succeeded)
                    {
                        if (profesor.Apellido.Equals("Shokida8"))
                        {
                            await _userManager.AddToRoleAsync(profesor, Configs.AdminRolName);
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(profesor, Configs.ProfesorRolName);
                        }
                    }
                    else
                    {
                        // Manejar el error de creación del usuario
                        // Puedes usar result.Errors para obtener detalles del error
                    }
                }
            }
        }


        private List<Telefono> telefonos = new List<Telefono>() {

            new Telefono("+54","1169675403"),
            new Telefono("+55","1169675404"),
            new Telefono("+56","1169675405")

        };


        private async Task CrearTelefonos()
        {
            var personasEnDb = _context.Personas.ToList();

            for (int i = 0; i < telefonos.Count; i++)
            {
                var telefono = telefonos[i];
                if (!_context.Telefonos.Any(t => t.Numero == telefono.Numero))
                {
                    if (i < personasEnDb.Count)
                    {
                        telefono.PersonaId = personasEnDb[i].Id;
                        telefono.Persona = personasEnDb[i];
                    }
                    _context.Telefonos.Add(telefono);
                }
            }
            await _context.SaveChangesAsync();
        }



        private List<Persona> personas = new List<Persona>()
        {
         new Persona ("Nacho","Rosello", "nacho@ort.ar", "11111111"),
         new Persona ("Nacho1","Rosello1", "nacho1@ort.ar", "11111112"),
         new Persona ("Nacho2","Rosello2", "nacho2@ort.ar", "11111113"),
        };

        private async Task CrearPersonas()
        {
            foreach (var persona in personas)
            {
                if (!_context.Personas.Any(p => p.Nombre == persona.Nombre))
                {
                    _context.Personas.Add(persona);
                }
            }
            await _context.SaveChangesAsync();
        }


        private List<Materia> materias = new List<Materia>()
        {
            new Materia(1, "Matemáticas", 101, "CálculoyÁlgebra", 5,1),
            new Materia(2, "Física", 102, "MecánicayTermodinámica", 5,2),
            new Materia(3, "Química", 103, "QuímicaGeneral", 5,4)
        };

        public async Task CrearMaterias()
        {
            foreach (var materia in materias)
            {
                if (!_context.Materias.Any(m => m.Id == materia.Id))
                {
                    _context.Materias.Add(materia);
                }
            }
            await _context.SaveChangesAsync();
        }


        private List<MateriaCursada> materiasCursadas = new List<MateriaCursada>()
        {
            new MateriaCursada(1, "Matemáticas", 2024, "1º Cuatrimestre"),
            new MateriaCursada(2, "Física", 2024, "1º Cuatrimestre"),
            new MateriaCursada(3, "Química", 2024, "2º Cuatrimestre")
        };

        public async Task CrearMateriasCursadas()
        {
            foreach (var materiaCursada in materiasCursadas)
            {
                if (!_context.MateriasCursadas.Any(mc => mc.Id == materiaCursada.Id))
                {
                    _context.MateriasCursadas.Add(materiaCursada);
                }
            }
            await _context.SaveChangesAsync();
        }


        private List<Calificacion> calificaciones = new List<Calificacion>()
        {
            new Calificacion(1, 8, 1, 32, 46),
            new Calificacion(2, 9, 2, 33, 47),
            new Calificacion(3, 10, 3, 34, 48)
        };


        public async Task CrearCalificaciones()
        {
            foreach (var calificacion in calificaciones)
            {
                if (!_context.Calificaciones.Any(c => c.Id == calificacion.Id))
                {
                    _context.Calificaciones.Add(calificacion);
                }
            }
            await _context.SaveChangesAsync();
        }



        private List<Direccion> direcciones = new List<Direccion>()
        {
         new Direccion (24, "Calle1", 111, "3 de Enero", "Buenos aires", "Argentina"),
         new Direccion (34, "Calle2", 112, "3 de Febrero", "Buenos aires", "Argentina"),
         new Direccion (49, "Calle3", 113, "3 de Marzo", "Buenos aires", "Argentina"),
        };

        private async Task CrearDirecciones()
        {
            foreach (var direccion in direcciones)
            {
                if (!_context.Direcciones.Any(p => p.Calle == direccion.Calle))
                {
                    _context.Direcciones.Add(direccion);
                }
            }
            await _context.SaveChangesAsync();
        }

        /*   #region Creaciones
           private async Task CrearRoles()
           {
               foreach (var rolName in roles)
               {
                   if (!await _roleManager.RoleExistsAsync(rolName))
                   {
                       await _roleManager.CreateAsync(new Rol(rolName));
                   }
               }
           }


           private async Task CrearAlumnos()
           {
               if (!_context.Alumnos.Any())
               {

                   Alumno cliente = new Alumno()
                   {
                       Nombre = "Charly",
                       Apellido = "Garcia",
                       Email = "charly@ort.edu.ar",
                       Dni = "37273736",
                       Activo = false,
                       NumeroMatricula = 1234
                   };
                   cliente.UserName = cliente.Email;
                   await _userManager.CreateAsync(cliente, Configs.PasswordGenerica);
                   await _userManager.AddToRoleAsync(cliente, Configs.AlumnoRolName);
                   //_context.Cliente.Add(cliente);
                   //_context.SaveChanges();

                   Direccion direccion = new Direccion()

                   {
                       Calle = "Zapiola",
                       Numero = 704,
                       Localidad = "3 de Febrero",
                       Provincia = "Buenos Aires",
                       Pais = "Argentina",
                       Id= cliente.Id
                   };

                   _context.Direcciones.Add(direccion);
                   _context.SaveChanges();

                   Carrera carrera = new Carrera()
                   {
                       Nombre = "Ingeniería Mecánica",
                       Id = cliente.Id,

                   };

                   _context.Carreras.Add(carrera);
                   _context.SaveChanges();

                   Alumno cliente2 = new Alumno()
                   {

                       Nombre = "Charly2",
                       Apellido = "Garcia2",
                       Email = "charly2@ort.edu.ar",
                       Dni = "37273737",
                       Activo = false,
                       NumeroMatricula = 1235
                   };

                   cliente2.UserName = cliente2.Email;
                   await _userManager.CreateAsync(cliente2, Configs.PasswordGenerica);
                   await _userManager.AddToRoleAsync(cliente2, Configs.AlumnoRolName);

                   Direccion direccion2 = new Direccion()

                   {
                       Calle = "Zapiola2",
                       Numero = 705,
                       Localidad = "3 de Febrero",
                       Provincia = "Buenos Aires",
                       Pais = "Argentina",
                       Id = cliente2.Id
                   };

                   _context.Direcciones.Add(direccion2);
                   _context.SaveChanges();

                   Carrera carrera2 = new Carrera()
                   {
                       Nombre = "Licenciatura en física",
                       Id = cliente2.Id,

                   };

                   _context.Carreras.Add(carrera2);
                   _context.SaveChanges();

                   Alumno cliente3 = new Alumno()
                   {
                       Nombre = "Charly3",
                       Apellido = "Garcia3",
                       Email = "charly3@ort.edu.ar",
                       Dni = "37273738",
                       Activo = false,
                       NumeroMatricula = 1236
                   };
                   cliente3.UserName = cliente3.Email;
                   await _userManager.CreateAsync(cliente3, Configs.PasswordGenerica);
                   await _userManager.AddToRoleAsync(cliente3, Configs.AlumnoRolName);

                   Direccion direccion3 = new Direccion()

                   {
                       Calle = "Zapiola3",
                       Numero = 706,
                       Localidad = "3 de Febrero",
                       Provincia = "Buenos Aires",
                       Pais = "Argentina",
                       Id = cliente3.Id
                   };

                   _context.Direcciones.Add(direccion3);
                   _context.SaveChanges();

                   Carrera carrera3 = new Carrera()
                   {
                       Nombre = "Ingeniería en inteligencia Artificial",
                       Id = cliente3.Id,

                   };

                   _context.Carreras.Add(carrera3);
                   _context.SaveChanges();
               }
           }


           private async Task CrearProfesores()
           {
               if (!_context.Profesores.Any())
               {

                   Profesor profesor = new Profesor()
                   {
                       Nombre = "Charly4",
                       Apellido = "Garcia",
                       Email = "charly4@ort.edu.ar",
                       Dni = "47273736",
                       Legajo = "1234"
                   };
                   profesor.UserName = profesor.Email;
                   await _userManager.CreateAsync(profesor, Configs.PasswordGenerica);
                   await _userManager.AddToRoleAsync(profesor, Configs.ProfesorRolName);
                   //_context.Cliente.Add(cliente);
                   //_context.SaveChanges();

                   Direccion direccion6 = new Direccion()

                   {
                       Calle = "Zapiola",
                       Numero = 704,
                       Localidad = "3 de Febrero",
                       Provincia = "Buenos Aires",
                       Pais = "Argentina",
                       Id = profesor.Id
                   };

                   _context.Direcciones.Add(direccion6);
                   _context.SaveChanges();

                   Profesor profesor2 = new Profesor()
                   {

                       Nombre = "Charly2",
                       Apellido = "Garcia2",
                       Email = "charly2@ort.edu.ar",
                       Dni = "57273736",
                       Legajo = "12345"
                   };
                   profesor2.UserName = profesor2.Email;
                   await _userManager.CreateAsync(profesor2, Configs.PasswordGenerica);
                   await _userManager.AddToRoleAsync(profesor2, Configs.ProfesorRolName);

                   Direccion direccion4 = new Direccion()

                   {
                       Calle = "Zapiola2",
                       Numero = 705,
                       Localidad = "3 de Febrero",
                       Provincia = "Buenos Aires",
                       Pais = "Argentina",
                       Id = profesor2.Id
                   };

                   _context.Direcciones.Add(direccion4);
                   _context.SaveChanges();

                   Profesor profesor3 = new Profesor()
                   {
                       Nombre = "Charly3",
                       Apellido = "Garcia3",
                       Email = "charly3@ort.edu.ar",
                       Dni = "67273736",
                       Legajo = "1234"
                   };
                   profesor3.UserName = profesor3.Email;
                   await _userManager.CreateAsync(profesor3, Configs.PasswordGenerica);
                   await _userManager.AddToRoleAsync(profesor3, Configs.ProfesorRolName);

                   Direccion direccion5 = new Direccion()

                   {
                       Calle = "Zapiola3",
                       Numero = 706,
                       Localidad = "3 de Febrero",
                       Provincia = "Buenos Aires",
                       Pais = "Argentina",
                       Id = profesor3.Id
                   };

                   _context.Direcciones.Add(direccion5);
                   _context.SaveChanges();

               }
           }
           #endregion*/


        /* private Carrera? CrearCarrera(String nombre)
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
         }*/

        /*private Materia? CrearMateria(String nombre, int codMateria, String descripcion, int cupoMax, int carreraId) {
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
        }*/


        /*private async Task AgregarAlumnos()
        {
            if (_context.Alumnos.Any())
            {
                int? dirId = GetNextDireccionId();

                if (dirId.HasValue)
                {
                    var alumno1 = await CrearAlumno("Luis Alberto", "Spinetta", "luisalbertospinetta@edu.ort.ar", "21956986", true, 00001, 0);
                    if (alumno1 != null)
                    {
                        
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
        */
    }
}

