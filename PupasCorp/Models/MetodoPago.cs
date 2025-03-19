using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class MetodoPago
{
    public int IdMetodoPago { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
