using _2024_InstitutoEducativo.Data;
using _2024_InstitutoEducativo.Helpers;
using _2024_InstitutoEducativo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _2024_InstitutoEducativo.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly InstitutoContext _context;
        private readonly UserManager<Persona> _userManager;

        public AlumnosController(InstitutoContext context, UserManager<Persona> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }

        // GET: Alumnos
        public async Task<IActionResult> Index()
        {
            var institutoContext = _context.Alumnos.Include(a => a.Carrera);
            return View(await institutoContext.ToListAsync());
        }

        // GET: Alumnos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .Include(a => a.Carrera)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumnos/Create
        public IActionResult Create()
        {
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre");
            return View();
        }

        // POST: Alumnos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroMatricula,CarreraId,Id,Nombre,Apellido,Email,Dni")] Alumno alumno)
        {
            if (alumno.CarreraId == 0)
            {

                alumno.CarreraId = _context.Carreras.FirstOrDefault(c => c.Nombre == "Ingeniería Mecánica")?.Id ?? 2;
            }

            if (ModelState.IsValid)
            {
                alumno.UserName = alumno.Email;
                var resultadoNewAlumno = await _userManager.CreateAsync(alumno, Configs.PasswordGenerica);

                if (resultadoNewAlumno.Succeeded)
                {
                    IdentityResult resultadoAddRole;
                    string rolDefinido = Configs.AlumnoRolName;

                    resultadoAddRole = await _userManager.AddToRoleAsync(alumno, rolDefinido);

                    if (resultadoAddRole.Succeeded)
                    {
                        return RedirectToAction("Index", "Alumnos");
                    }
                    else
                    {
                        return Content($"No se ha podido agregar el rol {rolDefinido} ");
                    }
                }
                foreach (var error in resultadoNewAlumno.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                //_context.Add(empleado);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }

            // ViewData["DireccionId"] = new SelectList(_context.Direcciones, "Id", "Calle", empleado.DireccionId);
           ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre", alumno.CarreraId);
             return View(alumno);

        }
        
        



        // GET: Alumnos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre", alumno.CarreraId);
            return View(alumno);
        }

        // POST: Alumnos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarreraId,Id,Nombre,Apellido,Email,Dni")] Alumno alumnoDelForm)
        {
            if (id != alumnoDelForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try

                {

                    var alumnoEnDb = _context.Alumnos.Find(alumnoDelForm.Id);
                    if (alumnoEnDb == null)
                    {
                        return NotFound();
                    }

                    if (alumnoDelForm.CarreraId == 0)
                    {
                        alumnoDelForm.CarreraId = _context.Carreras.FirstOrDefault(c => c.Nombre == "Ingeniería Mecánica")?.Id ?? 2;
                    }

                    
                    alumnoEnDb.NumeroMatricula = new Random().Next(1, 1000001);
                    alumnoEnDb.CarreraId = alumnoDelForm.CarreraId;
                    alumnoEnDb.Id = alumnoDelForm.Id;
                    alumnoEnDb.Nombre = alumnoDelForm.Nombre;
                    alumnoEnDb.Apellido = alumnoDelForm.Apellido;
                    alumnoEnDb.Dni = alumnoDelForm.Dni;
                    

                    if(!ActualizarEmail(alumnoDelForm, alumnoEnDb))
                    {
                        ModelState.AddModelError("Email", "El mail ya está en uso");
                        return View(alumnoDelForm);
                    }

                    _context.Update(alumnoEnDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumnoDelForm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre", alumnoDelForm.CarreraId);
            return RedirectToAction("Index", "Home");
        }

        private bool ActualizarEmail(Alumno alumnoForm, Alumno alumnoDb)
        {
            bool resultado = true;
            try
            {
                if (!alumnoDb.NormalizedEmail.Equals(alumnoForm.Email.ToUpper()))
                {
                    //si no son iguales - tengo que procesar
                    //verifico si ya existe el email
                    if (ExistEmail(alumnoForm.Email))
                    {
                        //si existe, no puede ser actualizado
                        resultado = false;
                    }
                    else
                    {
                        //como no existe, puedo actualizar
                        alumnoDb.Email = alumnoForm.Email;
                        alumnoDb.NormalizedEmail = alumnoForm.Email.ToUpper();
                        alumnoDb.UserName = alumnoForm.Email;
                        alumnoDb.NormalizedUserName = alumnoForm.NormalizedEmail;
                    }
                }
                else
                {
                    //son iguales, No actualicé pero está actualizado
                }
            }
            catch
            { 
                resultado = false;
            }
            return resultado;
        }

        private bool ExistEmail(string email)
        {
            return _context.Personas.Any(p=>p.NormalizedEmail == email.ToUpper());
        }

        // GET: Alumnos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .Include(a => a.Carrera)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // POST: Alumnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno != null)
            {
                _context.Alumnos.Remove(alumno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.Id == id);
        }

        /*[HttpPost]
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.AlumnoRolName}")]
        public async Task<IActionResult> ListarMateriasAlumno()
        {
            var user = await _userManager.GetUserAsync(User);
            var alumno = await _context.Alumnos
                                       .Include(a => a.Carrera)
                                       .FirstOrDefaultAsync(a => a.Id == user.Id);

            if (alumno == null)
            {
                return NotFound("Alumno no encontrado.");
            }

            var materias = await _context.Materias
                                         .Where(m => m.CarreraId == alumno.CarreraId)
                                         .ToListAsync();

            return View(materias);
        }*/


        /*public async Task<IActionResult> VerAlumnosProfesor()
        {
            var materiasCursadasProfesor = await _context.MateriasCursadas.FindAsync()
               .Include(mc => mc.Alumno)
               .Include(mc => mc.Profesor)
               .FirstOrDefaultAsync(mc => mc.ProfesorId == Int32.Parse(_userManager.GetUserId(User)));
            
            if (materiasCursadasProfesor == null)
            {
                return NotFound();
            }
            return View(materiasCursadasProfesor);
        }*/


        //METODOS:

        /*// Método para registrar alumno
        public void RegistrarAlumno(Alumno alumno)
        {
             if (alumno != null)
            {
                _context.Alumnos.Add(alumno);
                _context.SaveChanges();
            }
        }*/


        /*/// Método para inscribir alumno en materia
        public void InscribirEnMateria(MateriaCursada materia)
        {
            // Verificar si el alumno está activo y si la materia pertenece a su carrera
            // Implementación de inscripción
             var alumno = _context.Alumnos.Find(materia.AlumnoId);
            var carrera = _context.Carreras.Find(alumno?.CarreraId);

            if (alumno != null && alumno.Activo && carrera != null && carrera.Materias.Any(m => m.Id == materia.MateriaId))
            {
                _context.MateriasCursadas.Add(materia);
                _context.SaveChanges();
            }

        }*/


        /*// Método para cancelar inscripción en materia
        public void CancelarInscripcion(MateriaCursada materia)
        {
            // Verificar si no tiene calificaciones asociadas
            // Implementación de cancelación
             var materiaCursada = _context.MateriasCursadas.Include(m => m.Calificaciones).FirstOrDefault(m => m.Id == materia.Id);

            if (materiaCursada != null && !materiaCursada.Calificaciones.Any())
            {
                _context.MateriasCursadas.Remove(materiaCursada);
                _context.SaveChanges();
            }
        }*/


        /*// Método para obtener materias cursadas
        public List<MateriaCursada> ObtenerMateriasCursadas()
        {
            // Implementación para obtener materias cursadas
             return _context.MateriasCursadas.Include(m => m.Materia).ToList();
        }*/


        /*// Método para obtener calificaciones
        public List<Calificacion> ObtenerCalificaciones()
        {
            // Implementación para obtener calificaciones
            return _context.Calificaciones.Include(c => c.MateriaCursada).ToList();
        }*/

    }
}
