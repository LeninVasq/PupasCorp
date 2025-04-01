namespace PupasCorp.Models.ViewModels
{
    public class CategoriaIngre
    {
        public int? Id { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }
        public string? Estado { get; set; }

        public IFormFile? Foto { get; set; }
        public string? Fotobase { get; set; }
    }
}
