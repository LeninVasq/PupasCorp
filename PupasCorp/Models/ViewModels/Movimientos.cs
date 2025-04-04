namespace PupasCorp.Models.ViewModels
{
    public class Movimientos
    {
        public int? IdMovimientos { get; set; }

        public int IdIngrediente { get; set; }

        public int IdTipoMovimientos { get; set; }

        public decimal? CostoUnitario { get; set; }

        public float? Cantidad { get; set; }

        public string? Motivo { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public string? Estado { get; set; }

        public int IdUsuario { get; set; }
    }
}
