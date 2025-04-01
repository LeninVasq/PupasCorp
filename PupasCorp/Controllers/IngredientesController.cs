using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PupasCorp.Models;
using PupasCorp.Models.ViewModels;

namespace PupasCorp.Controllers
{
    public class IngredientesController : Controller
    {
        private readonly PupascorpContext _context;

        public IngredientesController(PupascorpContext context)
        {
            _context = context;

        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            ViewData["Unidad"] = new SelectList(_context.UnidadMedida, "IdUnidadMedida", "Nombre");
            var ingredientes = _context.Ingredientes
                                        .Include(t => t.IdCategoriasIngredientesNavigation)
                                        .Where(i => i.IdCategoriasIngredientes == id) // Filtramos por 'id'
                                        .ToList();
            return View(ingredientes);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoriaIngre model)
        {
            return View();
        }
    }
}
