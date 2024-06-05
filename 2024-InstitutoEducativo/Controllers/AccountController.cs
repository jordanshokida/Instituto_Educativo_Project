using _2024_InstitutoEducativo.Models;
using _2024_InstitutoEducativo.ViewModels;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace _2024_InstitutoEducativo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Persona> _usermanager;
        private readonly SignInManager<Persona> _signInManager;

        public AccountController(UserManager<Persona> usermanager, SignInManager<Persona> signInManager)
        {
            this._usermanager = usermanager;
            this._signInManager = signInManager;
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
                //Avanzamos con la registracion
                Alumno clienteAcrear = new Alumno()
                {
                    Email = viewModel.Email,
                    UserName = viewModel.Email

                };
                
               var resultadoCreate =  await _usermanager.CreateAsync(clienteAcrear,viewModel.Password);

                if (resultadoCreate.Succeeded)
                {
                    await _signInManager.SignInAsync(clienteAcrear,isPersistent:false);

                    return RedirectToAction("Edit", "Alumnos", new {id = clienteAcrear.Id});
                }

                foreach(var error in resultadoCreate.Errors)
                {
                    //Proceso los errores al momento de crear
                    ModelState.AddModelError(String.Empty,error.Description);
                }

            }
            return View(viewModel);
        }

    }
}
