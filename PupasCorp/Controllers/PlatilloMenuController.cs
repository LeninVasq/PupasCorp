using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PupasCorp.Models;

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

            HttpContext.Session.SetString("IdMenu", id.ToString());

            var PlatilloMenu = _context.PlatilloMenus
                                        .Include(t => t.IdPlatilloNavigation)
                                        .Where(i => i.IdPlatillo == id)
                                        .ToList();
            return View(PlatilloMenu);
        }

    }
}
