using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupasCorp.Models;
using PupasCorp.Models.ViewModels;

namespace PupasCorp.Controllers
{
    public class TipoMovimientoController : Controller
    {
        private readonly PupascorpContext _context;
        public TipoMovimientoController(PupascorpContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
       => View(await _context.TiposMovimientos.ToListAsync());


        [HttpPost]
        public async Task<IActionResult> Update(TipoMovimiento model)
        {
            if (ModelState.IsValid)
            {
                var tipoMovimiento = await _context.TiposMovimientos.FindAsync(model.IdTipoMovimientos);
                if (tipoMovimiento != null)
                {
                    TempData["Update"] = "Se ha actualizado";

                    tipoMovimiento.TipoMovimientos = model.TipoMovimientos;

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["Update"] = "no se encontro" + model.IdTipoMovimientos;
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
        public async Task<IActionResult> Create(TipoMovimiento model)
        {
            if (ModelState.IsValid)
            {



                var tiposMovimiento = new TiposMovimiento()
                {
                    TipoMovimientos = model.TipoMovimientos
                };
                _context.Add(tiposMovimiento);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Se ha resgistrado exitasamente el tipo de movimiento";
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

    }
}