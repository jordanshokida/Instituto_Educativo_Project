using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2024_InstitutoEducativo.Data;
using _2024_InstitutoEducativo.Models;
using Microsoft.AspNetCore.Authorization;
using _2024_InstitutoEducativo.Helpers;
using Microsoft.AspNetCore.Identity;
using _2024_InstitutoEducativo.ViewModels;

namespace _2024_InstitutoEducativo.Controllers
{
    public class CalificacionesController : Controller
    {
        private readonly InstitutoContext _context;
        private readonly UserManager<Persona> _usermanager;

        public CalificacionesController(InstitutoContext context, UserManager<Persona> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }

        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
        // GET: Calificaciones
        public async Task<IActionResult> Index()
        {
            var institutoContext = _context.Calificaciones.Include(c => c.Alumno).Include(c => c.MateriaCursada).Include(c => c.Profesor);
            return View(await institutoContext.ToListAsync());
        }

        // GET: Calificaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificaciones
                .Include(c => c.Alumno)
                .Include(c => c.MateriaCursada)
                .Include(c => c.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // GET: Calificaciones/Create
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.ProfesorRolName}")]
        public IActionResult Create()
        {
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "Id", "Apellido");
            ViewData["MateriaCursadaId"] = new SelectList(_context.MateriasCursadas, "Id", "Cuatrimestre");
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido");
            return View();
        }

        // POST: Calificaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.ProfesorRolName}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NotaFinal,MateriaCursadaId,ProfesorId,AlumnoId")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "Id", "Apellido", calificacion.AlumnoId);
            ViewData["MateriaCursadaId"] = new SelectList(_context.MateriasCursadas, "Id", "Cuatrimestre", calificacion.MateriaCursadaId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido", calificacion.ProfesorId);
            return View(calificacion);
        }


