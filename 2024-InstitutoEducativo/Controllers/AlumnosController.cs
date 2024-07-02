using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2024_InstitutoEducativo.Data;
using _2024_InstitutoEducativo.Models;

namespace _2024_InstitutoEducativo.Controllers
{
    public class AlumnosController : Controller
    {
        private readonly InstitutoContext _context;

        public AlumnosController(InstitutoContext context)
        {
            _context = context;
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
                _context.Add(alumno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
        public async Task<IActionResult> Edit(int id, [Bind("NumeroMatricula,CarreraId,Id,Nombre,Apellido,Email,Dni")] Alumno alumnoDelForm)
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

                    
                    alumnoEnDb.NumeroMatricula = alumnoDelForm.NumeroMatricula;
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre", alumnoDelForm.CarreraId);
            return View(alumnoDelForm);
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
