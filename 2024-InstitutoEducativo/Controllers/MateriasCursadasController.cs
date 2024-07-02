using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2024_InstitutoEducativo.Data;
using _2024_InstitutoEducativo.Models;
using _2024_InstitutoEducativo.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace _2024_InstitutoEducativo.Controllers
{
    public class MateriasCursadasController : Controller
    {
        private readonly InstitutoContext _context;

        public MateriasCursadasController(InstitutoContext context)
        {
            _context = context;
        }

        // GET: MateriaCursadas
        public async Task<IActionResult> Index()
        {
            ViewData["MateriasCursadas"] = new SelectList(_context.MateriasCursadas.ToList(), "Id", "Nombre");

            var institutoContext = _context.MateriasCursadas.Include(m => m.Alumno).Include(m => m.Materia).Include(m => m.Profesor);
            return View(await institutoContext.ToListAsync());
        }

        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.ProfesorRolName}")]
        // GET: MateriaCursadas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiaCursada = await _context.MateriasCursadas
                .Include(m => m.Alumno)
                .Include(m => m.Materia)
                .Include(m => m.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materiaCursada == null)
            {
                return NotFound();
            }

            return View(materiaCursada);
        }

        // GET: MateriaCursadas/Create
        public IActionResult Create()
        {
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "Id", "Apellido");
            ViewData["MateriaId"] = new SelectList(_context.Materias, "Id", "Descripcion");
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido");
            return View();
        }

        // POST: MateriaCursadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,AnioCursada,Cuatrimestre,MateriaId,ProfesorId,AlumnoId")] MateriaCursada materiaCursada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materiaCursada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "Id", "Apellido", materiaCursada.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "Id", "Descripcion", materiaCursada.MateriaId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido", materiaCursada.ProfesorId);
            return View(materiaCursada);
        }

        // GET: MateriaCursadas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiaCursada = await _context.MateriasCursadas.FindAsync(id);
            if (materiaCursada == null)
            {
                return NotFound();
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "Id", "Apellido", materiaCursada.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "Id", "Descripcion", materiaCursada.MateriaId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido", materiaCursada.ProfesorId);
            return View(materiaCursada);
        }

        // POST: MateriaCursadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,AnioCursada,Cuatrimestre,MateriaId,ProfesorId,AlumnoId")] MateriaCursada materiaCursada)
        {
            if (id != materiaCursada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materiaCursada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriaCursadaExists(materiaCursada.Id))
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
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "Id", "Apellido", materiaCursada.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "Id", "Descripcion", materiaCursada.MateriaId);
            ViewData["ProfesorId"] = new SelectList(_context.Profesores, "Id", "Apellido", materiaCursada.ProfesorId);
            return View(materiaCursada);
        }

        // GET: MateriaCursadas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiaCursada = await _context.MateriasCursadas
                .Include(m => m.Alumno)
                .Include(m => m.Materia)
                .Include(m => m.Profesor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materiaCursada == null)
            {
                return NotFound();
            }

            return View(materiaCursada);
        }

        // POST: MateriaCursadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materiaCursada = await _context.MateriasCursadas.FindAsync(id);
            if (materiaCursada != null)
            {
                _context.MateriasCursadas.Remove(materiaCursada);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriaCursadaExists(int id)
        {
            return _context.MateriasCursadas.Any(e => e.Id == id);
        }



        //METODOS:

        /*// Método para agregar calificación
        public void AgregarCalificacion(Calificacion calificacion)
        {
            // Implementación para agregar calificación
             _context.Calificaciones.Add(calificacion);
            _context.SaveChanges();
        }*/


        /*// Método para obtener alumnos inscriptos
        public List<Alumno> ObtenerAlumnosInscriptos()
        {
            // Implementación para obtener alumnos inscriptos
            return _context.Alumnos.Include(a => a.MateriasCursadas).ToList();
        }*/


        /*// Método para obtener promedio de calificaciones
        public double ObtenerPromedioCalificaciones()
        {
            // Implementación para obtener promedio de calificaciones
            return _context.Calificaciones.Average(c => c.NotaFinal);
        }*/





    }
}
