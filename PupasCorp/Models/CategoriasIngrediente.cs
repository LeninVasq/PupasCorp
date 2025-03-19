using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class CategoriasIngrediente
{
    public int IdCategoriasIngredientes { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Foto { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
}
