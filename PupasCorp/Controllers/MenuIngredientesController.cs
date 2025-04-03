using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PupasCorp.Models;
using PupasCorp.Models.ViewModels;

namespace PupasCorp.Controllers
{
    public class MenuIngredientesController : Controller
    {
        private readonly PupascorpContext _context;


        public MenuIngredientesController(PupascorpContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            ViewData["Ingredientes"] = new SelectList(_context.Ingredientes, "IdIngrediente", "Nombre");

            HttpContext.Session.SetString("IdMenu", id.ToString());

            var menuingre = _context.MenuIngredientes
                                        .Include(t => t.IdMenuNavigation)
                                        .Where(i => i.IdMenu == id) 
                                        .ToList();
            return View(menuingre);
        }


        [HttpPost]
        public async Task<IActionResult> Create(MenuIngre model)
        {
            var Id = int.Parse(HttpContext.Session.GetString("IdMenu"));

            if (ModelState.IsValid)
            {
               
                    
                    var ingre = new MenuIngrediente()
                    {
                        IdMenu = Id,
                        IdIngrediente = model.IdIngrediente,
                        Cantidad  = model.Cantidad,
                    };
                    _context.Add(ingre);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Se ha resgistrado exitasamente el nuevo ingrediente";
                    return RedirectToAction("Index", new { id = Id });
                }
            
            return RedirectToAction("Index");
        }

    }
}
