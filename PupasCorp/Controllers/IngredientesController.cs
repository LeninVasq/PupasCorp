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
            ViewData["Movimiento"] = new SelectList(_context.TiposMovimientos, "IdTipoMovimientos", "TipoMovimientos");

            HttpContext.Session.SetString("Id", id.ToString());

            var ingredientes = _context.Ingredientes
                                        .Include(t => t.IdCategoriasIngredientesNavigation)
                                        .Include(t => t.IdUnidadMedidaNavigation)
                                        .Where(i => i.IdCategoriasIngredientes == id) // Filtramos por 'id'
                                        .ToList();
            return View(ingredientes);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateIngre(Ingredien model)
        {
            var Id = int.Parse(HttpContext.Session.GetString("Id"));

            if (ModelState.IsValid)
            {
                var ingrediente = await _context.Ingredientes.FindAsync(model.IdIngrediente);

                if (ingrediente != null)
                {

                    var idunidad = int.Parse(model.IdUnidadMedida);

                    bool estado = false;
                    if (model.Estado == "1")
                    {
                        estado = true;
                    }
                    else
                    {
                        estado = false;
                    }

                    ingrediente.Nombre = model.Nombre;
                    ingrediente.Foto = model.FotoBase64;
                    ingrediente.Descripcion = model.Descripcion;
                    ingrediente.IdUnidadMedida = idunidad;
                    ingrediente.Estado = estado;

                    TempData["Update"] = "Se ha actualizado";

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { id = Id });

                }
                else
                {
                    TempData["Update"] = "no se encontro " + model.IdIngrediente;
                    return RedirectToAction("Index", new { id = Id });


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
        public async Task<IActionResult> Create(Ingredien model)
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
                        Foto = "data:image/png;base64," + base64String,
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
