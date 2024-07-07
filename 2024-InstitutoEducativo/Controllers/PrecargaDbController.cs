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
            _context.Database.EnsureDeleted();
            _context.Database.Migrate();
            //Re-createDatabase
            //CrearRoles
            CrearRoles().Wait();
            /*CrearUsuario().Wait();*/
            /* AgregarDirecciones();
             AgregarTelefonos(); */
            CrearCarreras().Wait();
            CrearAlumnos().Wait();          
            CrearProfesores().Wait();
            CrearEmpleados().Wait();
            CrearPersonas().Wait();
            CrearMaterias().Wait();                      
            CrearMateriasCursadas().Wait();
            CrearCalificaciones().Wait();            
            CrearDirecciones().Wait();
            CrearTelefonos().Wait();
            cargarListas().Wait();
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
            new Empleado("Empleado1", "Empleado1", "empleado1@hotmail.com", "37273736","0234"),
            new Empleado("Empleado2", "Empleado2", "empleado2@hotmail.com", "37273737","1235"),
            new Empleado("Empleado3", "Empleado3", "empleado3@hotmail.com", "37273738","1236"),
            new Empleado("Empleado4", "Empleado4", "empleado4@hotmail.com", "37273739","1237"),
            new Empleado("Empleado5", "Empleado5", "empleado5@hotmail.com", "37273730","1238"),
            new Empleado("Empleado6", "Empleado6", "empleado6@hotmail.com", "37273731","1239"),
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
                        if (empleado.Apellido.Equals("Empleado1"))
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
            new Alumno("Alumno1", "Alumno1", "alumno1@hotmail.com", "57273736", 0234),
            new Alumno("Alumno2", "Alumno2", "alumno2@hotmail.com", "67273737",1135),
            new Alumno("Alumno3", "Alumno3", "alumno3@hotmail.com", "77273738",1336),
            new Alumno("Alumno4", "Alumno4", "alumno4@hotmail.com", "87273739",1537),
            new Alumno("Alumno5", "Alumno5", "alumno5@hotmail.com", "97273730",1738),
            new Alumno("Alumno6", "Alumno6", "alumno6@hotmail.com", "07273431",1939),
            new Alumno("Alumno7", "Alumno7", "alumno7@hotmail.com", "07273331",1934),
            new Alumno("Alumno8", "Alumno8", "alumno8@hotmail.com", "07273231",1935),
            new Alumno("Alumno9", "Alumno9", "alumno9@hotmail.com", "07273131",1936),
            new Alumno("Alumno10", "Alumno10", "alumno10@hotmail.com", "07373731",1937),
            new Alumno("Alumno11", "Alumno11", "alumno11@hotmail.com", "07473731",1938),
            new Alumno("Alumno12", "Alumno12", "alumno12@hotmail.com", "07573731",1929),
            new Alumno("Alumno13", "Alumno13", "alumno13@hotmail.com", "07673731",1919),
            new Alumno("Alumno14", "Alumno14", "alumno14@hotmail.com", "07773731",1949),
            new Alumno("Alumno15", "Alumno15", "alumno15@hotmail.com", "07873731",1959),
            new Alumno("Alumno16", "Alumno16", "alumno16@hotmail.com", "07973731",1969),
            new Alumno("Alumno17", "Alumno17", "alumno17@hotmail.com", "07274731",1979),
            new Alumno("Alumno18", "Alumno18", "alumno18@hotmail.com", "07275731",1989),
            new Alumno("Alumno19", "Alumno19", "alumno19@hotmail.com", "07276731",1999),
            
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
                        if (alumno.Apellido.Equals("Alumno1"))
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
            new Carrera("Ingeniero de BigData"),
            new Carrera("Ingeniería Robótica")
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
            new Profesor("Profesor1", "Profesor1", "profesor1@hotmail.com", "36273736","1334"),
            new Profesor("Profesor2", "Profesor2", "profesor2@hotmail.com", "35273737","1435"),
            new Profesor("Profesor3", "Profesor3", "profesor3@hotmail.com", "34273738","1536"),
            new Profesor("Profesor4", "Profesor4", "profesor4@hotmail.com", "33273739","1637"),
            new Profesor("Profesor5", "Profesor5", "profesor5@hotmail.com", "32273730","1738"),
            new Profesor("Profesor6", "Profesor6", "profesor6@hotmail.com", "31273731","1839"),
            
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

        private async Task cargarListas()
        {
            foreach (var profesor in profesores)
            {
                foreach (var materiasC in materiasCursadas)
                {
                    if (materiasC.ProfesorId == profesor.Id)
                    {
                        profesor.MateriasCursadaActiva.Add(materiasC);
                    }
                }
                foreach (var calificacion in calificaciones)
                {
                    if (calificacion.ProfesorId == profesor.Id)
                    {
                        profesor.CalificacionesRealizadas.Add(calificacion);
                    }
                }
                _context.Profesores.Update(profesor);
                await _context.SaveChangesAsync();
            }

            foreach (var alumno in alumnos)
            {
                foreach (var materiasC in materiasCursadas)
                {
                    if (materiasC.AlumnoId == alumno.Id)
                    {
                        alumno.MateriasCursadas.Add(materiasC);
                    }
                }
                foreach (var calificacion in calificaciones)
                {
                    if (calificacion.AlumnoId == alumno.Id)
                    {
                        alumno.Calificaciones.Add(calificacion);
                    }
                }
                _context.Alumnos.Update(alumno);
                await _context.SaveChangesAsync();
            }

            foreach (var carrera in carreras)
            {
                foreach (var materia in materias)
                {
                    if (materia.CarreraId == carrera.Id)
                    {
                        carrera.Materias.Add(materia);
                    }
                }
                foreach (var alumno in alumnos)
                {
                    if(alumno.CarreraId == carrera.Id)
                    {
                        carrera.Alumnos.Add(alumno);
                    }
                }
                _context.Carreras.Update(carrera);
                await _context.SaveChangesAsync();
            }

            foreach(var materiasC in materiasCursadas)
            {
                foreach (var calificacion in calificaciones)
                {
                    if (calificacion.MateriaCursadaId == materiasC.Id)
                    {
                        materiasC.Calificaciones.Add(calificacion);
                    }
                }
                _context.MateriasCursadas.Update(materiasC);
                await _context.SaveChangesAsync();
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
            new Materia( "Matemáticas", 101, "CálculoyÁlgebra", 5,1),
            new Materia("Física", 102, "MecánicayTermodinámica", 5,2),
            new Materia("Química", 103, "QuímicaGeneral", 5,3),
            new Materia("Taller de Programacion 1", 104, "Java", 5,2),
            new Materia("Taller de Programacion 2", 105, "React", 5,2),
            new Materia("Programacion 1", 106, "Uml", 5,2),
            new Materia("Programacion 2", 107, "Patrones de diseño", 5,2),
            new Materia("PNT1", 108, "Proyecto .NET", 5,2),
            new Materia("PNT2", 109, "Proyecto React", 5,2)
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
            new MateriaCursada("Matemáticas", 2024, "1º",1,20,1),
            new MateriaCursada("Física", 2024, "1º",2,21,2),
            new MateriaCursada("Química", 2024, "2º",3,22,3),
            new MateriaCursada("Taller de Programacion 1", 2024,"1º",4,23,4),
            new MateriaCursada("Taller de Programacion 2", 2024, "2º",5,20,5),
            new MateriaCursada("Programacion 1", 2024,"1º",6,21,6),
            new MateriaCursada("Programacion 2", 2024, "2º",7,22,7),
            new MateriaCursada("PNT1", 2024,"1º",8,20,9),
            new MateriaCursada("PNT2", 2024, "2º",9,23,8)
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
            new Calificacion( 8, 1, 20, 1),
            new Calificacion(9, 2, 21, 2),
            new Calificacion( null , 3, 22, 3),
            new Calificacion( 8, 4, 23, 1),
            new Calificacion(null, 5, 20, 2),
            new Calificacion( null , 6, 21, 3),
            new Calificacion( null , 8, 20, 1),
            new Calificacion( null , 9, 23, 2)
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
         new Direccion (1, "Calle1", 111, "3 de Enero", "Buenos aires", "Argentina"),
         new Direccion (8, "Calle2", 112, "3 de Febrero", "Buenos aires", "Argentina"),
         new Direccion (16, "Calle3", 113, "3 de Marzo", "Buenos aires", "Argentina"),
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

