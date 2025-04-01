namespace PupasCorp.Models.ViewModels
{
    public class ingredi
    {
        public int? IdIngrediente { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public int? Stock { get; set; }

        public IFormFile? Foto { get; set; }

        public bool? Estado { get; set; }

        public string? IdUnidadMedida { get; set; }

        public int? IdCategoriasIngredientes { get; set; }
    }
}
