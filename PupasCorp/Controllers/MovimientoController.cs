using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PupasCorp.Models;
using PupasCorp.Models.ViewModels;
using System;

namespace PupasCorp.Controllers
{
    public class MovimientoController : Controller
    {
        private readonly PupascorpContext _context;

        public MovimientoController(PupascorpContext context)
        {
            _context = context;

        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movimientos model)
        {
            var Id = int.Parse(HttpContext.Session.GetString("Id"));
            var Idusuario = int.Parse(HttpContext.Session.GetString("IdUsuario"));

            if (ModelState.IsValid)
            {

                var movimiento = new Movimiento()
                {
                    IdIngrediente = model.IdIngrediente,
                    IdTipoMovimientos = model.IdTipoMovimientos,
                    CostoUnitario = model.CostoUnitario,
                    Cantidad = model.Cantidad,
                    FechaVencimiento = model.FechaVencimiento,
                    Motivo = model.Motivo,
                    IdUsuario = Idusuario,
                    Estado = true


                };
                var tipomovimiento = await _context.TiposMovimientos.FindAsync(model.IdTipoMovimientos);

                if(tipomovimiento.TipoMovimientos == "Ingreso")
                {
                    var ingrediente = await _context.Ingredientes.FindAsync(model.IdIngrediente);
                    var sctokantiguo = ingrediente.Stock;
                    var sctokNuevo = sctokantiguo + model.Cantidad;
                    ingrediente.Stock = sctokNuevo;
                }
                else
                {
                    var ingrediente = await _context.Ingredientes.FindAsync(model.IdIngrediente);
                    var sctokantiguo = ingrediente.Stock;
                    var sctokNuevo = sctokantiguo - model.Cantidad;
                    ingrediente.Stock = sctokNuevo;
                }
                
                await _context.SaveChangesAsync();
                _context.Add(movimiento);
                await _context.SaveChangesAsync();


                TempData["Mensaje"] = "Se ha resgistrado exitasamente el nuevo ingrediente";
                    return RedirectToAction("Index", "Ingredientes", new { id = Id });
                
            }
            return RedirectToAction("Index");
        }

    }
}
