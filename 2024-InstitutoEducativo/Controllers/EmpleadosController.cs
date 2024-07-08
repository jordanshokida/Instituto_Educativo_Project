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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace _2024_InstitutoEducativo.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly InstitutoContext _context;
        private readonly UserManager<Persona> _userManager;

        public EmpleadosController(InstitutoContext context, UserManager<Persona> usermanager)
        {
            _context = context;
            _userManager = usermanager;
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
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Legajo,Id,Nombre,Apellido,Email,Dni")] Empleado empleado)
        {
            VerificarLegajo(empleado);

            if (ModelState.IsValid)
            {
                empleado.UserName = empleado.Email;
                var resultadoNewEmpleado = await _userManager.CreateAsync(empleado, Configs.PasswordGenerica);

                if (resultadoNewEmpleado.Succeeded)
                {
                    IdentityResult resultadoAddRole;
                    string rolDefinido = Configs.EmpleadoRolName;

                    resultadoAddRole = await _userManager.AddToRoleAsync(empleado, rolDefinido);

                    if (resultadoAddRole.Succeeded)
                    {
                        return RedirectToAction("Index", "Empleados");
                    }
                    else
                    {
                        return Content($"No se ha podido agregar el rol {rolDefinido} ");
                    }
                }
                foreach (var error in resultadoNewEmpleado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                //_context.Add(empleado);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }

            // ViewData["DireccionId"] = new SelectList(_context.Direcciones, "Id", "Calle", empleado.DireccionId);
            return View(empleado);
        }

        private void VerificarLegajo(Empleado empleado)
        {
            if (LegajoExist(empleado))
            {
                ModelState.AddModelError("Legajo", "El legajo ya existe, verificado en BE");
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
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
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
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Legajo,Id,Nombre,Apellido,Email,Dni")] Empleado empleadoDelForm)
        {

            

            

            if (id != empleadoDelForm.Id)
            {
                return NotFound();
            }

            VerificarLegajo(empleadoDelForm);

            if (ModelState.IsValid)
            {
                try
                {
                    var empleadoEnDb = _context.Empleados.Find(empleadoDelForm.Id);
                    if (empleadoEnDb == null)
                    {
                        return NotFound();
                    }

 

                    empleadoEnDb.Legajo = empleadoDelForm.Legajo;                                      
                    empleadoEnDb.Nombre = empleadoDelForm.Nombre;
                    empleadoEnDb.Apellido = empleadoDelForm.Apellido;
                    empleadoEnDb.Email = empleadoDelForm.Email;
                    empleadoEnDb.Dni = empleadoDelForm.Dni;


                    if (!ActualizarEmail(empleadoDelForm, empleadoEnDb))
                    {
                        ModelState.AddModelError("Email", "El mail ya está en uso");
                        return View(empleadoDelForm);
                    }

                    _context.Update(empleadoEnDb);
                    await _context.SaveChangesAsync();
                    /*_context.Update(empleado);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));*/
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleadoDelForm.Id))
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
            return RedirectToAction("Index", "Home");
        }


        private bool ActualizarEmail(Empleado empleadoForm, Empleado empleadoDb)
        {
            bool resultado = true;
            try
            {
                if (!empleadoDb.NormalizedEmail.Equals(empleadoForm.Email.ToUpper()))
                {

                    if (ExistEmail(empleadoForm.Email))
                    {

                        resultado = false;
                    }
                    else
                    {

                        empleadoDb.Email = empleadoForm.Email;
                        empleadoDb.NormalizedEmail = empleadoForm.Email.ToUpper();
                        empleadoDb.UserName = empleadoForm.Email;
                        empleadoDb.NormalizedUserName = empleadoForm.NormalizedEmail;
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




        // GET: Empleados/Delete/5
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
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
        [Authorize(Roles = $"{Configs.AdminRolName},{Configs.EmpleadoRolName}")]
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


       

    }
}
