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

            
           ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre", alumno.CarreraId);
             return View(alumno);

        }




        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
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
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
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
                    
                    if (ExistEmail(alumnoForm.Email))
                    {
                        
                        resultado = false;
                    }
                    else
                    {
                        
                        alumnoDb.Email = alumnoForm.Email;
                        alumnoDb.NormalizedEmail = alumnoForm.Email.ToUpper();
                        alumnoDb.UserName = alumnoForm.Email;
                        alumnoDb.NormalizedUserName = alumnoForm.NormalizedEmail;
                    }
                }
                else
                {
                    
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
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
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
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
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






        [Authorize(Roles = $"{Configs.AlumnoRolName}")]
        public async Task<IActionResult> Inscribir(int id)
        {
            var userId = Int32.Parse(_userManager.GetUserId(User));

            var alumno = await _context.Alumnos
                .Include(p => p.MateriasCursadas)
                .Include(p => p.Calificaciones)
                .FirstOrDefaultAsync(a => a.Id == userId);

            if (alumno == null)
            {
                return NotFound("Alumno no encontrado.");
            }

            var materia = await _context.Materias
                                        .Include(m => m.MateriaCursada)
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (materia == null)
            {
                return NotFound("Materia no encontrada.");
            }

            var materiaCursada = new MateriaCursada
            {
                Id = new Random().Next(9,400),
                Nombre = materia.MateriaNombre,
                AnioCursada = DateTime.Now.Year,
                Cuatrimestre = "1", 
                MateriaId = materia.Id,
                ProfesorId = new Random().Next(20,26),
                AlumnoId = alumno.Id
            };
            var calificacion = new Calificacion
            {
                Id = new Random().Next(20, 400),
                NotaFinal = null,
                MateriaCursadaId = materiaCursada.Id,
                ProfesorId = materiaCursada.ProfesorId,
                AlumnoId = alumno.Id
            };


            var profesor = await _context.Profesores
                                         .Include(p => p.MateriasCursadaActiva)
                                         .FirstOrDefaultAsync(p => p.Id == materiaCursada.ProfesorId);

            if (profesor != null)
            {
                bool exists = profesor.MateriasCursadaActiva
                                      .Any(mc => mc.MateriaId == materiaCursada.MateriaId
                                                 && mc.Cuatrimestre == materiaCursada.Cuatrimestre);
                if (!exists)
                {
                    profesor.MateriasCursadaActiva.Add(materiaCursada);
                }
            }

            if (alumno.MateriasCursadas.Count < 6 && profesor != null){
                alumno.MateriasCursadas.Add(materiaCursada);
                alumno.Calificaciones.Add(calificacion);
                _context.Alumnos.Update(alumno);
                _context.MateriasCursadas.Add(materiaCursada);
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["InscripcionExitosa"] = true;
            }
            catch (DbUpdateException ex)
            {
                
                return StatusCode(500, "Ocurrió un error al inscribir a la materia: " + ex.Message);
            }         

            return RedirectToAction("Details", "Materias");
        }


        
    }
}
