namespace PupasCorp.Models.ViewModels
{
    public class Menus
    {
        public int IdMenu { get; set; }

        public string? Nombre { get; set; }

        public string? Foto { get; set; }
        public IFormFile? Fotob { get; set; }

        public string? Descripcion { get; set; }

        public string? Estado { get; set; }

        public double? Precio { get; set; }

    }
}
