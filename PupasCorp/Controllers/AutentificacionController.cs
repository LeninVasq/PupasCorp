using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PupasCorp.Models;
using PupasCorp.Models.ViewModels;
using PupasCorp.Otros;
using System.Data;
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
        public async Task<IActionResult> Login(Autentificacion auth)
        {
            if (auth == null || string.IsNullOrEmpty(auth.Correo) || string.IsNullOrEmpty(auth.Contrasenia))
            {
                ModelState.AddModelError("", "El correo o la contraseña no pueden estar vacíos.");
                return View();
            }

            var fusion = auth.Correo + auth.Contrasenia;

            var resultado = await _context.Set<Login>()
                .FromSqlRaw("EXEC Logins @Correo, @Contrasenia",
                    new SqlParameter("@Correo", auth.Correo),
                    new SqlParameter("@Contrasenia", auth.Contrasenia))
                .ToListAsync();

            if (resultado.FirstOrDefault()?.IdUsuario != 0)
            {
                encriptacion encrip = new encriptacion(); // instancio la clase de encriptacion para poder usar los metodos
                var Id = resultado.FirstOrDefault()?.IdUsuario;
                string encrypted = encrip.Encrypt(fusion);

                var mensaje = "Creacion de token";
                var token = await _context.Set<Tokens>()
                    .FromSqlRaw("EXEC Token @Token,@Mensaje ,@Id",
                        new SqlParameter("@Token", encrypted),
                        new SqlParameter("@Mensaje", mensaje),
                        new SqlParameter("@Id", Id))
                    .ToListAsync();

                TempData["Login"] = token.FirstOrDefault()?.Token;
                return RedirectToAction("Index", "Home");

            }
            else
            {

                TempData["Mensaje"] = "Correo o contraseña incorrectos";
                return RedirectToAction("Login");
            }
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
