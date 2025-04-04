using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace PupasCorp.Filtros
{
    public class verificasession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var controller = context.RouteData.Values["controller"]?.ToString();
                var action = context.RouteData.Values["action"]?.ToString();

                // Evitar que se aplique el filtro en las páginas de Login (o registro si tienes)
                if ((controller == "Autentificacion" && action == "Login") ||
                    (controller == "Autentificacion" && action == "Registro"))
                {
                    base.OnActionExecuting(context);
                    return;
                }

                var session = context.HttpContext.Session;
                var Idtipo = session.GetString("Id_tipo_usuario");

                if (string.IsNullOrEmpty(Idtipo))
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Autentificacion", action = "Login" })
                    );
                }
            }
            catch
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Autentificacion", action = "Login" })
                );
            }

            base.OnActionExecuting(context);
        }
    }
}
