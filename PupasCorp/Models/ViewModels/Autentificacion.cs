using System.ComponentModel.DataAnnotations;

namespace PupasCorp.Models.ViewModels
{
    public class Autentificacion
    {

        public string? Nombre { get; set; }
        public string? Apellido { get; set; }

        public string? Correo { get; set; }
        public string? Contrasenia { get; set; }
        public string? Telefono { get; set; }


    }
}
