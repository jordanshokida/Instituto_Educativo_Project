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
    public class ProfesoresController : Controller
    {
        private readonly InstitutoContext _context;

        public ProfesoresController(InstitutoContext context)
        {
            _context = context;
        }

        // GET: Profesores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profesores.ToListAsync());
        }

        // GET: Profesores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // GET: Profesores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Legajo,Id,Nombre,Apellido,Email,Dni")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profesor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profesor);
        }

        // GET: Profesores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }
            return View(profesor);
        }

        // POST: Profesores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Legajo,Id,Nombre,Apellido,Email,Dni")] Profesor profesor)
        {
            if (id != profesor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorExists(profesor.Id))
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
            return View(profesor);
        }

        // GET: Profesores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // POST: Profesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor != null)
            {
                _context.Profesores.Remove(profesor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesorExists(int id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }




        //METODOS:

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

        //Metodo para ver alumnos por materiacursada
        /*public IActionResult VerAlumnos(int materiaCursadaId)
        {
            var materiaCursada = _context.MateriasCursadas
                .Include(mc => mc.Alumno)
                .FirstOrDefault(mc => mc.Id == materiaCursadaId);
            return View(materiaCursada);
        }*/

        // Método para obtener promedio de notas
        public IActionResult ObtenerPromedioNotas(int materiaId)
        {
            var materiaCursada = _context.MateriasCursadas
                .Include(mc => mc.Alumno)
                .FirstOrDefault(mc => mc.Id == materiaId);

            if (materiaCursada != null)
            {
                var promedio = materiaCursada.Alumno.Calificaciones.Average(a => a.NotaFinal);
                return View(promedio);
            }
            return NotFound();
        }




    }
}
