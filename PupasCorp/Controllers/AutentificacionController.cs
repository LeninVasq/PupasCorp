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
                var Id_tipo_usuario = resultado.FirstOrDefault()?.IdTipoUsuario;
                string encrypted = encrip.Encrypt(fusion);

                HttpContext.Session.SetString("IdUsuario", Id.ToString());
                HttpContext.Session.SetString("Id_tipo_usuario", Id_tipo_usuario.ToString());





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

         public async Task<IActionResult> logout()
        {

            var Id = int.Parse(HttpContext.Session.GetString("IdUsuario"));
                var mensaje = "Eliminacion";
                var token = await _context.Set<Tokens>()
                    .FromSqlRaw("EXEC Token @Token,@Mensaje ,@Id",
                        new SqlParameter("@Token", "Null"),
                        new SqlParameter("@Mensaje", mensaje),
                        new SqlParameter("@Id", Id))
                    .ToListAsync();
            HttpContext.Session.Clear();

            TempData["Logout"] = token.FirstOrDefault()?.Token;
                return RedirectToAction("Login");

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

                var registro = await _context.Set<Registro>()
                .FromSqlRaw("EXEC Registrar_usuario " +
                "@Nombre, @Apellido," +
                " @Correo, @Contrasenia, " +
                "@Telefono , @Id_tipo_usuario",
                    new SqlParameter("@Nombre", auth.Nombre),
                    new SqlParameter("@Apellido", auth.Apellido),
                    new SqlParameter("@Correo", auth.Correo),
                    new SqlParameter("@Telefono", auth.Telefono),
                    new SqlParameter("@Id_tipo_usuario", 2),
                    new SqlParameter("@Contrasenia", auth.Contrasenia))
                .ToListAsync();

                if (registro.FirstOrDefault()?.Mensaje == "Registrado exitosamente")
                {
                    return RedirectToAction("Login");

                }
                else if (registro.FirstOrDefault()?.Mensaje != "Registrado exitosamente")
                {
                    TempData["Mensaje"] = string.Join(" | ", registro.FirstOrDefault()?.Mensaje);
                    return RedirectToAction("Registro");
                }
                else
                {
                    TempData["Mensaje"] = "Hubo un error";
                    return RedirectToAction("Registro");


                }
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
