using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PupasCorp.Models;
using PupasCorp.Models.ViewModels;
using PupasCorp.Otros;

namespace PupasCorp.Controllers
{
    public class CategoriasIngredientesController : Controller
    {
        private readonly PupascorpContext _context;

        public CategoriasIngredientesController(PupascorpContext context)
        {
            _context = context;
            
        }

        public async Task<IActionResult> Index()
        => View(await _context.CategoriasIngredientes.ToListAsync());

        
       public IActionResult ProductModal()
       {
            var categoriaIngre = new CategoriaIngre();
            return View(categoriaIngre);
       }

        [HttpPost]
        public async Task<IActionResult> Update(CategoriaIngre model)
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
                var categoriaingre = await _context.CategoriasIngredientes.FindAsync(model.Id);
                if (categoriaingre != null)
                {
                    TempData["Update"] = "Se ha actualizado";

                    categoriaingre.Nombre = model.Nombre;
                    categoriaingre.Foto = model.Fotobase;
                    categoriaingre.Descripcion = model.Descripcion;
                    categoriaingre.Estado = estado;

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["Update"] = "no se " + model.Id;
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
        public async Task<IActionResult> Create(CategoriaIngre model)
        {
            if (ModelState.IsValid)
            {
                //Base64 B64 = new Base64();
                if (model.Foto != null && model.Foto.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        
                        model.Foto.CopyTo(ms);
                        byte[] imageBytes = ms.ToArray();
                        string base64String = Convert.ToBase64String(imageBytes);

                        var categoria_ingredientes = new CategoriasIngrediente()
                        {
                        Nombre = model.Nombre,
                        Foto = "data:image/png;base64,"+base64String,
                        Descripcion = model.Descripcion,
                        Estado = true                    
                        };
                        _context.Add(categoria_ingredientes);
                        await _context.SaveChangesAsync();
                        TempData["Mensaje"] = "Se ha resgistrado exitasamente la categoria";
                        return RedirectToAction("Index");
                    }
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
