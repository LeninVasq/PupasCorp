using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PupasCorp.Models;
using PupasCorp.Models.ViewModels;

namespace PupasCorp.Controllers
{
    public class PlatilloMenuController : Controller
    {
        private readonly PupascorpContext _context;

        public PlatilloMenuController(PupascorpContext context)
        {
            _context = context;

        }
        public IActionResult Index(int id)
        {
            ViewData["Menus"] = new SelectList(_context.Menus, "IdMenu", "Nombre");

            HttpContext.Session.SetString("IdPlatillo", id.ToString());

            var PlatilloMenu = _context.PlatilloMenus
                                        .Include(t => t.IdPlatilloNavigation)
                                        .Where(i => i.IdPlatillo == id)
                                        .ToList();
            return View(PlatilloMenu);
        }


        [HttpPost]
        public async Task<IActionResult> update(Platillo_Menu model)
        {
            var Id = int.Parse(HttpContext.Session.GetString("IdPlatillo"));

            if (ModelState.IsValid)
            {

                var PlatilloMenus = await _context.PlatilloMenus.FindAsync(model.IdPlatilloMenu);
                if (PlatilloMenus != null)
                {
                    TempData["Update"] = "Se ha actualizado";

                    PlatilloMenus.Cantidad = model.Cantidad;

                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Se ha resgistrado exitasamente el nuevo ingrediente";
                    return RedirectToAction("Index", new { id = Id });
                }
                else
                {
                    TempData["Update"] = "no se " + model.IdPlatilloMenu;
                    return RedirectToAction("Index", new { id = Id });


                }
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> delete(Platillo_Menu model)
        {
            var Id = int.Parse(HttpContext.Session.GetString("IdPlatillo"));

            if (ModelState.IsValid)
            {

                var menuingre = await _context.PlatilloMenus.FindAsync(model.IdPlatilloMenu);
                if (menuingre != null)
                {
                    TempData["Update"] = "Se ha eliminado";
                    _context.PlatilloMenus.Remove(menuingre);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { id = Id });
                }
                else
                {
                    TempData["Update"] = "no se elimino " + model.IdPlatilloMenu;
                    return RedirectToAction("Index", new { id = Id });


                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Platillo_Menu model)
        {
            var Id = int.Parse(HttpContext.Session.GetString("IdPlatillo"));

            if (ModelState.IsValid)
            {


                var platillomenu = new PlatilloMenu()
                {
                    IdPlatillo = Id,
                    IdMenu = model.IdMenu,
                    Cantidad = model.Cantidad,
                };
                _context.Add(platillomenu);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Se ha resgistrado exitasamente el nuevo ingrediente";
                return RedirectToAction("Index", new { id = Id });
            }

            return RedirectToAction("Index");
        }

    }
}
