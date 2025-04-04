using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class TiposMovimiento
{
    public int IdTipoMovimientos { get; set; }

    public string TipoMovimientos { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
