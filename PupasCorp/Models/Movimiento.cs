using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class Movimiento
{
    public int IdMovimientos { get; set; }

    public int IdIngrediente { get; set; }

    public int IdTipoMovimientos { get; set; }

    public decimal? CostoUnitario { get; set; }

    public int? Cantidad { get; set; }

    public string? Motivo { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public bool Estado { get; set; }

    public int IdUsuario { get; set; }

    public virtual Ingrediente IdIngredienteNavigation { get; set; } = null!;

    public virtual TiposMovimiento IdTipoMovimientosNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
