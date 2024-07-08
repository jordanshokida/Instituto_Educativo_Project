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
using _2024_InstitutoEducativo.ViewModels;
using Microsoft.Data.SqlClient;

namespace _2024_InstitutoEducativo.Controllers
{
    public class ProfesoresController : Controller
        
    {
        private readonly UserManager<Persona> _userManager;
        private readonly InstitutoContext _context;

        public ProfesoresController(InstitutoContext context, UserManager<Persona> usermanager)
        {
            _context = context;
            _userManager = usermanager;
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
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Legajo,Id,Nombre,Apellido,Email,Dni")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                profesor.UserName = profesor.Email;
                var resultadoNewProfesor = await _userManager.CreateAsync(profesor, Configs.PasswordGenerica);

                if (resultadoNewProfesor.Succeeded)
                {
                    IdentityResult resultadoAddRole;
                    string rolDefinido = Configs.ProfesorRolName;

                    resultadoAddRole = await _userManager.AddToRoleAsync(profesor, rolDefinido);                 

                    if (resultadoAddRole.Succeeded)
                    {
                        return RedirectToAction("Index", "Profesores");
                    }
                    else
                    {
                        return Content($"No se ha podido agregar el rol {rolDefinido} ");
                    }
                }
                foreach (var error in resultadoNewProfesor.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                //_context.Add(empleado);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }

            // ViewData["DireccionId"] = new SelectList(_context.Direcciones, "Id", "Calle", empleado.DireccionId);
            return View(profesor);
        }

        // GET: Profesores/Edit/5
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
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

        private void VerificarLegajo(Profesor profesor)
        {
            if (LegajoExist(profesor))
            {
                ModelState.AddModelError("Legajo", "El legajo ya existe, verificado en BE");
            }
        }
        private bool LegajoExist(Profesor profesor)
        {
            bool resultado = false;
            if (!string.IsNullOrEmpty(profesor.Legajo))
            {
                if (profesor.Id != null && profesor.Id != 0)
                {
                    //
                    resultado = _context.Empleados.Any(e => e.Legajo == profesor.Legajo && e.Id != profesor.Id);
                }
                else
                {
                    //Es una creación, solo me interesa que no exista lapatente
                    resultado = _context.Empleados.Any(e => e.Legajo == profesor.Legajo);
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

        // POST: Profesores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Legajo,Id,Nombre,Apellido,Email,Dni")] Profesor profesorDelForm)
        {
            if (id != profesorDelForm.Id)
            {
                return NotFound();
            }

            VerificarLegajo(profesorDelForm);

            if (ModelState.IsValid)
            {
                try
                {
                    var profesorEnDb = _context.Profesores.Find(profesorDelForm.Id);
                    if (profesorEnDb == null)
                    {
                        return NotFound();
                    }


                    profesorEnDb.Legajo = profesorDelForm.Legajo;
                    profesorEnDb.Nombre = profesorDelForm.Nombre;
                    profesorEnDb.Apellido = profesorDelForm.Apellido;
                    profesorEnDb.Email = profesorDelForm.Email;
                    profesorEnDb.Dni = profesorDelForm.Dni;


                    if (!ActualizarEmail(profesorDelForm, profesorEnDb))
                    {
                        ModelState.AddModelError("Email", "El mail ya está en uso");
                        return View(profesorDelForm);
                    }

                    _context.Update(profesorEnDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorExists(profesorDelForm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dbex)
                {
                    ProcesarDuplicado(dbex);
                }
                
            }
            return RedirectToAction("Index", "Home");
        }



        private bool ActualizarEmail(Profesor profesorDelForm, Profesor profesorEnDb)
        {
            bool resultado = true;
            try
            {
                if (!profesorEnDb.NormalizedEmail.Equals(profesorDelForm.Email.ToUpper()))
                {

                    if (ExistEmail(profesorDelForm.Email))
                    {

                        resultado = false;
                    }
                    else
                    {

                        profesorEnDb.Email = profesorDelForm.Email;
                        profesorEnDb.NormalizedEmail = profesorDelForm.Email.ToUpper();
                        profesorEnDb.UserName = profesorDelForm.Email;
                        profesorEnDb.NormalizedUserName = profesorDelForm.NormalizedEmail;
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
            return _context.Personas.Any(p => p.NormalizedEmail == email.ToUpper());
        }


        // GET: Profesores/Delete/5
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
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
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
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
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.ProfesorRolName}")]
        public async Task<IActionResult> ListarMateriasCursadas()
        {
            int userId = Int32.Parse(_userManager.GetUserId(User));

            var materiasCursadas = await _context.MateriasCursadas
                .Where(mc => mc.ProfesorId == userId)
                .Include(mc => mc.Materia)
                .ThenInclude(mc => mc.Carrera)
                .ToListAsync();

            return View(materiasCursadas);
        }

       
    }

}

