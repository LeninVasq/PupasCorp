using Microsoft.AspNetCore.Mvc;
using PupasCorp.Models;
using Microsoft.EntityFrameworkCore;
using PupasCorp.Models.ViewModels;

namespace PupasCorp.Controllers
{
    public class PlatilloController : Controller
    {
        private readonly PupascorpContext _context;

        public PlatilloController(PupascorpContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> Index()
        => View(await _context.Platillos.ToListAsync());

        [HttpPost]
        


        [HttpPost]
        public async Task<IActionResult> update(PlatilloI model)
        {
            if (ModelState.IsValid)
            {
                

                    var IdUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario"));
                    var platillo = await _context.Platillos.FindAsync(model.IdPlatillo);

                bool estado = false;
                    if (model.Mostrar == "1")
                    {
                        estado = true;
                    }
                    else
                    {
                        estado = false;
                    }

                if (platillo != null)
                {
                    TempData["Update"] = "Se ha actualizado";

                    platillo.Nombre = model.Nombre;
                    platillo.Comentario = model.Comentario;
                    platillo.Mostrar = estado;
                    platillo.IdUsuario = IdUsuario;
                    platillo.Foto = model.Foto;

                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Se ha actualizado";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Update"] = "no se " + model.IdPlatillo;
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
        public async Task<IActionResult> Create(PlatilloI model)
        {
            if (ModelState.IsValid)
            {
                //Base64 B64 = new Base64();
                if (model.fotob != null && model.fotob.Length > 0)
                {

                    var IdUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario"));

                    bool estado = false;
                        if (model.Mostrar == "1")
                        {
                            estado = true;
                        }
                        else
                        {
                            estado = false;
                        }
                    using (var ms = new MemoryStream())
                    {

                        model.fotob.CopyTo(ms);
                        byte[] imageBytes = ms.ToArray();
                        string base64String = Convert.ToBase64String(imageBytes);
                        var platillo = new Platillo()
                        {
                            Nombre = model.Nombre,
                            Comentario = model.Comentario,
                            Mostrar = estado,
                            IdUsuario = IdUsuario,
                            Foto = "data:image/png;base64," + base64String,
                        };
                        _context.Add(platillo);
                        await _context.SaveChangesAsync();
                        TempData["Mensaje"] = "Se ha resgistrado exitasamente la categoria";
                        return RedirectToAction("Index");
                    }
                    
                }
                else
                {
                    TempData["Mensaje"] = "No se encontro foto";

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