        // GET: Calificaciones/ListarAlumnos
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.ProfesorRolName}")]
        public async Task<IActionResult> ListarAlumnos()
        {
            int profesorId = Int32.Parse(_usermanager.GetUserId(User));

            var alumnos = await _context.Calificaciones
                .Where(c => c.ProfesorId == profesorId)
                .Include(c => c.Alumno)
                .Include(c => c.MateriaCursada)
                .Select(c => new CalificacionViewModel
                {
                    AlumnoId = c.AlumnoId,
                    NombreCompleto = c.Alumno.NombreCompleto,
                    MateriaCursadaId = c.MateriaCursadaId,
                    Nombre = c.MateriaCursada.Nombre,                   
                    NotaFinal = c.NotaFinal
                })
                .ToListAsync();

            return View(alumnos);
        }



    public async Task<IActionResult> CalificacionesAlumno()
        {
            int userId = Int32.Parse(_usermanager.GetUserId(User));

            // Obtener las calificaciones del alumno incluyendo las materias cursadas y sus profesores
            var calificaciones = await _context.Calificaciones
                .Include(c => c.MateriaCursada)
                .Include(mc => mc.Profesor)
                .Include(a => a.Alumno).Where(c => c.AlumnoId == userId)
                .ToListAsync();

            if (calificaciones == null || calificaciones.Count == 0)
            {
                return NotFound();
            }

            return View(calificaciones);
        }       


        // GET: Calificaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificaciones.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "Id", "Apellido", calificacion.AlumnoId);
            ViewData["MateriaCursadaId"] = new SelectList(_context.MateriasCursadas, "Id", "Cuatrimestre", calificacion.MateriaCursadaId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido", calificacion.ProfesorId);
            return View(calificacion);
        }

        // POST: Calificaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NotaFinal,MateriaCursadaId,ProfesorId,AlumnoId")] Calificacion calificacion)
        {
            if (id != calificacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificacionExists(calificacion.Id))
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
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "Id", "Apellido", calificacion.AlumnoId);
            ViewData["MateriaCursadaId"] = new SelectList(_context.MateriasCursadas, "Id", "Cuatrimestre", calificacion.MateriaCursadaId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido", calificacion.ProfesorId);
            return View(calificacion);
        }

        // GET: Calificaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificaciones
                .Include(c => c.Alumno)
                .Include(c => c.MateriaCursada)
                .Include(c => c.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // POST: Calificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);
            if (calificacion != null)
            {
                _context.Calificaciones.Remove(calificacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalificacionExists(int id)
        {
            return _context.Calificaciones.Any(e => e.Id == id);
        }



        // GET: Calificaciones/CrearCalificacion
        /* [Authorize(Roles = $"{Configs.AdminRolName},{Configs.ProfesorRolName}")]
         public async Task<IActionResult> CrearCalificacion(int id)
         {
             var materiaCursada = await _context.MateriasCursadas
                 .Include(mc => mc.Alumno)
                 .FirstOrDefaultAsync(mc => mc.Id == id);

             if (materiaCursada == null)
             {
                 return NotFound();
             }

             var viewModel = new CalificacionViewModel
             {
                 AlumnoId = materiaCursada.Alumno.Id,
                 MateriaCursadaId = materiaCursada.Id,
                 NombreCompleto = materiaCursada.Alumno.NombreCompleto
             };

             return View(viewModel);
         }*/

        /* [Authorize(Roles = $"{Configs.AdminRolName},{Configs.ProfesorRolName}")]
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> CrearCalificacion(CalificacionViewModel viewModel)
         {
             if (ModelState.IsValid)
             {
                 int userId = Int32.Parse(_usermanager.GetUserId(User));

                 // Buscar si ya existe una calificación para esta materia y alumno
                 var existingCalificacion = await _context.Calificaciones
                     .FirstOrDefaultAsync(c =>
                         c.MateriaCursadaId == viewModel.MateriaCursadaId &&
                         c.AlumnoId == viewModel.AlumnoId);

                 if (existingCalificacion != null)
                 {
                     // Si existe, actualiza la nota existente
                     existingCalificacion.NotaFinal = viewModel.NotaFinal;
                     existingCalificacion.ProfesorId = userId;
                     _context.Update(existingCalificacion);
                 }
                 else
                 {
                     // Si no existe, crea una nueva calificación
                     var newCalificacion = new Calificacion
                     {
                         NotaFinal = viewModel.NotaFinal,
                         MateriaCursadaId = viewModel.MateriaCursadaId,
                         AlumnoId = viewModel.AlumnoId,
                         ProfesorId = userId
                     };
                     _context.Add(newCalificacion);
                     Alumno alumno = _context.Alumnos.FirstOrDefault(a => a.Id == viewModel.AlumnoId);
                     alumno.Calificaciones.Add(newCalificacion);
                     Profesor profesor = _context.Profesores.FirstOrDefault(p => p.Id == userId);
                     profesor.CalificacionesRealizadas.Add(newCalificacion);
                     MateriaCursada materiaCursada = _context.MateriasCursadas.FirstOrDefault(mc => mc.Id == viewModel.MateriaCursadaId);
                     materiaCursada.Calificaciones.Add(newCalificacion);
                     _context.Alumnos.Update(alumno);
                     _context.Profesores.Update(profesor);
                     _context.MateriasCursadas.Update(materiaCursada);

                 }

                 await _context.SaveChangesAsync();
                 return RedirectToAction("ListarAlumnos", new { alumnoId = viewModel.AlumnoId });
             }

             return View(viewModel);
         }*/


        [HttpGet]
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.ProfesorRolName}")]
        public async Task<IActionResult> CrearCalificacion(int id)
        {
            var materiaCursada = await _context.MateriasCursadas
                .Include(mc => mc.Alumno)
                .FirstOrDefaultAsync(mc => mc.AlumnoId == id);

            if (materiaCursada == null)
            {
                return NotFound();
            }

            var viewModel = new CalificacionViewModel
            {
                AlumnoId = materiaCursada.Alumno.Id,
                MateriaCursadaId = materiaCursada.Id,
                NombreCompleto = materiaCursada.Alumno.NombreCompleto
            };

            return View(viewModel);
        }

        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.ProfesorRolName}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearCalificacion(CalificacionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int userId = Int32.Parse(_usermanager.GetUserId(User));



                var existingCalificacion = await _context.Calificaciones
                    .FirstOrDefaultAsync(c =>
                        c.MateriaCursadaId == viewModel.MateriaCursadaId &&
                        c.AlumnoId == viewModel.AlumnoId);

                var alumno = _context.Alumnos.Include(a => a.Calificaciones)
                    .FirstOrDefault(a => a.Id == viewModel.AlumnoId);

                var profesor = _context.Profesores.Include(p => p.CalificacionesRealizadas)
                    .FirstOrDefault(a => a.Id == userId);

                var materiacursada = _context.MateriasCursadas.Include(mc => mc.Calificaciones)
                    .FirstOrDefault(a => a.Id == viewModel.MateriaCursadaId);


                if (existingCalificacion != null)
                {
                    existingCalificacion.NotaFinal = viewModel.NotaFinal;
                    existingCalificacion.ProfesorId = userId;
                    _context.Update(existingCalificacion);
                    alumno.Calificaciones.Add(existingCalificacion);
                    profesor.CalificacionesRealizadas.Add(existingCalificacion);
                    materiacursada.Calificaciones.Add(existingCalificacion);
                    _context.Update(materiacursada);
                    _context.Update(profesor);
                    _context.Update(alumno);
                }
                else
                {
                    var newCalificacion = new Calificacion
                    {
                        NotaFinal = viewModel.NotaFinal,
                        MateriaCursadaId = viewModel.MateriaCursadaId,
                        AlumnoId = viewModel.AlumnoId,
                        ProfesorId = userId
                    };
                    _context.Add(newCalificacion);
                    alumno.Calificaciones.Add(newCalificacion);
                    profesor.CalificacionesRealizadas.Add(newCalificacion);
                    materiacursada.Calificaciones.Add(newCalificacion);
                    _context.Update(materiacursada);
                    _context.Update(profesor);
                    _context.Update(alumno);

                }

                await _context.SaveChangesAsync();
                return RedirectToAction("ListarAlumnos", new { alumnoId = viewModel.AlumnoId });
            }

            return View(viewModel);
        }






    }
}
