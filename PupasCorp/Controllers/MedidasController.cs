using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupasCorp.Models;
using PupasCorp.Models.ViewModels;

namespace PupasCorp.Controllers
{
    public class MedidasController : Controller
    {
        private readonly PupascorpContext _context;

        public MedidasController(PupascorpContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        => View(await _context.UnidadMedida.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(Medida model)
        {
            if (ModelState.IsValid)
            {



                var medida = new UnidadMedidum()
                {
                    Nombre = model.Nombre,
                    Estado = true
                };
                _context.Add(medida);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Se ha resgistrado exitasamente la medida";
                return RedirectToAction("Index");
            }


            var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
            //sirve para enviar un mensaje a vista
            //TempData["Mensaje"] = string.Join(" | ", errors);

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Cambiar_estado(Medida model)
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

                var medida = await _context.UnidadMedida.FindAsync(model.IdUnidadMedida);

                if (medida != null)
                {
                    TempData["Update"] = "Se ha actualizado";

                    medida.Nombre = model.Nombre;
                    medida.Estado = estado;

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["Update"] = "no se "+ model.IdUnidadMedida;
                    return RedirectToAction("Index");


                }

                TempData["Update"] = "No Se ha actualizado";

                return RedirectToAction("Index");
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