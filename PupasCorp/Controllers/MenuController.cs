using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupasCorp.Models;
using PupasCorp.Models.ViewModels;
using System.Net;

namespace PupasCorp.Controllers
{
    public class MenuController : Controller
    {
        private readonly PupascorpContext _context;

        public MenuController(PupascorpContext context)
        {
            _context = context;

        }

        public async Task<ActionResult> DetalleMenu(int id)
        {
            if (id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
                return HttpNotFound("No se encontró el menú con ID: " + id);

            return View("~/Views/Usuario/Menus/DetalleMenu.cshtml", menu); 
        }

        private ActionResult HttpNotFound(string v)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> IndexUsuario()
        => View("~/Views/Usuario/Menus/Index.cshtml", await _context.Menus.ToListAsync());


        public async Task<IActionResult> Index()
        => View(await _context.Menus.ToListAsync());


        [HttpPost]
        public async Task<IActionResult> Update(Menus model)
        {
            if (ModelState.IsValid)
            {
                bool estado = false;
                if (model.Estado == "1")
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                }
                var menu = await _context.Menus.FindAsync(model.IdMenu);
                if (menu != null)
                {
                    TempData["Update"] = "Se ha actualizado";

                    menu.Nombre = model.Nombre;
                    menu.Foto = model.Foto;
                    menu.Descripcion = model.Descripcion;
                    menu.Estado = estado;
                    menu.Precio = model.Precio;

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["Update"] = "no se " + model.IdMenu;
                    return RedirectToAction("Index");


                }

            }

            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            //sirve para enviar un mensaje a vista
            //TempData["Mensaje"] = string.Join(" | ", errors);
            TempData["Mensaje"] = "HOLA";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Menus model)
        {
            if (ModelState.IsValid)
            {
                //Base64 B64 = new Base64();
                if (model.Foto != null && model.Foto.Length > 0)
                {
                    
                        var menu = new Menu()
                        {
                            Nombre = model.Nombre,
                            Foto = model.Foto,
                            Descripcion = model.Descripcion,
                            Estado = true,
                            Precio = model.Precio,
                        };
                        _context.Add(menu);
                        await _context.SaveChangesAsync();
                        TempData["Mensaje"] = "Se ha resgistrado exitasamente la categoria";
                        return RedirectToAction("Index");
                    
                }
                else
                {
                    return RedirectToAction("Index");

                }
            }


            var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
            //sirve para enviar un mensaje a vista
            //TempData["Mensaje"] = string.Join(" | ", errors);
            TempData["Mensaje"] = "HOLA";

            return View(model);
        }


    }
}
