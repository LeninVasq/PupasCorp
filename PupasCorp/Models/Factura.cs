using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int IdEntrega { get; set; }

    public DateTime? FechaEmision { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? Iva { get; set; }

    public decimal? Total { get; set; }

    public int IdMetodoPago { get; set; }

    public bool Estado { get; set; }

    public string? Observaciones { get; set; }

    public virtual Entrega IdEntregaNavigation { get; set; } = null!;

    public virtual MetodoPago IdMetodoPagoNavigation { get; set; } = null!;
}
