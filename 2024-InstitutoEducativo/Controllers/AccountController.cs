using _2024_InstitutoEducativo.Data;
using _2024_InstitutoEducativo.Helpers;
using _2024_InstitutoEducativo.Models;
using _2024_InstitutoEducativo.ViewModels;
using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Runtime.InteropServices;

namespace _2024_InstitutoEducativo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Persona> _usermanager;
        private readonly SignInManager<Persona> _signInManager;
        private readonly RoleManager<Rol> _rolManager;
        private readonly InstitutoContext _context;

        public AccountController(UserManager<Persona> usermanager, SignInManager<Persona> signInManager, RoleManager<Rol> rolManager, InstitutoContext context)
        {
            this._usermanager = usermanager;
            this._signInManager = signInManager;
            this._rolManager = rolManager;
            this._context = context;
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([Bind("Email,Password,ConfirmacionPassword")]RegistroUsuario viewModel)
        {
            if (ModelState.IsValid)
            {
              
                Alumno alumnoAcrear = new Alumno()
                {
                    Email = viewModel.Email,
                    UserName = viewModel.Email,
                    CarreraId = 1,
                };

                var resultadoCreate = await _usermanager.CreateAsync(alumnoAcrear, viewModel.Password);

                if (resultadoCreate.Succeeded)
                {
                    var resultadoCreateAddRole = await _usermanager.AddToRoleAsync(alumnoAcrear, Configs.AlumnoRolName);


                    if (resultadoCreateAddRole.Succeeded)
                    {
                        await _signInManager.SignInAsync(alumnoAcrear, isPersistent: false);

                        return RedirectToAction("Edit", "Alumnos", new { id = alumnoAcrear.Id });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"No se puede agregar el rol {Configs.AlumnoRolName}");
                    }

                    foreach (var error in resultadoCreate.Errors)
                    {
                        //Proceso los errores al momento de crear
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }

                }
                return View(viewModel);
            
        }


        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Login viewModel) 
        {
            if (ModelState.IsValid)
            {
               var resultado = await _signInManager.PasswordSignInAsync(viewModel.Email,viewModel.Password,viewModel.Recordarme,false);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(String.Empty, "Inicio de sesión inválido");

            }
            return View(viewModel);
        }

        public async Task<IActionResult> CerrarSesion()
        {
             await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> EmailDisponible(string email)
        {
            var personaExistente = await _usermanager.FindByEmailAsync(email);
            if (personaExistente == null)
            {
                //No hay una persona existente con ese email
                return Json(true);
            }else
            {
                //El email ya está en uso
                return Json($"El correo {email} ya está en uso.");
            }
        }












    }
}
