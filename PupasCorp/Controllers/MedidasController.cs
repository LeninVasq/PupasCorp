using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PupasCorp.Models;

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
    }
}
