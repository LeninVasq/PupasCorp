using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class MetodoEntrega
{
    public int IdMetodoEntrega { get; set; }

    public string? MetodoEntrega1 { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();
}
