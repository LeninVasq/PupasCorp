namespace PupasCorp.Models.ViewModels
{
    public class Ingredien
    {
        public int? IdIngrediente { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public int? Stock { get; set; }

        public IFormFile? Foto { get; set; }
        public string? FotoBase64 { get; set; }

        public string? Estado { get; set; }

        public string? IdUnidadMedida { get; set; }

        public int? IdCategoriasIngredientes { get; set; }
    }
}
