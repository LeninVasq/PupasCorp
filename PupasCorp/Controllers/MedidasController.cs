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
            TempData["Mensaje"] = "HOLA";

            return View(model);

        }
    }
}