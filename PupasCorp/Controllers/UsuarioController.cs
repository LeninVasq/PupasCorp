using Microsoft.AspNetCore.Mvc;

namespace PupasCorp.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
