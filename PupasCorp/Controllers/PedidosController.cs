using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupasCorp.Models;

namespace PupasCorp.Controllers
{
    public class PedidosController : Controller
    {

        private readonly PupascorpContext _context;

        public PedidosController(PupascorpContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> IndexUsuario()
        {
            var Id = int.Parse(HttpContext.Session.GetString("IdUsuario"));

            var pedidos = _context.Pedidos
                                        .Include(t => t.IdUsuarioNavigation)
                                        .Where(i => i.IdUsuario == Id)
                                        .ToList();
            return View("~/Views/Usuario/Pedidos/Index.cshtml", pedidos);

        }

        public async Task<IActionResult> Index()
        {
                var pedidos = _context.Pedidos
                                            .Include(t => t.IdUsuarioNavigation)
                                            .ToList();
                return View(pedidos);    
        }


    }
}
