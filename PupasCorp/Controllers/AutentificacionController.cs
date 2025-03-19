using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PupasCorp.Models;
using PupasCorp.Models.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PupasCorp.Controllers
{
    public class AutentificacionController : Controller
    {

        private readonly PupascorpContext _context;

        public AutentificacionController(PupascorpContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Login(Autentificacion auth)
        {
            if (auth == null || string.IsNullOrEmpty(auth.Correo) || string.IsNullOrEmpty(auth.Contrasenia))
            {
                ModelState.AddModelError("", "El correo o la contraseña no pueden estar vacíos.");
                return View();
            }

            // Verifica si el correo y la contraseña existen
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo == auth.Correo && u.Contrasenia == auth.Contrasenia);

            if (usuario == null)
            {
                
                TempData["Mensaje"] = "Correo o contraseña incorrectos.";
                return RedirectToAction("Login");
            }

            // Si el usuario existe, redirige a la página principal
            return RedirectToAction("Privacy", "Home");
        }


        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(Autentificacion auth)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario()
                {
                    Nombre = auth.Nombre,
                    Apellido = auth.Apellido,
                    Correo = auth.Correo,
                    Telefono = auth.Telefono,
                    Contrasenia = auth.Contrasenia,
                    IdTipoUsuario = 1
                };
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            
            //devuelve en que campo me da errores
            var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
            //sirve para enviar un mensaje a vista
            TempData["Mensaje"] = string.Join(" | ", errors);

            return View(auth);
        }
    }

}
