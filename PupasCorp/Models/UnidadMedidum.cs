using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class UnidadMedidum
{
    public int IdUnidadMedida { get; set; }

    public string? Nombre { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
}
