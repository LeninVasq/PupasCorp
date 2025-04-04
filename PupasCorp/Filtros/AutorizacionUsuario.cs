using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PupasCorp.Filtros
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class AutorizacionUsuarioAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _tipoPermitido;

        public AutorizacionUsuarioAttribute(string tipoPermitido)
        {
            _tipoPermitido = tipoPermitido;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var session = context.HttpContext.Session;
            var idTipoUsuario = session.GetString("Id_tipo_usuario");

            // Si no hay sesión o no coincide el tipo de usuario
            if (string.IsNullOrEmpty(idTipoUsuario) || idTipoUsuario != _tipoPermitido)
            {
                // Redirige al login o muestra acceso denegado
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "Autentificacion",
                        action = "Login"
                    })
                );
            }
        }
    }
}
