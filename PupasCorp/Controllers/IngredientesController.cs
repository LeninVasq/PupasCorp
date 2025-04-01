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

        
        public IActionResult Index(int id)
        {
            ViewData["Unidad"] = new SelectList(_context.UnidadMedida, "IdUnidadMedida", "Nombre");
            HttpContext.Session.SetString("Id", id.ToString());

            var ingredientes = _context.Ingredientes
                                        .Include(t => t.IdCategoriasIngredientesNavigation)
                                        .Where(i => i.IdCategoriasIngredientes == id) // Filtramos por 'id'
                                        .ToList();
            return View(ingredientes);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ingredi model)
        {
            var Id = int.Parse(HttpContext.Session.GetString("Id"));
            var idunidad = int.Parse(model.IdUnidadMedida);

            if (ModelState.IsValid)
            {
                using (var ms = new MemoryStream())
                {
                    model.Foto.CopyTo(ms);
                    byte[] imageBytes = ms.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    var ingre = new Ingrediente()
                    {
                        Nombre = model.Nombre,
                        Descripcion = model.Descripcion,
                        Stock = 0,
                        Foto = base64String,
                        IdUnidadMedida = idunidad,
                        IdCategoriasIngredientes = Id,
                        Estado = true
                    };
                    _context.Add(ingre);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Se ha resgistrado exitasamente el nuevo ingrediente";
                    return RedirectToAction("Index", new { id = Id });
                }
            }
            return RedirectToAction("Index");
        }
    }
}
