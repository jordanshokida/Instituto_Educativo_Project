using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2024_InstitutoEducativo.Data;
using _2024_InstitutoEducativo.Models;
using Microsoft.Data.SqlClient;
using _2024_InstitutoEducativo.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace _2024_InstitutoEducativo.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly InstitutoContext _context;

        public EmpleadosController(InstitutoContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empleados.ToListAsync());
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Legajo,Id,Nombre,Apellido,Email,Dni")] Empleado empleado)
        {
            VerificarLegajo(empleado);

            if (ModelState.IsValid)
            {
                _context.Add(empleado);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }catch(DbUpdateException dbex)
                {
                    ProcesarDuplicado(dbex);                 
                }                             
            }
            return View(empleado);
        }

        private void VerificarLegajo(Empleado empleado)
        {
            if (LegajoExist(empleado))
            {
                ModelState.AddModelError("Legajo", "El legajo ya existe, verificao en BE");
            }
        }

        private bool LegajoExist(Empleado empleado)
        {
            bool resultado = false;
            if (!string.IsNullOrEmpty(empleado.Legajo))
            {
                if(empleado.Id != null && empleado.Id != 0)
                {
                    //
                    resultado = _context.Empleados.Any(e => e.Legajo == empleado.Legajo && e.Id != empleado.Id);
                }
                else
                {
                    //Es una creación, solo me interesa que no exista lapatente
                    resultado = _context.Empleados.Any(e => e.Legajo == empleado.Legajo);
                }
               
            }
            return resultado;
        }

        private void ProcesarDuplicado(DbUpdateException dbex)
        {
            SqlException innerException = dbex.InnerException as SqlException;
            if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
            {
                ModelState.AddModelError("Legajo", ErrorMsge.LegajoExistente);
            }
            else
            {
                ModelState.AddModelError(string.Empty, dbex.Message);
            }
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Legajo,Id,Nombre,Apellido,Email,Dni")] Empleado empleado)
        {

            

            

            if (id != empleado.Id)
            {
                return NotFound();
            }

            VerificarLegajo(empleado);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(DbUpdateException dbex) 
                {
                    ProcesarDuplicado(dbex);
                }
            }
            return View(empleado);
        }

        

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }


        //METODOS:

        /* // Método para crear alumno
         public void CrearAlumno(Alumno alumno)
         {
             // Implementación de creación de alumno
         }*/


        /*// Método para crear profesor
        public void CrearProfesor(Profesor profesor)
        {
            // Implementación de creación de profesor
        }*/


        /*// Método para crear carrera
        public void CrearCarrera(Carrera carrera)
        {
            // Implementación de creación de carrera
        }*/


        /*// Método para crear materia
        public void CrearMateria(Materia materia)
        {
            // Implementación de creación de materia
        }*/


        /*// Método para asignar profesor a materia cursada
        public void AsignarProfesorAMateria(Profesor profesor, MateriaCursada materia)
        {
            // Implementación de asignación de profesor
        }*/


    }
}
