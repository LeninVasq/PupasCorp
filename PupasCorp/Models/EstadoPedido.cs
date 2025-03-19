using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class EstadoPedido
{
    public int IdEstadoPedido { get; set; }

    public string? EstadoPedido1 { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();
}
