using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupasCorp.Models;

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
            var ingredientes = _context.Ingredientes
                                        .Include(t => t.IdCategoriasIngredientesNavigation)
                                        .Where(i => i.IdCategoriasIngredientes == id) // Filtramos por 'id'
                                        .ToList();
            return View(ingredientes);
        }
    }
}
