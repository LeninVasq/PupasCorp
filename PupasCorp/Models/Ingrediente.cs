using System;
using System.Collections.Generic;

namespace PupasCorp.Models;

public partial class Ingrediente
{
    public int IdIngrediente { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? Stock { get; set; }

    public string? Foto { get; set; }

    public bool Estado { get; set; }

    public int IdUnidadMedida { get; set; }

    public int IdCategoriasIngredientes { get; set; }

    public virtual CategoriasIngrediente IdCategoriasIngredientesNavigation { get; set; } = null!;

    public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; } = null!;

    public virtual ICollection<MenuIngrediente> MenuIngredientes { get; set; } = new List<MenuIngrediente>();

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
