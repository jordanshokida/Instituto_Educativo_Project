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

       
        public async Task<IActionResult> CalificacionesAlumno()
        {
            Alumno alumno = await _context.Alumnos
                .Include(c => c.Calificaciones)
                .ThenInclude(c => c.MateriaCursada)
                .ThenInclude(c => c.Profesor)                
                .FirstOrDefaultAsync(c => c.Id == Int32.Parse(_usermanager.GetUserId(User)));

            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
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

        // Método para listar materias cursadas
        public IActionResult ListarMateriasCursadas(int profesorId)
        {
            var materiasCursadas = _context.MateriasCursadas
                .Where(mc => mc.ProfesorId == profesorId)
                .Include(mc => mc.Materia)
                .Include(mc => mc.Alumno)
                .ToList();
            return View(materiasCursadas);
        }


        // Método para calificar alumno
        public IActionResult CalificarAlumno(int alumnoId, int materiaId)
        {
            var viewModel = new Calificacion
            {
                AlumnoId = alumnoId,
                MateriaCursadaId = materiaId
            };
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult CalificarAlumno(Calificacion model)
        {
            if (ModelState.IsValid)
            {
                var materiaCursada = _context.MateriasCursadas.Find(model.MateriaCursadaId);
                var alumno = _context.Alumnos.Find(model.AlumnoId);

                if (materiaCursada != null && alumno != null)
                {
                    materiaCursada.AlumnoId = model.AlumnoId;
                    _context.SaveChanges();
                }
                return RedirectToAction("VerAlumnos", new { materiaCursadaId = model.MateriaCursadaId });
            }
            return View(model);
        }

    }
}
