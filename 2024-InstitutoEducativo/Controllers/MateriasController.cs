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
using Microsoft.AspNetCore.Identity;

namespace _2024_InstitutoEducativo.Controllers
{
    public class MateriasController : Controller
    {
        private readonly InstitutoContext _context;
        private readonly UserManager<Persona> _usermanager;

        public MateriasController(InstitutoContext context, UserManager<Persona> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }

        // GET: Materias
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
        public async Task<IActionResult> Index()
        {
            var institutoContext = _context.Materias.Include(m => m.Carrera);
            return View(await institutoContext.ToListAsync());
        }

        

        // GET: Materias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            int userId = Int32.Parse(_usermanager.GetUserId(User));

            if (userId == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            
            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(a => a.Id == userId); 
            if (alumno == null)
            {
                return NotFound("Alumno no encontrado.");
            }

            
            System.Diagnostics.Debug.WriteLine($"AlumnoId: {alumno.Id}, CarreraId: {alumno.CarreraId}");

            
            var materias = await _context.Materias
                                         .Where(m => m.CarreraId == alumno.CarreraId)
                                         .Include(m => m.Carrera)
                                         .ToListAsync();

            
            System.Diagnostics.Debug.WriteLine($"Materias obtenidas: {materias.Count()}");

            
            var materiasCursadas = _context.MateriasCursadas.Include(mc => mc.Calificaciones).ToList();

            ViewBag.MateriasCursadas = materiasCursadas;           

            return View(materias);
        }

        // GET: Materias/Create
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
        public IActionResult Create()
        {
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre");
            return View();
        }

        // POST: Materias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MateriaNombre,CodMateria,Descripcion,CupoMaximo,CarreraId")] Materia materia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre", materia.CarreraId);
            return View(materia);
        }

        // GET: Materias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre", materia.CarreraId);
            return View(materia);
        }

        // POST: Materias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MateriaNombre,CodMateria,Descripcion,CupoMaximo,CarreraId")] Materia materia)
        {
            if (id != materia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriaExists(materia.Id))
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
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "Id", "Nombre", materia.CarreraId);
            return View(materia);
        }

        // GET: Materias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await _context.Materias
                .Include(m => m.Carrera)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materia == null)
            {
                return NotFound();
            }

            return View(materia);
        }

        // POST: Materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia != null)
            {
                _context.Materias.Remove(materia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriaExists(int id)
        {
            return _context.Materias.Any(e => e.Id == id);
        }



        
    }



}

